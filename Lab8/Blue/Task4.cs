using System.Numerics;

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
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _scores[i];
                    }
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        sum += _scores[i];
                    }
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

            public void Print()
            {
                return;
            }

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


            public Group(string name)
            {
                _name = name;
                _manTeams = new Team[12];
                _womanTeams = new Team[12];
                _manCount = 0;
                _womanCount = 0;
            }

            public void Add(Team name)
            {
                if (name is ManTeam && _manCount < 12)
                {
                    _manTeams[_manCount] = name;
                    _manCount++;
                }
                if (name is WomanTeam && _womanCount < 12)
                {
                    _womanTeams[_womanCount] = name;
                    _womanCount++;
                }
            }
            public void Add(Team[] names)
            {
                if (_womanCount >= 12 && names is WomanTeam || _manCount >= 12 && names is ManTeam)
                {
                    return;
                }
                for (int i = 0; i < names.Length; i++)
                {
                    Add(names[i]);
                }
            }
            public void Sort()
            {
                SortTeams(_womanTeams);
                SortTeams(_manTeams);
            }
            private void SortTeams(Team[] team)
            {
                for (int i = 0; i < team.Length; i++)
                {
                    for (int j = 1; j < team.Length - i; j++)
                    {
                        if (team[j - 1].TotalScore < team[j].TotalScore)
                        {
                            (team[j - 1], team[j]) = (team[j], team[j - 1]);
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                int teamCount = size / 2;
                group1.Sort();
                group2.Sort();

                Group resultMan = new Group("");
                Group resultWoman = new Group("");

                for (int i = 0; i < group1._manCount; i++)
                    resultMan.Add(group1._manTeams[i]);
                for (int i = 0; i < group1._womanCount; i++)
                    resultWoman.Add(group1._womanTeams[i]);

                for (int i = 0; i < group2._manCount; i++)
                    resultMan.Add(group2._manTeams[i]);
                for (int i = 0; i < group2._womanCount; i++)
                    resultWoman.Add(group2._womanTeams[i]);

                resultMan.Sort();
                resultWoman.Sort();

                for (int i = 0; i < teamCount; i++)
                {
                    result.Add(resultMan._manTeams[i]);
                    result.Add(resultWoman._womanTeams[i]);
                }
                result.Sort();
                return result;
            }

            public void Print()
            {
                return;
            }
        }
    }
}
