using Lab8.Blue;

namespace Lab8
{
    public class Program
    {
        public static void Main()
        {
            // Classwork_5_03
            // var animal1 = new Classwork_5_03.Animal("bobka",Classwork_5_03.Breed.Cat ,10);
            var animal2 = new Classwork_5_03.Dog("bibka",2);
            
            Classwork_5_03.Animal hiddenAnimal = new Classwork_5_03.Dog("grizli",9);
            // Classwork_5_03.Dog animal3 = new Classwork_5_03.Animal("animla3",Classwork_5_03.Breed.Bear,10);
            //                                                                            <- так делать нельзя

            // animal1.Greeting();
            animal2.Greeting();
            
            // animal1.Print();
            animal2.Print();
            
            hiddenAnimal.Greeting();
            hiddenAnimal.Print();

            var animals = new Classwork_5_03.Animal[] {animal2,hiddenAnimal};
            for (int i = 0; i < animals.Length; i++)
            {
                var a = animals[i] as Classwork_5_03.Dog;
                if (animals[i] is Classwork_5_03.Dog)
                {
                    var animal = animals[i] as Classwork_5_03.Dog;
                    System.Console.WriteLine(animal.hasCastration);
                }
                if (a != null)
                {
                    System.Console.WriteLine("опаааа");
                }
            }
        }
    }
}