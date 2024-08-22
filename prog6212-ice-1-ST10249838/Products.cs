namespace ICE1
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }

        public void ProcessSale(dynamic product, int quantity)
        {
            if (product.StockQuantity >= quantity)
            {
                product.StockQuantity -= quantity;
                Console.WriteLine($"{quantity} {product.Name}(s) sold. Remaining stock: {product.StockQuantity}");
            }
            else
            {
                Console.WriteLine($"Insufficient stock for {product.Name}. Current stock: {product.StockQuantity}");
            }
        }

        public static Product operator +(Product product, int quantity)
        {
            product.StockQuantity += quantity;
            return product;
        }

        public static Product operator -(Product product, int quantity)
        {
            if (product.StockQuantity >= quantity)
            {
                product.StockQuantity -= quantity;
            }
            else
            {
                Console.WriteLine($"Cannot remove {quantity} items. Only {product.StockQuantity} in stock.");
            }
            return product;
        }
    }

    public class Electronics : Product
    {
        public int WarrantyPeriod { get; set; }
    }

    public class Grocery : Product
    {
        public DateTime ExpirationDate { get; set; }
    }

    public class Clothing : Product
    {
        public string? Size { get; set; }
        public string? Material { get; set; }
    }
}