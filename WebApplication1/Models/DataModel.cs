namespace Validaciones.Models
{
    public class DataModel
    {
        public string Message { get; set; }
        public List<Products> Data { get; set; }
    }

    public class Products 
    { 
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
