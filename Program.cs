using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _05._03._26_Классы.Profession;

namespace _05._03._26_Классы
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Profession prof1 = new Profession("electric", 100);
            Teacher teach1 = new Teacher("math teacher", 120, "mathematics");
            Profession hiddenTeacher = new Teacher("inf teacher", 20, "informatics");

            Profession[] professions = new Profession[] { teach1, hiddenTeacher };
            for (int i = 0; i < professions.Length; i++)
            {
                if (professions[i] is Teacher)
                {
                    Teacher t = professions[i] as Teacher;
                    Console.WriteLine(t.Sub);
                }
            }

            for (int i = 0; i < professions.Length; i++)
            {
                Teacher t = professions[i] as Teacher;
                if (t!=null)
                {
                    Console.WriteLine(t.Sub);
                }
            }
            //prof1.Greetings();
            //prof1.Print();

            hiddenTeacher.Greetings(); 
            hiddenTeacher.Print();


            //teach1.Greetings();
            //teach1.Print();
        }
    }

    public abstract class Profession
    {
        protected string _name;
        protected int _salary;

        public Profession(string name, int salary)
        {
            _name = name;
            _salary = salary;
        }
        
        public virtual void Greetings()
        {
            Console.WriteLine("I don't know my profession");
        }

        public void Print()
        {
            Console.WriteLine($"Profession: {_name} {_salary}");
        }

        //public override abstract void Bye();
    }
    public class Teacher : Profession
    {
        private string _sub;
        private bool _hasClass;


        public string Sub => _sub;
        public Teacher(string name, int salary, string sub) : base(name, salary)
        {
            _salary *= 2;
            _sub = sub;
            _hasClass = false;
        }

        public Teacher(string name, int salary, string sub, bool hasClass) : this(name, salary, sub)
        {
            _hasClass = hasClass;
        }

        public override void Greetings()
        {
            Console.WriteLine("I know my profession - Teacher!");
        }
    }
}
