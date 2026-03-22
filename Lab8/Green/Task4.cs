namespace Lab8.Green
{
    public class Task4
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _jumps;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Jumps => _jumps.ToArray();

            public double BestJump
            {
                get { return _jumps.Max(); }
            }

            public void SetJumps(int index, double jamp)
            {
                _jumps[index] = jamp;
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _jumps = new double[3];
            }

            public void Jump(double result)
            {
                for (int i = 0; i < _jumps.Length; i++)
                {
                    if (_jumps[i] == 0)
                    {
                        _jumps[i] = result;
                        break;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                int n = array.Length;

                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (array[j].BestJump < array[j + 1].BestJump)
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
                Console.WriteLine("Лучший результат:" + BestJump);
            }
        }

        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;
            
            public string Name => _name;
            public Participant[] Participants => _participants;

            protected Discipline(string name)
            {
                _name = name;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }
            
            public void Add(Participant[] participant)
            {
                int oldLength = _participants.Length;
                Array.Resize(ref _participants, _participants.Length + participant.Length);
                for (int i = 0; i < _participants.Length; i++)
                {
                    _participants[oldLength + i] = participant[i];
                }
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }

            public abstract void Retry(int index);

            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine(Participants);
            }
        }

        public class LongJump : Discipline
        {
            public LongJump() : base("Long jump")
            {
            }

            public override void Retry(int index)
            {
                double r = Participants[index].BestJump;
                Participants[index].SetJumps(0, r);
                Participants[index].SetJumps(1, 0.0);
                Participants[index].SetJumps(2, 0.0);
            }
        }

        public class HighJump : Discipline
        {
            public HighJump() : base("High Jump")
            {
            }

            public override void Retry(int index)
            {
                Participants[index].SetJumps(2, 0.0);
            }
        }
    }
}
