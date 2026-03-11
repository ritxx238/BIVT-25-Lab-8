using System.ComponentModel.DataAnnotations;

namespace Lab8.Blue
{
    public class Task4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;

            public int TotalScore => _scores.Sum();
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

            public Team(string name)
            {
                _name = name;
                _scores = [];
            }

            // добавление очков в команду
            public void PlayMatch(int result)
            {
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[^1] = result;
            }

            public void Print()
            {
                return;
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {
            }
        }

        public class Group
        {
            private string _name;
            private Team[] _manTeams;
            private Team[] _womanTeams;

            private int _womanCount;
            private int _manCount;

            public string Name => _name;

            public Team[] ManTeams
            {
                get
                {
                    Team[] copy = new Team[_manTeams.Length];
                    Array.Copy(_manTeams, copy, _manTeams.Length);
                    return copy;
                }
            }

            public Team[] WomanTeams
            {
                get
                {
                    Team[] copy = new Team[_womanTeams.Length];
                    Array.Copy(_womanTeams, copy, _womanTeams.Length);
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

            // добавление одной команды
            public void Add(Team team)
            {
                if (team is ManTeam && _manCount < 12)
                    _manTeams[_manCount++] = team;
                else if (team is WomanTeam && _womanCount < 12)
                    _womanTeams[_womanCount++] = team;
            }

            // добавление нескольких команд
            public void Add(Team[] teams)
            {
                foreach (var team in teams)
                    Add(team);
            }

            private void SortTeams(Team[] teams)
            {
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                    }
                }
            }

            public void Sort()
            {
                SortTeams(_womanTeams);
                SortTeams(_manTeams);
            }

            private static Team[] MergeSort(Team[] teams1, Team[] teams2, int countTeam)
            {
                Team[] result = new Team[countTeam];
                int i = 0, j = 0, k = 0;

                // Слияние пока есть элементы в обоих массивах
                while (i < countTeam / 2 && j < countTeam / 2)
                {
                    if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    {
                        result[k++] = teams1[i++];
                    }
                    else
                    {
                        result[k++] = teams2[j++];
                    }
                }

                // Копируем оставшиеся элементы из первого массива
                while (i < countTeam / 2)
                {
                    result[k++] = teams1[i++];
                }

                // Копируем оставшиеся элементы из второго массива
                while (j < countTeam / 2)
                {
                    result[k++] = teams2[j++];
                }

                return result;
            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                if (group1._manTeams == null || group2._manTeams == null || group2._womanTeams == null ||
                    group1._womanTeams == null)
                    return null;

                group1.Sort();
                group2.Sort();
                
                if (size % 2 != 0)
                    return null;
                
                result._manTeams = MergeSort(group1._manTeams, group2._manTeams, size);
                result._womanTeams = MergeSort(group1._womanTeams, group2._womanTeams, size);
                
                return result;
            }
            public void Print()
            {
                return;
            }
        }
    }
}