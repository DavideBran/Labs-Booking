public class Computer
{
    private int _ID;
    private string[] _programs;

    public int ID { get => _ID; }

    public Reservation[] reservation = new Reservation[9];

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

    public bool addReserv(Reservation reserv)
    {
        Reservation? checkReserv;
        if ((checkReserv = Array.Find(reservation, r => r == null)) != null)
        {
            checkReserv = reserv;
            return true;
        }
        return false;
    }
}