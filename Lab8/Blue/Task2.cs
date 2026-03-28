namespace Lab8.Blue;
public class Task2
{
    public struct Participant
    {
        private string _name;
        private string _surname;
        private int[,] _marks;
        private int _jumpCount;

        public string Name => _name;
        public string Surname => _surname;

        public int[,] Marks => (int[,])_marks.Clone() ; 

        public int TotalScore => _marks.Cast<int>().Sum();
        

        public Participant(string name, string surname)
        {
            _name = name;
            _surname = surname;
            _marks = new int[2, 5];
            _jumpCount = 0;
        }

        public void Jump(int[] result)
        {
            if (result == null || _marks == null || _jumpCount >= 2) return;
            for (int j = 0; j < result.Length; j++)
                _marks[_jumpCount, j] = result[j];
            _jumpCount++;
        }

        public static void Sort(Participant[] array) =>
            Array.Sort(array, (a, b) => b.TotalScore.CompareTo(a.TotalScore));

        public void Print() {}
    }


    public abstract class WaterJump
    {
        private string _name;
        private Participant[] _participants;
        private int _bank;

        public string Name => _name;
        public Participant[] Participants => (Participant[])_participants.Clone();
        public int Bank => _bank;

        public abstract double[] Prize { get; }

        protected WaterJump(string Name, int Bank)
        {
            _bank = Bank;
            _name = Name;
            _participants = [];
        }

        public void Add(Participant participant)
        {
            Participant[] array = new Participant[_participants.Length + 1];
            _participants.CopyTo(array, 0);
            array[_participants.Length] = participant;
            _participants = array;
        }

        public void Add(Participant[] participants)
        {
            if (participants == null) return;
            Participant[] array = new Participant[_participants.Length + participants.Length];
            _participants.CopyTo(array, 0);
            participants.CopyTo(array, _participants.Length);
            _participants = array;
        }
    }


    public class WaterJump3m : WaterJump
    {
        public WaterJump3m(string Name, int Bank) : base(Name, Bank) { }

        public override double[] Prize
        {
            get
            {
                if (Participants.Length < 3) return [];
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
                if (Participants.Length < 3) return [];

                int m = Participants.Length / 2;
                if (m < 3) m = 3;
                if (m > 10) m = 10;

                double n = 20.0 / m;
                double[] participants = new double[m];

                participants[0] = (40.0 + n) / 100.0 * Bank;
                participants[1] = (25.0 + n) / 100.0 * Bank;
                participants[2] = (15.0 + n) / 100.0 * Bank;

                for (int i = 3; i < m; i++)
                {
                    participants[i] = n / 100.0 * Bank;
                }
                return participants;
            }
        }
    }

}
