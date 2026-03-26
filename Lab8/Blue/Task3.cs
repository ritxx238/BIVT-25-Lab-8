namespace Lab8.Blue;

public class Task3
{
    public class Participant
    {
        private string _name;
        private string _surname;
        protected int[] _penaltyTimes;
        private bool _isExpelled;

        public string Name => _name;
        public string Surname => _surname;
        virtual public bool IsExpelled => _isExpelled;

        public int[] Penalties => (int[]) _penaltyTimes.Clone() ;

        public int Total =>  _penaltyTimes.Sum();

        public Participant(string name, string surname)
        {
            _name = name;
            _surname = surname;
            _penaltyTimes = [];
            _isExpelled = false;
        }

        virtual public void PlayMatch(int time)
        {
            if (time == -1) return;
            if (time == 10) _isExpelled = true;

            int[] newTimes = new int[_penaltyTimes.Length + 1];
            Array.Copy(_penaltyTimes, newTimes, _penaltyTimes.Length);
            newTimes[_penaltyTimes.Length] = time;
            _penaltyTimes = newTimes;


        }

        public static void Sort(Participant[] array) =>
            Array.Sort(array, (a, b) => a.Total.CompareTo(b.Total));
        

        public void Print() { return; }
    }

    public class BasketballPlayer : Participant
    {
        public BasketballPlayer(string Name, string Surname) : base(Name, Surname) { }

        public override bool IsExpelled
        {
            get
            {
                if (Penalties.Length == 0) return false;
                double c = _penaltyTimes.Count(x => x == 5);
                double games = Penalties.Length;

                if (c / games > .1 || Total > 2 * games) return true;

                return false;

            }
        }
        public override void PlayMatch(int fouls)
        {
            if (fouls > 5 || fouls < 0) return;
            base.PlayMatch(fouls);
        }


    }
    
    public class HockeyPlayer : Participant
    {
        private static int _players = 0;
        private static int _totalTime = 0;

        private int Players => _players;
        private int TotalTime => _totalTime;

        public HockeyPlayer(string name, string surname) : base(name, surname)
        {
            _players++;

        }

        public override bool IsExpelled =>
            (_penaltyTimes.Any(x => x == 10) || Total > (TotalTime * .1 / Players));

    
        public override void PlayMatch(int time)
        {

            if (time < 0) return;
            base.PlayMatch(time);
            _totalTime += time;

        }

    }

}
