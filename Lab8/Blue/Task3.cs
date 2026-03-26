namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            private string _Name;
            private string _Surname;
            protected int[] _PenaltyTimes;
            private bool _IsExpelled;

            public string Name => _Name;
            public string Surname => _Surname;
            virtual public bool IsExpelled => _IsExpelled;

            virtual public int[] Penalties
            {
                get
                {
                    if (_PenaltyTimes == null)
                        return null;
                    
                    int[] copy = new int[_PenaltyTimes.Length];
                    Array.Copy(_PenaltyTimes, copy, _PenaltyTimes.Length);
                    return copy;
                }
            }
            virtual public int Total
            {
                get
                {
                    if (_PenaltyTimes == null)return 0;
                    return _PenaltyTimes.Sum();;
                }
            }

            public Participant(string Name, string Surname)
            {
                _Name = Name; 
                _Surname = Surname;
                _PenaltyTimes = [];
                _IsExpelled = false;
            }

            virtual public void PlayMatch(int time)
            {
               if (time == -1)
                    return;
                if (time == 10)
                    _IsExpelled = true;

                int[] newTimes = new int[_PenaltyTimes.Length + 1];
                Array.Copy(_PenaltyTimes, newTimes, _PenaltyTimes.Length);
                newTimes[_PenaltyTimes.Length] = time;
                _PenaltyTimes = newTimes;
                
                
            }

            public static void Sort(Participant[] array)
            {
                if (array == null)
                    return;

                Array.Sort(array, (a, b) => a.Total.CompareTo(b.Total));
            }

            public void Print()
            {
                Console.WriteLine($"Name: {_Name}\nSurname: {_Surname}\nIsExpelled: {_IsExpelled}\nTotal: {Total}");
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string Name,string Surname) : base(Name, Surname){}

            public override bool IsExpelled
            {
                get
                {  
                    if(Penalties.Length==0)return false;
                    double c=_PenaltyTimes.Count(x=>x==5);
                    double games=Penalties.Length;
                    
                    if( c / games>.1 || Total > 2 * games)
                    {
                        return true;    
                    }
                    return false;
                    
                }
            }
            public override void PlayMatch(int fouls)
            {
                if(fouls>5||fouls<0)return;
                base.PlayMatch(fouls);
            }
            
            
        }
        public class HockeyPlayer : Participant
        {
            private static int _players=0;
            private static int _totalTime=0;

            private int Players=>_players;
            public override int Total => base.Total;
            private int TotalTime=>_totalTime;
            
            public HockeyPlayer(string Name,string Surname) : base(Name, Surname)
            {
                _players++;

            }

            public override bool IsExpelled
            {
                get
                {
                    if(Players<1)return false;
                    if(base.IsExpelled || Total>(TotalTime*.1/Players))return true;
                    return false;
                }
            }
            public override void PlayMatch(int time)
            {
                
                if(time<0)return;
                base.PlayMatch(time);
                _totalTime+=time;
                

            }
            
        }
    }

    
}
