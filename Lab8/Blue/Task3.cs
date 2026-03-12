using System;

namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltyTimes;

            public Participant(string name, string surname)
            {
                if (name != null)
                    _name = name;
                if (surname != null)
                    _surname = surname;
                _penaltyTimes = new int[0];
            }

            public string Name => _name;
            public string Surname => _surname;

            public virtual int[] Penalties
            {
                get
                {
                    int[] copy = new int[_penaltyTimes.Length];
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                        copy[i] = _penaltyTimes[i];
                    return copy;
                }
            }

            public virtual int Total
            {
                get
                {
                    if (_penaltyTimes == null) return 0;
                    int total = 0;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                        total += _penaltyTimes[i];
                    return total;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                        if (_penaltyTimes[i] == 10) return true;
                    return false;
                }
            }

            public virtual void PlayMatch(int time)
            {
                int[] newPenaltyTimes = new int[_penaltyTimes.Length + 1];
                for (int i = 0; i < _penaltyTimes.Length; i++)
                    newPenaltyTimes[i] = _penaltyTimes[i];
                newPenaltyTimes[_penaltyTimes.Length] = time;
                _penaltyTimes = newPenaltyTimes;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public virtual void Print()
            {
                return;
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;

                    int matchesWith5Fouls = 0;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                        if (_penaltyTimes[i] == 5) matchesWith5Fouls++;

                    if (matchesWith5Fouls > _penaltyTimes.Length * 0.1) return true;
                    if (Total > _penaltyTimes.Length * 2) return true;

                    return false;
                }
            }

            public override void PlayMatch(int time)
            {
                if (time < 0 || time > 5) return; // условие чек
                int[] newPenaltyTimes = new int[_penaltyTimes.Length + 1]; // можно заменить base.PlayMatch(time)
                for (int i = 0; i < _penaltyTimes.Length; i++)
                    newPenaltyTimes[i] = _penaltyTimes[i];
                newPenaltyTimes[_penaltyTimes.Length] = time;
                _penaltyTimes = newPenaltyTimes;
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _totalTime = 0;
            private static int _players = 0;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                        if (_penaltyTimes[i] >= 10) return true;

                    double avgTotalTime = (double)_totalTime / _players;
                    if (Total > avgTotalTime * 0.1) return true;

                    return false;
                }
            }

            public override void PlayMatch(int time)
            {
                int[] newPenaltyTimes = new int[_penaltyTimes.Length + 1]; // можно заменить base.PlayMatch(time)
                for (int i = 0; i < _penaltyTimes.Length; i++)
                    newPenaltyTimes[i] = _penaltyTimes[i];
                newPenaltyTimes[_penaltyTimes.Length] = time;
                _penaltyTimes = newPenaltyTimes;
                _totalTime += time;
            }

            internal static void ResetStaticFields()
            {
                _totalTime = 0;
                _players = 0;
            }
        }
    }
}