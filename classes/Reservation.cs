using System.Dynamic;

public class Reservation
{
    public DateTime _reservStart;

    public DateTime _reservEnd;

    private User _applicant;

    public DateTime End { get => _reservEnd; }
    public DateTime Start { get => _reservStart; }

    private void setEnd(int temp)
    {
        _reservEnd = _reservStart.AddHours(temp);
    }

    public Reservation(DateTime reservStart, int reservTemp, User applicant)
    {
        _reservStart = reservStart;
        setEnd(reservTemp);
        _applicant = applicant;
    }
}

public class WorkingStationReserve : Reservation
{
    private Computer _workingStation;
    private Labs _lab;

    public WorkingStationReserve(DateTime reserveStart, int reservTemp, User applicant, Computer ws, Labs lab) : base(reserveStart, reservTemp, applicant)
    {
        _workingStation = ws;
        _lab = lab;
    }
}

public class LabReserve : Reservation{

    private Labs _lab;
    public LabReserve(DateTime reserveStart, int reservTemp, User applicant, Labs lab) : base(reserveStart, reservTemp, applicant)
    {
        _lab = lab;
    }
}