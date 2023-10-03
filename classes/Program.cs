
using System.Net.WebSockets;
using System.Reflection.Metadata;

class Program
{

    static void Main()
    {
        string[] programs = "VScode Photoshop VisualStudio CodeBlock".Split(" ");
        WorkingStation[] ws = { new WorkingStation("XF18CC", programs), new WorkingStation("XF19C2C", programs), new WorkingStation("JK11GC", programs), new WorkingStation("QM91CC", programs) };
        WorkingStation[] ws1 = { new WorkingStation("P12FCG", programs), new WorkingStation("P12412", programs), new WorkingStation("KLS221", programs) };
        WorkingStation[] ws2 = { new WorkingStation("2UIVBS", programs), new WorkingStation("LKJ178", programs), new WorkingStation("KKJL102", programs), new WorkingStation("PPOKHJ11", programs), new WorkingStation("IL1109", programs) };

        User[] users = { new Teacher("Albus", "Silente", "VOLDEMORT"), new Teacher("Marco", "Rossi", "MMRRssi"), new Teacher("Pinuccia", "Nicosia", "PININICO123"), new Student("Davide", "Brancato", "1000029849"), new Student("Marcello", "Italiano", "X81000132") };
        Labs[] labs = { new(1, ws, "Laboratorio Andromeda"), new(2, ws1, "Laboratorio Oppenheimer"), new(3, ws2, "Laboratorio Einstein") };

        School school = new("Pirandello", labs, users);

        Console.WriteLine(school);

        Console.WriteLine("Are you a Student? (y/n)");
        string? response;
        bool KeepGoing = true;
        Student? student = null;
        if ((response = Console.ReadLine()) != null && response.ToLower() == "y")
        {
            Console.Write("insert your matricola: ");
            while (KeepGoing)
            {
                if ((student = school.FindStudent(Console.ReadLine())) == null)
                {
                    Console.WriteLine("Matricola not found! Are you a Student? (y/n)");
                    if ((response = Console.ReadLine()) != null && response.ToLower() == "n")
                    {
                        KeepGoing = false;
                    }
                }
                else { KeepGoing = false; }
            }
            Console.WriteLine("You want select a particular Lab? ");
            school.ShowLabs();
            Console.WriteLine("\n\n");
            if ((response = Console.ReadLine()) != null && student != null)
            {
                Labs? lab = school.FindLab(response);
                if (lab != null && response != "")
                {
                    Console.WriteLine("You need a Particular Program?");
                    string? program = Console.ReadLine();
                    //show all the computer in the lab and the day (or all computer with the program)
                    if (program != null && program != "") { school.showAvaibility(lab, program); }
                    else { school.showAvaibility(lab); }
                    Console.WriteLine("Select a day");
                    if ((response = Console.ReadLine()) != null && response != "")
                    {
                        int day = int.Parse(response);
                        Console.WriteLine("Want a particular hour? (from 9 to 18)");
                        //make the book for a desired hour
                        if ((response = Console.ReadLine()) != null && response != "")
                        {
                            int hour = int.Parse(response);
                            if (hour >= 9 && hour <= 18)
                            {
                                //needed a particular program
                                if (program != null && program != "") { school.Book(student, day, hour, program, lab); }
                                else { school.Book(student, day, hour, lab); }
                            }
                        }
                        //the user want the first hour avaible
                        else { school.Book(student, day); }
                    }
                    //Prenotation without Day, but with a lab
                    else { school.Book(student, lab); }
                }
                //Prenotation without a specific lab
                else
                {
                    Console.WriteLine("You need a Particular Program?");
                    string? program = Console.ReadLine();
                    Console.WriteLine("Select a day");
                    if ((response = Console.ReadLine()) != null && response != "")
                    {
                        int day = int.Parse(response);
                        Console.WriteLine("Want a particular hour? (from 9 to 18)");
                        //make the book for a desired hour
                        if ((response = Console.ReadLine()) != null && response != "")
                        {
                            int hour = int.Parse(response);
                            if (hour >= 9 && hour <= 18)
                            {
                                //needed a particular program
                                if (program != null && program != "") { school.Book(student, day, hour, program); }
                                else { school.Book(student, day, hour); }
                            }
                        }
                        //the user want the first hour avaible
                        else { school.Book(student, day); }
                    }
                    //Prenotation without Day lab and hour
                    else { school.Book(student); }
                }

            }
        }
        else
        {
            KeepGoing = false;
            Teacher? teacher = null;
            Console.WriteLine("Insert Teacher Pass");
            while (!KeepGoing)
            {
                if ((response = Console.ReadLine()) != null && response != "")
                {
                    teacher = school.FindTeacher(response);
                    if (teacher != null)
                    {
                        KeepGoing = true;
                        break;
                    }
                    Console.WriteLine("Insert Teacher Pass");
                }
            }
            int? hour = null;
            Console.WriteLine("Select an hour to prenote (9 - 18)");
            while (KeepGoing)
            {
                if ((response = Console.ReadLine()) != null && response != "")
                {
                    hour = int.Parse(response);
                    if (hour != null)
                    {
                        KeepGoing = false;
                    }
                }
                else
                {
                    Console.WriteLine("You have to select an hour! ");
                    Console.WriteLine("Select an hour to prenote (9 - 18)");
                }
            }
            Console.WriteLine("Select a Day for Book the lab");
            if ((response = Console.ReadLine()) != null && response != "")
            {
                int? day;
                Labs? lab;
                if ((day = int.Parse(response)) != null)
                {
                    Console.WriteLine("Select a Lab to prenote");
                    //show only the lab that can be fully prenotated on that day
                    school.ShowLabsAllAvaible((int)day, (int)hour);
                    if ((response = Console.ReadLine()) != null && (lab = school.FindLab(response)) != null)
                    {
                        //Book the lab for a day at an hour
                        school.Book(teacher, (int)day, (int)hour, lab);
                    }
                    //Book for a day without a lab
                    else
                    {
                        school.Book(teacher, (int)day, (int)hour);
                    }
                }
                else
                {
                    //Book without a day
                    school.Book(teacher, (int)hour);
                }
            }
        }
    }

}
