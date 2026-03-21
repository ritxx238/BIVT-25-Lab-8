using System.Diagnostics.Contracts;

namespace Lab8.Green
{
    public class Task3
    {
        public class Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private int _id;
            private bool _expalled = false;
            static int counter = 0;

            public string Name => _name;
            public int ID => _id;
            public string Surname => _surname;
            public int[] Marks => _marks.ToArray();
            public bool IsExpelled => _expalled;
            public double AverageMark
            {
                get
                {
                    int n = 0;
                    double sum = 0;

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] > 0)
                        {
                            sum += _marks[i];
                            n++;
                        }
                    }

                    if (n == 0)
                    {
                        return 0;
                    }

                    return sum / n;
                }
            }

            static Student ()
            {
                counter = 1;
            }


            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _id = counter;
                counter++;
            }

            public void Restore()
            {
                _expalled = false;
            }

            public void Exam(int mark)
            {
                if (_expalled)
                {
                    return;
                }

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        if (mark > 2)
                        {
                            _marks[i] = mark;
                            break;
                        }
                        if (mark == 2)
                        {
                            _marks[i] = mark;
                            _expalled = true;
                            break;
                        }
                    }
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
                Console.WriteLine("Хороший результат:" + IsExpelled);
            }
        }

        public class Commission
        {
            public static void Sort(Student[] students)
            {
                int n = students.Length;

                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (students[j].ID > students[j + 1].ID)
                        {
                            (students[j], students[j + 1]) = (students[j + 1], students[j]);
                        }
                    }
                }
            }
            public static Student[] Expel(ref Student[] students)
            {
                int c = 0;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled == true)
                    {
                        c++;
                    }
                }

                int countexpelled = c;
                int countnon = students.Length - c;

                Student[] expelled = new Student[c];
                Student[] nonexp = new Student[countnon];

                int k = 0;
                int h = 0;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled == true)
                    {
                        expelled[k]= students[i];
                        k++;
                    }
                    else
                    {
                        nonexp[h] = students[i];
                        h++;
                    }
                }

                students = nonexp;

                return expelled.ToArray();
            }

            public static void Restore(ref Student[] students, Student restored)
            {
                bool flag = false;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] == restored)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    Array.Resize(ref students, students.Length + 1);
                    students[students.Length - 1] = restored;
                }
                
                Sort(students);
            }
        }

    }
}
