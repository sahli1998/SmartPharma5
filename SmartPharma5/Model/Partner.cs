//using Acr.UserDialogs;
using Acr.UserDialogs;
using MvvmHelpers.Commands;
using MySqlConnector;
using SmartPharma5.View;
using SQLite;
using System.ComponentModel;
using System.Data;
//using Xamarin.Forms;
//using static Xamarin.Essentials.Permissions;

namespace SmartPharma5.Model
{



    public class Partner
    {
        [PrimaryKey, Column("Id")]
        public uint Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Reference")]
        public string Reference { get; set; }
        [Column("CreateDate")]
        public DateTime CreateDate { get; set; }
        [Column("ChecSocity")]
        public bool ChecSocity { get; set; }
        [Column("Website")]
        public string Website { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }
        public string FullAdress { get; }
        [Column("Mobile")]
        public string Mobile { get; set; }
        [Column("Fax")]
        public string Fax { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        public Image Image { get; set; }
        public ImageSource Photo { get; }
        [Column("Note")]
        public string Note { get; set; }
        [Column("Customer")]
        public bool Customer { get; set; }
        [Column("Supplier")]
        public bool Supplier { get; set; }
        [Column("PaymentMethodSupplier")]
        public int PaymentMethodSupplier { get; set; }
        [Column("PaymentConditionSupplier")]
        public int PaymentConditionSupplier { get; set; }
        [Column("PaymentConditionCustomer")]
        public int PaymentConditionCustomer { get; set; }
        [Column("Socity")]
        public int Socity { get; set; }
        [Column("Number")]
        public string Number { get; set; }
        [Column("Street")]
        public string Street { get; set; }
        [Column("City")]
        public string City { get; set; }
        [Column("State")]
        public string State { get; set; }
        [Column("Country")]
        public string Country { get; set; }
        [Column("PostalCode")]
        public string PostalCode { get; set; }
        [Column("DeliveryNumber")]
        public string DeliveryNumber { get; set; }
        [Column("DeliveryStreet")]
        public string DeliveryStreet { get; set; }
        [Column("DeliveryCity")]
        public string DeliveryCity { get; set; }
        [Column("DeliveryState")]
        public string DeliveryState { get; set; }
        [Column("DeliveryCountry")]
        public string DeliveryCountry { get; set; }
        [Column("DeliveryPostalCode")]
        public string DeliveryPostalCode { get; set; }
        [Column("CreditLimit")]
        public decimal CreditLimit { get; set; }
        [Column("Currency")]
        public uint Currency { get; set; }
        [Column("JobPosition")]
        public string JobPosition { get; set; }
        [Column("CustomsCode")]
        public string CustomsCode { get; set; }
        [Column("VatCode")]
        public string VatCode { get; set; }
        [Column("TradeRegister")]
        public string TradeRegister { get; set; }
        [Column("Picture")]
        public byte[] Picture { get; set; }
        [Column("PaymentMethodCustomer")]
        public uint PaymentMethodCustomer { get; set; }
        [Column("RestAmount")]
        public decimal RestAmount { get; set; }
        [Column("DueDate")]
        public DateTime DueDate { get; set; }
        [Column("Actif")]
        public bool Actif { get; set; }
        [Column("CustomerDiscount")]
        public decimal CustomerDiscount { get; set; }
        [Column("SupplierDiscount")]
        public decimal SupplierDiscount { get; set; }
        [Column("VatExemption")]
        public bool VatExemption { get; set; }
        [Column("CustumerWithholdingTax")]
        public uint CustumerWithholdingTax { get; set; }
        [Column("SupplierWithholdingTax")]
        public uint SupplierWithholdingTax { get; set; }
        [Column("Activity")]
        public string Activity { get; set; }
        [Column("Category")]
        public uint Category { get; set; }
        public string Category_Name { get; set; }
        public decimal? Rest { get; set; }
        public DateTime? Due_date { get; set; }

        public AsyncCommand ShowProfile { get; set; }

        public AsyncCommand ShowForms { get; set; }
        public Partner()
        {
            ShowProfile = new AsyncCommand(showProfileFonc);
        }
        public Partner(uint id, string name)
        {
            Id = id;
            Name = name;
            ShowProfile = new AsyncCommand(showProfileFonc);
            ShowForms = new AsyncCommand(showFormFonc);
        }
        public Partner(uint id, string name, string phone, string country, string email, string reference, int pcc, uint pmc, decimal? rest, DateTime? due_date)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Country = country;
            Email = email;
            Reference = reference;
            PaymentConditionCustomer = pcc;
            PaymentMethodCustomer = pmc;
            Rest = rest;
            Due_date = due_date;
            ShowProfile = new AsyncCommand(showProfileFonc);
            ShowForms = new AsyncCommand(showFormFonc);

        }

