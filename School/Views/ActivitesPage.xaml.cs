namespace School.Views;

using System.ComponentModel.DataAnnotations;
using System.Linq;
using School.Models;

public partial class ActivitesPage : ContentPage
{
	private EnseignantsPage enseignantsPage;

	public ActivitesPage(){
		InitializeComponent();
	}
	public ActivitesPage(EnseignantsPage enseignantsPage)
	{
		InitializeComponent();
		this.enseignantsPage = enseignantsPage;

	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		if( enseignantsPage != null){
			this.BindingContext = this;
			Console.WriteLine("Number of teachers in the list: " + enseignantsPage.enseignantsList.Count);
			if( TeacherPicker != null){
						TeacherPicker.ItemsSource = enseignantsPage.enseignantsList.Select(ens => ens.DisplayName).ToList();
			}}
		else{Console.WriteLine("EnseignantsPage.enseignantsList is null");}
    }

    private void OnAddActivityClicked(object sender, EventArgs e){
		string activityName = ActivityNameEntry.Text;
		string CodeName = ActivityCodeEntry.Text;
		int ects;

		if(int.TryParse(ActivityEctsEntry.Text, out ects)){
			Enseignants selectedTeacher = TeacherPicker.SelectedItem as Enseignants;
			if(selectedTeacher != null){
				Activity newactivity = new Activity(activityName,CodeName,ects, selectedTeacher);

				DisplayAlert("Success","Activity added succesfully","ok");
				ActivityNameEntry.Text = string.Empty;
				ActivityCodeEntry.Text = string.Empty;
				ActivityEctsEntry.Text = string.Empty;
			}
			else{
				DisplayAlert("Error","Teacher not selected","ok");
			}
		}
		else{
			DisplayAlert("Error","Invalid ECTS input","ok");
		}
	}
}