using System.Collections.Concurrent;
using System.Data;

public class User
{
    private string _name;

    private string _surname;

    //convert the week day into number (needed for search the computer in lab )
    protected int TakeDaysNumber(DateTime date)
    {
        string[] days = "monday tuesday Wednesday thursday friday".Split(" ");
        string today = date.DayOfWeek.ToString().ToLower();
        if (today != "sunday" && today != "saturday")
        {
            for (int i = 0; i < days.Length; i++)
            {
                if (today == days[i])
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public string Name { get => _name; }

    public string Surname { get => _surname; }


    public User(string name, string surname)
    {
        _name = name;
        _surname = surname;
    }

    //Default Book, book a working station from now
    public void BookWs(int term, string? neededProgram, Labs lab)
    {
        //Is the date time inserted right?
        DateTime today = DateTime.Now;
        if (today.AddHours(term) > today.Date.AddHours(18) || TakeDaysNumber(today.Date) == -1 || today < today.Date.AddHours(9))
        {
            Console.WriteLine($"Sorry, you can't reserve the Working Station for {DateTime.Now}. Try a different Day");
            return;
        }

        int day = TakeDaysNumber(today);
        Computer? ws;

        if (neededProgram != null)
        {
            ws = lab.FindComputer(neededProgram, day, today, term);
            if (ws == null)
            {
                Console.WriteLine($"Sorry the working station with {neededProgram} is already taken, try with different day or lab");
                return;
            }
        }
        else
        {
            //first free working station in the lab
            ws = lab.FindComputer(neededProgram, day, today, term);
            if (ws == null)
            {
                Console.WriteLine($"Sorry the {lab} is full, try with different day or lab");
                return;
            }
        }

        //Adding computer reserv
        WorkingStationReserve reserv = new WorkingStationReserve(today, term, this, ws, lab);
        ws.addReserv(reserv);
        Console.Write($"Working station booked on {today.Month}/{today.Day} {today.Hour}:{today.Minute}");
        Console.WriteLine($" to {reserv.Start.Hour}:{reserv.End.Minute}");


    }

    //Personalizated Book, if the user need a particolar program
    public void BookWs(int term, string? neededProgram, DateTime reservStart, Labs lab)
    {
        if (reservStart < reservStart.Date.AddHours(9) || reservStart.AddHours(term) > reservStart.Date.AddHours(18) || TakeDaysNumber(reservStart.Date) == -1)
        {
            Console.WriteLine($"Sorry, you can't reserve the Working Station for {reservStart}. Try a different Day");
            return;
        }

        int day = TakeDaysNumber(reservStart.Date);
        Computer? ws;

        if (neededProgram != null)
        {
            //working station with the desired program
            ws = lab.FindComputer(neededProgram, day, reservStart, term);
            if (ws == null) { return; }
        }
        else
        {
            //first free working station in the lab
            ws = lab.FindComputer(neededProgram, day, reservStart, term);
            if (ws == null) { return; }
        }

        //Adding computer reserv
        WorkingStationReserve reserv = new WorkingStationReserve(reservStart, term, this, ws, lab);
        ws.addReserv(reserv);
        Console.Write($"Working station booked on {reservStart.Month}/{reservStart.Day} {reservStart.Hour}:{reservStart.Minute}");
        Console.WriteLine($" to {reserv.End.Hour}:{reserv.End.Minute}");
    }
}

public class Student : User
{
    public Student(string name, string surname) : base(name, surname) { }
}

public class Teacher : User
{
    public Teacher(string name, string surname) : base(name, surname) { }

    public enum BookType { WORKINGSTATION, LAB }

    //entire lab prenotation
    public void BookLab(Labs lab, DateTime date, int reservTemp, Teacher teacher)
    {
        if (lab.CheckAvaibility(TakeDaysNumber(date), date, reservTemp, teacher))
        {
            Console.WriteLine($"{lab} has been prenotated for {date.Hour} to {date.Hour + reservTemp}");
        }
        else 
        {
            Console.WriteLine($"Sorry, {lab} can't be prenoted, please try with other day or time");
        }
    }


}