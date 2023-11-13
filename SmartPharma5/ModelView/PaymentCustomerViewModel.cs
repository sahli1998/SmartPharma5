using MvvmHelpers;
using MvvmHelpers.Commands;

/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Avant :
using SmartPharma5.View;
using MvvmHelpers;
using MvvmHelpers.Commands;
Après :
using SmartPharma5;
using SmartPharma5.Model;
using SmartPharma5.Commands;
*/
using SmartPharma5.Model;
using SmartPharma5.View;
//using Xamarin.Essentials;

namespace SmartPharma5.ViewModel
{
    internal class PaymentCustomerViewModel : BaseViewModel
    {
        public AsyncCommand ExitCommand { get; set; }
        public AsyncCommand LogoutCommand { get; set; }
        public bool actpopup = false;
        public bool ActPopup { get => actpopup; set => SetProperty(ref actpopup, value); }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Partner> TapCommand { get; }
        public Payment Payment { get; set; }
        private ObservableRangeCollection<Partner> clientList;

        public ObservableRangeCollection<Partner> ClientList { get => clientList; set => SetProperty(ref clientList, value); }


        private bool testCon = false;
        public bool TestCon { get => testCon; set => SetProperty(ref testCon, value); }

        private bool loading = false;
        public bool Loading { get => loading; set => SetProperty(ref loading, value); }
        public PaymentCustomerViewModel()
        {
        }
        public PaymentCustomerViewModel(Payment Payment)
        {
            RefreshCommand = new AsyncCommand(Refresh);
            TapCommand = new AsyncCommand<Partner>(TapCommandAsync);
            ExitCommand = new AsyncCommand(Exit);
            LogoutCommand = new AsyncCommand(Logout);
            ClientList = new ObservableRangeCollection<Partner>();
            Task.Run(() => LoadCustomer());
        }

        private async Task TapCommandAsync(object partner)
        {
            Loading = true;
            await Task.Delay(500);
            uint idagent = (uint)Preferences.Get("idagent", Convert.ToUInt32(null));
            Payment = new Payment((int)idagent, partner as Partner);
            await App.Current.MainPage.Navigation.PushAsync(new PaymentView(Payment));
            Loading = false;
        }


        private async Task LoadCustomer()
        {
            ActPopup = true;
            //await Task.Delay(2000);
            ClientList.Clear();
            try
            {
                var C = Task.Run(() => Partner.GetPartnaire());
                ClientList = new ObservableRangeCollection<Partner>(await C);
            }
            catch (Exception ex)
            {
                ClientList = new ObservableRangeCollection<Partner>();
                TestCon = true;

                ClientList = new ObservableRangeCollection<Partner>();

            }

            ActPopup = false;
        }
        private async Task Refresh()
        {
            await Task.Delay(2000);
            ActPopup = true;
            ClientList.Clear();
            try
            {
                var C = Task.Run(() => Partner.GetPartnaire());
                ClientList = new ObservableRangeCollection<Partner>(await C);
                TestCon = false;
            }
            catch (Exception ex)
            {
                ClientList = new ObservableRangeCollection<Partner>();
                TestCon = true;

                ClientList = new ObservableRangeCollection<Partner>();

            }
            ActPopup = false;

        }
        private async Task Logout()
        {
            var r = await App.Current.MainPage.DisplayAlert("Warning", "Are you sure you want to lougout!", "Yes", "No");
            if (r)
            {
                Preferences.Set("idagent", Convert.ToUInt32(null));
                //await App.Current.MainPage.Navigation.PushAsync(new LoginView());
            }
        }

        private async Task Exit()
        {
            var r = await App.Current.MainPage.DisplayAlert("Warning", "Are you sure you want to exit!", "Yes", "No");
            if (r)
            {
                await App.Current.MainPage.Navigation.PopToRootAsync();
            }
        }
    }
}
