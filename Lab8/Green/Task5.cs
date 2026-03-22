namespace Lab8.Green
{
    public class Task5
    {
        public struct Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks => _marks.ToArray();
            public double AverageMark => _marks.Average();

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
            }

            public void Exam(int mark)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        break;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine("Фамилия:" + Surname);
                Console.WriteLine("Имя:" + Name);
                Console.WriteLine("Средняя оценка:" + AverageMark);
            }
        }

        public class Group
        {
            private string _name;
            private Student[] _student;

            public string Name => _name;
            public Student[] Students => _student.ToArray();

            public virtual double AverageMark
            {
                get
                {
                    double sum = 0;

                    for (int i = 0; i < Students.Length; i++)
                    {
                        sum += _student[i].AverageMark;
                    }

                    if (_student.Length == 0)
                    {
                        return 0;
                    }

                    return sum / Students.Length;
                }
            }

            public Group(string goup)
            {
                _name = goup;
                _student = new Student[0];
            }

            public void Add(Student student)
            {
                Array.Resize(ref _student, _student.Length + 1);
                _student[_student.Length - 1] = student;
            }

            public void Add(Student[] students)
            {
                int oldLength = _student.Length;
                Array.Resize(ref _student, _student.Length + students.Length);
                for (int i = 0; i < _student.Length; i++)
                {
                    _student[oldLength + i] = students[i];
                }
            }

            public static void SortByAverageMark(Group[] array)
            {
                int n = array.Length;

                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1 - i; j++)
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
                Console.WriteLine("Имя:" + Name);
                Console.WriteLine("Средняя оценка:" + AverageMark);
            }
        }

        public class EliteGroup : Group
        {
            public EliteGroup(string goup) : base(goup)
            {
            }

            public override double AverageMark
            {
                get
                {
                    //5 - 1
                    //4 - 1.5
                    //3 - 2
                    //2 - 2.5

                    double sumMarks = 0;
                    double sumCoaf = 0;

                    for (int i = 0; i < Students.Length; i++)
                    {
                        if (Students[i].Marks.Length == 0) continue;
                        for (int j = 0; j < Students[i].Marks.Length; j++)
                        {
                            if (Students[i].Marks[j] == 5)
                            {
                                sumMarks += Students[i].Marks[j] * 1.0;
                                sumCoaf += 1.0;
                            }

                            if (Students[i].Marks[j] == 4)
                            {
                                sumMarks += Students[i].Marks[j] * 1.5;
                                sumCoaf += 1.5;
                            }

                            if (Students[i].Marks[j] == 3)
                            {
                                sumMarks += Students[i].Marks[j] * 2.0;
                                sumCoaf += 2.0;
                            }

                            if (Students[i].Marks[j] == 2)
                            {
                                sumMarks += Students[i].Marks[j] * 2.5;
                                sumCoaf += 2.5;
                            }
                        }
                    }

                    return sumMarks / sumCoaf;
                }
            }
        }

        public class SpecialGroup : Group
        {
            public SpecialGroup(string goup) : base(goup)
            {
            }

            public override double AverageMark
            {
                get
                {
                    //5 - 1
                    //4 - 0.75
                    //4 - 0.5
                    //2 - 0.25

                    double sumMarks = 0;
                    double sumCoaf = 0;

                    for (int i = 0; i < Students.Length; i++)
                    {
                        for (int j = 0; j < Students[i].Marks.Length; j++)
                        {
                            if (Students[i].Marks[j] == 5)
                            {
                                sumMarks += Students[i].Marks[j] * 1.0;
                                sumCoaf += 1.0;
                            }

                            if (Students[i].Marks[j] == 4)
                            {
                                sumMarks += Students[i].Marks[j] * 0.75;
                                sumCoaf += 0.75;
                            }

                            if (Students[i].Marks[j] == 3)
                            {
                                sumMarks += Students[i].Marks[j] * 0.5;
                                sumCoaf += 0.5;
                            }

                            if (Students[i].Marks[j] == 2)
                            {
                                sumMarks += Students[i].Marks[j] * 0.25;
                                sumCoaf += 0.25;
                            }
                        }
                    }

                    return sumMarks / sumCoaf;
                }
            }
        }
    }
}
