namespace School.Views;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using School.Models;

public partial class EtudiantsPage : ContentPage
{
	public ObservableCollection<Etudiants> etudiantsList {get; set;} = new ObservableCollection<Etudiants>();
	public EtudiantsPage()
	{
		InitializeComponent();

		BindingContext = this;

		LoadEtudiantsList();
	}

	public void LoadEtudiantsList()
	{
		etudiantsList.Clear();

		foreach (var ens in Etudiants.LoadAll())
		{
			etudiantsList.Add(ens);
		}
	}

	private void OnAddStudentClicked(object sender, EventArgs e){
		string firstname = FirstnameEntry.Text;
		string lastname = LastnameEntry.Text; 

		Etudiants newetudiant = new Etudiants(firstname,lastname);
		newetudiant.Save();
		etudiantsList.Add(newetudiant);

		DisplayAlert("Succes","Student was successfully added !","ok");

		FirstnameEntry.Text = string.Empty;
		LastnameEntry.Text = string.Empty;
	}
}