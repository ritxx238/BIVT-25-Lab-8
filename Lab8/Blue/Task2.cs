using static Lab8.Blue.Task2;

namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks => (int[,])_marks.Clone();

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    foreach (int mark in _marks)
                    {
                        sum += mark;
                    }
                    return sum;
                }
            }




            public Participant(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
                _marks = new int[2, 5];


            }
            public void Jump(int[] result)
            {
                Print();
                int t1 = 0;
                Console.WriteLine(_marks.GetLength(1));
                for (int i = 0; i < _marks.GetLength(1); i++)
                {
                    t1 += _marks[0, i];
                }
                if (t1 == 0)
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        _marks[0, i] = result[i];
                    }
                }
                else
                {
                    int t2 = 0;
                    for (int i = 0; i < _marks.GetLength(1); i++)
                    {
                        t2 += _marks[1, i];
                    }
                    if (t2 == 0)
                    {
                        for (int i = 0; i < result.Length; i++)
                        {
                            _marks[1, i] = result[i];
                        }
                    }
                }

            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant buf = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = buf;
                        }
                    }

                }
            }
            public void Print()
            {
                string markss = "";
                foreach (int i in _marks)
                {

                    markss += $"{i.ToString()}, ";

                }
                Console.WriteLine($"Name: {Name} \n Surname: {Surname} \n Marks: {markss}");
            }


        }
        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

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
            public abstract double[] Prize {  get; }
            
            protected WaterJump(string Name,  int Bank)
            {
                _name = Name;
                _bank = Bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length-1] = participant;
            }
            public void Add(Participant[] participants)
            { 
                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }

        }
        public class WaterJump3m : WaterJump
        {
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                    {
                        return [];
                    }
                    else
                    {
                        return [Bank * 0.5, Bank * 0.3, Bank * 0.2];
                    }
                }
            }
            public WaterJump3m(string name, int bank) : base(name,bank) {}
        }
        public class WaterJump5m : WaterJump
        {
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                    {
                        return [];
                    }
                    else
                    {
                        int half = (Participants.Length - 1) / 2;
                        if (half > 10)
                        {
                            half = 10;
                        }
                        if (half < 3)
                        {
                            half = 3;
                        }

                        double[] prizes = new double[half + 1];

                        for (int i = 0; i <= half; i++)
                        {
                            prizes[i] = Bank * (20 / (Participants.Length / 2)) / 100;
                        }

                        prizes[0] += Bank * 0.4;
                        prizes[1] += Bank * 0.25;
                        prizes[2] += Bank * 0.15;
                        return prizes;

                    }
                }
            }
                public WaterJump5m(string name, int bank) : base(name,bank) {}
            }
        }
    }
