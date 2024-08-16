namespace Motorcycle.ViewModel
{
    public class RegistrarVentaViewModel
    {
        public RegistrarVentaViewModel()
        {
            Productos = new List<ProductoCarrito>();
        }
        public  List<ProductoCarrito> Productos { get; set; } 
        public int IdProducto { get; set; }
        public int IdCliente { get; set; }
        public int Cantidad { get; set; }

    }
}
