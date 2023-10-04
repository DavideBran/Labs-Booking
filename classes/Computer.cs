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
            _computerUsage++;
            return true;
        }
        return false;
    }

    public bool tryBook(User applicant, int hour, string? program)
    {
        if (program != null && program != "")
        {
            if (getProgram(program))
            {
                return AddReserv(applicant, hour);
            }
            return false;
        }
        else
        {
            bool ret = AddReserv(applicant, hour);
            return ret;
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
            if (_reservation[i] == null) { Console.Write("  ".PadRight(5) + "°".PadRight(5) + "|".PadRight(5)); }
            else { Console.Write("  ".PadRight(5) + "*".PadRight(5) + "|".PadRight(5)); }
        }
    }

    public int UsageBy(Student student)
    {
        int usage= 0;
        foreach (User st in _reservation)
        {
            if(st != null && st.GetType() == typeof(Student) && ((Student)st).Matricola == student.Matricola) usage++;
        }
        return usage;
    }
}