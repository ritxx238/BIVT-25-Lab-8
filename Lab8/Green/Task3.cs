namespace Lab8.Green
{
    public class Task3
    {
        public class Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private bool _isExpelled;
            private int _id;

            private static int _nextId;

            static Student()
            {
                _nextId = 1;
            }

            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks
            {
                get
                {
                    int[] copy = new int[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
                        copy[i] = _marks[i];
                    return copy;
                }
            }
            public bool IsExpelled => _isExpelled;
            public int ID => _id;

            public double AverageMark
            {
                get
                {
                    double sum = 0;
                    int count = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] > 0)
                        {
                            sum += _marks[i];
                            count++;
                        }
                    }

                    if (count == 0)
                    {
                        return 0;
                    }

                    return sum / count;
                }
            }

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _isExpelled = false;
                _id = _nextId++;
            }

            public void Exam(int mark)
            {
                if (_isExpelled)
                {
                    return;
                }

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;

                        if (mark == 2)
                        {
                            _isExpelled = true;
                        }

                        return;
                    }
                }
            }

            public void Restore()
            {
                _isExpelled = false;
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
                return;
            }
        }

        public class Commission
        {
            public static void Sort(Student[] students)
            {
                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int j = 0; j < students.Length - 1 - i; j++)
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
                int expelledCount = 0;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled)
                    {
                        expelledCount++;
                    }
                }


                Student[] expelled = new Student[expelledCount];
                Student[] active = new Student[students.Length - expelledCount];

                int expelledIndex = 0;
                int activeIndex = 0;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled)
                    {
                        expelled[expelledIndex++] = students[i];
                    }
                    else
                    {
                        active[activeIndex++] = students[i];
                    }

                }
                students = active;
                return expelled;
            }

            public static void Restore(ref Student[] students, Student restored)
            {
                if (!restored.IsExpelled)
                {
                    return;
                }

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].ID == restored.ID)
                    {
                        return;
                    }
                }

                restored.Restore();

                Student[] newArray = new Student[students.Length + 1];

                int insertIndex = 0;

                while (insertIndex < students.Length && students[insertIndex].ID < restored.ID)
                {
                    newArray[insertIndex] = students[insertIndex];
                    insertIndex++;
                }

                newArray[insertIndex] = restored;

                for (int i = insertIndex; i < students.Length; i++)
                {
                    newArray[i + 1] = students[i];
                }

                students = newArray;
            }
        }
    }
}
