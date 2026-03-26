using System;
using System.Linq;

namespace Lab8.Green
{
    public class Task3
    {
        public class Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private bool _expelled;
            private int _id;

            private static int _nextid;

            static Student()
            {
                _nextid = 1;
            }

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _expelled = false;
                _id = _nextid++;
            }

            public int ID => _id;
            public string Name => _name ?? string.Empty;
            public string Surname => _surname ?? string.Empty;
            public bool IsExpelled => _expelled;
            public int[] Marks => _marks?.ToArray() ?? new int[0];

            public double AverageMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;

                    int validMarksCount = 0;
                    double sum = 0;

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] > 0)
                        {
                            sum += _marks[i];
                            validMarksCount++;
                        }
                    }

                    if (validMarksCount == 0) return 0;
                    return sum / validMarksCount;
                }
            }

            public void Exam(int mark)
            {
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        if (mark > 2)
                        {
                            _marks[i] = mark;
                        }
                        else
                        {
                            _expelled = true;
                            _marks[i] = mark;
                        }
                        return;
                    }
                }
            }

            public void Restore()
            {
                _expelled = false;
            }

            public static void SortByAverageMark(Student[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
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
                Console.WriteLine($"Name: {_name}");
                Console.WriteLine($"Surname: {_surname}");
                Console.WriteLine($"AverageMark: {AverageMark:F2}");
                Console.WriteLine($"Expelled: {_expelled}");
            }
        }

        public class Commission
        {
            public static void Sort(Student[] students)
            {
                if (students == null || students.Length == 0) return;

                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int j = 0; j < students.Length - 1 - i; j++)
                    {
                        if (students[j] != null && students[j + 1] != null &&
                            students[j].ID > students[j + 1].ID)
                        {
                            Student temp = students[j];
                            students[j] = students[j + 1];
                            students[j + 1] = temp;
                        }
                    }
                }
            }

            public static Student[] Expel(ref Student[] students)
            {
                if (students == null) return new Student[0];

                int expelledCount = 0;
                int activeCount = 0;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] != null)
                    {
                        if (students[i].IsExpelled)
                            expelledCount++;
                        else
                            activeCount++;
                    }
                }

                Student[] expelledStudents = new Student[expelledCount];
                Student[] activeStudents = new Student[activeCount];

                int expelledIndex = 0;
                int activeIndex = 0;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] != null)
                    {
                        if (students[i].IsExpelled)
                        {
                            expelledStudents[expelledIndex++] = students[i];
                        }
                        else
                        {
                            activeStudents[activeIndex++] = students[i];
                        }
                    }
                }

                students = activeStudents;
                return expelledStudents;
            }

            public static void Restore(ref Student[] students, Student restored)
            {
                if (students == null || restored == null) return;

                if (!restored.IsExpelled) return;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] != null && students[i].ID == restored.ID) return;
                }

                restored.Restore();

                Student[] newArray = new Student[students.Length + 1];

                for (int i = 0; i < students.Length; i++)
                {
                    newArray[i] = students[i];
                }

                newArray[students.Length] = restored;

                students = newArray;

                Sort(students);
            }
        }
    }
}