        public Partner(uint id, string name, string phone, string country, string postale_code, string street, string state, string category_name, string vat_code, string fax, string number)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Country = country;
            State = state;
            PostalCode = postale_code;
            State = state;
            Street = street;
            Category_Name = category_name;
            VatCode = vat_code;
            Fax = fax;
            Number = number;
            ShowProfile = new AsyncCommand(showProfileFonc);
            ShowForms = new AsyncCommand(showFormFonc);


        }
        public Partner(int id, string name, uint category, string phone, string street, string city, string postale_code, string state, string email, ImageSource img, string category_name)
        {
            Id = (uint)id;
            Category = category;
            Name = name;
            Phone = phone;
            FullAdress = street + " " + city + " " + postale_code;
            State = state;
            Email = email;
            Photo = img;
            Category_Name = category_name;
            ShowProfile = new AsyncCommand(showProfileFonc);
            ShowForms = new AsyncCommand(showFormFonc);

        }
        public Partner(uint id, string name, string phone, string country, string email, string reference, int pcc, uint pmc)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Country = country;
            Email = email;
            Reference = reference;
            PaymentConditionCustomer = pcc;
            PaymentMethodCustomer = pmc;
            ShowProfile = new AsyncCommand(showProfileFonc);
            ShowForms = new AsyncCommand(showFormFonc);

        }
        private async Task showFormFonc()
        {
            await App.Current.MainPage.Navigation.PushAsync(new FormListView(this));
        }
        private async Task showProfileFonc()
        {

            UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
            await Task.Delay(500);
            await App.Current.MainPage.Navigation.PushAsync(new ProfileUpdate(Id));
            UserDialogs.Instance.HideLoading();

        }
        public static async Task<BindingList<Partner>> GetPartnaire()
        {
            string sqlCmd = "SELECT cp.Id,cp.name,cp.mobile,cp.country,cp.email,cp.reference,cp.payment_condition_customer," +
                    "cp.payment_method_customer,yy.due_date,yy.rest " +
                    "FROM commercial_partner cp Left Join " +
                    "(SELECT partner, sum(restAmount) as rest, min(due_date) as due_date " +
                    "from(SELECT  sale_balance.restAmount, sale_balance.due_date, commercial_partner.Id as partner " +
                    "FROM     commercial_partner LEFT OUTER JOIN sale_balance " +
                    "ON sale_balance.IdPartner = commercial_partner.Id LEFT OUTER JOIN " +
                    "commercial_partner_category ON commercial_partner.category = commercial_partner_category.Id " +
                    "WHERE(FORMAT(sale_balance.restAmount, 3) <> 0)) xx " +
                    "group by partner " +
                    "order by due_date ) yy on cp.Id = yy.partner " +
                    "WHERE(cp.customer = 1) AND(cp.chec_socity = 1) AND(cp.actif = 1)" +
                    " order by yy.due_date is null, yy.due_date asc;";
            BindingList<Partner> list = new BindingList<Partner>();



            if (await DbConnection.Connecter3())
            {

                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    try
                    {
                        list.Add(new Partner(
                            Convert.ToUInt32(reader["Id"]),
                            reader["name"].ToString(),
                            reader["mobile"].ToString(),
                            reader["country"].ToString(),
                            reader["email"].ToString(),
                            reader["reference"].ToString(),
                            int.Parse(reader["payment_condition_customer"].ToString()),
                            Convert.ToUInt32(reader["payment_method_customer"]),
                            reader["rest"] is decimal ? Convert.ToDecimal(reader["rest"]) : (decimal?)null,
                            reader["due_date"] is DateTime ? Convert.ToDateTime(reader["due_date"].ToString()) : (DateTime?)null));

                    }
                    catch (Exception ex)
                    {
                        reader.Close();
                        return null;
                        //await App.Current.MainPage.DisplayAlert("Warning", "Connetion Time out", "Ok");
                        //await App.Current.MainPage.Navigation.PopAsync();
                    }

                }

                reader.Close();
                DbConnection.Deconnecter();
            }
            else
            {
                return null;
            }

            return list;
        }

        //--------------------------------------------------------------Get All Partner By Med Sahli --------------------------------------------------- 

        public async static Task<List<Partner>> GetPartnerList()
        {
            List<Partner> list = new List<Partner>();
            ImageSource img = ImageSource.FromResource("@drawable/userregular.png");
            MySqlDataReader reader = null;
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "select commercial_partner.Id,commercial_partner.name,category,commercial_partner.phone,commercial_partner.street,commercial_partner.city,commercial_partner.postal_code,commercial_partner.state,commercial_partner.email,commercial_partner_category.name as category_name from commercial_partner\r\nleft join commercial_partner_category on commercial_partner_category.Id =  commercial_partner.category where not(customer=0 and supplier=1);";

                try
                {

                    MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        try
                        {
                            list.Add(new Partner(
                  Convert.ToInt32(reader["Id"]),
                  reader["name"] is null ? "" : reader["name"].ToString(),
                  reader["category"] is null ? 0 : Convert.ToUInt32(reader["category"]),
                  reader["phone"] is null ? "" : reader["phone"].ToString(),
                  reader["street"] is null ? "" : reader["street"].ToString(),
                  reader["city"] is null ? "" : reader["city"].ToString(),
                  reader["postal_code"] is null ? "" : reader["postal_code"].ToString(),
                  reader["state"] is null ? "" : reader["state"].ToString(),
                  reader["email"] is null ? "" : reader["email"].ToString(),
                  img,
                   reader["category_name"] is null ? "" : reader["category_name"].ToString()));
                        }
                        catch (Exception ex)
                        {
                        }

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {

                    reader.Close();
                    return null;
                    //App.Current.MainPage.DisplayAlert("Warning", "Connection Failed", "Ok");
                    //App.Current.MainPage.Navigation.PopAsync();
                }




            }
            else
            {

                reader.Close();
                return null;
                //App.Current.MainPage.DisplayAlert("Warning", "Connection Failed", "Ok");
                //App.Current.MainPage.Navigation.PopAsync();





            }

            return list;

        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public async static Task<List<Partner>> GetPartnaireForFormByIdAgent(uint idagent)
        {

            string sqlCmd = "select commercial_partner.Id,commercial_partner.name,category,commercial_partner.phone,commercial_partner.street,commercial_partner.city,commercial_partner.postal_code,commercial_partner.state,commercial_partner.email,commercial_partner_category.name as category_name from commercial_partner\r\nleft join commercial_partner_category on commercial_partner_category.Id =  commercial_partner.category ;";
            //where not(customer=0 and supplier=1)
            List<Partner> list = new List<Partner>();

            ImageSource img = ImageSource.FromResource("@drawable/userregular.png");

            MySqlDataReader reader = null;
            if (await DbConnection.Connecter3())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        try
                        {

                            list.Add(new Partner(
                           Convert.ToInt32(reader["Id"]),
                           reader["name"] is null ? "" : reader["name"].ToString(),
                           reader["category"] is null ? 0 : Convert.ToUInt32(reader["category"]),
                           reader["phone"] is null ? "" : reader["phone"].ToString(),
                           reader["street"] is null ? "" : reader["street"].ToString(),
                           reader["city"] is null ? "" : reader["city"].ToString(),
                           reader["postal_code"] is null ? "" : reader["postal_code"].ToString(),
                           reader["state"] is null ? "" : reader["state"].ToString(),
                           reader["email"] is null ? "" : reader["email"].ToString(),
                           img,
                            reader["category_name"] is null ? "" : reader["category_name"].ToString()
                           ));
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    reader.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }

            return list;
        }
        public static async Task<BindingList<Partner>> GetPartnaireByIdAgent(uint idagent)
        {
            string sqlCmd = "SELECT cp.Id,cp.name,cp.mobile,cp.country,cp.email,cp.reference,cp.payment_condition_customer," +
                     "cp.payment_method_customer,yy.due_date,yy.rest " +
                     "FROM commercial_partner cp Left Join " +
                     "(SELECT partner, sum(restAmount) as rest, min(due_date) as due_date " +
                     "from(SELECT  sale_balance.restAmount, sale_balance.due_date, commercial_partner.Id as partner " +
                     "FROM     commercial_partner LEFT OUTER JOIN sale_balance " +
                     "ON sale_balance.IdPartner = commercial_partner.Id LEFT OUTER JOIN " +
                     "commercial_partner_category ON commercial_partner.category = commercial_partner_category.Id " +
                     "WHERE(FORMAT(sale_balance.restAmount, 3) <> 0)) xx " +
                     "group by partner " +
                     "order by due_date)  yy on cp.Id = yy.partner " +
                     "WHERE(cp.customer = 1) AND(cp.chec_socity = 1) AND(cp.actif = 1) AND (sale_agent = " + idagent + ")" +
                     " order by yy.due_date is null, yy.due_date asc;";
            BindingList<Partner> list = new BindingList<Partner>();

            DbConnection.Deconnecter();
            if (await DbConnection.Connecter3())
            {
                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        list.Add(new Partner(
                            Convert.ToUInt32(reader["Id"]),
                            reader["name"].ToString(),
                            reader["mobile"].ToString(),
                            reader["country"].ToString(),
                            reader["email"].ToString(),
                            reader["reference"].ToString(),
                            int.Parse(reader["payment_condition_customer"].ToString()),
                            Convert.ToUInt32(reader["payment_method_customer"]),
                            reader["rest"] is decimal ? Convert.ToDecimal(reader["rest"]) : (decimal?)null,
                            reader["due_date"] is DateTime ? Convert.ToDateTime(reader["due_date"].ToString()) : (DateTime?)null));
                    }
                    catch (Exception ex)
                    {
                        reader.Close();
                        return null;
                        //await App.Current.MainPage.DisplayAlert("Warning", "Connetion Time out", "Ok");
                        //await App.Current.MainPage.Navigation.PopAsync();
                    }

                }
                reader.Close();
                DbConnection.Deconnecter();
            }
            else
            {

                return null;
            }

            return list;
        }
        public async static Task<Partner> GetCommercialPartnerById(int idpartner)
        {
            string sqlCmd = "SELECT partner.Id,partner.vat_code,partner.fax ,partner.category, partner.country , partner.mobile,partner.name, partner.email, partner.number, partner.phone, partner.postal_code , partner.street , partner.state ,category.name category_name FROM commercial_partner partner left join commercial_partner_category category on category.Id = partner.category  WHERE(partner.Id = " + (uint)idpartner + ");";
            Partner partner = new Partner();

            if (await DbConnection.Connecter3())
            {
                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        var name = reader["name"].ToString();
                        var country = reader["country"];
                        partner = new Partner(Convert.ToUInt32(reader["Id"]),
                            reader["name"].ToString(),
                            reader["mobile"].ToString(),
                            reader["country"].ToString(),
                            reader["postal_code"].ToString(),
                            reader["street"].ToString(),
                            reader["state"].ToString(),
                            reader["category_name"].ToString(),
                            reader["vat_code"].ToString(),
                            reader["fax"].ToString(),
                            reader["number"].ToString()

                            );

                    }
                    catch (Exception ex)
                    {


                    }
                }
                reader.Close();


            }
            return partner;

        }
        public static async Task<BindingList<Partner>> GetWholesalerList()
        {
            BindingList<Partner> list = new BindingList<Partner>();

            string sqlCmd = "SELECT Id,chec_socity,city,country,create_date,credit_limit,currency,customer,"
                         + " customs_code,delivery_city,delivery_country,delivery_number,delivery_postal_code,delivery_state,"
                         + " delivery_street,email,fax,job_position,mobile,name,note,number,"
                         + " payment_condition_customer,payment_condition_supplier,payment_method_customer,payment_method_supplier,"
                         + " phone,picture,postal_code,reference,socity,state,street,supplier,"
                         + " trade_register,vat_code,website,rest_amount,due_date,customer_discount,vat_exemption,"
                         + " custumer_withholding_tax,category"
                         + " FROM commercial_partner "
                         + " WHERE(chec_socity = 1) AND (actif = 1) AND (category = 2) OR (category = 3) OR (category = 4);";



            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCmd, DbConnection.ConnectionString);
            adapter.SelectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();

            adapter.Fill(dt);


            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new Partner(
                        uint.Parse(dr["Id"].ToString()),
                        dr["name"].ToString(),
                        dr["mobile"].ToString(),
                        dr["country"].ToString(),
                        dr["email"].ToString(),
                        dr["reference"].ToString(),
                        Convert.ToInt32(dr["payment_condition_customer"]),
                        Convert.ToUInt32(dr["payment_method_customer"].ToString())));
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Warning", ex.ToString(), "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            return list;
        }
        public static Task<Partner> GetCommercialPartnerByIdForPayment(int idpartner)
        {
            string sqlCmd = "SELECT Id,country,mobile,name, email, number, payment_condition_customer, payment_condition_supplier, payment_method_customer, payment_method_supplier, " +
                "phone, reference FROM commercial_partner WHERE(Id = " + (uint)idpartner + ");";
            Partner partner = new Partner();
            DbConnection.Deconnecter();
            DbConnection.Connecter();

            MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    partner = new Partner(Convert.ToUInt32(reader["Id"]),
                        reader["name"].ToString(),
                        reader["mobile"].ToString(),
                        reader["country"].ToString(),
                        reader["email"].ToString(),
                        reader["reference"].ToString(),
                        int.Parse(reader["payment_condition_customer"].ToString()),
                        Convert.ToUInt32(reader["payment_method_customer"]));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            DbConnection.Deconnecter();

            return Task.FromResult(partner);

        }


        public async static Task<List<tempValueName>> GetCommercialPartnerTempName(int idpartner)
        {
            string sqlCmd = "SELECT *  FROM marketing_profile_attribut_value_temp WHERE partner = " + idpartner + " and attribut_name='name' and state=1;";
            List<tempValueName> partner = new List<tempValueName>();
            DbConnection.Deconnecter();
            DbConnection.Connecter();

            MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    partner.Add(new tempValueName(reader["string_value"].ToString(), Convert.ToDateTime(reader["create_date"])));


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            DbConnection.Deconnecter();

            return partner;

        }


        public async static Task<List<tempValueName>> GetCommercialPartnerTempState(int idpartner)
        {
            string sqlCmd = "SELECT *  FROM marketing_profile_attribut_value_temp WHERE partner = " + idpartner + " and attribut_name='state' and state=1;";
            List<tempValueName> partner = new List<tempValueName>();
            DbConnection.Deconnecter();
            DbConnection.Connecter();

            MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    partner.Add(new tempValueName(reader["string_value"].ToString(), Convert.ToDateTime(reader["create_date"])));


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            DbConnection.Deconnecter();

            return partner;

        }

        public async static Task<List<tempValueName>> GetCommercialPartnerTempStreet(int idpartner)
        {
            string sqlCmd = "SELECT *  FROM marketing_profile_attribut_value_temp WHERE partner = " + idpartner + " and attribut_name='street' and state=1;";
            List<tempValueName> partner = new List<tempValueName>();
            DbConnection.Deconnecter();
            DbConnection.Connecter();

            MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    partner.Add(new tempValueName(reader["string_value"].ToString(), Convert.ToDateTime(reader["create_date"])));


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            DbConnection.Deconnecter();

            return partner;

        }




        public async static Task<List<tempValueName>> GetCommercialPartnerTempCategory(int idpartner)
        {

            string sqlCmd = "SELECT  partner.create_date ,category.name  category_name FROM marketing_profile_attribut_value_temp  partner " +
                "left join commercial_partner_category category on category.Id = partner.int_value WHERE attribut_name='category' and partner=" + idpartner + " and state=1;";


            List<tempValueName> partner = new List<tempValueName>();
            DbConnection.Deconnecter();
            DbConnection.Connecter();

            MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    partner.Add(new tempValueName(reader["category_name"].ToString(), Convert.ToDateTime(reader["create_date"])));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            DbConnection.Deconnecter();

            return partner;

        }


        public static async Task updateNamePartner(int id, string name)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into marketing_profile_attribut_value_temp(partner,attribut_name,string_value,user,create_date,state,employe) values (" + id + ",'name','" + name + "'," + user_contrat.iduser + " , '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1 , " + user_contrat.id_employe + ") ;";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                    cmd.ExecuteScalar();
                    //UserDialogs.Instance.Alert("ADDED TO TEMP VALUES");


                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }

        public static async Task updateCategoryPartner(int id, int category)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into marketing_profile_attribut_value_temp(partner,attribut_name,int_value,user,create_date,state,employe) values (" + id + ",'category'," + (category) + "," + user_contrat.iduser + " ,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1 , " + user_contrat.id_employe + ") ;";
                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                try
                {
                    cmd.ExecuteScalar();
                    //UserDialogs.Instance.Alert("ADDED TO TEMP VALUES");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }


        public static async Task updateStreetPartner(int id, string street)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into marketing_profile_attribut_value_temp(partner,attribut_name,string_value,user,create_date,state,employe) values (" + id + ",'street','" + street + "'," + user_contrat.iduser + " , '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1 , " + user_contrat.id_employe + ") ;";
                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                try
                {
                    cmd.ExecuteScalar();
                    // UserDialogs.Instance.Alert("ADDED TO TEMP VALUES");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }


        public static async Task updateCodePostalePartner(int id, string code_postale)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into marketing_profile_attribut_value_temp(partner,attribut_name,string_value,user,create_date,state,employe) values (" + id + ",'postal_code','" + code_postale + "'," + user_contrat.iduser + " , '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1 , " + user_contrat.id_employe + ") ;";
                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                try
                {
                    cmd.ExecuteScalar();
                    //UserDialogs.Instance.Alert("ADDED TO TEMP VALUES");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }


        public static async Task updateStatePartner(int id, string state)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into marketing_profile_attribut_value_temp(partner,attribut_name,string_value,user,create_date,state,employe) values (" + id + ",'state','" + state + "'," + user_contrat.iduser + " , '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1 , " + user_contrat.id_employe + ") ;";
                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                try
                {
                    cmd.ExecuteScalar();
                    // UserDialogs.Instance.Alert("ADDED TO TEMP VALUES");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }


        public static async Task updateCountryPartner(int id, string country)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into marketing_profile_attribut_value_temp(partner,attribut_name,string_value,user,create_date,state,employe) values (" + id + ",'country','" + country + "'," + user_contrat.iduser + " , '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1 , " + user_contrat.id_employe + ") ;";
                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                try
                {
                    cmd.ExecuteScalar();
                    //UserDialogs.Instance.Alert("ADDED TO TEMP VALUES");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }


        public static async Task updateNumberePartner(int id, string number)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into marketing_profile_attribut_value_temp(partner,attribut_name,string_value,user,create_date,state,employe) values (" + id + ",'number','" + number + "'," + user_contrat.iduser + " , '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1 , " + user_contrat.id_employe + ") ;";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                    cmd.ExecuteScalar();
                    //                      UserDialogs.Instance.Alert("ADDED TO TEMP VALUES");


                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }
        public static async Task updateFaxPartner(int id, string fax)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into marketing_profile_attribut_value_temp(partner,attribut_name,string_value,user,create_date,state,employe) values (" + id + ",'fax','" + fax + "'," + user_contrat.iduser + " , '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1 , " + user_contrat.id_employe + ") ;";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                    cmd.ExecuteScalar();
                    //UserDialogs.Instance.Alert("ADDED TO TEMP VALUES");


                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }

        public static async Task updateMobilePartner(int id, string mobile)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into marketing_profile_attribut_value_temp(partner,attribut_name,string_value,user,create_date,state,employe) values (" + id + ",'mobile','" + mobile + "'," + user_contrat.iduser + " , '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1 , " + user_contrat.id_employe + ") ;";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                    cmd.ExecuteScalar();
                    //UserDialogs.Instance.Alert("ADDED TO TEMP VALUES");


                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }


        public static async Task updateVatCodePartner(int id, string vat_code)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into marketing_profile_attribut_value_temp(partner,attribut_name,string_value,user,create_date,state,employe) values (" + id + ",'vat_code','" + vat_code + "'," + user_contrat.iduser + " , '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1 , " + user_contrat.id_employe + ") ;";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                    cmd.ExecuteScalar();
                    // UserDialogs.Instance.Alert("ADDED TO TEMP VALUES");


                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }


        public static async Task<int?> InsertNewPartner(string name, string street, string city, string state, string postal_code, string country, string email, string fax, bool? customer, bool? supplier, int? category, string vat_code)
        {
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "insert into commercial_partner(create_date,name,street,city,state,postal_code,country,email,fax,customer,supplier,category,vat_code) values ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + name + "','" + street + "','" + city + "','" + state + "','" + postal_code + "','" + country + "','" + email + "','" + fax + "'," + customer + "," + supplier + "," + category + ",'" + vat_code + "' );select max(id) from commercial_partner ; ";
                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                try
                {
                    return int.Parse(cmd.ExecuteScalar().ToString());

                }
                catch (Exception ex)
                {
                    return null;

                }
            }
            else
            {
                return null;

            }
        }

    }
    public class tempValueName
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime create_date { get; set; }
        public tempValueName(string name, DateTime create_date)
        {

            this.name = name;
            this.create_date = create_date;
        }
    }

}
