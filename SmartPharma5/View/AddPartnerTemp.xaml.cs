using DevExpress.Maui.Editors;
using SmartPharma5.Model;
using SmartPharma5.ViewModel;

namespace SmartPharma5.View;

public partial class AddPartnerTemp : ContentPage
{
    public bool isUserInteraction = false;
    public AddPartnerTemp()
    {
        InitializeComponent();
        BindingContext = new AddPartnerTempMV();
    }
    private void comboBox_TouchUp(object sender, EventArgs e)
    {
        isUserInteraction = true;

    }

    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isUserInteraction)
        {
            try
            {
                ComboBoxEdit picker = (ComboBoxEdit)sender;
                int selectedIndex = picker.SelectedIndex;
                bool find_fils = false;
                atooerp_element test10 = picker.SelectedItem as atooerp_element;
                //atooerp_element item = picker.ItemsSource as atooerp_element;
                var ovm = BindingContext as AddPartnerTempMV;

                foreach (ProfileAttributes attribute in ovm.ListAttributes)
                {

                    if (attribute.HasMultiple)
                    {

                        var test1 = attribute.HasMultiple;
                        var test2 = attribute.type_parent_multi_value;
                        var test3 = test10.id_type;

                        if (attribute.type_parent_multi_value == test10.id_type && attribute.Rank > test10.Rank)
                        {

                            find_fils = true;


                            attribute.Selected_item = null;
                            attribute.Multiple_value = 0;
                            attribute.List_item = attribute.List_item_fixe;

                            attribute.List_item = attribute.List_item.Where(p => p.parent == test10.id || p.parent == null).ToList();
                        }

                        if (find_fils == true && attribute.type_parent_multi_value != test10.id_type)
                        {
                            return;
                        }

                    }
                }


            }
            catch (Exception ex)
            {

            }

            isUserInteraction = false;
        }

    }





    private void ComboBoxEdit_Tap(object sender, System.ComponentModel.HandledEventArgs e)
    {
        isUserInteraction = true;

    }
}