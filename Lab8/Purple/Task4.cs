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
            }
            public void Run(double time)
            {
                _time = time;
            }
            public static void Sort(Sportsman[] array)
            {
                Array.Sort(array, (a, b) => a.Time.CompareTo(b.Time));
            } 
            public void Print()
            {
                Console.WriteLine($"name: {_name} surname: {_surname} time: {_time}");
            }

        }

        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname)
            {
            }

            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }
        public class SkiWoman : Sportsman
        { 
            public SkiWoman(string name, string surname) : base(name, surname)
            {
            }

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
                Array.Copy(group._sportsmen, _sportsmen, _sportsmen.Length);

            }
            public void Add(Sportsman sportsman)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }
            public void Add(Sportsman[] sportsman)
            {
                int k = 0;
                int n = _sportsmen.Length;
                if (sportsman == null || sportsman.Length == 0) return;
                Array.Resize(ref _sportsmen, _sportsmen.Length + sportsman.Length);
                for (int i = n; i < _sportsmen.Length; i++)
                {
                    _sportsmen[i] = sportsman[k++];
                }
            }
            public void Add(Group group)
            {
                if (group.Sportsmen == null || group.Sportsmen.Length == 0) return;
                Add(group.Sportsmen);
            }
            public void Sort()
            {
                int n = _sportsmen.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (_sportsmen[j].Time > _sportsmen[j + 1].Time)
                            (_sportsmen[j], _sportsmen[j + 1]) = (_sportsmen[j + 1], _sportsmen[j]);
                    }
                }
            }
            public static Group Merge(Group group1, Group group2)
            {

                Group finalyst = new Group("Финалисты");
                finalyst.Add(group1);
                finalyst.Add(group2);
                finalyst.Sort();
                return finalyst;
            }
            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = new SkiMan[0];
                women = new SkiWoman[0];
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
                if (_sportsmen.Length < 2 || _sportsmen == null) return;
                Split(out var men, out var women);
                Sportsman.Sort(men);
                Sportsman.Sort(women);
                string now = "w";
                if (men[0].Time < women[0].Time) now = "m";
                int iman = 0;
                int iwoman = 0;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (now == "m" && iman< men.Length)
                    {
                        _sportsmen[i] = men[iman++];
                        now = "w";
                    }
                    else if (now == "w" && iwoman < women.Length)
                    {
                        _sportsmen[i] = women[iwoman++];
                        now = "m";
                    }
                    else if (iman < men.Length)
                    {
                        _sportsmen[i] = men[iman++];
                    }
                    else if (iwoman < women.Length)
                    {
                        _sportsmen[i] = women[iwoman++];
                    }


                }
            }
            public void Print()
            {
                Console.WriteLine($"title: {_name}");
                foreach (Sportsman s in _sportsmen)
                    s.Print();
            }
        }
    }
}
