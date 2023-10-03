using System.Data;
using System.Net.Sockets;

public class Computer
{
    private string _ID;
    private string[] _programs;

    public string[] ProgramsList { get => _programs; }

    public string ID { get => _ID; }

    public bool getProgram(string program)
    {
        return Array.Find(_programs, prg => prg == program) != null;
    }

    public Computer(string id, string[] programs)
    {
        _programs = programs;
        _ID = id;
    }

}

public class WorkingStation : Computer
{

    private int _computerUsage = 0;

    private User?[] _reservation = new User[10];

    //Self-Regulation
    private void cleanReservation()
    {
        int nowHour = DateTime.Now.Hour;
        emptyTheReserv(nowHour);
    }

    private void emptyTheReserv(int hour)
    {
        if (hour <= 9 || hour > 18)
        {
            Array.Fill(_reservation, null);
            return;
        }
        for (int i = 0; i < hour - 9; i++)
        {
            _reservation[i] = null;
        }
    }
    //Making reservation
    private int AddReserv(User applicant)
    {
        cleanReservation();
        for (int i = 0; i < _reservation.Length; i++)
        {
            if (_reservation[i] == null)
            {
                _reservation[i] = applicant;
                return i + 9;
            }
        }
        return -1;
    }

    private bool AddReserv(User applicant, int hour)
    {
        if (_reservation[hour - 9] == null) { return true; }
        return false;
    }
    public int Usage { get => _computerUsage; }

    public WorkingStation(string id, string[] program) : base(id, program) { Array.Fill(_reservation, null); }

    public WorkingStation(WorkingStation copy) : base(copy.ID, copy.ProgramsList)
    {
        _computerUsage = copy._computerUsage;
        Array.Fill(_reservation, null);
    }

    //Booking Methods
    public int tryBook(User applicant)
    {
        return AddReserv(applicant);
    }

    public int tryBook(User applicant, string program)
    {
        if (getProgram(program))
        {
            return AddReserv(applicant);
        }
        return -1;
    }

    public bool tryBook(User applicant, string program, int hour)
    {
        if (getProgram(program))
        {
            return AddReserv(applicant, hour);
        }
        return false;
    }
    public bool tryBook(int hour, User applicant)
    {
        if (AddReserv(applicant, hour)) { return true; }
        return false;
    }

    //Menu Methods
    public bool hasProgram(string program)
    {
        if (getProgram(program)) { return true; }
        return false;
    }

    public bool isPrenotable(int hour)
    {
        if(_reservation[hour - 9] == null) return true;
        else return false;
    }
}