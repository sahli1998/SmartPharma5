//using Acr.UserDialogs;
using Acr.UserDialogs;
using MvvmHelpers;
using MvvmHelpers.Commands;
using MySqlConnector;
using SmartPharma5.Model;
using SmartPharma5.View;

namespace SmartPharma5.ViewModel
{
    class AddPartnerTempMV : BaseViewModel
    {
        private string inputName;
        public string InputName { get => inputName; set => SetProperty(ref inputName, value); }

        


        private bool actPop;
        public bool ActPop { get => actPop; set => SetProperty(ref actPop, value); } 

        private string inputEmail;
        public string InputEmail { get => inputEmail; set => SetProperty(ref inputEmail, value); }

        private string inputCountry;
        public string InputCountry { get => inputCountry; set => SetProperty(ref inputCountry, value); }

        private string inputCity;
        public string InputCity { get => inputCity; set => SetProperty(ref inputCity, value); }

        private string inputStreet;
        public string InputStreet { get => inputStreet; set => SetProperty(ref inputStreet, value); }

        private string inputNumber;
        public string InputNumber { get => inputNumber; set => SetProperty(ref inputNumber, value); }

        private string inputPostaleCode;
        public string InputPostaleCode { get => inputPostaleCode; set => SetProperty(ref inputPostaleCode, value); }

        private string inputFax;
        public string InputFax { get => inputFax; set => SetProperty(ref inputFax, value); }

        private int inputCategory;
        public int InputCategory { get => inputCategory; set => SetProperty(ref inputCategory, value); }

        private string inputState;
        public string InputState { get => inputState; set => SetProperty(ref inputState, value); }


        private bool inputCustomer;
        public bool InputCustomer { get => inputCustomer; set => SetProperty(ref inputCustomer, value); }


        private bool inputSupplier;
        public bool InputSupplier { get => inputSupplier; set => SetProperty(ref inputSupplier, value); }

        private string vat_code;
        public string Vat_Code { get => vat_code; set => SetProperty(ref vat_code, value); }

        public AsyncCommand HomePage { get; }

        private List<Comercial_category> catgory_list;
        public List<Comercial_category> Catgory_list { get => catgory_list; set => SetProperty(ref catgory_list, value); }


        private Comercial_category selected_category;
        public Comercial_category Selected_category { get => selected_category; set => SetProperty(ref selected_category, value); }


        private List<Profile> profiles;
        public List<Profile> Profiles { get => profiles; set => SetProperty(ref profiles, value); }


        private Profile selected_profile;
        public Profile Selected_profile { get => selected_profile; set => SetProperty(ref selected_profile, value); }

        private List<ProfileAttributes> listAttributes;
        public List<ProfileAttributes> ListAttributes { get => listAttributes; set => SetProperty(ref listAttributes, value); }

        public AsyncCommand AddPartnerTemp { get; }
        public AsyncCommand ChangeProfile { get; }








        public AddPartnerTempMV()
        {
            ActPop = false;
            HomePage = new AsyncCommand(homePage);
            ChangeProfile = new AsyncCommand(changeProfileMV);
            AddPartnerTemp = new AsyncCommand(InsertPartnerTemp);

            try
            {
                Catgory_list = Comercial_category.GetCommercialCategory().Result;
                Profiles = Profile.getAllProfile().Result;

            }
            catch (Exception ex)
            {

            }

        }
        private async Task homePage()
        {
            UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
            await Task.Delay(500);

            try
            {

                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()));
            }
            catch (Exception ex)
            {
                await DbConnection.ErrorConnection();
            }

