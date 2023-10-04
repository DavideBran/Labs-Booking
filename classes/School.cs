using System.ComponentModel;
using System.Data.Common;
using System.Net.WebSockets;
using System.Reflection.Metadata;

public class School
{
    private Labs[] _labs;
    private User[] _users;

    private string _schoolName;

    public School(string name, Labs[] labs, User[] users)
    {
        _labs = labs;
        _users = users;
        _schoolName = name;
    }

    //Show function
    public string Name { get => _schoolName; }

    public int TeacherCount()
    {
        int counter = 0;
        foreach (User Employ in _users)
        {
            if (Employ.GetType() == typeof(Teacher))
            {
                counter++;
            }
        }
        return counter;
    }

    public int StudensCount()
    {
        int counter = 0;
        foreach (User Employ in _users)
        {
            if (Employ.GetType() == typeof(Student))
            {
                counter++;
            }
        }
        return counter;
    }

    public int LabsCount()
    {
        int counter = 0;
        foreach (Labs lab in _labs) { counter++; }
        return counter;
    }

    public void ShowTeacher()
    {
        Console.Write("\n\t\tTeacher |");
        foreach (User Employ in _users)
        {
            if (Employ.GetType() == typeof(Teacher))
            {
                Console.Write($" {Employ.Name}, {Employ.Surname} |");
            }
        }
        Console.WriteLine();
    }

    public void ShowStudent()
    {
        Console.Write("\n\t\tStudens |");
        foreach (User Employ in _users)
        {
            if (Employ.GetType() == typeof(Student))
            {
                Console.Write($" {((Student)Employ).Matricola} |");
            }
        }
        Console.WriteLine();
    }

    public void ShowLabs()
    {
        Console.Write("\n\t\tLabs |");
        foreach (Labs Lab in _labs)
        {
            Console.Write($" {Lab.Name}, with {Lab.WsAvaible} Working Station |");
        }
        Console.WriteLine();
    }

    public override string ToString()
    {
        Console.WriteLine($"\t\t{Name} has {TeacherCount()} Teacher, {StudensCount()} Studens and {LabsCount()} Labs: ");
        ShowTeacher();
        ShowStudent();
        ShowLabs();
        return "";
    }

    //Control Methods
    public Student? FindStudent(string? matricola)
    {
        if (matricola == null)
        {
            Console.WriteLine("ERROR: Invalid Matricola");
            return null;
        }

        foreach (User student in _users)
        {
            if (student.GetType() == typeof(Student) && ((Student)student).Matricola == matricola) { return (Student)student; }
        }
        return null;
    }

    public Teacher? FindTeacher(string? pass)
    {
        if (pass == null)
        {
            Console.WriteLine("ERROR: Invalid Pass");
            return null;
        }

        foreach (User teacher in _users)
        {
            if (teacher.GetType() == typeof(Teacher) && ((Teacher)teacher).isTeacher(pass)) { return (Teacher)teacher; }
        }
        return null;
    }

    public Labs? FindLab(string labName)
    {
        foreach (Labs Lab in _labs)
        {
            if (Lab.Name == labName)
            {
                return Lab;
            }
        }
        return null;
    }

    //teacher Labs booking
    public void showLabsAvaibility(int day)
    {
        Console.Write("\n\t\t" + "Name".PadRight(10) + $"| {0 + 9}/{0 + 10} " + "|");
        for (int i = 1; i < 9; i++)
        {
            Console.Write(" " + $"{i + 9}/{i + 10} " + "|");
        }
        Console.WriteLine();

        for (int i = 0; i < _labs.Length; i++)
        {
            Console.Write($"\t\t{_labs[i].Name}".PadRight(15) + "|");
            _labs[i].showLabsAvaibility(day);
            Console.WriteLine();
        }
        /*
            
                            Insert Lab ID and Start Hour of Prenotation
                                
                                |  09/10 |  10/11 |  11/12 |
                            Lab1 |   *   |  *   |    °   |
                            Lab2 |   °   |  °   |    °   |
                            Lab3 |   *   |  °   |    °   |
        */
    }

    public bool Book(Teacher applicant, int day, int hour, Labs lab)
    {
        if (lab.Book(applicant, day, hour))
        {
            Console.WriteLine($"\t\tPrenotation: {applicant.Name} {applicant.Surname}\n\t\t| {lab.Name} |\n\t\t| Prenotation Day: {day} from {hour} to {hour + 1} |");
            return true;
        }
        return false;
    }

    //Studens and Teacher Working Station Booking
    public void showWorkingStationAvaibility(Labs lab, int day)
    {
        lab.showWorkingStationAvaibility(day);
    }

