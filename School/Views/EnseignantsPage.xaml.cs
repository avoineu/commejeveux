namespace School.Views;
using School.Models;
using System.Collections.ObjectModel;
using System;
using Microsoft.Maui.Controls;

public partial class EnseignantsPage : ContentPage
{
	public ObservableCollection<Enseignants> enseignantsList {get; set;} = new ObservableCollection<Enseignants>();

	public EnseignantsPage()
	{
		InitializeComponent();

		BindingContext = this;

		LoadEnseignantsList();
		DisplayTotalSalary();
	}
	public void LoadEnseignantsList()
	{
		enseignantsList.Clear();

		foreach (var ens in Enseignants.LoadAll())
		{
			enseignantsList.Add(ens);
		}
	}

	public string TotalSalary(){
		int totalsalary = 0;
		if(enseignantsList == null){
			return "0 de cout total";
		}
		foreach(var enseignant in enseignantsList){
			totalsalary += enseignant.Salary;
		}
		return String.Format("Le cout mensuelle total des salaires est de {0}€ \nLe cout annuel total des salaires est de {1}€",totalsalary,totalsalary*12);
	}

	private void DisplayTotalSalary(){
		SalaryTotalLabel.Text = TotalSalary();
	}
	private void OnAddTeacherClicked(object sender, EventArgs e)
        {
            string firstname = FirstnameEntry.Text;
            string lastname = LastnameEntry.Text;

            if (int.TryParse(SalaryEntry.Text, out int salary))
            {
                Enseignants enseignant = new Enseignants(firstname, lastname, salary);
				enseignantsList.Add(enseignant);
				enseignant.Save();

				DisplayAlert("Success", "Teacher added successfully. It worked!", "OK");

				FirstnameEntry.Text = string.Empty;
				LastnameEntry.Text = string.Empty;
        		SalaryEntry.Text = string.Empty;
				DisplayTotalSalary();
            }
            else
            {
                DisplayAlert("Error", "Invalid salary input", "OK");
            }
        }
}