public class Client
{
    private School _school;

    public School school { get => _school; }

    public Client(School school)
    {
        _school = school;
        TextMenu();
    }

    public void TextMenu()
    {
        Console.WriteLine("Are you a Student? (y/n)");
        string? response;
        Student? student = null;
        Labs? lab = null;
        if ((response = Console.ReadLine()) != null && response.ToLower() == "y")
        {
            bool keep = true;
            //verify Student
            while (keep)
            {
                Console.WriteLine("Insert Matricola");
                if ((response = Console.ReadLine()) != null && response != "" && (student = school.FindStudent(response)) != null)
                {
                    keep = false;
                }
            }

            Console.WriteLine("\t\tSelect a day\n\t\t1-Mon\n\t\t2-Tue\n\t\t3-Wed\n\t\t3-Thu\n\t\t4-Fri");
            int? day;
            if ((day = int.Parse(Console.ReadLine())) != null && (day < 5 && day > 0))
            {
                Console.WriteLine("Select a Lab (insert the laboratory Name)");
                school.ShowLabs();
                while ((response = Console.ReadLine()) != null && response != "")
                {
                    if ((lab = school.FindLab(response)) != null)
                    {
                        Console.WriteLine("Insert ID and Start hour of prenotation (ID/hour)");
                        school.showWorkingStationAvaibility(lab, (int)day);
                        break;
                    }
                    else { Console.WriteLine("ERROR: Invalid Laboratory... Insert a valid Name"); }
                }
                if ((response = Console.ReadLine()) != null)
                {
                    string[] id_hour = response.Split("/");
                    Console.WriteLine("need a particula program? ");
                    if ((response = Console.ReadLine()) != null && response != "")
                    {
                        school.Book(student, (int)day, int.Parse(id_hour[1]), id_hour[0], response, (Labs)lab);
                    }
                    school.Book(student, (int)day, int.Parse(id_hour[1]), id_hour[0], null, (Labs)lab);
                    school.showWorkingStationAvaibility(lab, (int)day);
                }
            }
        }
    }
}