            UserDialogs.Instance.HideLoading();

        }

        private async Task InsertPartnerTemp()
        {
            try
            {
                if (InputName is null)
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Must Write the Name first", "Ok"); return;
                }

                if (InputCategory == -1)
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Must Choose Category first", "Ok");return;
                }


                int? p = await ProfileAttributes.InsertTempPartner(InputName, InputCountry, InputStreet, InputCity, InputState, InputPostaleCode, InputEmail, InputFax, Selected_category.id, InputCustomer, InputSupplier, Vat_Code);


                if (Selected_profile != null)
                {
                    foreach (ProfileAttributes attribute in listAttributes)
                    {

                        if (attribute.is_null == false)
                        {
                            if (attribute.HasString && attribute.String_value == null)
                            {
                                await App.Current.MainPage.DisplayAlert("Warnning", attribute.LabelName + " is Required", "OK");
                                return;

                            }
                            else if (attribute.HasBool && attribute.Bool_value == null)
                            {
                                await App.Current.MainPage.DisplayAlert("Warnning", attribute.LabelName + " is Required", "OK");
                                return;
                            }
                            else if (attribute.HasDate && attribute.Date_value == null)
                            {
                                await App.Current.MainPage.DisplayAlert("Warnning", attribute.LabelName + " is Required", "OK");
                                return;
                            }
                            else if (attribute.HasNumber && attribute.Number_value == null)
                            {
                                await App.Current.MainPage.DisplayAlert("Warnning", attribute.LabelName + " is Required", "OK");
                                return;
                            }
                            else if (attribute.HasDecimal && attribute.Number_value == null)
                            {
                                await App.Current.MainPage.DisplayAlert("Warnning", attribute.LabelName + " is Required", "OK");
                                return;
                            }
                            else if (attribute.HasMultiple && attribute.selected_item == null)
                            {
                                await App.Current.MainPage.DisplayAlert("Warnning", attribute.LabelName + " is Required", "OK");
                                return;
                            }

                        }
                    }

                    int? id_instance = await ProfileAttributes.InsertInstanceWithTempPartner(p, Selected_profile.Id);
                    foreach (ProfileAttributes attribute in listAttributes)
                    {
                        string sqlCmd = "";
                        if (attribute.HasString && attribute.String_value != "")
                        {
                            sqlCmd = "insert into marketing_profile_attribut_value_temp(profile_attribute,profile_instance_temp,user,employe,state,create_date,string_value) values (" + attribute.Id + "," + id_instance + "," + user_contrat.iduser + "," + user_contrat.id_employe + ",1,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + attribute.String_value + "');";
                        }
                        else if (attribute.HasBool)
                        {
                            sqlCmd = "insert into marketing_profile_attribut_value_temp(profile_attribute,profile_instance_temp,user,employe,state,create_date,boolean_value) values (" + attribute.Id + "," + id_instance + "," + user_contrat.iduser + "," + user_contrat.id_employe + ",1,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + attribute.Bool_value + ");";
                        }
                        else if (attribute.HasDate && attribute.Date_value.ToString() != "")
                        {
                            sqlCmd = "insert into marketing_profile_attribut_value_temp(profile_attribute,profile_instance_temp,user,employe,state,create_date,date_value) values (" + attribute.Id + "," + id_instance + "," + user_contrat.iduser + "," + user_contrat.id_employe + ",1,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + attribute.Date_value?.ToString("yyyy-MM-dd HH:mm:ss") + "');";
                        }
                        else if (attribute.HasNumber && attribute.Number_value.ToString() != "")
                        {
                            sqlCmd = "insert into marketing_profile_attribut_value_temp(profile_attribute,profile_instance_temp,user,employe,state,create_date,int_value) values (" + attribute.Id + "," + id_instance + "," + user_contrat.iduser + "," + user_contrat.id_employe + ",1,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Convert.ToInt32(attribute.Number_value) + ");";
                        }
                        else if (attribute.HasDecimal && attribute.Number_value.ToString() != "")
                        {
                            sqlCmd = "insert into marketing_profile_attribut_value_temp(profile_attribute,profile_instance_temp,user,employe,state,create_date,decimal_value) values (" + attribute.Id + "," + id_instance + "," + user_contrat.iduser + "," + user_contrat.id_employe + ",1,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + attribute.Number_value + ");";
                        }
                        else if (attribute.HasMultiple && attribute.Multiple_value.ToString() != "" && attribute.Selected_item is not null)
                        {
                           

                            sqlCmd = "insert into marketing_profile_attribut_value_temp(profile_attribute,profile_instance_temp,user,employe,state,create_date,type) values (" + attribute.Id + "," + id_instance + "," + user_contrat.iduser + "," + user_contrat.id_employe + ",1,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + attribute.Selected_item.id + ");";
                        }
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                            cmd.ExecuteScalar();

                        }
                        catch (Exception ex)
                        {
                            //await App.Current.MainPage.DisplayAlert("Warnning", "Failed Connection", "OK");
                            //return;


                        }

                    }

                }

            }
            catch (Exception ex)
            {
                return;

            }
            await App.Current.MainPage.DisplayAlert("Info","Partner Request Added Succefuly!","Done");
            UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
            await Task.Delay(500);
            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()));
            //await App.Current.MainPage.Navigation.PopAsync();
            UserDialogs.Instance.HideLoading();


        }

        private async Task changeProfileMV()
        {
            ActPop = true;
            UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
            await Task.Delay(500);
            try
            {
                
                
                ListAttributes = await ProfileAttributes.GetAttributesProfile2(Convert.ToInt32(Selected_profile.Id));
                ActPop = false; 


            }
            catch (Exception ex)
            {

            }
            UserDialogs.Instance.HideLoading();
        }
    }
}
