using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class EmailSender
{
    private readonly SmtpSettings _smtpSettings;

    public EmailSender(IConfiguration configuration)
    {
        _smtpSettings = configuration.GetSection("SmtpSettings").Get<SmtpSettings>()
            ?? throw new ArgumentNullException(nameof(_smtpSettings), "La configuración SMTP no puede ser nula.");

        if (string.IsNullOrEmpty(_smtpSettings.SenderEmail))
        {
            throw new ArgumentNullException(nameof(_smtpSettings.SenderEmail), "El correo del remitente no puede ser nulo.");
        }

        if (string.IsNullOrEmpty(_smtpSettings.SenderName))
        {
            _smtpSettings.SenderName = string.Empty; // O lanzar una excepción si el nombre del remitente es obligatorio
        }
    }

    public async Task SendEmailAsync(string recipientEmail, string subject, string body)
    {
        MailMessage newMail = new MailMessage
        {
            From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        newMail.To.Add(new MailAddress(recipientEmail));

        using (var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = _smtpSettings.EnableSsl
        })
        {
            await client.SendMailAsync(newMail);
        }
    }
}

public class SmtpSettings
{
    public string? Server { get; set; }
    public int Port { get; set; }
    public string? SenderName { get; set; }
    public string? SenderEmail { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool EnableSsl { get; set; }
}
