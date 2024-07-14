using SmartPharma5.Model;
using SmartPharma5.ModelView;

namespace SmartPharma5.View;

public partial class EditContactPage : ContentPage
{
	public int idContact;


    public EditContactPage(int idContact)
	{
		this.idContact = idContact;
		InitializeComponent();
		BindingContext = new editContactPageMV(idContact);
    }
}