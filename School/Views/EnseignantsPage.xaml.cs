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
	}
	public void LoadEnseignantsList()
	{
		enseignantsList.Clear();

		foreach (var ens in Enseignants.LoadAll())
		{
			enseignantsList.Add(ens);
		}
	}
	private void OnAddTeacherClicked(object sender, EventArgs e)
        {
            string firstname = FirstnameEntry.Text;
            string lastname = LastnameEntry.Text;
			Console.WriteLine("Hi");

            if (int.TryParse(SalaryEntry.Text, out int salary))
            {
                Enseignants enseignant = new Enseignants(firstname, lastname, salary);
				enseignantsList.Add(enseignant);
				enseignant.Save();
				Console.WriteLine("Teacher added: " + enseignant.DisplayName);
        		Console.WriteLine("Total teachers in list: " + enseignantsList.Count);

				DisplayAlert("Success", "Teacher added successfully. It worked!", "OK");

				FirstnameEntry.Text = string.Empty;
				LastnameEntry.Text = string.Empty;
        		SalaryEntry.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Error", "Invalid salary input", "OK");
            }
        }
}