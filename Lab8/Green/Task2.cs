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

            public virtual void Print()
            {
                Console.WriteLine($"Имя: {Name}, фамилия: {Surname}");
            }
        }

        public class Student : Human
        {
            
            private int[] _marks;
            private int _examCount;
            private bool _isExcellentCounted;
            private static int _excellentCount;
            public static int ExcellentAmount => _excellentCount;

            public Student(string name, string surname): base(name, surname)
            {
                _marks = new int[4];
                _examCount = 0;
                _isExcellentCounted = false;
            }

            
            public int[] Marks
            {
                get
                {
                    if (_marks == null)
                        return new int[0];

                    int[] copy = new int[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
                        copy[i] = _marks[i];
                    return copy;
                }
            }

            public double AverageMark
            {
                get
                {
                    if (_marks == null) return 0;

                    double sum = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        sum += _marks[i];
                    }
                    return sum / 4;
                }
            }

            public bool IsExcellent
            {
                get
                {
                    if (_marks == null) return false;

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        int mark = _marks[i];
                        if (mark < 4 || mark > 5) { return false; }

                    }
                    return true;
                }
            }

            public void Exam(int mark)
            {
                if (_examCount >= 4) { return; }
                if (_marks == null)
                    _marks = new int[4];
                _marks[_examCount] = mark;
                _examCount++;

                if (IsExcellent && !_isExcellentCounted)
                {
                    _excellentCount++;
                    _isExcellentCounted = true;
                }
            }

            public static void SortByAverageMark(Student[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
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
                base.Print();
                string marksStr = string.Join(", ", Marks);
                Console.WriteLine($"Студент: {Name} {Surname}, оценки: {marksStr}, средний балл: {AverageMark:F2}, отличник: {IsExcellent}");
            }
        }
    }
}
