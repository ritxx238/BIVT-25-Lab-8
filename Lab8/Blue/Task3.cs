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
            public int[] Penalties => (int[])_penalty.Clone();
            public int Total
            {
                get
                {
                    int sum = 0;
                    foreach (int mark in _penalty)
                    {
                        sum += mark;
                    }
                    return sum;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    foreach (int mark in _penalty)
                    {

                        if (mark == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public Participant(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
                _penalty = new int[0];
            }
            public virtual void PlayMatch(int time)
            {
                //if (time == 0 || time == 2 || time == 5 || time == 10)
                //{
                    
                //}
                Array.Resize(ref _penalty, _penalty.Length + 1);
                _penalty[_penalty.Length - 1] = time;
            }
            public static void Sort(Participant[] array)
            {
                var sorted = array.OrderBy(p => p.Total).ToArray();

                Array.Copy(sorted, array, array.Length);
            }
            public void Print()
            {
                Console.WriteLine($"Name: {_name}\nSurname: {_surname}\n");
                foreach (int i in _penalty)
                {
                    Console.WriteLine(i);
                }
            }

        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string Name, string Surname) : base(Name, Surname)
            {
            }
            public override bool IsExpelled
            {
                get
                {
                    double count = 0;
                    double sum = 0;
                    for (int i = 0; i < _penalty.Length; i++)
                    {
                        if (_penalty[i] == 5)
                        {
                            count += 1;
                            sum += _penalty[i];
                        }
                    }
                    if (count / _penalty.Length > 0.1)
                    {
                        return true;
                    }
                    if (sum / _penalty.Length >= 2)
                    {
                        return true;
                    }
                    return false;
                }
            }
            public override void PlayMatch(int fall)
            {
                if (fall < 0 || fall > 5)
                {
                    return;
                }
                Array.Resize(ref _penalty, _penalty.Length + 1);
                _penalty[_penalty.Length - 1] = fall;
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
                    for (int i = 0; i < _penalty.Length; i++)
                    {
                        if (_penalty[i] == 10)
                        {
                            return true;
                        }
                    }
                    int sum = 0;
                    for (int i = 0; i < _penalty.Length; i++)
                    {
                        sum += _penalty[i];
                    }
                    if (sum > 0.1 * _totalTime / _players)
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
                Array.Resize(ref _penalty, _penalty.Length + 1);
                _penalty[_penalty.Length - 1] = time;
                _totalTime += time;
            }
        }
    }
}
