using static Lab8.Blue.Task4;

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

            protected Team(string name)
            {
                _name = name;
                _scores = new int[12];
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

            public void PlayMatch(int result)
            {
                int[] newMatch = new int[_scores.Length + 1]; // создаем массив на 1 больше
                Array.Copy(_scores, newMatch, _scores.Length); // копируем старый
                newMatch[newMatch.Length - 1] = result; // добавляем рез
                _scores = newMatch;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} : {TotalScore}");

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

                MergeTeams(ref result._manTeams, ref result._manCount, group1._manTeams, group2._manTeams, group1._manCount, group2._manCount, teamCount);

                MergeTeams(ref result._womanTeams, ref result._womanCount, group1._womanTeams, group2._womanTeams, group1._womanCount, group2._womanCount, teamCount);
                result.Sort();
                return result;
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

            public void Print()
            {

            }
        }
    }
}
