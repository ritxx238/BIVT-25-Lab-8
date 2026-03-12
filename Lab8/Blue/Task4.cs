using System;

namespace Lab8.Blue
{
    public class Task4
    {
        public abstract class Team
        {
            private string _name;  
            private int[] _scores;   

            protected Team(string name)
            {
                if (name != null)
                {
                    _name = name;
                }
                else
                {
                    _name = "";
                }
                _scores = new int[12];
            }

            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return new int[0];

                    int[] copy = new int[_scores.Length];
                    Array.Copy(_scores, copy, _scores.Length);
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;

                    int total = 0;
                    for (int i = 0; i < _scores.Length; i++)
                        total += _scores[i];

                    return total;
                }
            }

            public void PlayMatch(int result)
            {
                int[] newScores = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                    newScores[i] = _scores[i];
                newScores[_scores.Length] = result;

                _scores = newScores;
            }

            public void Print()
            {
                Console.Write("Бибиби");

            }
        }

        // Класс мужской команды
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }

        // Класс женской команды
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }

        public class Group
        {
            private string _name;        
            private Team[] _manTeams; 
            private Team[] _womanTeams;
            private int _manCount;
            private int _womanCount;

            private const int MAX_TEAMS = 12; 

            public Group(string name)
            {
                if (name != null)
                {
                    _name = name;
                }
                else
                {
                    _name = "";
                }
                _manTeams = new Team[MAX_TEAMS];
                _womanTeams = new Team[MAX_TEAMS];
                _manCount = 0;
                _womanCount = 0;
            }


            public string Name => _name;
            public Team[] ManTeams => GetTeamsCopy(_manTeams);
            public Team[] WomanTeams => GetTeamsCopy(_womanTeams);
            private Team[] GetTeamsCopy(Team[] source)
            {
                if (source == null) return new Team[0];

                Team[] copy = new Team[source.Length];
                for (int i = 0; i < source.Length; i++)
                    copy[i] = source[i];

                return copy;
            }


            private void AddToArray(ref Team[] array, ref int count, Team team)
            {
                if (array == null)
                {
                    array = new Team[MAX_TEAMS];
                    count = 0;
                }

                if (count >= MAX_TEAMS) return;

                // проверяем, чтобы команда с таким именем уже не была добавлена
                for (int i = 0; i < count; i++)
                    if (array[i].Name == team.Name) return;

                array[count] = team;
                count++;
            }


            public void Add(Team team)
            {
                if (team is ManTeam)
                    AddToArray(ref _manTeams, ref _manCount, team);

                else if (team is WomanTeam)
                    AddToArray(ref _womanTeams, ref _womanCount, team);
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;

                for (int i = 0; i < teams.Length; i++)
                    Add(teams[i]);
            }

            
            // Сортировка команд в группе
            public void Sort()
            {
                GigaMegaSortirovka(ref _manTeams, _manCount);
                GigaMegaSortirovka(ref _womanTeams, _womanCount);
            }

            
            private void GigaMegaSortirovka(ref Team[] array, int count)
            {
                if (array == null || count <= 1) return;

                Team[] realOne = new Team[count];

                // копируем только заполненную часть массива
                for (int i = 0; i < count; i++)
                    realOne[i] = array[i];

               
                for (int i = 0; i < realOne.Length - 1; i++)
                    for (int j = 0; j < realOne.Length - 1 - i; j++)
                        if (realOne[j].TotalScore < realOne[j + 1].TotalScore)
                            (realOne[j], realOne[j + 1]) = (realOne[j + 1], realOne[j]);

                
                for (int i = 0; i < count; i++)
                    array[i] = realOne[i];
            }

            private static void MergeTeams(ref Team[] resultArray, ref int resultCount, Team[] array1, Team[] array2, int count1, int count2, int takeCount)
            {

                for (int i = 0; i < takeCount && i < count1; i++)
                {
                    resultArray[resultCount] = array1[i];
                    resultCount++;
                }


                for (int i = 0; i < takeCount && i < count2; i++)
                {
                    resultArray[resultCount] = array2[i];
                    resultCount++;
                }
            } 
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");

                int countTeam = size / 2;


                group1.Sort();
                group2.Sort();

                // объединяем мужские команды
                MergeTeams(ref result._manTeams, ref result._manCount, group1._manTeams, group2._manTeams, group1._manCount, group2._manCount, countTeam);

                // объединяем женские команды
                MergeTeams(ref result._womanTeams, ref result._womanCount, group1._womanTeams, group2._womanTeams, group1._womanCount, group2._womanCount, countTeam);
                result.Sort();
                return result;
            }

            public void Print()
            {
                Console.WriteLine("Амогус");
            }
        }
    }
} 