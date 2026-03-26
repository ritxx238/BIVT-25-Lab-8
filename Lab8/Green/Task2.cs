using System.Collections;

namespace Lab8.Green
{
    public class Task2
    {
        public class Human
        {
            protected string _name;
            protected string _surname;
            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }
            public string Name => _name;
            public string Surname => _surname;

            public virtual void Print()
            {
                Console.WriteLine($"Name: {_name}");
                Console.WriteLine($"Surname: {_surname}");
            }
        }
        public class Student : Human
        {
            private int _count;
            private int[] _marks;
            private int _examCount;
            private static int _excelAmount;

            public Student(string name, string surname) : base(name, surname) 
            {
                _marks = new int[4];
                _examCount = 0;
            }
            public int[] Marks => _marks.ToArray();
            public static int ExcellentAmount => _excelAmount;



            public double AverageMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0)
                        return 0;

                    double sum = 0;

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] > 0)
                        {
                            sum += _marks[i];

                        }
                    }
                    double AvMark = sum / _marks.Length;

                    return AvMark;
                }
            }


            public bool IsExcellent
            {
                get
                {
                    if (_marks == null || _marks.Length == 0)
                        return false;

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] < 4)
                            return false;
                    }



                    return true;
                }
            }



            public void Exam(int mark)
            {

                if (mark < 2 || mark > 5)
                {
                    return;
                }

                if (_examCount >= 4)
                {
                    return;
                }
                bool wasExcellent = IsExcellent;
                _marks[_examCount] = mark;
                _examCount++;
                if(!wasExcellent && IsExcellent &&  _examCount >= 4)
                {
                    _excelAmount++;
                }
            }

            public static void SortByAverageMark(Student[] array)
            {
                if (array == null || array.Length == 0)
                    return;


                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].AverageMark < array[j + 1].AverageMark)
                        {

                            Student temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name:{_name}");
                Console.WriteLine($"Surename: {_surname}");
                Console.WriteLine($"AverageMark: {AverageMark}");
                Console.WriteLine($"Excellent: {IsExcellent}");
            }
        }
    }
}
