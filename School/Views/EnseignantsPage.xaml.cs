namespace School.Views;
using School.Models;
using System.Collections.ObjectModel;
using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

public partial class EnseignantsPage : ContentPage
{
	private ObservableCollection<Enseignants> enseignantsList;
	private StackLayout stackLayout;
	public EnseignantsPage()
	{
		InitializeComponent();
		enseignantsList = new ObservableCollection<Enseignants>();

		BindingContext = this;

		stackLayout = new StackLayout();

		var listView = new ListView();
		listView.ItemsSource = enseignantsList;

		stackLayout.Children.Add(listView);
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