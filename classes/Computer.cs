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

    private User?[] _reservation = new User[9];

    public int Usage { get => _computerUsage; }

    public WorkingStation(string id, string[] program) : base(id, program) { Array.Fill(_reservation, null); }

    public WorkingStation(WorkingStation copy) : base(copy.ID, copy.ProgramsList)
    {
        _computerUsage = copy._computerUsage;
        Array.Fill(_reservation, null);
    }

    //Booking Methods

    private bool AddReserv(User applicant, int hour)
    {
        if (_reservation[hour - 9] == null)
        {
            _reservation[hour - 9] = applicant;
            return true;
        }
        return false;
    }

    public bool tryBook(User applicant, int hour, string? program)
    {
        if (program != null)
        {
            if (getProgram(program))
            {
                return AddReserv(applicant, hour);
            }
            return false;
        }
        else
        {
            return AddReserv(applicant, hour);
        }
    }

    //Booking Labs method

    public bool IsPrenotable(int hour)
    {
        if (_reservation[hour - 9] == null) return true;
        else return false;
    }

    //menu methods

    public void ShowAvaibility()
    {
        Console.Write($"\t\t{ID}|");
        for (int i = 0; i < _reservation.Length; i++)
        {
            if (_reservation[i] == null) { Console.Write("  °  |"); }
            else { Console.Write("  * |"); }
        }
    }
}