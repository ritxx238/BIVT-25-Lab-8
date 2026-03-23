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
            private int _topPlace;
            private double _totalMark;
            private int _marksCount;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks => _marks.ToArray();
            public int[] Places => _places.ToArray();
            public int TopPlace => _topPlace;
            public double TotalMark => _totalMark;
            public int Score
            {
                get
                {
                    int sum = 0;
                    foreach (var p in _places) sum += p;
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _topPlace = 0;
                _totalMark = 0;
                _marksCount = 0;
            }

            public void Evaluate(double result)
            {
                if (_marks == null) _marks = new double[7];
                if (_marksCount < 7)
                {
                    _marks[_marksCount] = result;
                    _totalMark += result;
                    _marksCount++;
                }
            }

            public static void SetPlaces(Participant[] participants)
            {
                int n = participants.Length;
                for (int j = 0; j < 7; j++)
                {
                    int[] indices = new int[n];
                    for (int i = 0; i < n; i++) indices[i] = i;

                    Array.Sort(indices, (a, b) =>
                    {
                        var marksA = participants[a].Marks;
                        var marksB = participants[b].Marks;
                        return marksB[j].CompareTo(marksA[j]);
                    });

                    for (int rank = 0; rank < n; rank++)
                    {
                        int idx = indices[rank];
                        int place = rank + 1;
                        participants[idx]._places[j] = place;
                        if (j == 0 || place < participants[idx]._topPlace)
                            participants[idx]._topPlace = place;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 1; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;
                    while (j >= 0 && ShouldSwap(array[j], key))
                    {
                        array[j + 1] = array[j];
                        j--;
                    }
                    array[j + 1] = key;
                }
            }

            private static bool ShouldSwap(Participant left, Participant right)
            {
                if (left.Score > right.Score) return true;
                if (left.Score < right.Score) return false;
                if (left.TopPlace > right.TopPlace) return true;
                if (left.TopPlace < right.TopPlace) return false;
                return left.TotalMark < right.TotalMark;
            }

            public void Print()
            {
                Console.WriteLine($"{_surname} {_name}: Score {Score}, Top {TopPlace}, Total {TotalMark:F2}");
            }
        }

        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participants => _participants;
            public double[] Moods => _moods.ToArray();

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[7];
                
                for (int i = 0; i < Math.Min(7, moods.Length); i++)
                    _moods[i] = moods[i];

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
                foreach (var p in participants) Add(p);
            }

            public void Evaluate(double[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (GetMarksCount(i) < 7) 
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        }
                        break;
                    }
                }
            }

            private int GetMarksCount(int index)
            {
                var m = _participants[index].Marks;
                if (_participants[index].TotalMark != 0) return 7;
                return 0;
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
                    _moods[i] *= (1.0 + (i + 1) / 100.0);
            }
        }
    }
}