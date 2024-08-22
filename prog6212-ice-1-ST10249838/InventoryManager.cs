namespace ICE1
{
    public class InventoryManager
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public Product? GetProductById(int productId)
        {
            return products.FirstOrDefault(p => p.ProductID == productId);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.ProductID);
            if (existingProduct != null)
            {
                products.Remove(existingProduct);
                products.Add(product);
            }
        }

        public void ProcessSale(int productId, int quantity)
        {
            var product = GetProductById(productId);
            if (product != null)
            {
                product.ProcessSale(product, quantity);
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public IEnumerable<dynamic> GenerateReport()
        {
            return from p in products
                   select new { p.Name, p.Category, p.StockQuantity };
        }
    }
}