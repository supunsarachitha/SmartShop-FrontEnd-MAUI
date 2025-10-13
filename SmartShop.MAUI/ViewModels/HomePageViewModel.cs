using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace SmartShop.MAUI.ViewModels
{
    public class HomePageViewModel
    {
        public ICommand NavigateToProfileCommand { get; }
        public ICommand NavigateToInvoiceCommand { get; }
        public ICommand NavigateToHistoryCommand { get; }
        public ICommand NavigateToReportsCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }

        public HomePageViewModel()
        {
            NavigateToProfileCommand = new AsyncRelayCommand(NavigateToProfileAsync);
            NavigateToInvoiceCommand = new AsyncRelayCommand(NavigateToInvoiceAsync);
            NavigateToHistoryCommand = new AsyncRelayCommand(NavigateToHistoryAsync);
            NavigateToReportsCommand = new AsyncRelayCommand(NavigateToReportsAsync);
            NavigateToSettingsCommand = new AsyncRelayCommand(NavigateToSettingsAsync);
        }

        private async Task NavigateToProfileAsync()
        {
            await Shell.Current.GoToAsync("//ProfilePage");
        }

        private async Task NavigateToInvoiceAsync()
        {
            await Shell.Current.GoToAsync("//InvoicePage");
        }

        private async Task NavigateToHistoryAsync()
        {
            await Shell.Current.GoToAsync("//HistoryPage");
        }

        private async Task NavigateToReportsAsync()
        {
            await Shell.Current.GoToAsync("//ReportsPage");
        }

        private async Task NavigateToSettingsAsync()
        {
            await Shell.Current.GoToAsync("//SettingsPage");
        }
    }
}
