namespace Lab8.Blue
{
    public class Task5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;
            private bool _isSetted;

            private const int LastPlace = 18;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
                _isSetted = false;
            }

            public void SetPlace(int place)
            {
                if (!_isSetted && place > 0 && place <= LastPlace)
                {
                    _place = place;
                    _isSetted = true;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} - Место: {_place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;

            private const int CountOfSportsmenInTeam = 6;

            public string Name => _name;

            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] copy = new Sportsman[_sportsmen.Length];
                    Array.Copy(_sportsmen, copy, _sportsmen.Length);
                    return copy;
                }
            }

            public int SummaryScore
            {
                get
                {
                    int sum = 0;
                    foreach (var sportsman in _sportsmen)
                    {
                        if (sportsman.Place != 0)
                            sum += GetPoints(sportsman);
                    }

                    return sum;
                }
            }

            public int TopPlace
            {
                get
                {
                    int topPlace = 18;
                    foreach (var sportsman in _sportsmen)
                    {
                        if (sportsman.Place != 0 && sportsman.Place < topPlace)
                            topPlace = sportsman.Place;
                    }

                    return topPlace;
                }
            }

            private int GetPoints(Sportsman sportsman)
            {
                switch (sportsman.Place)
                {
                    case 1: return 5;
                    case 2: return 4;
                    case 3: return 3;
                    case 4: return 2;
                    case 5: return 1;
                    default: return 0;
                }
            }

            protected abstract double GetTeamStrength();

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen.Length < CountOfSportsmenInTeam)
                {
                    Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                    _sportsmen[^1] = sportsman;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                foreach (var sportsman in sportsmen)
                {
                    if (_sportsmen.Length < CountOfSportsmenInTeam)
                    {
                        Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                        _sportsmen[^1] = sportsman;
                    }
                    else
                        return;
                }
            }

            public static void Sort(Team[] teams)
            {
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            if (teams[j + 1].HasFirstPlace() && !teams[j].HasFirstPlace())
                            {
                                (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                            }
                        }
                    }
                }
            }

            private bool HasFirstPlace()
            {
                foreach (var sportsman in _sportsmen)
                {
                    if (sportsman.Place == 1)
                        return true;
                }

                return false;
            }

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0)
                    return null;

                Team champion = teams[0];
                double maxStrength = champion.GetTeamStrength();

                for (int i = 1; i < teams.Length; i++)
                {
                    double currentStrength = teams[i].GetTeamStrength();
                    if (currentStrength > maxStrength)
                    {
                        maxStrength = currentStrength;
                        champion = teams[i];
                    }
                }

                return champion;
            }

            public void Print()
            { }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }

            protected override double GetTeamStrength()
            {
                int sumOfPlaces = 0;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman.Place != 0)
                    {
                        sumOfPlaces += sportsman.Place;
                        count++;
                    }
                }

                if (count == 0)
                    return 0;

                double averagePlace = (double)sumOfPlaces / count;
                return 100 / averagePlace;
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            { }

            protected override double GetTeamStrength()
            {
                int sumOfPlaces = 0;
                int productOfPlaces = 1;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman.Place != 0)
                    {
                        sumOfPlaces += sportsman.Place;
                        productOfPlaces *= sportsman.Place;
                        count++;
                    }
                }

                if (count == 0 || productOfPlaces == 0)
                    return 0;

                return 100.0 * sumOfPlaces * count / productOfPlaces;
            }
        }
    }
}