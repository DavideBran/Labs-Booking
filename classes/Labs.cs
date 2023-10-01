using System.Net.WebSockets;

public class Labs
{
    private int _id;

    private Computer[][] _workingStation = new Computer[5][];

    // private void regulates(int day)
    // {
    //     for (int i = 0; i < _workingStation[day - 1].Length; i++)
    //     {
    //         Reservation? reserv = _workingStation[day][i].reservation;
    //         if (reserv != null && reserv.End >= DateTime.Now)
    //         {
    //             _workingStation[day][i].reservation = null;
    //         }
    //     }
    // }

    public Labs(int id, Computer[] workingStation)
    {
        _id = id;
        for (int i = 0; i < 5; i++) { _workingStation[i] = workingStation; }

    }

    public Computer[]? getAllWorkingStation(int day)
    {
        // regulates(day);
        Computer? bookedWs;
        if ((bookedWs = Array.Find(_workingStation[day - 1], c => c.reservation != null)) == null)
        {
            return _workingStation[day - 1];
        }
        return null;
    }

    public Computer? FindFirstComputer(int weekNumber, DateTime reserveDate, int temp)
    {
        for (int i = 0; i < _workingStation[weekNumber - 1].Length; i++)
        {
            Reservation[] reserv = _workingStation[weekNumber - 1][i].reservation;
            for (int j = 0; j < reserv.Length; j++)
            {
                if (reserv[j].End < reserveDate.AddHours(temp))
                {
                    return _workingStation[weekNumber - 1][i];
                }
            }
            if (_workingStation[weekNumber - 1][i].reservation != null)
            {
                return _workingStation[weekNumber - 1][i];
            }
        }
        return null;
    }

    public Computer? FindDesiredComputer(string DesiredProgram, int weekNumber)
    {
        Computer? ws;
        if ((ws = Array.Find(_workingStation[weekNumber - 1], ws => ws.getProgram(DesiredProgram))) != null) { return ws; }
        return null;
    }

    public Computer? FindComputer(string? DesiredProgram, int weekNumber, DateTime date, int temp)
    {
        Computer? ws;

        /* User want a particular Program*/
        if (DesiredProgram != null)
        {
            ws = FindDesiredComputer(DesiredProgram, weekNumber);
            if (ws != null)
            {
                return ws;
            }
            else
            {
                Console.WriteLine("The desired program is not avaible or the lab is full, Select a different Lab or day");
                return null;
            }
        }

        /* User need a computer (he doasn't need a particular program) */
        if ((ws = FindFirstComputer(weekNumber, date, temp)) != null) { return ws; }
        else
        {
            Console.WriteLine("No Working Station Avaibe, Select a different Lab");
            return null;
        }
    }
}