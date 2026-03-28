using System.Xml.Linq;
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
            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks => _marks.ToArray();
            public int[] Places => _places.ToArray();
            public int TopPlace => _topPlace;
            public double TotalMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;

                    double total = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        total += _marks[i];
                    }

                    return total;
                }
            }
            public int Score
            {
                get
                {
                    if (_places == null || _places.Length == 0) return 0;

                    int score = 0;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        score += _places[i];
                    }

                    return score;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _topPlace = 8;
                _totalMark = 0;
            }

            public void Evaluate(double result)
            {
                if (_marks == null) return;

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0.0)
                    {
                        _marks[i] = result;
                        _totalMark += result;
                        return;
                    }
                }
            }
            public static void SetPlaces(Participant[] participants)
            {
                int n = participants.Length;
                if (n == 0 || participants == null) return;
                for (int judge = 0; judge < 7; judge++)
                {
                    Array.Sort(participants, (a, b) => b._marks[judge].CompareTo(a._marks[judge]));
                    for (int p = 0; p < n; p++)
                    {
                        participants[p]._places[judge] = p + 1;
                        if (judge == 0 || (p + 1) < participants[p].TopPlace)
                        {
                            participants[p]._topPlace = p + 1;
                        }
                    }
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                Array.Sort(array, (a, b) =>
                {
                    if (a.Score != b.Score) return a.Score.CompareTo(b.Score);
                    else if (a.TopPlace != b.TopPlace) return a.TopPlace.CompareTo(b.TopPlace);
                    return b.TotalMark.CompareTo(a.TotalMark); //оценка => убывание
                });
            }
            public void Print()
            {
                Console.WriteLine($"Participant: {Name} {Surname}");
                Console.WriteLine($"Score: {Score}");
            }
        }
        public abstract class Skating
        {
            private Participant[] _participants;
            protected double[] _moods;
            public Participant[] Participants => _participants;
            public double[] Moods => _moods;

            public Skating(double[] moods)
            {
                if (moods == null || moods.Length != 7) return;

                _moods = (double[])moods.Clone();
                _participants = new Participant[0];
                ModificateMood();
            }
            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {
                if (marks == null || marks.Length != 7) return;

                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Marks[0] == 0)
                    {
                        for (int j = 0; j < marks.Length; j++)
                        {
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        }
                        break;
                    }
                }
            }
            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }
            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                foreach (var participant in participants)
                {
                    Add(participant);
                }
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods)
            {
            }

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += (double) (i + 1) / 10.0;
                }
            }
        }
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods)
            {
            }

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += _moods[i] * (i + 1) / 100.0;
                }
            }
        }
    }
}
