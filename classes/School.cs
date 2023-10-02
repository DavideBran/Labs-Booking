using System.Data.Common;
using System.Net.WebSockets;

public class School
{
    private Labs[] _labs;
    private User[] _users;

    public School(Labs[] labs, User[] users)
    {
        _labs = labs;
        _users = users;
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

    public void Book(Teacher applicant, int day, Labs lab, int hour)
    {
        if (lab.BookAll(applicant, day, hour)) { return; }
        Console.WriteLine($"Sorry, {lab.name} can't be prenoted on {day}");
    }

    public void Book(Labs lab, Teacher applicant, int hour)
    {
        if (lab.BookAll(applicant, hour)) { return; }
        else Console.WriteLine($"Sorry, {lab.name} is already Booked");
    }

    //Studens and Teacher Working Station Booking
    public void Book(Labs lab, int day, User applicant)
    {
        if (lab.Book(day, applicant)) return;
        else Console.WriteLine($"Sorry, the lab is full for {day}");
    }

    public void Book(int day, User applicant)
    {
        foreach (Labs lab in _labs)
        {
            if (lab.Book(day, applicant)) { return; }
        }
    }

    public void Book(int day, User applicant, string program)
    {
        foreach (Labs lab in _labs)
        {
            if (lab.Book(day, applicant, program)) { return; }
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

    public string name { get => _labName; }

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
                    Console.WriteLine($"{_workingStation[i][j].ID} in {name} Booked for day {i + 1} at {hour}");
                    return true;
                }
            }
        }
        return false;
    }
    
    public bool Book(User applicant, int hour, int day)
    {
        for (int i = 0; i < _workingStation[day].Length; i++)
        {
            if (_workingStation[day][i].tryBook(hour, applicant))
            {
                Console.WriteLine($"{_workingStation[day][i].ID} in {name} Booked for day {i + 1} at {hour}");
                return true;
            }
        }
        return false;
    }
    
    public bool Book(User applicant, int hour, int day, string program)
    {
        for (int i = 0; i < _workingStation[day].Length; i++)
        {
            if (_workingStation[day][i].tryBook(applicant, program, hour))
            {
                Console.WriteLine($"{_workingStation[day][i].ID} in {name} Booked for day {day} at {hour}");
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
                    Console.WriteLine($"{_workingStation[i][j].ID} in {name} Booked for day {i + 1} at {hour}");
                    return true;
                }
            }
        }
        return false;
    }

    public bool Book(int day, User applicant, string program)
    {
        int hour;
        for (int i = 0; i < _workingStation[day].Length; i++)
        {
            if ((hour = _workingStation[day][i].tryBook(applicant, program)) != -1)
            {
                Console.WriteLine($"{_workingStation[day][i].ID} in {name} Booked for day {day} at {hour}");
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
                Console.WriteLine($"{_workingStation[day][i].ID} in {name} Booked for day {day}");
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
                    Console.WriteLine($"{name} Booked for day {i + 1} at {hour}");
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
        Console.WriteLine($"{name} prenotated for {day} at {hour}");
        return true;
    }

    public override string ToString()
    {
        return "OCACA CESSU";
    }

    public void showAvaibility()
    {

    }

    public void showCompleteAvaibility()
    {

    }
}