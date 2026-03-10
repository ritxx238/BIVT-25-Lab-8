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
            private bool _counted;

            public string Name => _name;
            public string Surname => _surname;
            internal bool Counted => _counted;
            public double[] Marks
            {
                get
                {
                    double[] marks = new double[7];
                    Array.Copy(_marks, marks, 7);
                    return marks;
                }
            }
            public int[] Places
            {
                get
                {
                    int[] places = new int[7];
                    Array.Copy(_places, places, 7);
                    return places;
                }
            }

            public int TopPlace => _places.Min();
            public double TotalMark => _marks.Sum();
            public int Score
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        sum += _places[i];
                    }
                    return sum;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _counted = false;
            }
            int count = 0;
            public void Evaluate(double result)
            {
                _marks[count++] = result;
                _counted = true;
            }
            public static void SetPlaces(Participant[] participants)
            {
                Participant[] p = new Participant[participants.Length];
                for (int k = 0; k < participants[0]._marks.Length; k++)
                {
                    double[] marks = new double[participants.Length];
                    for (int i = 0; i < marks.Length; i++)
                    {
                        marks[i] = participants[i]._marks[k];
                    }

                    for (int i = 0; i < marks.Length; i++)
                    {
                        for (int j = i; j < marks.Length; j++)
                        {
                            if (marks[i] < marks[j])
                            {
                                (marks[i], marks[j]) = (marks[j], marks[i]);
                            }
                        }
                    }
                    for (int r = 0; r < participants.Length; r++)
                    {
                        double mark = participants[r]._marks[k];
                        for (int i = 0; i < marks.Length; i++)
                        {
                            if (mark == marks[i])
                            {
                                participants[r]._places[k] = i + 1;
                                break;
                            }
                        }
                    }
                }

                for (int i = 0; i < participants.Length; i++)
                {
                    for (int j = i; j < participants.Length; j++)
                    {
                        if (participants[i]._places[^1] > participants[j]._places[^1])
                        {
                            (participants[i], participants[j]) = (participants[j], participants[i]);
                        }
                    }
                }
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i].Score > array[j].Score)
                        {
                            (array[i], array[j]) = (array[j], array[i]);

                        }
                        else if (array[i].Score == array[j].Score)
                        {
                            if (array[i].TopPlace < array[j].TopPlace)
                            {
                                (array[i], array[j]) = (array[j], array[i]);
                            }
                            else if (array[i].TopPlace == array[j].TopPlace)
                            {
                                if (array[i].TotalMark < array[j].TotalMark)
                                {
                                    (array[i], array[j]) = (array[j], array[i]);
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
            protected Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participants => _participants;
            public double[] Moods => _moods;

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[7];
                Array.Copy(moods, _moods, moods.Length);
                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                for (int i = 0; i< _participants.Length; i++)
                {
                    if (!_participants[i].Counted)
                    {
                        for (int j = 0; j < _moods.Length; j++)
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
                _participants[^1] = participant;
            }
            public void Add(Participant[] participants)
            {
                int len = _participants.Length;
                int count = 0;
                Array.Resize(ref _participants, _participants.Length + participants.Length);
                for (int i = len; i < _participants.Length; i++)
                {
                    _participants[i] = participants[count++];
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

                for (int i = 0; i< _moods.Length; i++)
                {
                    Console.WriteLine(_moods[i]);
                    _moods[i] += (double)(i + 1) / 10.0;
                    Console.WriteLine(_moods[i]);
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
                    _moods[i] *= (1+(i+1)/100.0);
                }
            }
        }
    }
}
