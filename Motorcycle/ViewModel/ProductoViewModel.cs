namespace Motorcycle.ViewModel
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; } = null!;
        public string NombreProducto { get; set; } = null!;
        public string DescripcionProducto { get; set; } = null!;
        public IFormFile? FotoFile { get; set; } // Propiedad para el archivo de la foto
        public decimal ValorUnitarioProducto { get; set; }
        public int IdUsuario { get; set; }

    }
}
