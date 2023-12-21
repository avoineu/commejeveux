using System.Collections.ObjectModel;
using School.Models;

namespace School.Views;

public partial class BulletinPage : ContentPage
{
	public ObservableCollection<Etudiants> etudiantsList {get;set;} = new ObservableCollection<Etudiants>();
	private EtudiantsPage etudiantsPage = new EtudiantsPage();
	public BulletinPage()
	{
		InitializeComponent();

		BindingContext = this;
		
		foreach(var elem in Etudiants.LoadAll()){
			etudiantsList.Add(elem);
		}
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		etudiantsPage.LoadEtudiantsList();
		if(etudiantsPage != null){
			this.BindingContext = this ;
			if(StudentPicker != null){
				StudentPicker.ItemsSource = etudiantsPage.etudiantsList.Select(ens => ens.DisplayName).ToList();
				StudentPicker.IsVisible = true;
			}
		}
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
		etudiantsPage.LoadEtudiantsList();
    }

	private void OnAddBulletinClicked(object sender, EventArgs e){
		string selectedstudent = StudentPicker.SelectedItem.ToString();
		Etudiants etudiant2link = null;

		foreach(Etudiants elem in etudiantsList){
			if(Equals(selectedstudent,elem.DisplayName)){
				etudiant2link = elem;
			}
		}
		Console.WriteLine("ETUDIANT TO LINK "+etudiant2link.DisplayName);
		Console.WriteLine("PAS MARCHER : "+etudiant2link.Bulletin());
		string selectedbulletin = etudiant2link.Bulletin();
		Console.WriteLine("BULLETIN : "+selectedbulletin);

	}
}