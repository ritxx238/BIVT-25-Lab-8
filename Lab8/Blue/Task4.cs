namespace Lab8.Blue
{

    public class Task4
    {
        public abstract class Team
        {
            private string _Name;
            private int[] _Scores;
            public int TotalScore => _Scores.Sum();
            public string Name => _Name;

            public int[] Scores
            {
                get
                {
                    int[] copy = new int[_Scores.Length];
                    Array.Copy(_Scores, copy, _Scores.Length);
                    return copy;
                }
            }

            public Team(string name)
            {
                _Name = name;
                _Scores = [];
            }
            public void PlayMatch(int result)
            {
                int[] newScores = new int[_Scores.Length + 1];
                Array.Copy(_Scores, newScores, _Scores.Length);
                newScores[_Scores.Length] = result;
                _Scores = newScores;
            }

            public void Print()
            {
                return;
            }
        }

        public class Group
        {
            private string _Name;
            private   ManTeam[] _ManTeams;
            private WomanTeam[] _WomanTeams;
            private int _ManCount = 0;
            private int _WomanCount = 0;

            public string Name => _Name;

            public ManTeam[] ManTeams
            {
                get
                {
                    ManTeam[] copy = new ManTeam[_ManTeams.Length];
                    Array.Copy(_ManTeams, copy, _ManTeams.Length);
                    return copy;
                }
            }

            public WomanTeam[] WomanTeams
            {
                get
                {
                    WomanTeam[] copy = new WomanTeam[_WomanTeams.Length];
                    Array.Copy(_WomanTeams, copy, _WomanTeams.Length);
                    return copy;
                }
            }
            
            public Group(string name)
            {
                _Name = name;
                _ManTeams = new ManTeam[12];
                _WomanTeams = new WomanTeam[12];
                _ManCount = 0;
                _WomanCount = 0;
            }

            
            public void Add(Team team)
            {
                
                if(team is ManTeam a)
                {
                    if (_ManCount >= 12) return;
                    _ManTeams[_ManCount] = a;
                    _ManCount++;
                }
                else if(team is WomanTeam b)
                {
                    if (_WomanCount >= 12) return;
                    _WomanTeams[_WomanCount]=b;
                    _WomanCount++;
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;
                 foreach (var team in teams)
                 {
                    Add(team); 
                 }
            }

            private void ArraySort(Team[] teams,int count)
            {
             var sorted = teams.Take(count)
                                   .OrderByDescending(t => t.TotalScore)
                                   .ToArray();
                 Array.Copy(sorted, teams, count);   
            }
           
            
            public void Sort()
            {
                ArraySort(_WomanTeams,_WomanCount);
                ArraySort(_ManTeams,_ManCount);
            }

            private static void CollectAndAdd(Team[] t1, Team[] t2, int k, Group destination)
            {
               
                var best = t1.Take(k)
                            .Concat(t2.Take(k))
                            .Where(t => t != null);

                foreach (var t in best)
                {
                    destination.Add(t);
                }
            }
            
            public static Group Merge(Group g1, Group g2, int size)
            {
                Group res = new Group("Финалисты");
                g1.Sort();
                g2.Sort();

                int k = size / 2;

                CollectAndAdd(g1.ManTeams, g2.ManTeams,k,res);
                CollectAndAdd(g1.WomanTeams, g2.WomanTeams,k,res);

            
                res.Sort(); 
                return res;
            }
            
            public void Print()
            {
            }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string name):base(name){}
        }

        public class ManTeam : Team
        {
            public ManTeam(string name):base(name){}
        }
    }
    
}