using System;
using System.Linq;
namespace Lab8.Purple
{
    public class Task3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _top_place;
            private double _total_mark;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Marks
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return null;
                    double[] copy = new double[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int TopPlace => _top_place;
            public double TotalMark => _total_mark;

            public int[] Places
            {
                get
                {
                    if (_places == null || _places.Length == 0) return null;
                    int[] copy = new int[_places.Length];
                    Array.Copy(_places, copy, _places.Length);
                    return copy;
                }
            }
            public int Score
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _places.Length; i++) sum += _places[i];
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _top_place = 0;
                _total_mark = 0;
            }

            public void Evaluate(double result)
            {
                if (_marks == null || _marks.Length == 0) _marks = new double[7];
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = result;
                        _total_mark += result;
                        break;
                    }
                }
                
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                for (int judge = 0; judge < 7; judge++)
                {
                    Array.Sort(participants, (a, b) => b._marks[judge].CompareTo(a._marks[judge]));

                    for (int i = 0; i < participants.Length; i++)
                    {
                        participants[i]._places[judge] = i + 1;
                        if (judge == 0 || (i + 1) < participants[i]._top_place)
                        {
                            participants[i]._top_place = i + 1; 
                        }
                    }
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                Array.Sort(array, (a, b) =>
                {
                    if (a.Score != b.Score) return a.Score.CompareTo(b.Score);

                    else if (a.TopPlace != b.TopPlace) return a.TopPlace.CompareTo(b.TopPlace);

                    return b.TotalMark.CompareTo(a.TotalMark);
                });
            }

            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine(Surname);
                Console.WriteLine(Score);
            }
        }

        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participants => _participants;

            public double[] Moods
            {
                get
                {
                    if (_moods == null) return null;
                    double[] copy = new double[_moods.Length];
                    Array.Copy(_moods, copy, _moods.Length);
                    return copy;
                }
            }

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[7];
                if (moods != null)
                {
                    for (int i = 0; i < 7 && i < moods.Length; i++)
                        _moods[i] = moods[i];
                }
                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null) return;
                foreach (var p in participants) Add(p);
            }

            public void Evaluate(double[] marks)
            {
                if (_participants == null || marks == null) return;
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].TotalMark == 0)
                    {
                        for (int j = 0; j < 7 && j < marks.Length; j++)
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        break;
                    }
                }
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] += (i + 1) / 10.0;
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] += _moods[i] * (i + 1) / 100.0;
            }
        }
    }
}
