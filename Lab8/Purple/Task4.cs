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
                if (_time != 0) return;
                _time = time;
            }
            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Surname: {_surname}, Time: {_time}");
            }

            public static void Sort(Sportsman[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Time > array[j + 1].Time)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
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
                _name = group._name;
                _sportsmen = new Sportsman[group._sportsmen.Length];
                for (int i = 0; i < group._sportsmen.Length; i++)
                {
                    _sportsmen[i] = group._sportsmen[i];
                }
            }

            public void Add(Sportsman sportsman)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[^1] = sportsman;
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                for (int i = 0; i < sportsmen.Length; i++)
                {
                    Add(sportsmen[i]);
                }
            }
            public void Add(Group group)
            {
                if (group._sportsmen == null) return;
                Add(group._sportsmen);
            }
            public void Sort()
            {
                for (int i = 0; i < _sportsmen.Length - 1; i++)
                {
                    for (int j = 0; j < _sportsmen.Length - 1 - i; j++)
                    {
                        if (_sportsmen[j].Time > _sportsmen[j + 1].Time)
                        {
                            (_sportsmen[j], _sportsmen[j + 1]) = (_sportsmen[j + 1], _sportsmen[j]);
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group finalists = new Group("Финалисты");
                
                for (int i = 0; i < group1._sportsmen.Length; i++)
                {
                    finalists.Add(group1._sportsmen[i]);
                }
                for (int i = 0; i < group2._sportsmen.Length; i++)
                {
                    finalists.Add(group2._sportsmen[i]);
                }
                finalists.Sort();
                return finalists;
            }

            public void Print()
            {
                Console.WriteLine($"{_name}  {_sportsmen}");
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                int countMen = 0;
                int countWomen = 0;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiMan) countMen++;
                    else countWomen++;
                }

                men = new Sportsman[countMen];
                women = new Sportsman[countWomen];
                int k = 0;
                int j = 0;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiMan) men[k++] = _sportsmen[i];
                    else women[j++] = _sportsmen[i];
                }
            }

            public void Shuffle()
            {
                Split(out Sportsman[] men, out Sportsman[] women);
                Sportsman.Sort(men);
                Sportsman.Sort(women);
                Sportsman[] result = new Sportsman[_sportsmen.Length];
                bool isManFirst = false;
                if (men[0].Time < women[0].Time) isManFirst = true;
                int i = 0;
                int m = 0;
                int w = 0;
                while (m < men.Length && w < women.Length)
                {
                    if (isManFirst == true)
                    {
                        result[i++] = men[m++];
                        result[i++] = women[w++];
                    }
                    else
                    {
                        result[i++] = women[w++];
                        result[i++] = men[m++];
                    }
                }
                while (m < men.Length)
                {
                    result[i++] = men[m++];
                }
                while (w < women.Length)
                {
                    result[i++] = women[w++];
                }

                _sportsmen = result;
            }
        }
    }
}
