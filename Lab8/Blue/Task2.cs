namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            // Поля
            private string _name;
            private string _surname;
            private int[,] _marks;
            // Свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    int[,] copy = new int[2, 5];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            copy[i, j] = _marks[i, j];
                        }
                    }
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            sum += _marks[i, j];
                        }
                    }
                    return sum;
                }
            }
            // Конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        _marks[i, j] = 0;
                    }
                }
            }
            // Метод
            public void Jump(int[] result)
            {
                int k1 = 0; // 1 прыжок
                for (int i = 0; i < 5; i++)
                {
                    if (_marks[0, i] != 0)
                    {
                        k1++;
                        break;
                    }
                }
                if (k1 == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _marks[0, i] = result[i];
                    }
                    return;// выходим из метода
                }
                int k2 = 0; // второй прыжок
                for (int i = 0; i < 5; i++)
                {
                    if (_marks[1, i] != 0)
                    {
                        k2++;
                        break;
                    }
                }
                if (k2 == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _marks[1, i] = result[i];
                    }
                    return;
                }
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].TotalScore < array[j].TotalScore)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
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
            // Поля
            private string _name;
            private int _bank;
            private Participant[] _participants;
            // Свойства
            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants
            {
                get
                {
                    Participant[] copy = new Participant[_participants.Length];
                    for (int i = 0; i < _participants.Length; i++)
                    {
                        copy[i] = _participants[i];
                    }
                    return copy;
                }
            }
            public abstract double[] Prize
            {
                get;
            }
            // Конструктор
            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }
            // Методы
            public void Add(Participant participant)
            {
                Array.Resize(ref _participants,_participants.Length+1);
                _participants[^1] = participant;
            }
            public void Add(Participant[] participant)
            {
                for (int i = 0; i<participant.Length; i++)
                {
                    Add(participant[i]);
                }
            }
        }
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank)
            {
            }
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                        return null;
                    double[] prizes = new double[3];
                    prizes[0] = 0.5 * Bank;
                    prizes[1] = 0.3 * Bank;
                    prizes[2] = 0.2 * Bank;
                    return prizes;
                }
            }
        }
        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank)
            {
            }
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                        return null;

                    int k = 0;
                    for (int i = 0; i<Participants.Length;i++)
                    {
                        k++;
                    }

                    int half = k / 2;
                    if (half < 3)
                        half = 3;
                    if (half > 10)
                        half = 10;

                    double N = 20.0 / half;
                    double[] prizes = new double[half];
                    double prizesMidle = Bank * (N / 100.0);
                    for (int i = 0;i < half;i++)
                    {
                        prizes[i] = prizesMidle;
                    }
                    prizes[0] += 0.4 * Bank;
                    prizes[1] += 0.25 * Bank;
                    prizes[2] += 0.15 * Bank;
                    return prizes;
                }
            }
        }
    }
}
