using System;

namespace Lab8.Blue
{
    public class Task2
    {
       
        public struct Participant
        {
            private string _Name;
            private string _Surname;
            private int[,] _Marks;
            private int _jumpCount;

            public string Name => _Name;
            public string Surname => _Surname;

            public int[,] Marks
            {
                get
                {
                    if (_Marks == null) return null;
                    int rows = _Marks.GetLength(0);
                    int cols = _Marks.GetLength(1);
                    int[,] copy = new int[rows, cols];
                    for (int i = 0; i < rows; i++)
                        for (int j = 0; j < cols; j++)
                            copy[i, j] = _Marks[i, j];
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_Marks == null) return 0;
                    int score = 0;
                    foreach (int el in _Marks) score += el;
                    return score;
                }
            }

            public Participant(string name, string surname)
            {
                _Name = name;
                _Surname = surname;
                _Marks = new int[2, 5];
                _jumpCount = 0;
            }

            public void Jump(int[] result)
            {
                if (result == null || _Marks == null || _jumpCount >= 2) return;
                for (int j = 0; j < result.Length; j++)
                    _Marks[_jumpCount, j] = result[j];
                _jumpCount++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                Array.Sort(array, (a, b) => b.TotalScore.CompareTo(a.TotalScore));
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Баллы: {TotalScore}");
            }
        }

 
        public abstract class WaterJump
        {
            private string _Name;
            private Participant[] _Participants; 
            private int _Bank;

            public string Name => _Name;
            public Participant[] Participants => _Participants;
            public int Bank => _Bank;

            public abstract double[] Prize { get; }

            protected WaterJump(string Name, int Bank)
            {
                _Bank = Bank;
                _Name = Name;
                _Participants = new Participant[0];
            }

            public void Add(Participant p)
            {
                Participant[] ar = new Participant[_Participants.Length + 1];
                _Participants.CopyTo(ar, 0);
                ar[_Participants.Length] = p;
                _Participants = ar;
            }

            public void Add(Participant[] ps)
            {
                if (ps == null) return;
                Participant[] ar = new Participant[_Participants.Length + ps.Length];
                _Participants.CopyTo(ar, 0);
                ps.CopyTo(ar, _Participants.Length);
                _Participants = ar;
            }
        }

   
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string Name, int Bank) : base(Name, Bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return null; 
                    return [Bank * 0.5, Bank * 0.3, Bank * 0.2];
                }
            }
        }

       
        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string Name, int Bank) : base(Name, Bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return null;

                    int m = Participants.Length / 2;
                    if (m < 3) m = 3;
                    if (m > 10) m = 10;
                    if (m > Participants.Length) m = Participants.Length;

                    double n = 20.0 / m;
                    double[] pr = new double[m];

                    
                    pr[0] = (40.0 + n) / 100.0 * Bank;
                    pr[1] = (25.0 + n) / 100.0 * Bank;
                    pr[2] = (15.0 + n) / 100.0 * Bank;

                    for (int i = 3; i < m; i++)
                    {
                        pr[i] = n / 100.0 * Bank;
                    }
                    return pr;
                }
            }
        }
    }
}