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

    public int Convert24h(DateTime date)
    {
        string hour24 = date.ToString("HH:mm:ss");
        //setting the time into 24h format
        return int.Parse(hour24.Split(":")[0]);
    }

    public Labs(int id, Computer[] workingStation)
    {
        _id = id;
        for (int i = 0; i < 5; i++) { _workingStation[i] = workingStation; }

    }

    public Computer? FindFirstComputer(int weekNumber, DateTime reserveDate, int temp)
    {
        for (int i = 0; i < _workingStation[weekNumber - 1].Length; i++)
        {
            //controlla se la posizione datetime è occupata
            int hourConverted = Convert24h(reserveDate) - 9;
            if (_workingStation[weekNumber - 1][i].reservation[hourConverted] != null && _workingStation[weekNumber - 1][i].reservation[hourConverted + temp] != null)
            {
                return _workingStation[weekNumber - 1][i];
            }
        }
        return null;
    }

    public Computer? FindDesiredComputer(string desiredProgram, int weekNumber, DateTime reserveDate, int temp)
    {
        for (int i = 0; i < _workingStation[weekNumber - 1].Length; i++)
        {
            //controlla se la posizione datetime è occupata
            int hourConverted = Convert24h(reserveDate) - 9;
            if (_workingStation[weekNumber - 1][i].getProgram(desiredProgram) && _workingStation[weekNumber - 1][i].reservation[hourConverted] == null && _workingStation[weekNumber - 1][i].reservation[hourConverted + temp] == null)
            {
                return _workingStation[weekNumber - 1][i];
            }
        }

        return null;
    }

    public Computer? FindComputer(string? DesiredProgram, int weekNumber, DateTime date, int temp)
    {
        Computer? ws;

        /* User want a particular Program*/
        if (DesiredProgram != null)
        {
            ws = FindDesiredComputer(DesiredProgram, weekNumber, date, temp);
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

    public bool CheckAvaibility(int weekDay, DateTime reserveStart, int reservTemp, Teacher applicant)
    {
        int hour24 = Convert24h(reserveStart) - 9;
        for (int i = 0; i < _workingStation[weekDay - 1].Length; i++)
        {
            if (reservTemp == 1)
            {
                if (_workingStation[weekDay - 1][i].reservation[hour24] == null) continue;
            }
            else
            {
                for (int j = hour24; j < hour24 + reservTemp; j++)
                {
                    if (_workingStation[weekDay - 1][i].reservation[j] != null) return false;
                }
            }
        }
        addReserv(weekDay, applicant, hour24, reservTemp, reserveStart);
        return true;
    }

    private void addReserv(int weekDay, Teacher applicant, int timeSlice, int temp, DateTime date)
    {
        for (int i = 0; i < _workingStation[weekDay - 1].Length; i++)
        {
            for (int j = timeSlice; j < timeSlice + temp; j++)
            {
                _workingStation[weekDay - 1][i].reservation[j] = new WorkingStationReserve(date, temp, applicant, _workingStation[weekDay - 1][i], this);
            }
        }
    }
}