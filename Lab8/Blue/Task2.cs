namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int count;
            
            public string Name => _name;
            public string Surname => _surname;

            public int[,] Marks
            {
                get
                {
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    int total = 0;
                    foreach(int mark in _marks) total += mark;
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                count = 0;
            }

            public void Jump(int[] result)
            {
                if (count < 2 && result.Length == 5)
                {
                    for (int k = 0; k < 5; k++) _marks[count, k] = result[k];
                    count++;
                }
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{Name},{Surname},{TotalScore}");
            }
        }
        
        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;
            
            public string  Name => _name;
            public int Bank => _bank;
            public Participant[] Participants
            {
                get
                {
                    Participant[] copy = new Participant[_participants.Length];
                    Array.Copy(_participants, copy, _participants.Length);
                    return copy;
                }
            }

            public abstract double[] Prize { get; }
            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }
            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
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
            public WaterJump3m(string name, int bank) : base(name, bank) { }
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return new double[0];
                    
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
                    if (Participants.Length < 3) return new double[0];
                    
                    int half = Participants.Length / 2;
                    
                    double [] prizes = new double[half];
                    
                    if (half >= 1) prizes[0] = Bank * 0.4;
                    if (half >= 2) prizes[1] = Bank * 0.25;
                    if (half >= 3) prizes[2] = Bank * 0.15;
                    if (half > 0)
                    {
                        double ost = Bank * 20 / half / 100;
                        for (int i = 0; i < half; i++) prizes[i] += ost;
                    }
                    return prizes;
                }
            }
        }
    }
}
