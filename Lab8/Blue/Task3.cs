namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penalty;
            
            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    int[] copy = new int[_penalty.Length];
                    Array.Copy(_penalty, 0, copy, 0, copy.Length);
                    return copy;
                }
            }
            public int Total => _penalty.Sum();

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalty = [];
            }

            public virtual bool IsExpelled
            {
                get {
                    bool isExpelled = true;
                    if (_penalty == null || _penalty.Length == 0)
                        return !isExpelled;

                    foreach (var time in _penalty)
                    {
                        if (time >= 10)
                            return isExpelled;
                    }

                    return !isExpelled;
                }
            }

            public virtual void PlayMatch(int time)
            {
                if (time < 0)
                    return;
                
                Array.Resize(ref _penalty, _penalty.Length + 1);
                _penalty[^1] = time;
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }

            public void Print()
            {
                return;
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override void PlayMatch(int fouls)
            {
                if (fouls is <= 0 or > 5)
                    return;
                Array.Resize(ref _penalty, _penalty.Length + 1);
                _penalty[^1] = fouls;
            }

            public override bool IsExpelled
            {
                get
                {
                    if ((double)Total > _penalty.Length * 2)
                        return true;

                    int countFiveFouls = 0;
                    for (int i = 0; i < _penalty.Length; i++)
                    {
                        if (_penalty[i] == 5)
                            countFiveFouls++;
                    }

                    // > 10% игр с 5 фолами
                    if ((double)countFiveFouls > 0.1 * _penalty.Length)
                        return true;
                    
                    return false;
                }
            }
        }
        public class HockeyPlayer : Participant
        {
            private static int _players;
            private static int _totalTime;

            static HockeyPlayer()
            {
                _players = 0;
                _totalTime = 0;
            }
            public HockeyPlayer(string name, string surname) : base(name, surname) { _players++; }

            public override void PlayMatch(int time)
            {
                if (time < 0)
                    return;
                
                Array.Resize(ref _penalty, _penalty.Length + 1);
                _penalty[^1] = time;
                
                _totalTime += time;
            }

            public override bool IsExpelled
            {
                get
                {
                    foreach (var time in _penalty)
                    {
                        if (time >= 10)
                            return true;
                    }
                    if (Total > (double)_totalTime * 0.1 / _penalty.Length)
                        return true;
                    
                    return false;
                }
            }
        }
    }
}