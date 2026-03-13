namespace Lab8.Blue
{
    public class Task2
    {

        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _balls;

            public Participant(string name, string surname)
            {
                if (name != null)
                {
                    _name = name;
                }
                if (surname != null)
                {
                    _surname = surname;
                }
                _balls = new int[2, 5];
            }

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    int[,] copy = new int[2, 5];
                    for (int i = 0; i < 2; i++)
                        for (int j = 0; j < 5; j++)
                            copy[i, j] = _balls[i, j];
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_balls == null) return 0;
                    int total = 0;
                    for (int i = 0; i < 2; i++)
                        for (int j = 0; j < 5; j++)
                            total += _balls[i, j];
                    return total;
                }
            }

            public void Jump(int[] result)
            {
                if (result == null || result.Length != 5) return;

                for (int i = 0; i < 2; i++)
                {
                    bool isEmpty = true;
                    for (int j = 0; j < 5; j++)
                    {
                        if (_balls[i, j] != 0)
                        {
                            isEmpty = false;
                            break;
                        }
                    }
                    if (isEmpty)
                    {
                        for (int j = 0; j < 5; j++)
                            _balls[i, j] = result[j];
                        break;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;
                for (int i = 0; i < array.Length - 1; i++)
                    for (int j = 0; j < array.Length - 1 - i; j++)
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {TotalScore} очков");
            }
        }


        public abstract class WaterJump
        {
            private string _name;
            //private int _count;
            private int _bank;
            private Participant[] _participants;


            public string Name => _name;
            public Participant[] Participants
            {
                get
                {
                    Participant[] copy = new Participant[_participants.Length];
                    for (int i = 0; i < _participants.Length; i++)
                        copy[i] = _participants[i];
                    return copy;
                }
            }
            public int Bank => _bank;


            protected WaterJump(string name, int bank)
            {
                if (name != null)
                {
                    _name = name;
                }
                else
                {
                    _name = "";
                }
                _bank = bank;
                _participants = new Participant[0];
            }



            public abstract double[] Prize { get; }

            public void Add(Participant participant)
            {
                Participant[] newArray = new Participant[_participants.Length + 1];

                for (int i = 0; i < _participants.Length; i++)
                    newArray[i] = _participants[i];

                newArray[_participants.Length] = participant;

                _participants = newArray;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null) return;

                for (int i = 0; i < participants.Length; i++)
                    Add(participants[i]);
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
                        return new double[0];

                    double[] prizes = new double[3];
                    prizes[0] = Bank * 0.5;
                    prizes[1] = Bank * 0.3;
                    prizes[2] = Bank * 0.2;
                    return prizes;
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
                        return new double[0];

                    int aboveMiddle = Participants.Length / 2;

                    double[] prizes = new double[aboveMiddle];

                    // Призовые для первых трех мест
                    if (aboveMiddle >= 1) prizes[0] = Bank * 0.4;  // 40% - 1 место = 400
                    if (aboveMiddle >= 2) prizes[1] = Bank * 0.25; // 25% - 2 место = 250
                    if (aboveMiddle >= 3) prizes[2] = Bank * 0.15; // 15% - 3 место = 150


                    if (aboveMiddle > 0)
                    {
                        double remainingPerPerson = Bank * (20.0 / aboveMiddle) / 100;

                        for (int i = 0; i < aboveMiddle; i++)
                        {
                            prizes[i] += remainingPerPerson;
                        }
                    }

                    return prizes;
                }
            }
        }
    }
}
