namespace Lab8.Blue
{
    public class Task4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;
            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    int[] copy = new int[_scores.Length];
                    Array.Copy(_scores, copy, _scores.Length);
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    foreach (int score in _scores) sum += score;
                    return sum;
                }
            }
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }
            public void PlayMatch(int result)
            {
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }
            public void Print() { return; }
        }
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }
        public class Group
        {
            private string _name;
            private Team[] _manteams;
            private Team[] _womanteams;
            private int _mancount;
            private int _womancount;
            public string Name => _name;
            public Team[] ManTeams
            {
                get
                {
                    Team[] copy = new Team[_manteams.Length];
                    Array.Copy(_manteams, 0, copy, 0, _manteams.Length);
                    return copy;
                }
            }
            public Team[] WomanTeams
            {
                get
                {
                    Team[] copy = new Team[_womanteams.Length];
                    Array.Copy(_womanteams, 0, copy, 0, _womanteams.Length);
                    return copy;
                }
            }
            public Group(string name)
            {
                _name = name;
                _manteams = new Team[12];
                _mancount = 0;
                _womanteams = new Team[12];
                _womancount = 0;
            }
            public void Add(Team team_add)
            {
                if (team_add is ManTeam && _mancount < 12) _manteams[_mancount++] = team_add;
                if (team_add is WomanTeam && _womancount < 12) _womanteams[_womancount++] = team_add;
            }
            public void Add(Team[] teams_add)
            {
                foreach (Team team_add in teams_add) Add(team_add);
            }
            private void _sorting(ref Team[] array, int count)
            {
                Team[] cp = new Team[count];
                Array.Copy(array, cp, count);
                for (int i = 0; i < cp.Length - 1; i++)
                {
                    for (int j = 0; j < cp.Length - 1 - i; j++) if (cp[j].TotalScore < cp[j + 1].TotalScore) (cp[j], cp[j + 1]) = (cp[j + 1], cp[j]);
                }
                Array.Copy(cp, array, count);
            }
            public void Sort()
            {
                _sorting(ref _manteams, _mancount);
                _sorting(ref _womanteams, _womancount);
            }
            private static void _merge(ref Team[] r_array, ref int r_count, Team[] a1, Team[] a2, int c1, int c2, int c3)
            {
                for (int i = 0; i < c3 && i < c1; i++) r_array[r_count++] = a1[i];
                for (int i = 0; i < c3 && i < c2; i++) r_array[r_count++] = a2[i];
            } 
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                group1.Sort();
                group2.Sort();
                _merge(ref result._manteams, ref result._mancount, group1._manteams, group2._manteams, group1._mancount, group2._mancount, size / 2);
                _merge(ref result._womanteams, ref result._womancount, group1._womanteams, group2._womanteams, group1._womancount, group2._womancount, size / 2);
                result.Sort();
                return result;
            }
            public void Print() { }
        }
    }
}
