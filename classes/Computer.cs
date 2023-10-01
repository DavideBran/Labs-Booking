using System.Data;

public class Computer
{
    private int _ID;
    private string[] _programs;

    public int ID { get => _ID; }

    public WorkingStationReserve[] reservation = new WorkingStationReserve[10];

    public bool booked = false;

    public bool getProgram(string program)
    {
        return reservation == null && Array.Find(_programs, prg => prg == program) != null;
    }

    public Computer(int id, string[] programs)
    {
        _programs = programs;
        _ID = id;
        Array.Fill(reservation, null);
    }

    public void addReserv(WorkingStationReserve reserv)
    {
        for(int i=reserv.lab.Convert24h(reserv.Start); i < reserv.lab.Convert24h(reserv.End); i++){
            reservation[i]= reserv;
        }
    }
}