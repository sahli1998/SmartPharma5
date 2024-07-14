using Acr.UserDialogs;
//using Android.Provider;
using MySqlConnector;
using SmartPharma5.Model;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartPharma5.View;

public partial class AddNewContact : ContentPage
{
    private List<Country> countries;
    public int partner_id { get; set; }
    public AddNewContact(int id)
	{
		InitializeComponent();
        this.partner_id = id;
        maritalStatusPicker.ItemsSource = GetListMaritalStatus().Result;
        jobPositionPicker.ItemsSource = GetListJobPosition().Result;
        LoadNationalitiesAsync().GetAwaiter();

    }
    public AddNewContact()
    {
        InitializeComponent();
        maritalStatusPicker.ItemsSource = GetListMaritalStatus().Result;
        jobPositionPicker.ItemsSource = GetListJobPosition().Result;
        LoadNationalitiesAsync().GetAwaiter();
    }
    private void OnHandicapToggled(object sender, ToggledEventArgs e)
    {
        handicapDescriptionStack.IsVisible = e.Value;
    }

    private async Task LoadNationalitiesAsync()
    {
        HttpClient client = new HttpClient();
        try
        {
            string apiUrl = "https://countriesnow.space/api/v0.1/countries"; // Replace with your actual API endpoint
            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var countriesResponse = JsonSerializer.Deserialize<CountriesResponse>(json);
                if (countriesResponse != null && countriesResponse.Data != null)
                {
                    countries = countriesResponse.Data;
                    var nationalities = new List<string>();
                    foreach (var country in countries)
                    {
                        nationalities.Add(country.country);
                    }
                    nationalityPicker.ItemsSource = nationalities;
                }
                else
                {
                    await DisplayAlert("Error", "Unable to parse nationalities", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Unable to fetch nationalities", "OK");
            }
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void OnNationalityChanged(object sender, EventArgs e)
    {
        if (nationalityPicker.SelectedIndex == -1)
        {
            //citiesPicker.ItemsSource = null;
            return;
        }

        var selectedCountry = nationalityPicker.SelectedItem.ToString();
        var country = countries.Find(c => c.country == selectedCountry);

        if (country != null)
        {
            //citiesPicker.ItemsSource = country.Cities;
        }
        else
        {
            //citiesPicker.ItemsSource = null;
        }
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        // Handle form submission here
        UserDialogs.Instance.Loading("Adding New Contact, please wait...");
        Item item1 = maritalStatusPicker.SelectedItem as Item;
        int job_position = item1.id;
        string firstName = firstNameEntry.Text;
        string lastName = lastNameEntry.Text;
        //string memo = memoEditor.Text;
        int sex = hommeRadioButton.IsChecked ? 1 : 2;
        Item item = maritalStatusPicker.SelectedItem as Item;
        int maritalStatus = item.id;
        DateTime birthDate = birthDatePicker.Date;
        string birthPlace = birthPlaceEntry.Text;
        string nationality = nationalityPicker.SelectedItem?.ToString();
        //string city = citiesPicker.SelectedItem?.ToString();
        string address = addressEntry.Text;
        string identity = identityEntry.Text;
        bool handicap = handicapSwitch.IsToggled;
        string handicapDescription = handicap ? handicapDescriptionEditor.Text : null;
        await InsertNewContact(lastName,firstName,sex,maritalStatus,birthDate,birthPlace,handicap,handicapDescription,job_position);
        await App.Current.MainPage.DisplayAlert("INFO","CONTACT SAVED SUCCEFULY","OK") ;
        await App.Current.MainPage.Navigation.PopAsync();
        await App.Current.MainPage.Navigation.PopAsync();
        UserDialogs.Instance.HideLoading();
    }


    public  async Task InsertNewContact(string lastName,string firstName,int sex,int matrialStatus,DateTime birthDate,string birthPalce,bool handicap,string? handicapdiscription,int job_position)
    {
        UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
        await Task.Delay(500);
        if (await DbConnection.Connecter3())
        {
            string sqlCmd = "insert into atooerp_person(create_date,first_name,last_name,sex,marital_status,birth_date,birth_place,handicap,handicap_description) " +
                "values ('"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , '"+firstName+"','"+lastName+"',"+sex+","+matrialStatus+",'"+birthDate.ToString("yyyy-MM-dd HH:mm:ss") + "','"+birthPalce+"',"+handicap+",'"+handicapdiscription+"') ;select max(Id) from atooerp_person;";
            MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
            try
            {
              int result = int.Parse(cmd.ExecuteScalar().ToString()); ;
                if(result != 0)
                {
                    string sqlCmd2 = "insert into commercial_partner_contact(partner,person,job_position) values ("+this.partner_id + ","+result+","+job_position+")";
                    MySqlCommand cmd1 = new MySqlCommand(sqlCmd2, DbConnection.con);
                    cmd1.ExecuteScalar();
                }
                
            }
            catch (Exception ex)
            {

            }
        }
        else
        {

        }
        UserDialogs.Instance.HideLoading();
    }

    public async static Task<List<Item>> GetListMaritalStatus()
    {

        string sqlCmd = "SELECT * from atooerp_person_marital_status ;";


        List<Item> partner = new List<Item>();
        DbConnection.Deconnecter();
        DbConnection.Connecter();

        MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);

        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            try
            {
                partner.Add(new Item(Convert.ToInt32(reader["id"]), Convert.ToString(reader["name"])));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        DbConnection.Deconnecter();

        return partner;

    }
    public async static Task<List<Item>> GetListJobPosition()
    {

        string sqlCmd = "SELECT * from commercial_job_position ;";


        List<Item> partner = new List<Item>();
        DbConnection.Deconnecter();
        DbConnection.Connecter();

        MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);

        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            try
            {
                partner.Add(new Item(Convert.ToInt32(reader["id"]), Convert.ToString(reader["name"])));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        DbConnection.Deconnecter();

        return partner;

    }

}

public class Item
{
   public int id { get; set; }
    public string name { get; set; }

    public Item(int id,string name)
    {
        this.id = id;
        this.name = name;      
    }
}

public class CountriesResponse
{
    [JsonPropertyName("error")]
    public bool Error { get; set; }

    [JsonPropertyName("msg")]
    public string Msg { get; set; }

    [JsonPropertyName("data")]
    public List<Country> Data { get; set; }
}

public class Country
{
    [JsonPropertyName("country")]
    public string country { get; set; }

    [JsonPropertyName("cities")]
    public List<string> Cities { get; set; }
}