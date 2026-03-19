using System.Runtime.Serialization.Formatters;

namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penalties;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    int[] copy = new int[_penalties.Length];
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        copy[i] = _penalties[i];
                    }
                    return copy;
                }
            }
            public int Total
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        sum += _penalties[i];
                    }
                    return sum;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        if (_penalties[i] == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalties = new int[0];
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length - i; j++)
                    {
                        if (array[j - 1].Total > array[j].Total)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }

            public virtual void PlayMatch(int time)
            {
                //if (time != 0 && time != 2 && time != 5 && time != 10)
                //{
                //    return;
                //}
                Array.Resize(ref _penalties, _penalties.Length + 1);
                _penalties[_penalties.Length - 1] = time;
            }

            public void Print()
            {
                return;
            }
        }

        public class BasketballPlayer : Participant
        {
            public override bool IsExpelled
            {
                get
                {
                    double count = 0;
                    double sum = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        if (_penalties[i] == 5)
                        {
                            count += 1;
                            sum += _penalties[i];
                        }
                    }
                    if (count / _penalties.Length > 0.1)
                    {
                        return true;
                    }
                    if (sum / _penalties.Length >= 2)
                    {
                        return true;
                    }
                    return false;
                }
            }

            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override void PlayMatch(int fall)
            {
                if (fall < 0 || fall > 5)
                {
                    return;
                }
                Array.Resize(ref _penalties, _penalties.Length + 1);
                _penalties[_penalties.Length - 1] = fall;
            }
        }
        public class HockeyPlayer : Participant
        {
            private static int _players;
            private static int _totalTime;

            public override bool IsExpelled 
            {
                get
                {
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        if (_penalties[i] == 10)
                        {
                            return true;
                        }
                    }
                    int sum = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        sum += _penalties[i];
                    }
                    if (sum > 0.1 * _totalTime/_players)
                    {
                        return true;
                    }

                    return false; 
                }
            }

            static HockeyPlayer()
            {
                _players = 0;
                _totalTime = 0;
            }

            public HockeyPlayer(string name, string surname) : base(name, surname) 
            { 
                _players++; 
            }

            public override void PlayMatch(int time)
            {
                Array.Resize(ref _penalties, _penalties.Length + 1);
                _penalties[_penalties.Length - 1] = time;
                _totalTime += time;
            }
        }
    }
}
