namespace Lab8.Blue;
public class Task5
{
    public class Sportsman
    {
        private string _name;
        private string _surname;
        private int _place;

        public string Name => _name;
        public string Surname => _surname;
        public int Place => _place;

        public Sportsman(string Name, string Surname)
        {
            _name = Name;
            _surname = Surname;
            _place = 0;
        }


        public void SetPlace(int place)
        {

            if (_place == 0 && place >= 1 && place <= 18)
            {
                _place = place;
            }
        }

        public void Print(){ return;  }
    }

    public abstract class Team
    {
        private string _name;
        private Sportsman[] _sportsmen;
        protected int _count;
        public string Name => _name;


        public Sportsman[] Sportsmen => (Sportsman[])_sportsmen.Clone();
        
        public int TotalScore
        {
            get
            {
                int total = 0;
                for (int i = 0; i < _count; i++)
                {
                    switch (_sportsmen[i].Place)
                    {
                        case 1: total += 5; break;
                        case 2: total += 4; break;
                        case 3: total += 3; break;
                        case 4: total += 2; break;
                        case 5: total += 1; break;
                        default: break;
                    }
                }
                return total;
            }
        }

        public int TopPlace
        {
            get
            {
                if (_count == 0) return 18;
                int best = _sportsmen[0].Place;
                for (int i = 1; i < _count; i++)
                {
                    if (_sportsmen[i].Place < best)
                        best = _sportsmen[i].Place;
                }
                return best;
            }
        }

        public Team(string Name)
        {
            _name = Name;
            _sportsmen = new Sportsman[6];
            _count = 0;
        }

        protected abstract double GetTeamStrength();


        public static Team GetChampion(Team[] teams)
        {
            if (teams == null || teams.Length == 0) return null;
            return teams.MaxBy(t => t.GetTeamStrength());
        }
        public void Add(Sportsman sportsman)
        {
            if (_count < 6)
            {
                _sportsmen[_count] = sportsman;
                _count++;
            }
        }


        public void Add(Sportsman[] sportsmen)
        {
            if (sportsmen == null) return;
            foreach (var s in sportsmen)
            {
                if (_count >= 6) break;
                Add(s);
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

        public void Print(){return;}
    }

    public class ManTeam : Team
    {
        public ManTeam(string Name) : base(Name) { }
        protected override double GetTeamStrength()
        {
            if (_count == 0) return 0;

            int place = Sportsmen.Sum(x => x.Place);
            double aver = (double)place / _count;
            if (aver == 0) return 0;
            return 100.0 / aver;

        }
    }
    public class WomanTeam : Team
    {
        public WomanTeam(string Name) : base(Name) { }

        protected override double GetTeamStrength()
        {
            if (_count == 0) return 0;

            int placeSum = Sportsmen.Sum(x => x.Place);
            int placePow = Sportsmen.Aggregate(1, (a, b) => a * b.Place);
            return 100.0 * placeSum * ((double)_count / placePow);
        }
    }

}
