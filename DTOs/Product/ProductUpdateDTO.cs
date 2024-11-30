namespace CRUDApp.DTOs
{
    public class ProductUpdateDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
