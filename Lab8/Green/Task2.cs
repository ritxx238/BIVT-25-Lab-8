namespace Lab8.Green
{
    public class Task2
    {
        public class Human
        {
            private string _name;
            private string _surname;
            
            public string Name => _name;
            public string Surname => _surname;
            
            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }
            
            public void Print()
            {
                Console.WriteLine("Фамилия:" + Surname);
                Console.WriteLine("Имя:" + Name);
            }
            
        }
        
        public class Student : Human
                {
                    private int[] _marks;
                    private static int _excell;

                    public int[] Marks => _marks.ToArray();
        
                    public double AverageMark => _marks.Average();

                    public static int ExcellentAmount
                    {
                        get
                        {
                            return _excell;
                        }
                    }
        
                    public bool IsExcellent
                    {
                        get
                        {
                            foreach (int mark in _marks)
                            {
                                if (mark < 4) return false;
                            }
                            return true;
                        }
                    }
        
                    public Student(string name, string surname) : base(name, surname)
                    {
                        _marks = new int[4];
                    }
        
                    public void Exam(int mark)
                    {
                        bool f = false;
                        for (int i = 0; i < _marks.Length; i++)
                        {
                            if (_marks[i] == 0)
                            {
                                _marks[i] = mark;
                                if (i == _marks.Length - 1)
                                {
                                    f =  true;
                                }
                                break;
                            }
                        }

                        if (f && IsExcellent)
                        {
                            _excell++;
                        }
                    }
        
                    public static void SortByAverageMark(Student[] array)
                    {
                        int n = array.Length;
        
                        for (int i = 0; i < n - 1; i++)
                        {
                            for (int j = 0; j < n - i - 1; j++)
                            {
                                if (array[j].AverageMark < array[j + 1].AverageMark)
                                {
                                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                                }
                            }
                        }
                    }
        
                    public void Print()
                    {
                        Console.WriteLine("Фамилия:" + Surname);
                        Console.WriteLine("Имя:" + Name);
                        Console.WriteLine("Оценки:" + Marks);
                        Console.WriteLine("Средняя оценка:" + AverageMark);
                        Console.WriteLine("Хороший результат:" + IsExcellent);
                    }
        
                }
    }
}
