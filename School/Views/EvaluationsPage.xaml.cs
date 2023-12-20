using System.Diagnostics;
using System.Collections.ObjectModel;
using School.Models;
namespace School.Views;

public partial class EvaluationsPage : ContentPage
{
	public ObservableCollection<Etudiants> etudiantsList {get; set;} = new ObservableCollection<Etudiants>();
	public ObservableCollection<Models.Activity> activityList {get; set;} = new ObservableCollection<Models.Activity>();

	private EtudiantsPage etudiantsPage = new EtudiantsPage();
	private ActivitesPage activityPage = new ActivitesPage();
	private EnseignantsPage enseignantsPage = new EnseignantsPage();
	public EvaluationsPage()
	{
		InitializeComponent();
		BindingContext = this;
		this.enseignantsPage = enseignantsPage;

		foreach(var elem in Etudiants.LoadAll()){
			etudiantsList.Add(elem);
		}

		foreach(var ens in Models.Activity.LoadAll(enseignantsPage.enseignantsList)){
		activityList.Add(ens);
		}
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		if( etudiantsPage != null){
			this.BindingContext = this;
			if( StudentPicker != null){
						StudentPicker.ItemsSource = etudiantsPage.etudiantsList.Select(ens => ens.DisplayName).ToList();
						StudentPicker.IsVisible = true;
			}
			if( Student2Picker != null){
						Student2Picker.ItemsSource = etudiantsPage.etudiantsList.Select(ens => ens.DisplayName).ToList();
						Student2Picker.IsVisible = true;
			}}
		if( activityPage != null){
			this.BindingContext = this;
			if( ActivityPicker != null){
						ActivityPicker.ItemsSource = activityPage.activityList.Select(elem => elem.Code).ToList();
						ActivityPicker.IsVisible = true;
			}
			if( Activity2Picker != null){
						Activity2Picker.ItemsSource = activityPage.activityList.Select(elem => elem.Code).ToList();
						Activity2Picker.IsVisible = true;
			}}
    }
}