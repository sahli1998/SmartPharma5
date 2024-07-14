//using Android.Content.Res;
using DevExpress.Maui.Controls;
using DevExpress.Maui.Editors;
using MvvmHelpers;
using SmartPharma5.Model;
using SmartPharma5.ViewModel;
using System.Collections.Generic;

namespace SmartPharma5.View;

public partial class ProductListView : ContentPage
{
    string search = string.Empty;
    string searchgratuite = string.Empty;
    public ProductListView()
    {
        InitializeComponent();
        BindingContext = new ProductListViewModel();
        //double largeurEcran = DeviceDisplay.MainDisplayInfo.Width ;
        //DexPoop.WidthRequest = largeurEcran;

        //// Créez un nouveau DXPopup
        ////DXPopup popup = new DXPopup();

        //// Définissez la largeur du popup pour qu'elle corresponde à la largeur de l'écran
        ////popup.Width = largeurEcran;

    }

    public ProductListView(Opportunity opportunity, ObservableRangeCollection<Product> productList)
    {
        try
        {
            InitializeComponent();
            BindingContext = new ProductListViewModel(opportunity, productList);
            //double largeurEcran = DeviceDisplay.MainDisplayInfo.Width;
            //DexPoop.WidthRequest = largeurEcran;
        }
        catch (Exception ex)
        {

        }
     
    }

    public ProductListView(SaleQuotation quotation, ObservableRangeCollection<Product> productList,List<SaleQuotationLine> salesQuotationLines,List<int> ListDeleted, List<int> ListUpdated, List<SaleQuotationLine> ListNew)
    {
        try
        {
            InitializeComponent();
            BindingContext = new ProductListViewModel(quotation,productList,  salesQuotationLines,  ListDeleted,  ListUpdated,  ListNew);


            //double largeurEcran = DeviceDisplay.MainDisplayInfo.Width;
            //DexPoop.WidthRequest = largeurEcran;
        }
        catch (Exception ex)
        {

        }

    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;
        QuantityLabel.Value = Convert.ToInt32(value);
    }

    private void AutoCompleteEdit_TextChanged(object sender, AutoCompleteEditTextChangedEventArgs e)
    {
        AutoCompleteEdit edit = sender as AutoCompleteEdit;
        search = edit.Text.ToLowerInvariant().ToString();
        var shop = BindingContext as ProductListViewModel;


        if (string.IsNullOrWhiteSpace(search))
        {
            ProductCollectionView.ItemsSource = shop.ProductList.ToList();
        }
        else
        {
            ProductCollectionView.ItemsSource = shop.ProductList.Where(i => i.name.ToLowerInvariant().Contains(search) && i.name.ToLowerInvariant().Contains(searchgratuite)).ToList();
        }

    }
    private void AutoCompleteEdit_TextChanged2(object sender, AutoCompleteEditTextChangedEventArgs e)
    {
        AutoCompleteEdit edit = sender as AutoCompleteEdit;
        searchgratuite = edit.Text.ToLowerInvariant().ToString();
        var shop = BindingContext as ProductListViewModel;


        if (string.IsNullOrWhiteSpace(searchgratuite))
        {
            ProductCollectionView.ItemsSource = shop.ProductList.ToList();
        }
        else
        {
            ProductCollectionView.ItemsSource = shop.ProductList.Where(i => i.name.ToLowerInvariant().Contains(search) && i.name.ToLowerInvariant().Contains(searchgratuite)).ToList();
        }

    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {



        if (sender is Frame frame && frame.BindingContext is Product prod)
        {
            var shop = BindingContext as ProductListViewModel;
            shop.Product = prod;
            shop.ProductPopup = true;
        }

    }
}