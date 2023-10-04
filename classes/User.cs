using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;

public abstract class User
{
    private string _name;

    private string _surname;

    public string Name { get => _name; }

    public string Surname { get => _surname; }



    public User(string name, string surname)
    {
        _name = name;
        _surname = surname;
    }
}

public class Student : User
{
    private string _matricola;

    public string Matricola { get => _matricola; }

    public Student(string name, string surname, string matricola) : base(name, surname)
    {
        _matricola = matricola;
    }

}

public class Teacher : User
{
    private string _teacherPass;

    protected string pass { get => _teacherPass; }

    public bool isTeacher(string pass)
    {
        if (_teacherPass == pass) { return true; }
        return false;
    }
    public Teacher(string name, string surname, string teacherPass) : base(name, surname)
    {
        _teacherPass = teacherPass;
    }

    public bool IsEquals(Teacher teacher)
    {
        if (teacher.pass == _teacherPass) { return true; }
        return false;
    }
}