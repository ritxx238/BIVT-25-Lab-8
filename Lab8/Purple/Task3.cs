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
            private int _topplace;
            private double _totalmark;
            private int n;
            public string Name => _name;
            public string Surname => _surname;

            public double[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    double[] marks = new double[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        marks[i] = _marks[i];
                    }

                    return marks;
                }
            }

            public int[] Places
            {
                get
                {
                    if (_places == null) return null;
                    int[] places = new int[_places.Length];
                    for (int i = 0; i < _places.Length; i++)
                    {
                        places[i] = _places[i];
                    }

                    return places;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_places == null) return default(int);
                    _topplace = _places[0];
                    for (int i = 1; i < _places.Length; i++)
                    {
                        if (_places[i] < _topplace)
                        {
                            _topplace = _places[i];
                        }
                    }

                    return _topplace;
                }
            }

            public double TotalMark
            {
                get
                {
                    double ans = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        ans += _marks[i];
                    }

                    return ans;
                }
            }

            public int Score
            {
                get
                {
                    int ans = 0;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        ans += _places[i];
                    }

                    return ans;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _topplace = 0;
                _totalmark = 0;
            }

            public void Evaluate(double result)
            {
                if (_marks == null) return;
                if (n < _marks.Length)
                {
                    _marks[n] = result;
                    n++;
                }
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;
                double[,] matrix = new double[participants.Length, 7];
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = participants[i]._marks[j];
                    }
                }

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int i1 = 0;
                    while (i1 < matrix.GetLength(0))
                    {
                        if (i1 == 0 || matrix[i1, j] <= matrix[i1 - 1, j])
                        {
                            i1++;
                        }
                        else
                        {
                            double tmp = matrix[i1, j];
                            matrix[i1, j] = matrix[i1 - 1, j];
                            matrix[i1 - 1, j] = tmp;
                            i1--;
                        }
                    }
                }

                int count = 0, _i = 0, _j = 0;
                while (count < matrix.Length)
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        if (participants[_i]._marks[_j] == matrix[i, _j])
                        {
                            participants[_i]._places[_j] = i + 1;
                        }
                    }

                    count++;
                    _i++;
                    if (_i == matrix.GetLength(0))
                    {
                        _i = 0;
                        _j++;
                    }
                }

                int i2 = 0;
                while (i2 < participants.Length)
                {
                    if (i2 == 0 || participants[i2]._places[6] >= participants[i2 - 1]._places[6])
                    {
                        i2++;
                    }
                    else
                    {
                        Participant tmp = participants[i2];
                        participants[i2] = participants[i2 - 1];
                        participants[i2 - 1] = tmp;
                        i2--;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                int i1 = 0;
                while (i1 < array.Length)
                {
                    if (i1 == 0 || array[i1].Score > array[i1 - 1].Score)
                    {
                        i1++;
                    }
                    else if (array[i1].Score == array[i1 - 1].Score)
                    {
                        int count = 0;
                        for (int i = 0; i < array[i]._places.Length; i++)
                        {
                            if (array[i1]._places[i] <= array[i1 - 1]._places[i])
                            {
                                count++;
                            }
                        }

                        if (count >= 4 && count != 7)
                        {
                            i1++;
                        }
                        else if (count == 7)
                        {
                            if (array[i1].TotalMark >= array[i1 - 1].TotalMark)
                            {
                                i1++;
                            }
                            else
                            {
                                Participant tmp = array[i1];
                                array[i1] = array[i1 - 1];
                                array[i1 - 1] = tmp;
                                i1--;
                            }
                        }
                        else
                        {
                            Participant tmp = array[i1];
                            array[i1] = array[i1 - 1];
                            array[i1 - 1] = tmp;
                            i1--;
                        }
                    }
                    else
                    {
                        Participant tmp = array[i1];
                        array[i1] = array[i1 - 1];
                        array[i1 - 1] = tmp;
                        i1--;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine(
                    $"Name: {Name},\nSurname: {Surname},\nScore: {Score},\nTopPlace: {TopPlace},\nTotalMark: {TotalMark}");
            }
        }

        public abstract class Skating
        {
            private Participant[] _participants;
            protected double[] _moods;
            private int _participantNo;
            public Participant[] Participants => _participants;

            public double[] Moods
            {
                get
                {
                    if (_moods == null) return null;
                    double[] moods = new double[_moods.Length];
                    for (int i = 0; i < _moods.Length; i++)
                    {
                        moods[i] = _moods[i];
                    }

                    return moods;
                }
            }

            public Skating(double[] moods)
            {
                _moods = moods;
                _participants = new Participant[0];
                _participantNo = 0;
                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                if (marks == null || _participants == null) return;
                if (_participantNo < _participants.Length)
                {
                    for (int i = 0; i < marks.Length; i++)
                    {
                        _participants[_participantNo].Evaluate(marks[i] * _moods[i]);
                    }

                    _participantNo++;
                }
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null) return;
                foreach (Participant p in participants)
                {
                    Add(p);
                }
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods){}

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += (i + 1) * 1.0 / 10;
                }
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods){}

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] = (int)(_moods[i]) + (i + 1) * 1.0 / 100;
                }
            }
        }
    }
}
