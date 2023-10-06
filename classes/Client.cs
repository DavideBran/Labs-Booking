public class Client
{
    private School _school;

    private Labs SelectDay(ref int? day, Labs? lab)
    {
        string? response;
        Labs retLab;
        Console.WriteLine("\t\tSelect a day\n\t\t1-Mon\n\t\t2-Tue\n\t\t3-Wed\n\t\t3-Thu\n\t\t4-Fri");
        while (true)
        {
            try
            {
                day = int.Parse(Console.ReadLine());
                day = day == null || day > 5 || day < 1 ? throw new InvalidDayException() : day;
                Console.WriteLine("Select a Lab (insert the laboratory Name)");
                Console.WriteLine(school.ShowLabs());
                while ((response = Console.ReadLine()) != null && response != "")
                {
                    if ((retLab = school.FindLab(response)) != null)
                    {
                        return retLab;
                    }
                    else { Console.WriteLine("insert a valid Laboratory"); }
                }
            }
            catch (InvalidDayException)
            {
                Console.WriteLine("Insert a valid Day");
            }
            catch (FormatException)
            {
                Console.WriteLine("Insert a valid Hour");
            }
        }
    }

    private void SelectDay(ref int? day)
    {
        Console.WriteLine("\t\tSelect a day\n\t\t1-Mon\n\t\t2-Tue\n\t\t3-Wed\n\t\t3-Thu\n\t\t4-Fri");
        while (true)
        {
            try
            {
                day = int.Parse(Console.ReadLine());
                day = day == null || day > 5 || day < 1 ? throw new Exception() : day;
                break;
            }
            catch
            {
                Console.WriteLine("Insert a valid Day");
            }
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
                string[] id_hour = null;
                int? hour = 0;
                try
                {
                    id_hour = response.Split("/");
                    id_hour = id_hour.Length == 2 ? id_hour : throw new ArgumentException();
                    hour = int.Parse(id_hour[1]);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Insert a valid Lab name or hour");
                }

                while (true)
                {
                    try
                    {
                        hour = hour == null || hour < 9 || hour >= 18 ? throw new InvalidHourException() : hour;
                        if (!school.Book(student, day, (int)hour, id_hour[0], program, lab)) { throw new Exception(); }

                    }
                    catch (InvalidHourException)
                    {
                        Console.WriteLine("Select a valid Hour or Working Station");
                        break;
                    }
                    catch (Exception)
                    {
                        if (program != null && program != "")
                        {
                            Console.WriteLine("Program Not found");
                        }
                        else
                        {
                            Console.WriteLine("Working Station is already Booked Select a different one");
                        }
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

    private Labs? SelectLaboratory(Teacher teacher, int day)
    {
        Labs? lab = null;
        while (true)
        {
            Console.WriteLine("Select Laboratory and hour to Book (LabName/hour)");
            string? response;
            school.showLabsAvaibility((int)day);
            while (true)
            {
                if ((response = Console.ReadLine()) != null && response != "")
                {
                    try
                    {
                        string[] labName_hour = response.Split("/");
                        labName_hour = labName_hour.Length == 2 ? labName_hour : throw new ArgumentException();
                        int? hour = int.Parse(labName_hour[1]);
                        hour = hour == null || hour < 9 || hour >= 18 ? throw new InvalidHourException() : hour;
                        lab = school.FindLab(labName_hour[0]);
                        if (labName_hour.Length == 2)
                        {
                            if (school.Book(teacher, day, (int)hour, lab)) { break; }
                        }
                    }
                    catch (InvalidHourException)
                    {
                        Console.WriteLine("Invalid Hour");
                    }
                    catch (InvalidLabException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Invalid Lab / hour insert!");
                    }
                }
                Console.WriteLine("Select a Valid Laboratory or press Q for Exit (If you want continue with prenotation press any key)");
                if (Console.ReadKey().Key.ToString() == "Q") { break; }
                Console.WriteLine();
            }
            Console.WriteLine("Wanna Book some other Labs? (y/n)");
            if ((response = Console.ReadLine()) == "n") { break; }
        }
        return lab;
    }

    private Teacher VerifyTeacher()
    {
        Teacher teacher;
        bool keep = true;
        string? response = null;
        while (keep)
        {
            Console.WriteLine("insert Teacher Pass");
            response = Console.ReadLine();
            if ((teacher = school.FindTeacher(response)) != null) { return teacher; }
            else { Console.WriteLine("Invalid Pass: Insert a Correct Pass..."); }
        }
        return null;
    }

    private Student VerifyStudent()
    {
        string? response = null;
        bool keep = true;
        Student student = null;
        while (keep)
        {
            Console.WriteLine("Insert Matricola");
            response = Console.ReadLine();
            if ((student = school.FindStudent(response)) != null)
            {
                return student;
            }
        }
        return null;
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
        int? day = 0;

        if ((response = Console.ReadLine()) != null && response.ToLower() == "y")
        {

            Student student = VerifyStudent();

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
            Teacher teacher = VerifyTeacher();

            SelectDay(ref day);

            lab = SelectLaboratory(teacher, (int)day);
            if (lab != null)
            {
                lab.printLabUsage(teacher);
            }
        }
    }
}