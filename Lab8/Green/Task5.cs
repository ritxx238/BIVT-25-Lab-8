namespace Lab8.Green
{
    public class Task5
    {
        public struct Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private int _examCount;

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                _examCount = 0;
            }
            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return new int[0];
                    int[] copy = new int[_marks.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _marks[i];
                    }
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
                    return sum / 5;
                }
            }

            public void Exam(int mark)
            {
                if (_examCount >= 5) return;
                if (_marks == null) _marks = new int[5];
                _marks[_examCount] = mark;
                _examCount++;
            }

            public void Print()
            {
                Console.WriteLine($"  Студент: {Name} {Surname}, оценки: {string.Join(", ", Marks)}, средний балл: {AverageMark:F2}");
            }
        }
        public class Group
        {
            private string _name;
            protected Student[] _students;

            public Group(string name)
            {
                _name = name;
                _students = new Student[0];
            }

            public string Name => _name;
            public Student[] Students
            {
                get
                {
                    if (_students == null) return new Student[0];
                    Student[] copy = new Student[_students.Length];
                    for (int i = 0; i < _students.Length; i++)
                    {
                        copy[i] = _students[i];
                    }
                    return copy;
                }
            }

            public virtual double AverageMark
            {
                get
                {
                    if (_students == null || _students.Length == 0) return 0;
                    double sum = 0;
                    for (int i = 0; i < _students.Length; i++)
                    {
                        sum += _students[i].AverageMark;
                    }
                    return sum / _students.Length;
                }
            }

            public void Add(Student student)
            {
                Array.Resize(ref _students, _students.Length + 1);
                _students[_students.Length - 1] = student;
            }

            public void Add(Student[] students)
            {
                foreach (Student s in students)
                {
                    Add(s);
                }
            }

            public static void SortByAverageMark(Group[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
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
                Console.WriteLine($"Группа: {Name}, средний балл группы: {AverageMark:F2}");
                Console.WriteLine("Список студентов:");
                for (int i = 0; i < Students.Length; i++)
                {
                    Console.Write("  ");
                    Students[i].Print();
                }
            }
        }

        public class EliteGroup : Group
        {
            public EliteGroup(string name) : base(name) { }

            public override double AverageMark
            {
                get
                {
                    if (_students == null || _students.Length == 0) return 0;
                    double ves = 0;
                    double ocenkaves = 0;
                    foreach (var s in _students)
                    {
                        int[] marks = s.Marks;
                        foreach (int mark in marks)
                        {
                            if (mark >= 2 && mark <= 5)
                            {
                                double w;
                                switch (mark)
                                {
                                    case 5: w = 1.0; break;
                                    case 4: w = 1.5; break;
                                    case 3: w = 2.0; break;
                                    case 2: w = 2.5; break;
                                    default: w = 0.0; break;
                                }
                                ocenkaves += mark * w;
                                ves += w;
                            }
                        }
                    }
                    return ves == 0 ? 0 : ocenkaves / ves;
                }
            }
        }

        public class SpecialGroup : Group
        {
            public SpecialGroup (string name) : base(name) { }

            public override double AverageMark
            {

                get
                {
                    if (_students == null || _students.Length == 0) return 0;
                    double ves = 0;
                    double ocenkaves = 0;
                    foreach (var s in _students)
                    {
                        int[] marks = s.Marks;
                        foreach (int mark in marks)
                        {
                            if (mark >= 2 && mark <= 5)
                            {
                                double w;
                                switch (mark)
                                {
                                    case 5: w = 1.0; break;
                                    case 4: w = 0.75; break;
                                    case 3: w = 0.5; break;
                                    case 2: w = 0.25; break;
                                    default: w = 0.0; break;
                                }
                                ocenkaves += mark * w;
                                ves += w;
                            }
                        }
                    }
                    return ves == 0 ? 0 : ocenkaves / ves;
                }
            }
        }
    }
}


