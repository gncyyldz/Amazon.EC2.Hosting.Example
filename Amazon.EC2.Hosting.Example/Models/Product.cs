namespace Amazon.EC2.Hosting.Example.Models
{
    public record Product(Guid Id)
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