    public bool Book(User applicant, int day, int hour, string ID, string? program, Labs lab)
    {
        if (!lab.Book(applicant, day, hour, ID, program)) { return false; }
        Console.WriteLine($"\t\t| Prenotation: {applicant.Name} {applicant.Surname} |\n\t\t| {lab.Name}, Working Station ID: {ID} |\n\t\t| Prenotation Day: {day} from {hour} to {hour + 1} |");
        return true;
    }
}

public class Labs
{
    private int _id;

    private string _labName;

    private WorkingStation[][] _workingStation = new WorkingStation[5][];

    private List<Teacher> _reserv = new();

    private int _usage = 0;

    private void DeepCopy(WorkingStation[] workingStation)
    {
        for (int i = 0; i < _workingStation.Length; i++)
        {
            _workingStation[i] = new WorkingStation[workingStation.Length];
            for (int j = 0; j < _workingStation[i].Length; j++)
            {
                _workingStation[i][j] = new WorkingStation(workingStation[j]);
            }
        }
    }

    public int WsAvaible { get => _workingStation[0].Length; }
    public string Name { get => _labName; }

    public int Usage { get => _usage; }

    public Labs(int id, WorkingStation[] workingStation, string labName)
    {
        _id = id;
        _labName = labName;

        DeepCopy(workingStation);
    }

    //Working Station Bookings
    public bool Book(User applicant, int day, int hour, string id, string? program)
    {
        for (int i = 0; i < _workingStation[day].Length; i++)
        {
            if (_workingStation[day][i].ID == id)
            {
                if (!_workingStation[day][i].tryBook(applicant, hour, program))
                {
                    if (program != null && program != "")
                    {
                        Console.WriteLine($"The Working Station do not have {program}");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("The Working Station is already Booked");
                        return false;
                    }
                }
                else { return true; }
            }
        }
        Console.WriteLine("Working Station not found");
        return false;
    }

    public void showWorkingStationAvaibility(int day)
    {
        Console.Write("\n\t\t" + "ID".PadLeft(5) + $"| {0 + 9}/{0 + 10}".PadRight(5) + "|");
        for (int i = 1; i < 9; i++)
        {
            Console.Write(" ".PadLeft(5) + $"{i + 9}/{i + 10} ".PadRight(5) + "|");
        }
        Console.WriteLine();
        for (int i = 0; i < _workingStation[day].Length; i++)
        {
            _workingStation[day][i].ShowAvaibility();
            Console.WriteLine();
        }

        /*
                            Insert Lab ID and Start Hour of Prenotation

                              ID |  09/10 |  10/11 |  13/14 |  14/15  |
                            Pos1  |   *   |  *   |    °   |    °   |
                            Pos2 |   °   |  °   |    °   |    °   |
                            Pos3 |   *   |  °   |    °   |    °   |
                            Pos4 |   *   |  *   |    °   |    °   |
        */
    }

    //Labs Booking 
    public bool Book(Teacher applicant, int day, int hour)
    {
        bool flag = true;
        for (int i = 0; i < _workingStation[day].Length; i++)
        {
            if (!_workingStation[day][i].IsPrenotable(hour))
            {
                return false;
                break;
            }
        }
        if (flag)
        {
            foreach (WorkingStation ws in _workingStation[day])
            {
                ws.tryBook(applicant, hour, null);
            }
            _usage++;
        }
        _reserv.Add(applicant);
        return true;
    }

    public void showLabsAvaibility(int day)
    {
        for (int hour = 9; hour < 18; hour++)
        {
            bool flag = true;
            for (int i = 0; i < _workingStation[day].Length; i++)
            {
                if (_workingStation[day][i].IsPrenotable(hour)) { continue; }
                else { flag = false; }
            }
            if (flag) { Console.Write("  °  |".PadRight(7)); }
            else Console.Write("  *  |".PadRight(7));
        }


        /* 
                            Insert Lab ID and Start Hour of Prenotation

                                |  09/10 |  10/11 |  11/12 |
                            Lab1 |   *   |  *   |    °   |
                            Lab2 |   °   |  °   |    °   |
                            Lab3 |   *   |  °   |    °   |
        */
    }

    public bool IsFullyPrenotable(int day, int hour)
    {
        foreach (WorkingStation ws in _workingStation[day])
        {
            if (!ws.IsPrenotable(hour)) { return false; }
        }
        return true;

    }

    //Usage
    public void printLabUsage(Teacher tch)
    {
        int usage = 0;
        foreach (Teacher teacher in _reserv)
        {
            if (teacher.IsEquals(tch)) { usage++; }
        }
        Console.WriteLine($"{tch.Name} has used the {Name} for {usage} hour");
    }

    public void printStudentUsage(Student student)
    {
        int userUsage= 0;
        for (int i = 0; i < _workingStation.Length; i++)
        {
            for (int j = 0; j < _workingStation[i].Length; j++)
            {
                userUsage+= _workingStation[i][j].UsageBy(student);
            }
        }
        Console.Write(userUsage);
    }
}