using System.Windows;

namespace ICE1
{
    public partial class MainWindow : Window
    {
        private InventoryManager inventoryManager;

        public MainWindow()
        {
            InitializeComponent();
            inventoryManager = new InventoryManager();
            CategoryComboBox.SelectionChanged += CategoryComboBox_SelectionChanged;
        }

        private void CategoryComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedCategory = GetSelectedCategory();
            ToggleAdditionalDetailsVisibility(selectedCategory);
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = CreateProductFromInput();
                if (product != null)
                {
                    inventoryManager.AddProduct(product);
                    MessageBox.Show("Product added successfully.");
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = CreateProductFromInput();
                if (product != null)
                {
                    var existingProduct = inventoryManager.GetProductById(product.ProductID);
                    if (existingProduct != null)
                    {
                        inventoryManager.UpdateProduct(product);
                        MessageBox.Show("Product updated successfully.");
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Product not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ProcessSaleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productID = int.Parse(ProductIDTextBox.Text);
                int quantity = int.Parse(StockQuantityTextBox.Text);

                inventoryManager.ProcessSale(productID, quantity);
                MessageBox.Show("Sale processed successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ViewReportButton_Click(object sender, RoutedEventArgs e)
        {
            var inventorySummary = inventoryManager.GenerateReport();
            InventoryReportListBox.Items.Clear();
            foreach (var item in inventorySummary)
            {
                InventoryReportListBox.Items.Add($"Name: {item.Name}, Category: {item.Category}, Stock: {item.StockQuantity}");
            }
        }

        private Product CreateProductFromInput()
        {
            int productID = int.Parse(ProductIDTextBox.Text);
            string name = ProductNameTextBox.Text;
            string category = GetSelectedCategory();
            double price = double.Parse(PriceTextBox.Text);
            int stockQuantity = int.Parse(StockQuantityTextBox.Text);

            Product product;

            switch (category)
            {
                case "Electronics":
                    int warrantyPeriod = int.Parse(WarrantyPeriodTextBox.Text);
                    product = new Electronics { ProductID = productID, Name = name, Category = category, Price = price, StockQuantity = stockQuantity, WarrantyPeriod = warrantyPeriod };
                    break;

                case "Grocery":
                    DateTime expirationDate = ExpirationDatePicker.SelectedDate!.Value;
                    product = new Grocery { ProductID = productID, Name = name, Category = category, Price = price, StockQuantity = stockQuantity, ExpirationDate = expirationDate };
                    break;

                case "Clothing":
                    string size = SizeTextBox.Text;
                    string material = MaterialTextBox.Text;
                    product = new Clothing { ProductID = productID, Name = name, Category = category, Price = price, StockQuantity = stockQuantity, Size = size, Material = material };
                    break;

                default:
                    throw new InvalidOperationException("Invalid category selected.");
            }

            return product;
        }

        private string GetSelectedCategory()
        {
            if (CategoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("No category selected.");
                throw new InvalidOperationException("No category selected.");
            }

            var selectedItem = (System.Windows.Controls.ComboBoxItem)CategoryComboBox.SelectedItem;
            return selectedItem.Content.ToString()!;
        }

        private void ToggleAdditionalDetailsVisibility(string category)
        {
            WarrantyPeriodTextBox.Visibility = (category == "Electronics") ? Visibility.Visible : Visibility.Collapsed;
            ExpirationDatePicker.Visibility = (category == "Grocery") ? Visibility.Visible : Visibility.Collapsed;
            SizeTextBox.Visibility = (category == "Clothing") ? Visibility.Visible : Visibility.Collapsed;
            MaterialTextBox.Visibility = (category == "Clothing") ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ClearInputFields()
        {
            ProductIDTextBox.Clear();
            ProductNameTextBox.Clear();
            PriceTextBox.Clear();
            StockQuantityTextBox.Clear();
            WarrantyPeriodTextBox.Clear();
            ExpirationDatePicker.SelectedDate = null;
            SizeTextBox.Clear();
            MaterialTextBox.Clear();
        }
    }
}