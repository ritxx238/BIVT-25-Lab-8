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
                if (_time != 0.0)
                    return;
                _time = time;
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_time}");
            }

            public static void Sort(Sportsman[] array)
            {
                Array.Sort(array, (x, y) => x.Time.CompareTo(y.Time));
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
            private Sportsman[] _sportsmens;
            
            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmens;
            
            public Group(string name)
            {
                _name = name;
                _sportsmens = [];
            }
            
            public Group(Group other)
            {
                _name = other._name;
                _sportsmens = new Sportsman[other._sportsmens.Length];
                Array.Copy(other._sportsmens, _sportsmens, other._sportsmens.Length);
            }

            public void Add(Sportsman sportsman) //для одного
            {
                Array.Resize(ref _sportsmens, _sportsmens.Length + 1);
                _sportsmens[_sportsmens.Length - 1] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman); //добавление одного участника
                }
            }

            public void Add(Group group)
            {
                foreach (var sportsmen in group.Sportsmen)
                {
                    Add(sportsmen);
                }
            }

            public void Sort()
            {
                Array.Sort(_sportsmens, (x, y) => x.Time.CompareTo(y.Time));
            }
            
            public static Group Merge(Group group1, Group group2)
            {
                Group group = new Group("Финалисты");
                group.Add(group1);
                group.Add(group2);
                group.Sort();
                return group;
            }
            
            public void Print()
            {
                Console.WriteLine($"{Name}, {string.Join(", ", _sportsmens.Select(x => x.Surname))}");
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = Array.Empty<Sportsman>();
                women = Array.Empty<Sportsman>();

                foreach (Sportsman sportsman in _sportsmens)
                {
                    if (sportsman is SkiMan)
                    {
                        Array.Resize(ref men, men.Length + 1);
                        men[men.Length - 1] = sportsman;
                    }
                    else if (sportsman is SkiWoman)
                    {
                        Array.Resize(ref women, women.Length + 1);
                        women[women.Length - 1] = sportsman;
                    }
                }
            }

            public void Shuffle()
            {
                Sort();
                Sportsman[] shuffled = new Sportsman[_sportsmens.Length];
                Split(out Sportsman[] men, out Sportsman[] women);
                
                int menIndex = 0;
                int womenIndex = 0;
                int currentIndex = 0;

                bool isMenNext = _sportsmens[0] is SkiMan; //кто идет следующим
                
                
                while (menIndex < men.Length && womenIndex < women.Length) //раскидали по men и women основную часть до конца одного из массивов
                {
                    if (isMenNext)
                    {
                        shuffled[currentIndex++] = men[menIndex++];
                        isMenNext = false;
                    }
                    else
                    {
                        shuffled[currentIndex++] = women[womenIndex++];
                        isMenNext = true;
                    }
                }

                while (menIndex < men.Length)
                {
                    shuffled[currentIndex++] = men[menIndex++];
                }
                //раскидываем остатки от конца того массива до конца вообще
                while (womenIndex < women.Length)
                {
                    shuffled[currentIndex++] = women[womenIndex++];
                }
                
                _sportsmens = shuffled;
            }
        }
    }
}