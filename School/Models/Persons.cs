﻿namespace School.Models;

public class Persons
{
    private string firstname;
    private string lastname;
    public Persons(string firstname, string lastname) {
        this.firstname = firstname;
        this.lastname = lastname;
    }

    public string DisplayName() {
        return String.Format("{0} {1}", lastname, firstname);
    }

    public string Firstname {
        get {
            return firstname;
        }
    }

    public string Lastname {
        get {
            return lastname;
        }
    }
}