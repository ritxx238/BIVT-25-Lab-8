namespace Lab8.Blue
{
    public class Task5
    { 
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place = 0;
            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }
            
            public void SetPlace(int place)
            {
                if (_place == 0) _place = place;
            }

            public void Print()
            {
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] copy = new Sportsman[_sportsmen.Length];
                    Array.Copy(_sportsmen,0, copy,0, _sportsmen.Length);
                    return copy;
                }
            }
            public int SummaryScore
            {
                get
                {
                    int sum = 0;
                    foreach (Sportsman sportsman in _sportsmen)
                    {
                        int place = sportsman.Place;
                        if (place == 1) sum += 5;
                        else if (place == 2) sum += 4;
                        else if (place == 3) sum += 3;
                        else if (place == 4) sum += 2;
                        else if (place == 5) sum += 1;
                    }
                    return sum;
                }
            }
            public int TopPlace
            {
                get
                {
                    int min = 18;
                    foreach (Sportsman sportsman in _sportsmen)
                    {
                        if (sportsman.Place > 0) min = int.Min(min, sportsman.Place);
                    }
                    return min;
                }
            }
            public Team(string name)
            {
                _name = name;
                _sportsmen = [];
            }
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen.Length < 6)
                {
                    Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                    _sportsmen[^1] = sportsman;
                }
            }
            public void Add(Sportsman[] sportsmen)
            {
                foreach (var sportsman in sportsmen) Add(sportsman);
            }
            public static void Sort(Team[] teams)
            {
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore) (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        if (teams[j].SummaryScore == teams[j + 1].SummaryScore && teams[j + 1].IsWin()) (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                    }
                }
            }
            private bool IsWin() { return TopPlace == 1; }
            public void Print() { }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                Team max = teams[0];
                for (int i = 1; i < teams.Length; i++) if (teams[i].GetTeamStrength() > max.GetTeamStrength()) max = teams[i];
                return max;
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double sum = 0, c = 0;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    sum += Sportsmen[i].Place; c++;
                }

                if (c == 0) return 0;
                return 100.0 / (sum / c);
            }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
            
            protected override double GetTeamStrength()
            {
                double sum = 0, p = 1, c = 0;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    sum += Sportsmen[i].Place; c++; p*=Sportsmen[i].Place;
                }

                if (c == 0 || p == 0) return 0;
                return 100.0 * sum * c / p;
            }
        }
    }
}
