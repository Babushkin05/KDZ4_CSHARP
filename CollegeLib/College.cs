using System;

namespace CollegeLib;
/// <summary>
/// College class.
/// </summary>
public class College
{
    // Field from csv table.
    public string Name { get; set; }

    public string Okrug { get; set; }

    public string Rayon { get; set; }

    public string FormOfIncorporation { get; set; }

    public string Submission { get; set; }

    public string TipUchrezhdenya { get; set; }

    public string VidUchrezhdeniya { get; set; }

    public string XCoordinate { get; set; }

    public string YCoordinate { get; set; }

    public string GlobalId { get; set; }

    public Contacts Contacts;

    // Contact field is required.
    public College(Contacts contact)
    {
        Contacts = contact;
    }

    // Indexator for fields.
    public string this[int index]
    {
        get
        {
            return index switch
            {
                1 => Name,
                2 => Contacts.Adress,
                3 => Okrug,
                4 => Rayon,
                5 => FormOfIncorporation,
                6 => Submission,
                7 => TipUchrezhdenya,
                8 => VidUchrezhdeniya,
                9 => Contacts.PhoneNumbers,
                10 => Contacts.WebSite,
                11 => Contacts.Email,
                12 => XCoordinate,
                13 => YCoordinate,
                14 => GlobalId,
            };
        }

        set
        {
            switch (index)
            {
                case 1:
                    Name = value;
                    break;
                case 2:
                    Contacts.Adress = value;
                    break;
                case 3:
                    Okrug = value;
                    break;
                case 4:
                    Rayon = value;
                    break;
                case 5:
                    FormOfIncorporation = value;
                    break;
                case 6:
                    Submission = value;
                    break;
                case 7:
                    TipUchrezhdenya = value;
                    break;
                case 8:
                    VidUchrezhdeniya = value;
                    break;
                case 9:
                    Contacts.PhoneNumbers = value;
                    break;
                case 10:
                    Contacts.WebSite = value;
                    break;
                case 11:
                    Contacts.Email = value;
                    break;
                case 12:
                    XCoordinate = value;
                    break;
                case 13:
                    YCoordinate = value;
                    break;
                case 14:
                    GlobalId = value;
                    break;
            }
        }
    }

    /// <summary>
    /// Override ToString().
    /// </summary>
    /// <returns>String for saving in file.</returns>
    public override string ToString()
    {
        string toString = "";
        for(int i = 1; i < 15; i++)
        {
            toString += '"'+this[i]+"\";";
        }
        return toString;
    }
}

