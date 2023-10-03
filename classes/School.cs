using System.ComponentModel;
using System.Data.Common;
using System.Net.WebSockets;

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
    public void Book(Teacher applicant, int hour)
    {
        foreach (Labs lab in _labs)
        {
            if (lab.BookAll(applicant, hour)) { return; }
        }
        Console.WriteLine($"Sorry, theresn't any lab that can be prenoted");
    }

    public void Book(Teacher applicant, int day, int hour)
    {
        foreach (Labs lab in _labs)
        {
            if (lab.BookAll(applicant, day, hour)) { return; }
        }
        Console.WriteLine($"Sorry, theresn't any lab that can be prenoted on {day}");
    }

    public void Book(Teacher applicant, int day, int hour, Labs lab)
    {
        if (lab.BookAll(applicant, day, hour)) { return; }
        Console.WriteLine($"Sorry, {lab.Name} can't be prenoted on {day}");
    }

    //Studens and Teacher Working Station Booking
    public void showAvaibility(Labs lab)
    {
        lab.showAvaibility();
    }

    public void showAvaibility(Labs lab, string program)
    {
        lab.showAvaibility(program);
    }

    public void Book(User applicant, Labs lab)
    {
        if (lab.Book(applicant)) return;
        else Console.WriteLine($"Sorry, No working Station avaible for the week in {lab.Name}");
    }
    public void Book(User applicant, int day, Labs lab)
    {
        if (lab.Book(day, applicant)) return;
        else Console.WriteLine($"Sorry, the lab is full for {day}");
    }

    public void Book(User applicant, int day, int hour, string program, Labs lab)
    {
        if (lab.Book(applicant, day, hour, program)) return;
        else Console.WriteLine($"Sorry, the lab is full for {day}");
    }

    public void Book(User applicant, int day, int hour, Labs lab)
    {
        if (lab.Book(applicant, day, hour)) return;
        else Console.WriteLine($"Sorry, the lab is full for {day}");
    }

    public void Book(User applicant, int day)
    {
        foreach (Labs lab in _labs)
        {
            if (lab.Book(day, applicant)) { return; }
        }
    }

    public void Book(User applicant, int day, int hour)
    {
        foreach (Labs lab in _labs)
        {
            if (lab.Book(applicant, day, hour)) { return; }
        }
    }

    public void Book(User applicant, int day, string program)
    {
        foreach (Labs lab in _labs)
        {
            if (lab.Book(applicant, day, program)) { return; }
        }
    }

    public void Book(User applicant, int day, int hour, string program)
    {
        foreach (Labs lab in _labs)
        {
            if (lab.Book(applicant, day, hour, program)) { return; }
        }
    }

    public void Book(User applicant)
    {
        foreach (Labs lab in _labs)
        {
            if (lab.Book(applicant)) { return; }
        }
        Console.WriteLine("Sorry, theresn't any free Lab for today...");
    }

    public void ShowLabsAllAvaible(int day, int hour)
    {
        Console.Write($"\t\t Prenotable Labs|");
        for (int i = 0; i < _labs.Length; i++)
        {
            if (_labs[i].IsFullyPrenotable(day, hour))
            {
                Console.Write($" {_labs[i].Name} |");
            }
        }
        Console.WriteLine();
    }
}

public class Labs
{
    private int _id;

    private string _labName;

    private WorkingStation[][] _workingStation = new WorkingStation[5][];

    private int _usage = 0;

    private void deepCopy(WorkingStation[] workingStation)
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

