namespace Lab8.Purple
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _target;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks
            {
                get
                {
                    int[] marks = new int[5];
                    for (int i = 0; i < 5; i++) marks[i] = _marks[i];
                    return marks;
                }
            }
            public double Result
            {
                get
                {
                    if (_distance == 0) return 0;
                    double result = 0;
                    int sum = 0;
                    int worstMark = Int32.MaxValue;
                    int bestMark = Int32.MinValue;
                    for (int i = 0; i < 5; i++)
                    {
                        sum += _marks[i];
                        if (_marks[i] < worstMark) worstMark = _marks[i];
                        if (_marks[i] > bestMark) bestMark = _marks[i];
                    }
                    sum = sum - worstMark - bestMark;
                    int extraPoints = 60 + (_distance - _target) * 2;
                    result = sum + extraPoints;
                    return result;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                _distance = 0;
                _target = 0;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null) return;
                _distance = distance;
                _target = target;
                for (int i = 0; i < 5; i++)
                {
                    _marks[i] = marks[i];
                }
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Result < array[j + 1].Result)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_marks} {_distance}");
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;

            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;

            public SkiJumping(string name, int standart)
            {
                _name = name;
                _standard = standart;
                _participants = new Participant[0];
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

            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Distance == 0)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        return;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name}, {_standard}");
            }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }
    }
}
