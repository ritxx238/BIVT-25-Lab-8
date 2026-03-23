using System;
using System.Linq;

namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool _hasTime;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0.0;
                _hasTime = false;
            }

            public void Run(double time)
            {
                if (_hasTime) return;
                _time = time;
                _hasTime = true;
            }

            public static void Sort(Sportsman[] array)
            {
                Array.Sort(array, (a, b) => a.Time.CompareTo(b.Time));
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_time:F4}");
            }
        }

        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) { }
            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) { }
            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }

            public Group(Group group)
            {
                _name = group.Name;
                _sportsmen = group.Sportsmen != null ? group.Sportsmen.ToArray() : new Sportsman[0];
            }

            public void Add(Sportsman elem)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = elem;
            }

            public void Add(Sportsman[] array)
            {
                foreach (var s in array) Add(s);
            }

            public void Add(Group group)
            {
                Add(group.Sportsmen);
            }

            public void Sort()
            {
                Sportsman.Sort(_sportsmen);
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = _sportsmen.Where(s => s is SkiMan).ToArray();
                women = _sportsmen.Where(s => s is SkiWoman).ToArray();
            }

            public void Shuffle()
            {
                Split(out var men, out var women);
                Sportsman.Sort(men);
                Sportsman.Sort(women);

                int total = _sportsmen.Length;
                var shuffled = new Sportsman[total];
                int mIdx = 0, wIdx = 0, k = 0;

                bool nextIsMan = true;
                if (men.Length > 0 && women.Length > 0)
                {
                    if (women[0].Time < men[0].Time) nextIsMan = false;
                }
                else if (women.Length > 0) nextIsMan = false;

                while (k < total)
                {
                    if (nextIsMan && mIdx < men.Length)
                    {
                        shuffled[k++] = men[mIdx++];
                        if (wIdx < women.Length) nextIsMan = false;
                    }
                    else if (!nextIsMan && wIdx < women.Length)
                    {
                        shuffled[k++] = women[wIdx++];
                        if (mIdx < men.Length) nextIsMan = true;
                    }
                    else
                    {
                        if (mIdx < men.Length) shuffled[k++] = men[mIdx++];
                        else if (wIdx < women.Length) shuffled[k++] = women[wIdx++];
                    }
                }
                _sportsmen = shuffled;
            }

            public static Group Merge(Group group1, Group group2)
            {
                var result = new Group("Финалисты");
                result.Add(group1);
                result.Add(group2);
                result.Sort();
                return result;
            }

            public void Print()
            {
                Console.WriteLine(_name);
                foreach (var s in _sportsmen) s.Print();
            }
        }
    }
}