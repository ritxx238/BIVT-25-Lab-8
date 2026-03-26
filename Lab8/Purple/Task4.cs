namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
            }

            public void Run(double time)
            {
                if (_time == 0)
                    _time = time;
            }

            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Surname: {_surname}, Time: {_time}");
            }

            public static void Sort(Sportsman[] array)
            {
                Array.Sort(array, (a, b) => a.Time.CompareTo(b.Time));
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
                _sportsmen = group.Sportsmen.ToArray();
            }

            public void Add(Sportsman sportsman)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                for (int i = 0; i < sportsmen.Length; i++)
                {
                    Add(sportsmen[i]);
                }
            }

            public void Add(Group group)
            {
                Add(group.Sportsmen);
            }

            public void Sort()
            {
                Sportsman[] sportsmen = new Sportsman[_sportsmen.Length];
                sportsmen = _sportsmen.OrderBy(x => x.Time).ToArray();
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    _sportsmen[i] = sportsmen[i];
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group ans = new Group("Финалисты");
                ans.Add(group1);
                ans.Add(group2);
                ans.Sort();
                return ans;
            }
            public void Print()
            {
                foreach(var i in _sportsmen)
                {
                    i.Print();
                }
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                int male = 0;
                int female = 0;
                foreach (var sportik in _sportsmen)
                {
                    if (sportik is SkiMan)
                    {
                        male++;
                    }
                    else
                    {
                        female++;
                    }
                }
                men = new Sportsman[male];
                women = new Sportsman[female];
                var mIndex = 0;
                var wIndex = 0;
                foreach (var sportik in _sportsmen)
                {
                    if (sportik is SkiMan)
                    {
                        men[mIndex] = sportik;
                        mIndex++;
                    }
                    else
                    {
                        women[wIndex] = sportik;
                        wIndex++;
                    }
                }
            }

            public void Shuffle()
            {
                Sportsman[] men;
                Sportsman[] women;
                Split(out men, out women);
                men = men.OrderBy(x => x.Time).ToArray();
                women = women.OrderBy(x => x.Time).ToArray();
                if (men[0].Time <= women[0].Time)
                {
                    int im = 0, iw = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (i % 2 == 0 && im < men.Length)
                            _sportsmen[i] = men[im++];
                        else if (iw < women.Length)
                            _sportsmen[i] = women[iw++];
                        else
                            _sportsmen[i] = men[im++];
                    }
                }
                else
                {
                    int im = 0, iw = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (i % 2 == 0 && iw < women.Length)
                            _sportsmen[i] = women[iw++];
                        else if (im < men.Length)
                            _sportsmen[i] = men[im++];
                        else
                            _sportsmen[i] = women[iw++];
                    }
                }
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
        
    }
}
