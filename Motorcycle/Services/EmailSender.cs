using Motorcycle.Services;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks; // Asegúrate de incluir esto para `Task`

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Marca el método como async y cambia el tipo de retorno a Task
    public async Task SendEmailAsync(string to, string subject, string message, string? attachmentPath = null)
    {
        // Verifica parámetros
        if (string.IsNullOrWhiteSpace(to))
            throw new ArgumentException("El destinatario no puede ser nulo o vacío.", nameof(to));
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException("El asunto no puede ser nulo o vacío.", nameof(subject));
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("El mensaje no puede ser nulo o vacío.", nameof(message));

        // Verifica configuración
        var smtpHost = _configuration["SmtpSettings:Host"];
        var smtpPortString = _configuration["SmtpSettings:Port"];
        var smtpUsername = _configuration["SmtpSettings:Username"];
        var smtpPassword = _configuration["SmtpSettings:Password"];

        // Comprobaciones
        if (string.IsNullOrWhiteSpace(smtpHost))
            throw new ArgumentException("La configuración del host SMTP no puede ser nula o vacía.", nameof(smtpHost));
        if (string.IsNullOrWhiteSpace(smtpPortString))
            throw new ArgumentException("La configuración del puerto SMTP no puede ser nula o vacía.", nameof(smtpPortString));
        if (string.IsNullOrWhiteSpace(smtpUsername))
            throw new ArgumentException("La configuración del nombre de usuario SMTP no puede ser nula o vacía.", nameof(smtpUsername));
        if (string.IsNullOrWhiteSpace(smtpPassword))
            throw new ArgumentException("La configuración de la contraseña SMTP no puede ser nula o vacía.", nameof(smtpPassword));

        var smtpPort = int.Parse(smtpPortString);

        var smtpClient = new SmtpClient(smtpHost)
        {
            Port = smtpPort,
            Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpUsername),
            Subject = subject,
            Body = message,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(to);

        if (attachmentPath != null)
        {
            if (File.Exists(attachmentPath))
            {
                mailMessage.Attachments.Add(new Attachment(attachmentPath));
            }
            else
            {
                throw new FileNotFoundException("El archivo de adjunto no se encontró.", attachmentPath);
            }
        }

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error al enviar el correo", ex);
        }
    }

}

