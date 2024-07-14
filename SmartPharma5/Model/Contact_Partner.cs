using MvvmHelpers;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPharma5.Model
{
    public class Contact_Partner :BaseViewModel
    {
        public int id {  get; set; }
        public int id_partner { get; set; }
        public string name_partner { get; set; }
        public string job_position { get; set; }



        private string firstName;
        public string FirstName { get => firstName; set => SetProperty(ref firstName, value); }

        private string lastName;
        public string LastName { get => lastName; set => SetProperty(ref lastName, value); }

        private int sex;
        public int Sex { get => sex; set => SetProperty(ref sex, value); }

        private int martal_status;
        public int Martal_status { get => martal_status; set => SetProperty(ref martal_status, value); }

        private DateTime birth_date;
        public DateTime Birth_date { get => birth_date; set => SetProperty(ref birth_date, value); }

        private string birth_place;
        public string Birth_place { get => birth_place; set => SetProperty(ref birth_place, value); }

        private int nationality;
        public int Nationality { get => nationality; set => SetProperty(ref nationality, value); }

        private int adress;
        public int Adress { get => adress; set => SetProperty(ref adress, value); }

        private int identity;
        public int Identity { get => identity; set => SetProperty(ref identity, value); }

        private int primary_occupation;
        public int Primary_occupation { get => primary_occupation; set => SetProperty(ref primary_occupation, value); }

        private bool handicap;
        public bool Handicap { get => handicap; set => SetProperty(ref handicap, value); }

        private string handicap_description;
        public string Handicap_description { get => handicap_description; set => SetProperty(ref handicap_description, value); }


        public Contact_Partner()
        {
            
        }
        public Contact_Partner(int id,int id_partner,string name_partner,string job_position,string firstName,string lastName)
        {
            this.id = id;
            this.id_partner = id_partner;
            this.name_partner = name_partner;
            this.job_position = job_position;
            this.firstName = firstName;
            this.lastName = lastName;

            
        }


        public async static Task<Contact_Partner> GetContact_PartnerById(int idContact)
        {
            string sqlCmd = "select * from commercial_partner_contact c\r\nleft join atooerp_person on atooerp_person.id = c.person\r\nwhere c.id = "+idContact+" ;";
            Contact_Partner partner = new Contact_Partner();

            if (await DbConnection.Connecter3())
            {
                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        partner.FirstName = Convert.ToString(reader["first_name"]);
                        partner.LastName = Convert.ToString(reader["last_name"]);
                        partner.Birth_date = Convert.ToDateTime(reader["birth_date"]);
                        partner.Birth_place = Convert.ToString(reader["birth_place"]);
                        partner.Sex = Convert.ToInt32(reader["sex"])  ;
                        partner.Martal_status = reader["marital_status"] != DBNull.Value ? Convert.ToInt32(reader["marital_status"]) : 0;
                        partner.Nationality =  reader["nationality"] != DBNull.Value ? Convert.ToInt32(reader["nationality"]) : 0;
                        partner.Identity =  reader["identity"] != DBNull.Value ? Convert.ToInt32(reader["identity"]) : 0;
                        partner.Primary_occupation =  reader["primary_occupation"] != DBNull.Value ? Convert.ToInt32(reader["primary_occupation"]) : 0;
                        partner.Handicap = Convert.ToBoolean(reader["handicap"]);
                        partner.Handicap_description = Convert.ToString(reader["handicap_description"]);
                    }
                    catch (Exception ex)
                    {


                    }
                }
                reader.Close();


            }
            return partner;

        }

        public async static Task<List<Item>> GetMaritalStatus()
        {
            string sqlCmd = "select * from atooerp_person_marital_status;";
            List<Item> Items = new List<Item>();

            if (await DbConnection.Connecter3())
            {
                MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        Item item = new Item();
                        item.Id = Convert.ToInt32(reader["Id"]);
                        item.Name = Convert.ToString(reader["name"]);

                        Items.Add(item);

 

                    }
                    catch (Exception ex)
                    {


                    }
                }
                reader.Close();


            }
            return Items;

        }
    }
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
