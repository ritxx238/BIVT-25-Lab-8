
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Security.Principal;
using System.Text.RegularExpressions;
namespace Lab8.Blue
{

    public class Task4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;
            public string Name => _name;
            public int[] Scores => _scores;

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    foreach (int score in _scores)
                    {
                        sum += score;
                    }
                    return sum;
                }
            }
            public Team(string Name)
            {
                _name = Name;
                _scores = new int[1];
            }
            public void PlayMatch(int result)
            {
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {
                Console.WriteLine($"Team: {Name}, Total Score: {TotalScore}");
            }


        }
        //public struct Group
        //{
        //    private string _name;
        //    private Team[] _teams;
        //    public string Name => _name;
        //    public Team[] Teams => _teams;
        //    private int _count;

        //    public void Add(Team Name)
        //    {
        //        if (_count < 12)
        //        {
        //            _teams[_count] = Name;
        //            _count++;
        //        }
        //    }
        //    public void Add(Team[] Names)
        //    {
        //        foreach (Team name in Names)
        //        {
        //            Add(name);

        //        }
        //    }
        //    public void Sort()
        //    {
        //        for (int i = 0; i < _teams.Length - 1; i++)
        //        {
        //            for (int j = 0; j < _teams.Length - 1 - i; j++) if (_teams[j].TotalScore < _teams[j + 1].TotalScore) (_teams[j], _teams[j + 1]) = (_teams[j + 1], _teams[j]);
        //        }
        //    }
        //    public void Print()
        //    {
        //        Console.WriteLine($"Group: {Name}");
        //        foreach (Team team in _teams)
        //        {
        //            team.Print();
        //        }
        //    }

        //}

        public class ManTeam : Team
        {
            public ManTeam(string Name) : base(Name)
            {

            }

        }
        public class WomanTeam : Team
        {
            public WomanTeam(string Name) : base(Name)
            {
            }
        }
        public class Group
        {
            private string _name;
            private int _manCount;
            private int _womanCount;
            private Team[] _manTeams;
            private Team[] _womanTeams;

            public string Name => _name;
            public Team[] ManTeams
            {
                get
                {
                    Team[] copy = new Team[_manTeams.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _manTeams[i];
                    }
                    return copy;

                }
            }
            public Team[] WomanTeams
            {
                get
                {
                    Team[] copy = new Team[_womanTeams.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _womanTeams[i];
                    }
                    return copy;
                }
            }
            public Group(string Name)
            {
                _name = Name;
                _manCount = 0;
                _womanCount = 0;
                _manTeams = new Team[12];
                _womanTeams = new Team[12];
            }
            public void Add(Team Name)
            {
                if (Name is ManTeam && _manCount < 12)
                {
                    _manTeams[_manCount] = Name;
                    _manCount++;
                }
                else if (Name is WomanTeam && _womanCount < 12)
                {
                    _womanTeams[_womanCount] = Name;
                    _womanCount++;
                }
            }
            public void Add(Team[] Teams)
            {

                if (_manCount >= 12 && Teams is ManTeam[] || _womanCount >= 12 && Teams is WomanTeam[]) return;
                foreach (Team Team in Teams)
                {
                    Add(Team);
                }
            }
            private void SortTeams(Team[] Teams)
            {
                for (int i = 0; i < Teams.Length; i++)
                {
                    for (int j = 1; j < Teams.Length - i; j++)
                    {
                        if (Teams[j - 1].TotalScore < Teams[j].TotalScore)
                        {
                            (Teams[j - 1], Teams[j]) = (Teams[j], Teams[j - 1]);
                        }
                    }
                }
            }
            public void Sort()
            {
                SortTeams(_manTeams);
                SortTeams(_womanTeams);
            }
            private static void MergeTeams(ref Team[] result, ref int resultCount, Team[] groupTeams1, Team[] groupTeams2, int count1, int count2, int сount)
            {

                for (int i = 0; i < сount && i < count1; i++)
                {
                    result[resultCount] = groupTeams1[i];
                    resultCount++;
                }


                for (int i = 0; i < сount && i < count2; i++)
                {
                    result[resultCount] = groupTeams2[i];
                    resultCount++;
                }
            }


            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");

                int countTeam = size / 2;


                group1.Sort();
                group2.Sort();

                MergeTeams(ref result._manTeams, ref result._manCount, group1._manTeams, group2._manTeams, group1._manCount, group2._manCount, countTeam);

                MergeTeams(ref result._womanTeams, ref result._womanCount, group1._womanTeams, group2._womanTeams, group1._womanCount, group2._womanCount, countTeam);
                result.Sort();
                return result;
            }

            public void Print()
            {
            
            }




        }
    }
}
