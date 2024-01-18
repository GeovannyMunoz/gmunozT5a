using gmunozT5a.Models;

namespace gmunozT5a.Views;

public partial class vPrincipal : ContentPage
{
    private int _id = 0;
	public vPrincipal()
	{
		InitializeComponent();
	}

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        statusMessaje.Text = "";
        if (_id == 0)
        {
            
            App.personRepo.AddNewPerson(txtNombre.Text);
            statusMessaje.Text = App.personRepo.StatusMessage;

        }
        else
        {
            App.personRepo.UpdatePerson(new Persona
            {
                Id= _id,
                Name = txtNombre.Text 
            });
            statusMessaje.Text = App.personRepo.StatusMessage;
        }

        txtNombre.Text = string.Empty;
        List<Persona> personas = App.personRepo.GetAllPepople();
        listViw.ItemsSource = personas;
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        statusMessaje.Text = "";
        List<Persona> personas = App.personRepo.GetAllPepople();
        listViw.ItemsSource = personas;
    }

    private async void listViw_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var persona = (Persona)e.Item;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Editar", "Eliminar");
   

        switch (action) 
        {
            case "Editar":
                _id = persona.Id;
                txtNombre.Text = persona.Name;
                break;
            case "Eliminar":
                App.personRepo.DeletePerson(persona);
                statusMessaje.Text = App.personRepo.StatusMessage;
                List<Persona> personas = App.personRepo.GetAllPepople();
                listViw.ItemsSource = personas;
                break;
        }
    }
}