        deepCopy(workingStation);
    }

    //Working Station Bookings
    public bool Book(User applicant, int hour)
    {
        for (int i = 0; i < _workingStation.Length; i++)
        {
            for (int j = 0; j < _workingStation[i].Length; j++)
            {
                if (_workingStation[i][j].tryBook(hour, applicant))
                {
                    Console.WriteLine($"{_workingStation[i][j].ID} in {Name} Booked for day {i + 1} at {hour}");
                    return true;
                }
            }
        }
        return false;
    }

    public bool Book(User applicant, int day, int hour)
    {
        for (int i = 0; i < _workingStation[day].Length; i++)
        {
            if (_workingStation[day][i].tryBook(hour, applicant))
            {
                Console.WriteLine($"{_workingStation[day][i].ID} in {Name} Booked for day {i + 1} at {hour}");
                return true;
            }
        }
        return false;
    }

    public bool Book(User applicant, int day, int hour, string program)
    {
        for (int i = 0; i < _workingStation[day].Length; i++)
        {
            if (_workingStation[day][i].tryBook(applicant, program, hour))
            {
                Console.WriteLine($"{_workingStation[day][i].ID} in {Name} Booked for day {day} at {hour}");
                return true;
            }
        }
        return false;
    }

    public bool Book(User applicant)
    {
        int hour;
        for (int i = 0; i < _workingStation.Length; i++)
        {
            for (int j = 0; j < _workingStation[i].Length; j++)
            {
                if ((hour = _workingStation[i][j].tryBook(applicant)) != -1)
                {
                    Console.WriteLine($"{_workingStation[i][j].ID} in {Name} Booked for day {i + 1} at {hour}");
                    return true;
                }
            }
        }
        return false;
    }

    public bool Book(User applicant, int day, string program)
    {
        int hour;
        for (int i = 0; i < _workingStation[day].Length; i++)
        {
            if ((hour = _workingStation[day][i].tryBook(applicant, program)) != -1)
            {
                Console.WriteLine($"{_workingStation[day][i].ID} in {Name} Booked for day {day} at {hour}");
                return true;
            }
        }
        return false;
    }

    public bool Book(int day, User applicant)
    {
        int hour;
        for (int i = 0; i < _workingStation[day].Length; i++)
        {
            if ((hour = _workingStation[day][i].tryBook(applicant)) != -1)
            {
                Console.WriteLine($"{_workingStation[day][i].ID} in {Name} Booked for day {day}");
                return true;
            }
        }
        return true;
    }

    //Labs Booking 
    public bool BookAll(Teacher applicant, int hour)
    {
        for (int i = 0; i < _workingStation.Length; i++)
        {
            for (int j = 0; j < _workingStation[i].Length; j++)
            {
                if (_workingStation[i][j].tryBook(hour, applicant))
                {
                    Console.WriteLine($"{Name} Booked for day {i + 1} at {hour}");
                    return true;
                }
            }
        }
        return false;
    }

    public bool BookAll(Teacher applicant, int day, int hour)
    {
        foreach (WorkingStation ws in _workingStation[day])
        {
            if (!ws.tryBook(hour, applicant)) { return false; }
        }
        Console.WriteLine($"{Name} prenotated for {day} at {hour}");
        return true;
    }

    public override string ToString()
    {
        return "OCACA CESSU";
    }

    public void showAvaibility()
    {
        Console.Write("\t\tWorking Station |");
        for (int i = 0; i < _workingStation.Length; i++)
        {
            for (int j = 0; j < _workingStation[i].Length; j++)
            {
                Console.Write($"\t\t {_workingStation[i][j].ID} |");
            }
            Console.WriteLine($"\t\t{i}-");
        }
    }
    public void showAvaibility(string program)
    {
        Console.Write("\t\tWorking Station |");
        for (int i = 0; i < _workingStation.Length; i++)
        {
            bool flag = false;
            for (int j = 0; j < _workingStation[i].Length; j++)
            {
                if (_workingStation[i][j].hasProgram(program))
                {
                    Console.Write($"\t\t {_workingStation[i][j].ID} |");
                    flag = true;
                }
            }
            if (flag)
            {
                Console.WriteLine($"\t\t{i}-");
            }
        }

    }

    public bool IsFullyPrenotable(int day, int hour)
    {
        foreach (WorkingStation ws in _workingStation[day]){
            if (!ws.isPrenotable(hour)) { return false; }
        }
        return true;
        
    }
}