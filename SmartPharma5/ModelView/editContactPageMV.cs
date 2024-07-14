using MvvmHelpers;
using MvvmHelpers.Commands;
using SmartPharma5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPharma5.ModelView
{
    class editContactPageMV : BaseViewModel
    {
        private Contact_Partner contact;
        public Contact_Partner Contact { get => contact; set => SetProperty(ref contact, value); }

        public Contact_Partner CurrentContact { get; set; }= new Contact_Partner();

        private List<Item> martinal;
        public List<Item> Martinal { get => martinal; set => SetProperty(ref martinal, value); }


        public AsyncCommand updateFirstNameCommand { get; set; }
        public AsyncCommand updateLastNameCommand { get; set; }
        public AsyncCommand updateSexCommand { get; set; }
        public AsyncCommand updateMaritalStatusCommand { get; set; }
        public AsyncCommand updateBirthDateCommand { get; set; }
        public AsyncCommand updateBirthPlaceCommand { get; set; }
        public AsyncCommand updateNationalityCommand { get; set; }
        public AsyncCommand updateAdressCommand { get; set; }
        public AsyncCommand updateIdentityCommand { get; set; }



        public editContactPageMV(int id)
        {
            this.Contact = Contact_Partner.GetContact_PartnerById(id).Result;
            this.CurrentContact = this.Contact;
            this.Martinal = Contact_Partner.GetMaritalStatus().Result;
            updateFirstNameCommand = new AsyncCommand(UpdateFirstName);
            updateLastNameCommand = new AsyncCommand(UpdateLastName);
            updateSexCommand = new AsyncCommand(UpdateSEX);
            updateMaritalStatusCommand = new AsyncCommand(UpdateMaritalStatus);
            updateBirthDateCommand = new AsyncCommand(UpdateBirthDate);
            updateBirthPlaceCommand = new AsyncCommand(UpdateBirthPlace);
            updateNationalityCommand = new AsyncCommand(UpdateNationality);
            updateAdressCommand = new AsyncCommand(UpdateAdress);
            updateIdentityCommand = new AsyncCommand(UpdateIdentity);

        }


        public async Task UpdateFirstName()
        {
            if (this.CurrentContact.FirstName == this.Contact.FirstName)
            {

            }
            else
            {

            }
            this.Contact = this.CurrentContact;

        }
        public async Task UpdateLastName()
        {
            if (this.CurrentContact.LastName == this.Contact.LastName)
            {

            }
            else
            {

            }


        }
        public async Task UpdateSEX()
        {
            if (this.CurrentContact.Sex == this.Contact.Sex)
            {

            }
            else
            {


            }

        }
        public async Task UpdateMaritalStatus()
        {
            if (this.CurrentContact.Martal_status == this.Contact.Martal_status)
            {

            }
            else
            {


            }

        }
        public async Task UpdateBirthDate()
        {
            if (this.CurrentContact.Birth_date == this.Contact.Birth_date)
            {

            }
            else
            {


            }

        }
        public async Task UpdateBirthPlace()
        {
            if (this.CurrentContact.Birth_place == this.Contact.Birth_place)
            {

            }
            else
            {


            }

        }
        public async Task UpdateNationality()
        {
            if (this.CurrentContact.Nationality == this.Contact.Nationality)
            {

            }
            else
            {


            }

        }
        public async Task UpdateAdress()
        {
            if (this.CurrentContact.Adress == this.Contact.Adress)
            {

            }
            else
            {


            }

        }
        public async Task UpdateIdentity()
        {
            if (this.CurrentContact.Identity == this.Contact.Identity)
            {

            }
            else
            {


            }

        }
        public async Task UpdateHandicap()
        {
            if (this.CurrentContact.Handicap == this.Contact.Handicap)
            {

            }
            else
            {


            }

        }
        public async Task UpdateHandicapDescription()
        {
            if (this.CurrentContact.Handicap_description == this.Contact.Handicap_description)
            {

            }
            else
            {


            }

        }

    }
}
