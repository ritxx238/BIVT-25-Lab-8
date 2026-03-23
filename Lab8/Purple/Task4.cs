using static Lab8.Purple.Task4;

namespace Lab8.Purple
{
    public class Task4
    {
        public class Sportsman
        {
            private string _name; private string _surname;
            private double _time;
            public string Name => _name; public string Surname => _surname;
            public double Time => _time;
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0.0;
            }
            //int c = 0;
            public void Run(double time)
            {
                //if (c == 0) 
                _time = time; 
                //c++;
            }
            public void Print()
            { Console.Write($"Name: {Name}\nSurname: {Surname}\nTime: {Time}\n\n"); }
            public static void Sort(Sportsman[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                    for (int j = 0; j < array.Length - i - 1; j++)
                        if (array[j].Time > array[j + 1].Time)
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
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
            private string _gname; private Sportsman[] _sportsman;
            public string Name => _gname; public Sportsman[] Sportsmen => _sportsman;
            public Group(string gname)
            {
                _gname = gname;
                _sportsman = new Sportsman[0];
            }
            public Group(Group group)
            {
                //string name = group.Name;
                //Sportsman[] sportsman = (Sportsman[])group.Sportsmen.Clone();
                _gname = group._gname;                 
                _sportsman = (Sportsman[])group._sportsman.Clone();
            }
            public void Split(out Sportsman[] men, out Sportsman[] women)
            { 
                men=new Sportsman[0]; women=new Sportsman[0];
                for (int i = 0; i < _sportsman.Length; i++)
                {
                    if (_sportsman[i] is SkiMan)
                    { Array.Resize(ref men, men.Length + 1); men[^1] = _sportsman[i]; }
                    else if (_sportsman[i] is SkiWoman)
                    { Array.Resize(ref women, women.Length + 1); women[^1] = _sportsman[i]; }
                }
            }
            public void Shuffle()
            {
                
                Sort();
                Split(out Sportsman[] men, out Sportsman[] women);
                Sportsman[] mas = new Sportsman[men.Length + women.Length];
                bool m1 = true; 
                if (men.Length > 0 && women.Length > 0)
                    if (women[0].Time < men[0].Time)
                        m1 = false;
                else if (men.Length == 0)
                    m1 = false;  
                int m = 0, w = 0, k = 0;
                bool nextm = m1;
                while (m < men.Length || w < women.Length)
                {
                    if (nextm && m < men.Length)
                        mas[k++] = men[m++];
                    else if (!nextm && w < women.Length)
                        mas[k++] = women[w++];
                    else if (m < men.Length)
                        mas[k++] = men[m++];
                    else if (w < women.Length)
                        mas[k++] = women[w++];
                    nextm = !nextm;
                }
                _sportsman = mas;
            }
            public void Add(Sportsman sportsman)
            {
                Array.Resize(ref _sportsman, _sportsman.Length + 1);
                _sportsman[^1] = sportsman;
            }
            public void Add(Sportsman[] sportsman)
            {
                int n = _sportsman.Length, k = 0;
                Array.Resize(ref _sportsman, _sportsman.Length + sportsman.Length);
                for (int i = n; i < _sportsman.Length; i++)
                    _sportsman[i] = sportsman[k++];
            }
            public void Add(Group group)
            {
                int n = _sportsman.Length, l = 0;
                Array.Resize(ref _sportsman, _sportsman.Length + group._sportsman.Length);
                for (int i = n; i < _sportsman.Length; i++)
                    _sportsman[i] = group._sportsman[l++];
            }
            public void Sort()
            {
                for (int i = 0; i < _sportsman.Length - 1; i++)
                    for (int j = 0; j < _sportsman.Length - i - 1; j++)
                        if (_sportsman[j].Time > _sportsman[j + 1].Time)
                            (_sportsman[j], _sportsman[j + 1]) = (_sportsman[j + 1], _sportsman[j]);
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
                Console.WriteLine($"{Name}");
                foreach (var i in _sportsman)
                {
                    i.Print();
                }
            }
        }
    }
}
