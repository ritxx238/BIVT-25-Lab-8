using System;
using System.Linq;

namespace Lab8.Purple
{
    public class Task1
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumpCount;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Coefs => (double[])_coefs.Clone();

            public int[,] Marks => (int[,])_marks.Clone();
            

            public double TotalScore
            {
                get
                {
                    double total = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        int min = int.MaxValue;
                        int max = int.MinValue;
                        int sum = 0;

                        for (int j = 0; j < 7; j++)
                        {
                            int val = _marks[i, j];
                            sum += val;
                            if (val < min) min = val;
                            if (val > max) max = val;
                        }
                        total += (sum - min - max) * _coefs[i];
                    }
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                _jumpCount = 0;
            }

            public void SetCriterias(double[] coefs)
            {
                for (int i = 0; i < Math.Min(4, coefs.Length); i++)
                {
                    _coefs[i] = coefs[i];
                }
            }

            public void Jump(int[] marks)
            {
                for (int i = 0; i < Math.Min(7, marks.Length); i++)
                {
                    _marks[_jumpCount, i] = marks[i];
                }
                _jumpCount++;
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 1; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;
                    while (j >= 0 && array[j].TotalScore < key.TotalScore)
                    {
                        array[j + 1] = array[j];
                        j--;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {TotalScore:F2}");
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _markIndex;

            public string Name => _name;

            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = marks;
                _markIndex = 0;
            }

            public int CreateMark()
            {
                int mark = _marks[_markIndex];
                _markIndex = (_markIndex + 1) % _marks.Length;
                return mark;
            }

            public void Print()
            {
                Console.WriteLine($"Judge: {_name}");
            }
        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges => _judges.ToArray();
            public Participant[] Participants => _participants.ToArray();

            public Competition(Judge[] judges)
            {
                _judges = judges;
                _participants = new Participant[0];
            }

            public void Evaluate(Participant jumper)
            {
                int[] marks = new int[7];
                for (int i = 0; i < 7; i++)
                {
                    marks[i] = _judges[i].CreateMark();
                }
                jumper.Jump(marks);
            }

            public void Add(Participant jumper)
            {
                int currentCount = _participants?.Length ?? 0;
                Array.Resize(ref _participants, currentCount + 1);
                _participants[currentCount] = jumper;
                
                Evaluate(jumper);
            }

            public void Add(Participant[] jumpers)
            {
                foreach (var jumper in jumpers)
                {
                    Add(jumper);
                }
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
    }
}