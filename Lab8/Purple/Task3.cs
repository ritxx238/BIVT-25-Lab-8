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
            public int TopPlace => _places.Min();
            public double TotalMark => _marks.Sum();
            public double[] Marks
            {
                get
                {
                    double[] marks = new double[7];
                    for (int i = 0; i < 7; i++) marks[i] = _marks[i];
                    return marks;
                }
            }
            public int[] Places
            {
                get
                {
                    int[] places = new int[7];
                    for (int i = 0; i < 7; i++) places[i] = _places[i];
                    return places;
                }
            }
            public int Score
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < 7; i++)
                        sum += _places[i];
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
            }

            public void Evaluate(double result)
            {
                int count = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (_marks[i] != 0) count++;
                }
                if (count >= 7) return;
                _marks[count] = result;
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

                        if (judge == 0 || (i + 1) < participants[i]._topPlace)
                            participants[i]._topPlace = i + 1;
                    }
                }
            }

            private static void Swap(Participant[] array, int j)
            {
                (array[j], array[j + 1]) = (array[j + 1], array[j]);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Score > array[j + 1].Score)
                        {
                            Swap(array, j);
                        }
                        else if (array[j].Score == array[j + 1].Score)
                        {
                            if (array[j].TopPlace > array[j + 1].TopPlace)
                            {
                                Swap(array, j);
                            }
                            else if (array[j].TopPlace == array[j + 1].TopPlace)
                            {
                                if (array[j].TotalMark < array[j + 1].TotalMark)
                                {
                                    Swap(array, j);
                                }
                            }
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_marks} {_places} {_topPlace} {_totalMark}");
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
                _participants = new Participant[0];
                _moods = new double[7];
                for (int i = 0; i < 7; i++) _moods[i] = moods[i];
                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                if (marks == null) return;

                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].TotalMark == 0)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        }
                        return;
                    }
                }
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                for (int i = 0; i < 7; i++)
                {
                    _moods[i] += (i+1) / 10.0;
                }
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                for (int i = 0; i < 7; i++)
                {
                    _moods[i] += _moods[i] * (i+1) / 100.0;
                }
            }
        }
    }
}
