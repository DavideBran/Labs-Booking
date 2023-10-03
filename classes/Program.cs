
using System.Net.WebSockets;
using System.Reflection.Metadata;

class Program
{

    static void Main()
    {
        string[] programs = "VScode Photoshop VisualStudio CodeBlock".Split(" ");
        WorkingStation[] ws = { new WorkingStation("XF18CC", programs), new WorkingStation("XF19C2", programs), new WorkingStation("JK11GC", programs), new WorkingStation("QM91CC", programs) };
        WorkingStation[] ws1 = { new WorkingStation("P12FC", programs), new WorkingStation("P12412", programs), new WorkingStation("KLS221", programs) };
        WorkingStation[] ws2 = { new WorkingStation("2UIVB", programs), new WorkingStation("LKJ178", programs), new WorkingStation("KKJL10", programs), new WorkingStation("PPOKHJ", programs), new WorkingStation("IL1109", programs) };

        User[] users = { new Teacher("Vincenzo", "Conti", "ContiV123"), new Teacher("Marco", "Rossi", "MMRRssi"), new Teacher("Pinuccia", "Nicosia", "PININICO123"), new Student("Davide", "Brancato", "1000029849"), new Student("Marcello", "Italiano", "X81000132") };
        Labs[] labs = { new(1, ws, "Laboratorio Andromeda"), new(2, ws1, "Laboratorio Oppenheimer"), new(3, ws2, "Laboratorio Einstein") };

        School school = new("Pirandello", labs, users);
        Client client = new(school);
    }
}
