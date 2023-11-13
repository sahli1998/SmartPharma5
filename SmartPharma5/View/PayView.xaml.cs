using SmartPharma5.Model;
using SmartPharma5.ViewModel;

namespace SmartPharma5.View;

public partial class PayView : ContentPage
{
    public PayView()
    {
        InitializeComponent();
        //BindingContext = new PaymentViewModel();
    }
    public PayView(Payment payment)
    {
        InitializeComponent();
        BindingContext = new PaymentViewModel(payment);
    }
    private void DataGridView_Tap(object sender, DevExpress.Maui.DataGrid.DataGridGestureEventArgs e)
    {
        var ovm = BindingContext as ViewModel.PaymentViewModel;
        ovm.Payment.SetAmount();
    }
}