using System.ComponentModel.Design;

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

            public virtual int[] Penalties
            {
                get
                {
                    int[] copy = new int[_penalty.Length];
                    Array.Copy(_penalty, copy, _penalty.Length);
                    return copy;
                }
            }

            public virtual int Total
            {
                get
                {
                    int ans = 0;
                    foreach (int val in _penalty) ans += val;
                    return ans;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    foreach (int val in _penalty)
                    {
                        if (val == 10) return true;
                    }
                    return false;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalty = new int[0];
            }
            public virtual void PlayMatch(int time)
            {
                Array.Resize(ref _penalty, _penalty.Length + 1);
                _penalty[_penalty.Length - 1] = time;
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
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
                return;
            }

        }

        public class BasketballPlayer : Participant
        {
            
            public BasketballPlayer(string name, string surname)  : base(name, surname) { }

            public override bool IsExpelled
            {
                get
                {
                    if (_penalty.Length == 0) return false;

                    int m5 = 0;
                    for (int i = 0; i < _penalty.Length; i++) if (_penalty[i] == 5) m5++;
                    
                    if (m5 > _penalty.Length * 0.1) return true;
                    if (Total > 2 * _penalty.Length) return true;
                    return false;
                }
            }

            public override void PlayMatch(int time)
            {
                if (time < 0 || time > 5) return;
                Array.Resize(ref _penalty, _penalty.Length + 1);
                _penalty[_penalty.Length - 1] = time;
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _players = 0;
            private static int _totalTime = 0;
            public HockeyPlayer(string name, string surname)  : base(name, surname) { _players++; }
            
            public override bool IsExpelled
            {
                get
                {
                    if (_penalty == null) return false;

                    for (int i = 0; i < _penalty.Length; i++)
                        if (_penalty[i] >= 10) return true;

                    double avg = (double)_totalTime / _players;
                    if (Total > avg * 0.1) return true;

                    return false;
                }
            }

            public override void PlayMatch(int time)
            {
                Array.Resize(ref _penalty, _penalty.Length + 1);
                _penalty[_penalty.Length - 1] = time;
            }
        }
    }
}
