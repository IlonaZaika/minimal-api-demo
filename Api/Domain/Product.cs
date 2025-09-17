namespace Api.Domain;
public class Product {
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public byte[]? RowVersion { get; set; } // for DbUpdateConcurrency
}