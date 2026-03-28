namespace Lab8.Blue;
public class Task4
{
    public abstract class Team
    {
        private string _name;
        private int[] _scores;

        public int TotalScore => _scores.Sum();

        public string Name => _name;

        public int[] Scores => (int[]) _scores.Clone();
        
        public Team(string name)
        {
            _name = name;
            _scores = [];
        }
        public void PlayMatch(int result)
        {
            int[] newScores = new int[_scores.Length + 1];
            Array.Copy(_scores, newScores, _scores.Length);
            newScores[_scores.Length] = result;
            _scores = newScores;
        }

        public void Print()
        {
            return;
        }
    }

    public class Group
    {
        private string _name;
        private ManTeam[] _manTeams;
        private WomanTeam[] _womanTeams;
        private int _manCount = 0;
        private int _womanCount = 0;

        public string Name => _name;

        public ManTeam[] ManTeams => (ManTeam[]) _manTeams.Clone();
        
        public WomanTeam[] WomanTeams =>(WomanTeam[]) _womanTeams.Clone();
        

        public Group(string name)
        {
            _name = name;
            _manTeams = new ManTeam[12];
            _womanTeams = new WomanTeam[12];
            _manCount = 0;
            _womanCount = 0;
        }


        public void Add(Team team)
        {

            if (team is ManTeam a)
            {
                if (_manCount >= 12) return;
                _manTeams[_manCount] = a;
                _manCount++;
            }
            else if (team is WomanTeam b)
            {
                if (_womanCount >= 12) return;
                _womanTeams[_womanCount] = b;
                _womanCount++;
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

        private void ArraySort(Team[] teams, int count)
        {
            var sorted = teams.Take(count)
                                  .OrderByDescending(t => t.TotalScore)
                                  .ToArray();
            Array.Copy(sorted, teams, count);
        }


        public void Sort()
        {
            ArraySort(_womanTeams, _womanCount);
            ArraySort(_manTeams, _manCount);
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

            CollectAndAdd(g1.ManTeams, g2.ManTeams, k, res);
            CollectAndAdd(g1.WomanTeams, g2.WomanTeams, k, res);

            res.Sort();
            return res;
        }

        public void Print()
        {
            return; 
        }
    }
    public class WomanTeam : Team
    {
        public WomanTeam(string name) : base(name) { }
    }

    public class ManTeam : Team
    {
        public ManTeam(string name) : base(name) { }
    }
}

