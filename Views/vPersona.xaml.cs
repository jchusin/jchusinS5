using jchusinS5.Models;
namespace jchusinS5.Views;


public partial class vPersona : ContentPage
{
    public vPersona()
    {
        InitializeComponent();
    }

    private void btnInserta_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        App.personRepo.AddNewPerson(txtName.Text);
        lblStatus.Text = App.personRepo.StatusMessage;
    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        List<Persona> people = App.personRepo.GetAllPeople();
        listaPersona.ItemsSource = people;
        lblStatus.Text = App.personRepo.StatusMessage;
    }

    private async void btnEditar_Clicked(object sender, EventArgs e)
    {

        // Obtener la persona seleccionada
        Persona selectedPerson = (sender as Button).CommandParameter as Persona;

        // Mostrar ventana emergente para editar el nombre
        string newName = await DisplayPromptAsync("Editar Nombre", "Ingrese el nuevo nombre", "Guardar", "Cancelar", null, -1, Keyboard.Text, selectedPerson.Name);

        if (!string.IsNullOrWhiteSpace(newName))
        {
            selectedPerson.Name = newName;
            App.personRepo.UpdatePerson(selectedPerson);
            List<Persona> people = App.personRepo.GetAllPeople();
            listaPersona.ItemsSource = people;
            lblStatus.Text = App.personRepo.StatusMessage;
            
        }

    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {

        // Obtener la persona seleccionada
        Persona selectedPerson = (sender as Button).CommandParameter as Persona;

        // Mostrar ventana de confirmación
        bool answer = await DisplayAlert("Eliminar Persona", "¿Está seguro de que desea eliminar a " + selectedPerson.Name + "?", "Sí", "Cancelar");

        if (answer)
        {
            // Eliminar la persona si el usuario confirma
            App.personRepo.DeletePerson(selectedPerson.Id);

            // Actualizar la lista de personas mostrada
            List<Persona> people = App.personRepo.GetAllPeople();
            listaPersona.ItemsSource = people;

            lblStatus.Text = App.personRepo.StatusMessage;
        }

    }
}