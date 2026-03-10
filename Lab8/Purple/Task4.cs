using System.Runtime.CompilerServices;

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
            int count = 0;
            public void Run(double time)
            {
                if (count == 0)
                {
                    _time = time;
                    count++;
                }
            }

            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j<array.Length; j++)
                    {
                        if (array[i].Time > array[j].Time)
                        {
                            (array[i], array[j]) = (array[j], array[i]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name}  {_surname} {_time}");
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
                string name = group.Name;
                _sportsmen = new Sportsman[group._sportsmen.Length];
                Array.Copy(group._sportsmen, _sportsmen, group._sportsmen.Length);
            }
            public void Add(Sportsman sportsman)
            {
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[^1] = sportsman;
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                int len = _sportsmen.Length;
                int count = 0;
                Array.Resize(ref _sportsmen, _sportsmen.Length + sportsmen.Length);
                for (int i = len; i < _sportsmen.Length; i++)
                {
                    _sportsmen[i] = sportsmen[count++];
                }
            }
            public void Add(Group group)
            {
                if (group.Sportsmen == null) return;
                int len = _sportsmen.Length;
                int count = 0;
                Array.Resize(ref _sportsmen, _sportsmen.Length + group._sportsmen.Length);
                for (int i = len; i < _sportsmen.Length; i++)
                {
                    _sportsmen[i] = group._sportsmen[count++];
                }
            }

            public void Sort()
            {
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    for (int j = i; j < _sportsmen.Length; j++)
                    {
                        if (_sportsmen[i].Time > _sportsmen[j].Time)
                        {
                            (_sportsmen[i], _sportsmen[j]) = (_sportsmen[j], _sportsmen[i]);
                        }
                    }
                }
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                int menLength = 0;
                int womenLength = 0;
                for (int i = 0; i<_sportsmen.Length; i++)
                {
                    if (_sportsmen[i].GetType().ToString() == "Lab8.Purple.Task4+SkiMan")
                    {
                        menLength++;
                    }
                    if (_sportsmen[i].GetType().ToString() == "Lab8.Purple.Task4+SkiWoman")
                    {
                        womenLength++;
                    }
                }

                men = new SkiMan[menLength];
                women = new SkiWoman[womenLength];
                int menIndex = 0;
                int womenIndex = 0;

                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i].GetType().ToString() == "Lab8.Purple.Task4+SkiMan")
                    {
                        men[menIndex++] = _sportsmen[i];
                    }
                    if (_sportsmen[i].GetType().ToString() == "Lab8.Purple.Task4+SkiWoman")
                    {
                        women[womenIndex++] = _sportsmen[i];
                    }
                }
            }

            public void Shuffle()
            {
                Sort();

                Split(out Sportsman[] men, out Sportsman[] women);

                int menIndex = 0;
                int womenIndex = 0;
                int index = 0;

                bool menFirst = false;

                if (men.Length > 0 && women.Length > 0) menFirst = men[0].Time <= women[0].Time;
                else if (men.Length > 0) menFirst = true;
                else menFirst = false;

                while (menIndex < men.Length || womenIndex < women.Length)
                {
                    if (menFirst)
                    {
                        if (menIndex < men.Length) _sportsmen[index++] = men[menIndex++];
                        else _sportsmen[index++] = women[womenIndex++];

                        menFirst = false;
                    }
                    else
                    {
                        if (womenIndex < women.Length) _sportsmen[index++] = women[womenIndex++];
                        else _sportsmen[index++] = men[menIndex++];

                        menFirst = true;
                    }
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                var finalists = new Group("Финалисты");
                finalists.Add(group1);
                finalists.Add(group2);
                finalists.Sort();
                return finalists;
            }

            public void Print()
            {
                Console.WriteLine($"{_name}  {_sportsmen}");
            }
        }
    }
}
