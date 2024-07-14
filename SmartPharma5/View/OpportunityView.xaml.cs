using Acr.UserDialogs;
using DevExpress.Maui.Editors;
using SmartPharma5.Model;
using SmartPharma5.ViewModel;

namespace SmartPharma5.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class OpportunityView : ContentPage
{
    public Opportunity Opportunity;
    private bool isDragging = false;
    public OpportunityView()
    {
        InitializeComponent();
        
    }
    private void OnButtonTouchMove(object sender, TouchEventArgs e)
    {
        if (isDragging)
        {
            // Update the position of the button based on touch coordinates
            var button = (Button)sender;
            //button.TranslationX = e.Location.X;
            //button.TranslationY = e.Location.Y;
            
        }
    }

    private void OnButtonTouchUp(object sender, TouchEventArgs e)
    {
        // Stop dragging when the touch is released
        isDragging = false;
    }
    public OpportunityView(Opportunity opportunity)
    {
        InitializeComponent();
        DbConnection.Deconnecter();
        Opportunity = opportunity;

        if (Opportunity.Id == 0)
            quotation.IsVisible = false;
        if (Opportunity.Id != 0)
            Opportunity = new Opportunity(Opportunity.Id);

        if (opportunity.Id != 0 && Opportunity.StateLines != null)
            foreach (SmartPharma5.Model.Opportunity.State S in Opportunity.StateLines)
            {
                Label header = new Label
                {
                    BackgroundColor = Colors.White,
                    Margin = 0,
                    Padding = new Thickness(4),
                    FontSize = 12,
                    TextColor = Colors.Black,
                    HorizontalOptions = LayoutOptions.Center

                };

                ImageButton imageButton = new ImageButton
                {
                    HorizontalOptions = LayoutOptions.End,
                    Margin = 0,
                    Padding = 0,
                    BackgroundColor = Colors.White,
                    CornerRadius = 0,


                    Source = "chevronrightsolid.png",
                    BorderColor = Colors.White,
                    Scale = 1,
                };


                header.Text = S.name.ToString() + "\n" + S.Date.ToString("dd/MM/yyyy  h:mm tt");
                TimeLine.Children.Add(header);
                TimeLine.Children.Add(imageButton);
                TimeLine.BackgroundColor = Colors.White;
                TimeLine.Margin = new Thickness(1, 4, 1, 0);
            }
        BindingContext = new OpportunityViewModel(Opportunity);
    }

    private void AutoCompleteEdit_TextChanged(object sender, DevExpress.Maui.Editors.AutoCompleteEditTextChangedEventArgs e)
    {
        AutoCompleteEdit edit = sender as AutoCompleteEdit;
        var search = edit.Text.ToLowerInvariant().ToString();
        var shop = BindingContext as OpportunityViewModel;


        if (string.IsNullOrWhiteSpace(search))
        {
           WholeCollectionView.ItemsSource = shop.WholesalerList.ToList();
        }
        else
        {
            WholeCollectionView.ItemsSource = shop.WholesalerList.Where(i => i.Name.ToLowerInvariant().Contains(search)).ToList();
        }
    }

    [Obsolete]
    protected override bool OnBackButtonPressed()
    {

        Device.BeginInvokeOnMainThread(async () =>
        {
            if (await App.Current.MainPage.DisplayAlert("Alert?", "Are you sure you want to exit this opportunity?\nYou will not be able to continue it.", "Yes", "No"))
            {
                base.OnBackButtonPressed();

                await App.Current.MainPage.Navigation.PopAsync();
            }
        });

        // Always return true because this method is not asynchronous.
        // We must handle the action ourselves: see above.
        return true;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Partner partner)
        {
            var shop = BindingContext as OpportunityViewModel;
            shop.WholesalerPopup = false;

            shop.Opportunity.Dealer = (int)partner.Id;
            shop.Opportunity.dealerName = (string)partner.Name;

            shop.WholeSalerRemoveIsvisible = true;
            shop.WholesalerTitleVisible = true;
        }
    }

    private async void SimpleButton_Clicked(object sender, EventArgs e)
    {
         //await Shell.Current.GoToAsync("../OpportunityView");

    }

    private async void SimpleButton_Clicked_1(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("..");
    }


}