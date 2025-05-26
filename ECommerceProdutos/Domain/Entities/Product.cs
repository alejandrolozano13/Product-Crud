namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Description { get; set; }
        public string DepartmentCode { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public bool Removed { get; set; } = false;
    }
}