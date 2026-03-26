namespace Lab8.Blue
{
    public class Task5
    {
        
        public class Sportsman{
            private string _Name;
            private string _Surname;
            private int _Place; 

            public string Name => _Name;
            public string Surname => _Surname;
            public int Place => _Place;

            public Sportsman(string Name, string Surname)
            {
                _Name = Name;
                _Surname = Surname;
                _Place = 0;
            }

           
            public void SetPlace(int place)
            {
                
                if (_Place == 0 && place >= 1 && place <= 18)
                {
                    _Place = place;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name: {_Name}, Surname: {_Surname}, Place: {_Place}");
            }
        }

        public abstract class Team
        {
            private string _Name;
            private Sportsman[] _Sportsmen; 
            protected int _Count; 
            public string Name => _Name;
            

            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] copy = new Sportsman[_Count];
                    Array.Copy(_Sportsmen, copy, _Count);
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    int total = 0;
        for (int i = 0; i < _Count; i++)
        {
            switch (_Sportsmen[i].Place)
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
                    if (_Count == 0) return 18;
                    int best = _Sportsmen[0].Place;
                    for (int i = 1; i < _Count; i++)
                    {
                        if (_Sportsmen[i].Place < best)
                            best = _Sportsmen[i].Place;
                    }
                    return best;
                }
            }

            public Team(string Name)
            {
                _Name = Name ;
                _Sportsmen = new Sportsman[6];
                _Count = 0;
            }

            protected abstract double GetTeamStrength();


            public static Team GetChampion(Team[] teams)
            {
                return teams?.MaxBy(t => t.GetTeamStrength());
            }
            public void Add(Sportsman sportsman)
            {
                if (_Count < 6)
                {
                    _Sportsmen[_Count] = sportsman;
                    _Count++;
                }
            }

           
            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                foreach (var s in sportsmen)
                {
                    if (_Count >= 6) break;
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

            public void Print()
            {
                Console.WriteLine($"Team: {_Name}, TotalScore: {TotalScore}, TopPlace: {TopPlace}, Count: {_Count}");
            }
        }

        public class ManTeam : Team 
        {
            public ManTeam(string Name):base(Name){}
            protected override double GetTeamStrength()
            {
                if (_Count == 0)return 0;
                
                int place=Sportsmen.Sum(x=>x.Place);
                double aver = (double) place / _Count;
                if(aver==0)return 0;
                return 100.0/aver;
                
            }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string Name):base(Name){}

            protected override double GetTeamStrength()
            {
                if (_Count == 0) return 0;
                
                int placeSum=Sportsmen.Sum(x=>x.Place);
                int placePow=Sportsmen.Aggregate(1,(a,b)=>a*b.Place);
                return 100.0*placeSum*((double)_Count/placePow);
            }
        }
    }
}