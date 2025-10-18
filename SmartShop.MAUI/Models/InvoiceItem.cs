using System.ComponentModel.DataAnnotations;

namespace SmartShop.MAUI.Models
{
    public class InvoiceItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}