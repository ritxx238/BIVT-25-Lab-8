using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Classwork_05._03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car(4, 4, "Tesla");
            //car1.Info();
            //car1.Print();
            Bus bus1 = new Bus(6, 24, "Burbusimos bus", 4000, true);
            //bus1.Info();
            //bus1.Print();
            Car hiddenCar = new Car(3, 2, "Perpetomobil");
            hiddenCar.Info();
            hiddenCar.Print();

            Car[] cars = new Car[] {car1, bus1, hiddenCar};
            for (int i = 0; i < cars.Length; i++)
            {
                if (cars[i] is  Bus)
                {
                    Bus c = cars[i] as Bus;
                    Console.WriteLine(c.Height);
                }
            }
        }
    }

    public class Car
    {
        protected int _numOfWheels;
        protected int _numOfPeople;
        protected string _model;

        public int NumOfWeels => _numOfWheels;
        public int NumOfPeople => _numOfPeople;
        public string Model => _model;



        public Car(int numOfWheels, int numOfPeople, string model)
        {
            _numOfWheels = numOfWheels;
            _numOfPeople = numOfPeople;
            _model = model;
        }

        public virtual void Info()
        {
            Console.WriteLine("Not information");
        }

        public void Print()
        {
            Console.WriteLine($"Количествор колес:{_numOfWheels}, количество пассажиров: {_numOfPeople}, название: {_model}");
        }

    }
    public class Bus : Car // через запятую прописать интерфейсы
    {

        private int _height;
        private bool _hasSignStop;

        public int Height => _height;

        public Bus(int numOfWheels, int numOfPeople, string model, int height) : base(numOfWheels, numOfPeople, model)
        {
            _height = height;
        }
        public Bus(int numOfWheels, int numOfPeople, string model, int height, bool hasSignStop) : this(numOfWheels, numOfPeople, model, height)
        {
            hasSignStop = _hasSignStop;
        }

        public override void Info() // Переопределение
        {
            Console.WriteLine("I am a car!");
        }

        public new void Print() // Сокрытие
        {
            Console.WriteLine($"Количествор колес:{_numOfWheels}, количество пассажиров: {_numOfPeople}, название: {_model}");
        }
    }
}