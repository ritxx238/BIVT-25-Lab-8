using System;

namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool _flag;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
                _flag = false;
            }

            public void Run(double time)
            {
                if (!_flag)
                {
                    _time = time;
                    _flag = true;
                }
            }

            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length - 1; i++)
                    for (int j = 0; j < array.Length - i - 1; j++)
                        if (array[j].Time > array[j + 1].Time)
                        {
                            Sportsman tmp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = tmp;
                        }
            }

            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine(Surname);
                Console.WriteLine(Time);
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
                _sportsmen = new Sportsman[group.Sportsmen.Length];
                Array.Copy(group.Sportsmen, _sportsmen, group.Sportsmen.Length);
            }

            public void Add(Sportsman sportsman)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                for (int i = 0; i < sportsmen.Length; i++)
                    Add(sportsmen[i]);
            }

            public void Add(Group group)
            {
                Add(group.Sportsmen);
            }

            public void Sort()
            {
                Sportsman.Sort(_sportsmen);
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group result = new Group("Финалисты");
                result.Add(group1);
                result.Add(group2);
                result.Sort();
                return result;
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = new Sportsman[0];
                women = new Sportsman[0];

                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiMan)
                    {
                        Array.Resize(ref men, men.Length + 1);
                        men[men.Length - 1] = _sportsmen[i];
                    }
                    else if (_sportsmen[i] is SkiWoman)
                    {
                        Array.Resize(ref women, women.Length + 1);
                        women[women.Length - 1] = _sportsmen[i];
                    }
                }
            }

            public void Shuffle()
            {
                Sort();

                bool womenFirst = _sportsmen.Length > 0 && _sportsmen[0] is SkiWoman;

                Sportsman[] men;
                Sportsman[] women;
                Split(out men, out women);

                int pos = 0, mi = 0, wi = 0;
                while (mi < men.Length || wi < women.Length)
                {
                    if (womenFirst)
                    {
                        if (wi < women.Length) _sportsmen[pos++] = women[wi++];
                        if (mi < men.Length) _sportsmen[pos++] = men[mi++];
                    }
                    else
                    {
                        if (mi < men.Length) _sportsmen[pos++] = men[mi++];
                        if (wi < women.Length) _sportsmen[pos++] = women[wi++];
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine(Name);
                for (int i = 0; i < _sportsmen.Length; i++)
                    _sportsmen[i].Print();
            }
        }
    }
}
