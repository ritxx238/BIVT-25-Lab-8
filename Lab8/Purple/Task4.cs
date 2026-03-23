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
                _time = time;
            }

            public void Print()
            { }

            public static void Sort(Sportsman[] array)
            {

                Sportsman[] sortArray = array.OrderBy(x => x.Time).ToArray();
                for (int i = 0; i < sortArray.Length; i++)
                {
                    array[i] = sortArray[i];
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

        public class Group
        {
            private string _groupName;
            private Sportsman[] _sportsmen;

            public string Name => _groupName;
            public Sportsman[] Sportsmen => _sportsmen;
            public Group(string groupName)
            {
                _groupName = groupName;
                _sportsmen = new Sportsman[0];
            }

            public Group(Group group)
            {
                _groupName = group.Name;
                _sportsmen = new Sportsman[group._sportsmen.Length];
                Array.Copy(group._sportsmen, _sportsmen, group.Sportsmen.Length);
            }

            public void Add(Sportsman sportik)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[^1] = sportik;
            }

            public void Add(Sportsman[] sportiks)
            {
                foreach (var sportik in sportiks)
                    Add(sportik);
            }

            public void Add(Group group)
            {
                var curGroupArray = group._sportsmen;
                Add(curGroupArray);
            }

            public void Sort()
            {
                var sortedArray = _sportsmen.OrderBy(x => x.Time).ToArray();
                for (var i = 0; i < sortedArray.Length; i++)
                    _sportsmen[i] = sortedArray[i];
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group merge = new Group("Финалисты");
                merge.Add(group1);
                merge.Add(group2);
                merge.Sort();
                return merge;
            }

            public void Print() { }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = new Sportsman[0];
                women = new Sportsman[0];


                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiWoman woman)
                    {
                        Array.Resize(ref women, women.Length + 1);
                        women[^1] = woman;
                    }
                    else if (_sportsmen[i] is SkiMan man)
                    {
                        Array.Resize(ref men, men.Length + 1);
                        men[^1] = man;
                    }
                }
            }

            public void Shuffle()
            {
                Sportsman[] men;
                Sportsman[] women;

                Split(out men, out women);

                Sportsman.Sort(men);
                Sportsman.Sort(women);

                int index = 0;
                if (men[0].Time > women[0].Time)
                {
                    for (int i = 0; i < Math.Min(men.Length, women.Length); i++)
                    {
                        _sportsmen[index++] = women[i];
                        _sportsmen[index++] = men[i];
                    }

                    if (women.Length > men.Length)
                    {
                        for (int i = Math.Min(men.Length, women.Length); i < women.Length; i++)
                        {
                            _sportsmen[index++] = women[i];
                        }
                    }
                    else
                    {
                        for (int i = Math.Min(men.Length, women.Length); i < men.Length; i++)
                        {
                            _sportsmen[index++] = men[i];
                        }
                    }
                }

                else
                {
                    for (int i = 0; i < Math.Min(men.Length, women.Length); i++)
                    {
                        _sportsmen[index++] = men[i];
                        _sportsmen[index++] = women[i];
                    }

                    if (women.Length > men.Length)
                    {
                        for (int i = Math.Min(men.Length, women.Length); i < women.Length; i++)
                        {
                            _sportsmen[index++] = women[i];
                        }
                    }
                    else
                    {
                        for (int i = Math.Min(men.Length, women.Length); i < men.Length; i++)
                        {
                            _sportsmen[index++] = men[i];
                        }
                    }
                }

            }
        }

    }
}
