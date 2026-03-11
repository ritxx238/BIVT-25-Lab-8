namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            
            private bool _FirstJump;
            private bool _SecondJump;
            
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, 0, copy, 0, _marks.Length);
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    int total = 0;
                    foreach (int value in _marks)
                        total += value;

                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _FirstJump = false;
                _SecondJump = false;
            }

            public void Jump(int[] result)
            {
                if (result.Length != 5 || result == null || (_FirstJump && _SecondJump))
                    return;
                if (!_FirstJump)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                        _marks[0, j] = result[j];
                    _FirstJump = true;
                }

                else if (!_SecondJump)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                        _marks[1, j] = result[j];
                    _SecondJump = true;
                }
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }

            public void Print()
            {
                return;
            }
        }

        public abstract class WaterJump
        {
            private readonly string _name;
            private readonly int _bank;
            private Participant[] _participants;
            
            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants.ToArray();
            public abstract double[] Prize { get; }

            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = [];
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }

            public void Add(Participant[] participants)
            {
                if (participants.Length == 0 || participants == null)
                    return;
                
                int oldSize = _participants.Length;
                Array.Resize(ref _participants, oldSize + participants.Length);

                for (int i = 0; i < participants.Length; i++)
                    _participants[oldSize + i] = participants[i];
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                        return null;

                    return [Bank * 0.5, Bank * 0.3, Bank * 0.2];
                }
            }
        }
        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                        return null;

                    int count = Participants.Length / 2;
                    count = Math.Max(count, 3);
                    count = Math.Min(count, 10);

                    double[] prize = new double[count];

                    double extra = Bank * (20.0 / count) / 100;

                    prize[0] = Bank * 0.40 + extra;
                    prize[1] = Bank * 0.25 + extra;
                    prize[2] = Bank * 0.15 + extra;

                    for (int i = 3; i < count; i++)
                        prize[i] = extra;

                    return prize;
                }
            }
        }
    }
}