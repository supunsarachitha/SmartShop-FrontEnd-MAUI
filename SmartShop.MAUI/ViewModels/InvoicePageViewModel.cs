using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SmartShop.MAUI.ViewModels
{
    public class InvoicePageViewModel : ObservableObject
    {
        public string InvoiceNumber { get; set; } = "INV-001";
        public string InvoiceDate { get; set; } = "2023-10-01";
        public string CustomerName { get; set; } = "John Doe";

        public ObservableCollection<InvoiceItem> InvoiceItems { get; set; } = new ObservableCollection<InvoiceItem>
        {
            new InvoiceItem { ItemName = "Item 1", Quantity = 2, Price = 10.00 },
            new InvoiceItem { ItemName = "Item 2", Quantity = 1, Price = 20.00 },
            new InvoiceItem { ItemName = "Item 3", Quantity = 3, Price = 15.00 }
        };

        public double TotalAmount => CalculateTotal();

        private double CalculateTotal()
        {
            double total = 0;
            foreach (var item in InvoiceItems)
            {
                total += item.Quantity * item.Price;
            }
            return total;
        }
    }

    public class InvoiceItem
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}