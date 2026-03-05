namespace Lab8;

public class Classwork_5_03
{
    public enum Breed
    {
        Dog = 1,
        Cat = 2,
        Bear = 3,
    }
    public abstract class Animal
    {
        protected string _name;
        protected Breed _breed;
        protected int _age;

        public Animal (string name,Breed breed, int age)
        {
            _name = name;
            _breed = breed;
            _age = age;
        }
        public virtual void Greeting()
        {
            System.Console.WriteLine("Greeting_Animal");
            System.Console.WriteLine("Hello world!");
            System.Console.WriteLine();
        }
        public void Print()
        {
            System.Console.WriteLine("Print_Animal");
            System.Console.WriteLine($@"Name: {_name}; Age: {_age}; Breed: {_breed}");
            System.Console.WriteLine();
        }
        public abstract void Buy();
    }
    public class Dog : Animal
    {
        private bool _hasCastration;
        private int _countOfChildrn;
        public bool hasCastration => _hasCastration;
        public Dog(string name,int age) : base (name,Breed.Dog,age)
        {
        }
        public Dog(string name, int age, int countOfChildrn, bool hasCastration) 
                                : this (name,age)
        {
            _hasCastration = false;
            _countOfChildrn = countOfChildrn;
        }
        public void Print() // сокрытие метода
        {
            System.Console.WriteLine("Print_Dog");
            System.Console.WriteLine("_________________________DOG_________________");
            System.Console.WriteLine($@"Name: {_name}; Age: {_age}; Breed: {_breed}");
            System.Console.WriteLine($@"Count of Children: {_countOfChildrn}; Has castration: {_hasCastration}");
            System.Console.WriteLine();
        }
        public override void Greeting() // переопределение
        {
            System.Console.WriteLine("Greeting_Dog");
            System.Console.WriteLine("GAV GAV GGGAVAAAVV GAV gAV Gavg gavg gvav");
            System.Console.WriteLine();
        }
        // переопределение можно делать со свойствами и полями все будет одинаково рабботать как и с методами
        public override void Buy()
        {
            System.Console.WriteLine("ads");
        }
    }


}
