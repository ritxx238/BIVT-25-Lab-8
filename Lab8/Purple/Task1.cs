namespace Lab8.Purple
{
    public class Task1
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumpIndex;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    double[] coefs = new double[4];
                    for (int i = 0; i < 4; i++) coefs[i] = _coefs[i];
                    return coefs;
                }
            }
            public int[,] Marks
            {
                get
                {
                    int[,] marks = new int[4, 7];
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            marks[i, j] = _marks[i, j];
                        }
                    }
                    return marks;
                }
            }
            public double TotalScore
            {
                get
                {
                    double total = 0;
                    for (int jump = 0; jump < 4; jump++)
                    {
                        int sum = 0;
                        int worstMark = Int32.MaxValue;
                        int bestMark = Int32.MinValue;
                        for (int judge = 0; judge < 7; judge++)
                        {
                            sum += _marks[jump, judge];
                            if (_marks[jump, judge] < worstMark) worstMark = _marks[jump, judge];
                            if (_marks[jump, judge] > bestMark) bestMark = _marks[jump, judge];
                        }

                        sum = sum - worstMark - bestMark;
                        total += sum * _coefs[jump];
                    }
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4];
                for (int i = 0; i < _coefs.Length; i++) _coefs[i] = 2.5;
                _marks = new int[4, 7];
                _jumpIndex = 0;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || coefs.Length != 4) return;
                for (int i = 0; i < 4; i++)
                    _coefs[i] = coefs[i];
            }

            public void Jump(int[] marks)
            {
                if (marks == null || _jumpIndex >= 4) return;
                for (int j = 0; j < 7; j++)
                    _marks[_jumpIndex, j] = marks[j];
                _jumpIndex++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_marks} {_coefs}");
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _index;

            public string Name => _name;

            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = new int[marks.Length];
                for (int i = 0; i < marks.Length; i++) _marks[i] = marks[i];
                _index = 0;
            }

            public int CreateMark()
            {
                int mark = _marks[_index];
                _index++;
                if (_index >= _marks.Length) _index = 0;
                return mark;
            }

            public void Print()
            {
                Console.WriteLine($"{_name}, {_marks}");
            }
        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges => _judges;
            public Participant[] Participants => _participants;

            public Competition(Judge[] judges)
            {
                _judges = judges;
                _participants = new Participant[0];
            }

            public void Evaluate(Participant jumper)
            {
                int[] marks = new int[7];
                for (int i = 0; i < 7; i++)
                {
                    marks[i] = _judges[i].CreateMark();
                }
                jumper.Jump(marks);
            }

            public void Add(Participant participant)
            {
                if (participant == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
                Evaluate(participant);
            }

            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
    } 
}
