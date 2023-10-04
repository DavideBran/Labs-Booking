public class Client
{
    private School _school;

    private Labs SelectDay(ref int day, Labs? lab)
    {
        string? response;
        Labs retLab;
        Console.WriteLine("\t\tSelect a day\n\t\t1-Mon\n\t\t2-Tue\n\t\t3-Wed\n\t\t3-Thu\n\t\t4-Fri");
        while (true)
        {
            if ((int.TryParse(Console.ReadLine(), out day)) && (day < 5 && day > 0))
            {
                Console.WriteLine("Select a Lab (insert the laboratory Name)");
                school.ShowLabs();
                while ((response = Console.ReadLine()) != null && response != "")
                {
                    if ((retLab = school.FindLab(response)) != null)
                    {
                        return retLab;
                    }
                    else { Console.WriteLine("ERROR: Invalid Laboratory... Insert a valid Name"); }
                }
            }
            else { Console.WriteLine("Insert a valid Day"); }
        }
    }

    private void SelectDay(ref int day)
    {
        Console.WriteLine("\t\tSelect a day\n\t\t1-Mon\n\t\t2-Tue\n\t\t3-Wed\n\t\t3-Thu\n\t\t4-Fri");
        while (true)
        {
            if ((int.TryParse(Console.ReadLine(), out day)) && (day < 5 && day > 0))
            {
                break;
            }
            Console.WriteLine("Insert a valid Day");
        }
    }

    private void SelectWorkingStation(Student student, int day, string? program, Labs lab)
    {
        while (true)
        {
            Console.WriteLine("Insert ID and Start hour of prenotation (ID/hour)");
            school.showWorkingStationAvaibility(lab, day);
            string? response;
            if ((response = Console.ReadLine()) != null)
            {
                string[] id_hour = response.Split("/");
                int hour;
                while (true)
                {
                    if (id_hour.Length == 2 && int.TryParse(id_hour[1], out hour) && hour >= 9 && hour < 18)
                    {
                        if (program != null && school.Book(student, day, hour, id_hour[0], program, lab)) { break; }
                        else if (program != null && program != "") { Console.WriteLine("Select differt Working Station or hour"); }
                        else
                        {
                            if (school.Book(student, day, int.Parse(id_hour[1]), id_hour[0], null, lab)) { break; }
                            else { Console.WriteLine("Select different hour or Working Station"); }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Select a valid Hour or Working Station");
                        break;
                    }
                }
                Console.WriteLine("\nExit? (y/n)");
                if ((response = Console.ReadLine()) == "y") { return; }
            }
            else
            {
                Console.WriteLine("Exit? (y/n)");
                if ((response = Console.ReadLine()) == "y")
                {
                    break;
                }
            }
        }
    }

    private Labs? SelectLaboratory(Teacher teacher, ref int day)
    {
        Labs? lab;
        while (true)
        {
            Console.WriteLine("Select Laboratory and hour to Book (LabName/hour)");
            string? response;
            school.showLabsAvaibility(day);
            while (true)
            {
                if ((response = Console.ReadLine()) != null && response != "")
                {
                    string[] labName_hour = response.Split("/");
                    int hour;
                    if (labName_hour.Length == 2 && int.TryParse(labName_hour[1], out hour) && hour < 18 && hour >= 9 && (lab = school.FindLab(labName_hour[0])) != null)
                    {
                        Console.WriteLine("Teacher " + teacher.Name + " Day " + day + " hour " + hour + " lab " + lab);
                        if (school.Book(teacher, day, hour, lab)) { break; }
                    }
                    Console.WriteLine("Select Different Lab or hour");
                }
            }
            Console.WriteLine("Wanna Book some other Labs? (y/n)");
            if ((response = Console.ReadLine()) == "n") { break; }
        }
        return lab==null? null : lab;

    }

    private Teacher verifyTeacher()
    {
        Teacher teacher;
        bool keep = true;
        string? response;
        while (keep)
        {
            Console.WriteLine("insert Teacher Pass");
            if ((response = Console.ReadLine()) != "" && response != null && (teacher = school.FindTeacher(response)) != null) { return teacher; }
            else { Console.WriteLine("Invalid Pass: Insert a Correct Pass..."); }
        }
        return null;
    }

    private Student verifyStudent()
    {
        string? response;
        bool keep = true;
        Student student = null;
        while (keep)
        {
            Console.WriteLine("Insert Matricola");
            if ((response = Console.ReadLine()) != null && response != "" && (student = school.FindStudent(response)) != null)
            {
                keep = false;
            }
        }
        return student;
    }
    public School school { get => _school; }

    public Client(School school)
    {
        _school = school;
        Console.WriteLine(school);
        TextMenu();
    }

    public void TextMenu()
    {
        Console.WriteLine("Are you a Student? (y/n)");
        string? response;
        Labs? lab = null;
        int day = 0;

        if ((response = Console.ReadLine()) != null && response.ToLower() == "y")
        {

            Student student = verifyStudent();

            lab = SelectDay(ref day, lab);

            Console.WriteLine("Need a particular program? ");
            string? program = Console.ReadLine();

            //User Selection
            SelectWorkingStation(student, (int)day, program, lab);
            Console.Write($"{student.Name} usage: ");
            lab.printStudentUsage(student);
        }
        else
        {
            Teacher teacher = verifyTeacher();

            SelectDay(ref day);

            lab= SelectLaboratory(teacher, ref day);
            if (lab != null)
            {
                lab.printLabUsage(teacher);
            }
        }
    }
}