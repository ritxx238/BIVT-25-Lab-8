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

            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;
                int i = 0;
                while (i < array.Length)
                {
                    if (i == 0 || array[i]._time >= array[i - 1]._time)
                    {
                        i++;
                    }
                    else
                    {
                        Sportsman tmp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = tmp;
                        i--;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Name: {Name},\nSurname: {Surname},\nTime: {Time}");
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
                Sportsman[] array = new Sportsman[group._sportsmen.Length];
                for (int i = 0; i < group._sportsmen.Length; i++)
                {
                    array[i] = group._sportsmen[i];
                }

                _sportsmen = array;
            }

            public void Add(Sportsman sportsman)
            {
                if (sportsman == null) return;
                Array.Resize(ref _sportsmen,_sportsmen.Length+1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
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
                Add(group._sportsmen);
            }
            public void Sort()
            {
                int i = 0;
                while (i < _sportsmen.Length)
                {
                    if (i == 0 || _sportsmen[i].Time >= _sportsmen[i - 1].Time)
                    {
                        i++;
                    }
                    else
                    {
                        Sportsman tmp = _sportsmen[i];
                        _sportsmen[i] = _sportsmen[i - 1];
                        _sportsmen[i - 1] = tmp;
                        i--;
                    }
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                Sportsman[] array = new Sportsman[group1._sportsmen.Length + group2._sportsmen.Length];
                int i1 = 0, i2 = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    if (i1 < group1._sportsmen.Length && i2 < group2._sportsmen.Length)
                    {
                        if (group1._sportsmen[i1].Time <= group2._sportsmen[i2].Time)
                        {
                            array[i] = group1._sportsmen[i1];
                            i1++;
                        }
                        else
                        {
                            array[i] = group2._sportsmen[i2];
                            i2++;
                        }
                    }
                    else if (i1 == group1._sportsmen.Length)
                    {
                        array[i] = group2._sportsmen[i2];
                        i2++;
                    }
                    else
                    {
                        array[i] = group1._sportsmen[i1];
                        i1++;
                    }
                }

                Group group = new Group("Финалисты");
                group._sportsmen = array;
                return group;
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                int count1 = 0;
                int count2 = 0;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiMan)
                    {
                        count1++;
                    }
                    else
                    {
                        count2++;
                    }
                }

                men = new SkiMan[count1];
                women = new SkiWoman[count2];
                count1 = 0;
                count2 = 0;
                for(int i = 0; i < _sportsmen.Length; i++)
                {
                    if(_sportsmen[i] is SkiMan)
                    {
                        men[count1] = _sportsmen[i];
                        count1++;
                    }
                    else
                    {
                        women[count2] = _sportsmen[i];
                        count2++;
                    }
                }

            }

            public void Shuffle()
            {
                Sort();
                Split(out Sportsman[] men, out Sportsman[] women);
                int i1 = 0, i2 = 0;
                int index = 0;
                if (men[0].Time <= women[0].Time)
                {
                    while (index < _sportsmen.Length)
                    {
                        if (i1 < men.Length && i2 < women.Length)
                        {
                            _sportsmen[index] = men[i1];
                            i1++;
                            index++;
                            _sportsmen[index] = women[i2];
                            i2++;
                            index++;
                        }
                        else if (i1 == men.Length)
                        {
                            _sportsmen[index] = women[i2];
                            i2++;
                            index++;
                        }
                        else
                        {
                            _sportsmen[index] = men[i1];
                            i1++;
                            index++;
                        }
                    }
                }
                else
                {
                    while (index < _sportsmen.Length)
                    {
                        if (i1 < men.Length && i2 < women.Length)
                        {
                            _sportsmen[index] = women[i2];
                            i2++;
                            index++;
                            _sportsmen[index] = men[i1];
                            i1++;
                            index++;
                        }
                        else if (i1 == men.Length)
                        {
                            _sportsmen[index] = women[i2];
                            i2++;
                            index++;
                        }
                        else
                        {
                            _sportsmen[index] = men[i1];
                            i1++;
                            index++;
                        }
                    }
                }

            }
            public void Print()
            {
                Console.WriteLine("Финалисты");
                foreach (Sportsman s in _sportsmen)
                {
                    s.Print();
                }
                
            }
        }

        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname):base(name,surname){}

            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name,string surname):base(name,surname){}

            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }
    }
}
