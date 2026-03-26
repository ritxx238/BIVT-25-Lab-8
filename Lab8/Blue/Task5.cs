
using System.ComponentModel.Design;
using static Lab8.Blue.Task5;

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
            public Sportsman(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
            }
            public void SetPlace(int place)
            {
                if (_place == 0)
                {
                    _place = place;
                }
            }
            public void Print()
            {
                Console.WriteLine($"Name: {Name} {Surname}, Place: {Place}");
            }

        }
        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmens;
            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] copy = new Sportsman[_sportsmens.Length];
                    Array.Copy(_sportsmens, 0, copy, 0, _sportsmens.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    foreach (Sportsman sportsmen in _sportsmens)
                    {
                        if (sportsmen.Place == 1) { sum += 5; }
                        else if (sportsmen.Place == 2) { sum += 4; }
                        else if (sportsmen.Place == 3) { sum += 3; }
                        else if (sportsmen.Place == 4) { sum += 2; }
                        else if (sportsmen.Place == 5) { sum += 1; }
                        ;
                    }
                    return sum;
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsmens.Length == 0)
                        return 18;
                    int topplace = 100;
                    foreach (Sportsman sportsmen in _sportsmens)
                    {
                        if (sportsmen.Place > 0) topplace = int.Min(topplace, sportsmen.Place);
                    }

                    Console.WriteLine($"Top Place: {topplace}");
                    return topplace;
                }
            }

            public Team(string Name)
            {
                _name = Name;
                _sportsmens = [];

            }
            public void Add(Sportsman Name)
            {
                if (_sportsmens.Length < 6)
                {
                    Array.Resize(ref _sportsmens, _sportsmens.Length + 1);
                    _sportsmens[_sportsmens.Length - 1] = Name;
                }
            }
            public void Add(Sportsman[] Names)
            {
                foreach (Sportsman sportsmen in Names)
                {
                    Add(sportsmen);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null) return;
                Array.Sort(teams, (a, b) =>
                {
                    if (a.TotalScore != b.TotalScore)
                        return b.TotalScore.CompareTo(a.TotalScore);
                    else
                        return a.TopPlace.CompareTo(b.TopPlace);
                });
            }
            public void Print()
            {
                Console.WriteLine($"Team: {Name}, Total Score: {TotalScore}");
                foreach (Sportsman sportsmen in _sportsmens)
                {
                    sportsmen.Print();
                }
            }
            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams.Length == 0) { return null; }
                Team champion = null;
                foreach (Team team in teams)
                {
                    if (champion == null || team.GetTeamStrength() > champion.GetTeamStrength())
                    {
                        champion = team;
                    }
                }
                return champion;

            }




        }

        public class ManTeam : Team
        {
            public ManTeam(string Name) : base(Name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen.Length == 0)
                {
                    return 0;
                }
                double sumSportsman = 0;
                for (int i = 0; i < Sportsmen.Length; i++) 
                {
                    sumSportsman += Sportsmen[i].Place;
                }
                double mid = sumSportsman / Sportsmen.Length;
                return (100 / mid);
            }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen.Length == 0)
                {
                    return 0;
                }
                int sumSportsman = 0;
                double pr = 1;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    sumSportsman += Sportsmen[i].Place;
                    pr *= Sportsmen[i].Place;
                }
                return ((100 * sumSportsman * Sportsmen.Length) / pr);
            }
        }
    }

}
