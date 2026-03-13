using System;

namespace Lab8.Blue
{
    public class Task5
    {
        public class Sportsman
        {
            private string _name;      
            private string _surname;     
            private int _place;          
            private bool _isPlaceSet;      

            public Sportsman(string name, string surname)
            {
                if (name != null)
                    _name = name;
                else
                    _name = "";
                if (surname != null)
                    _surname = surname;
                else
                    _surname = "";
                _place = 0;               
                _isPlaceSet = false;      
            }

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public void SetPlace(int place)
            {
                if (!_isPlaceSet)             
                {
                    _place = place;
                    _isPlaceSet = true;       
                }
            }

            public void Print()
            {
                return;
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private const int MAX_SPORTSMEN = 6;

            protected Team(string name)
            {
                if (name != null)
                    _name = name;
                else
                    _name = "";
                _sportsmen = new Sportsman[0];
            }

            public string Name => _name;

            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return new Sportsman[0];
                    Sportsman[] copy = new Sportsman[_sportsmen.Length];
                    for (int i = 0; i < _sportsmen.Length; i++)
                        copy[i] = _sportsmen[i];
                    return copy;
                }
            }

            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int total = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        int place = _sportsmen[i].Place;
                        if (place == 1) total += 5;
                        else if (place == 2) total += 4;
                        else if (place == 3) total += 3;
                        else if (place == 4) total += 2;
                        else if (place == 5) total += 1;
                    }
                    return total;                   // Сумма очков команды
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0)
                        return 18;

                    int topPlace = 18;               // Худшее место
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        int place = _sportsmen[i].Place;
                        if (place > 0 && place < topPlace)
                        {
                            topPlace = place;        // Нашли лучшее место
                        }
                    }
                    return topPlace;                  // Лучшее место в команде
                }
            }

            protected abstract double GetTeamStrength();  // Сила команды

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team champion = null;
                double maxStrength = -1;

                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] != null)
                    {
                        double strength = teams[i].GetTeamStrength();
                        if (strength > maxStrength)
                        {
                            maxStrength = strength;
                            champion = teams[i];           // Команда с макс. силой
                        }
                    }
                }
                return champion;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen.Length >= MAX_SPORTSMEN) return;

                Sportsman[] newSportsmen = new Sportsman[_sportsmen.Length + 1];
                for (int i = 0; i < _sportsmen.Length; i++)
                    newSportsmen[i] = _sportsmen[i];
                newSportsmen[_sportsmen.Length] = sportsman;
                _sportsmen = newSportsmen;                 // Добавили одного
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;

                for (int i = 0; i < sportsmen.Length; i++)
                {
                    Add(sportsmen[i]);                      // Добавили нескольких
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length <= 1) return;

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        bool needSwap = false;

                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                            needSwap = true;                 
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                            if (teams[j].TopPlace > teams[j + 1].TopPlace)
                                needSwap = true;            

                        if (needSwap)
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);  
                    }
                }
            }

            public void Print()
            {
                return;
            }
        }

        // Мужская команда
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen.Length == 0) return 0;

                double sumPlaces = 0;
                int count = 0;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    if (Sportsmen[i].Place > 0)
                    {
                        sumPlaces += Sportsmen[i].Place;
                        count++;
                    }
                }

                if (count == 0) return 0;
                return 100.0 / (sumPlaces / count);      // 100 / среднее место
            }
        }

        // Женская команда
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen.Length == 0) return 0;

                double sumPlaces = 0;
                double productPlaces = 1;
                int count = 0;

                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    if (Sportsmen[i].Place > 0)
                    {
                        sumPlaces += Sportsmen[i].Place;
                        productPlaces *= Sportsmen[i].Place;
                        count++;
                    }
                }

                if (count == 0 || productPlaces == 0) return 0;
                return 100.0 * sumPlaces * count / productPlaces;  // 100 * сумма * кол-во / произведение
            }
        }
    }
}