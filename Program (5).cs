using System.Security.Cryptography;
using System.Xml.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Animal animal = new Animal("Animal", 10);
            Cat cat = new Cat("Cat", 5, "black");
            Animal hiddenCat = new Cat("Kitty", 3, "white");      // в обратную сторону работать не будет так как кот является животным, но не каждое животное это кот
            animal.Print();
            animal.Voice();

            cat.Print();
            cat.Voice();

            hiddenCat.Print();
            hiddenCat.Voice();

            Cat cat2 = hiddenCat as Cat;        // вернет null если приведение невозможно
        }
    }

    public class Animal
    {
        private string _name;
        protected int _age;
        
        public string Name => _name;
        public int Age => _age;

        public Animal(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public virtual void Voice()     // virtual нужно чтобы можно было менять метод в наследниках
        {
            Console.WriteLine("?");
        }

        public virtual void Print()
        {
            Console.WriteLine(_name);
            Console.WriteLine(_age);
        }
    }

    public class Cat : Animal
    {
        private string _color;

        public string Color => _color;

        public Cat(string name, int age, string color) : base(name, age)
        {
            _age += 2;
            _color = color;
        }

        public override void Voice()
        {
            Console.WriteLine("meow =3");
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine(_color);
        }

        //public new void Print()      // сокрытие метода (new нужен чтобы vs понимал что ты намеренно сокрыл метод) 
        //{
        //    Console.WriteLine(_color);
        //}
    }
}
