using SmartShop.MAUI.ViewModels;

namespace SmartShop.MAUI.Views;

public partial class InvoicePage : ContentPage
{
	public InvoicePage(InvoicePageViewModel invoicePageViewModel)
	{
		InitializeComponent();
		BindingContext = invoicePageViewModel;
    }
}