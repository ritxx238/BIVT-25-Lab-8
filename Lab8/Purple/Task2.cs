using System.Text;

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
            private bool _counted;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            internal bool Counted => _counted;

            public int[] Marks
            {
                get
                {
                    int[] result = new int[_marks.Length];
                    Array.Copy(_marks, result, _marks.Length);
                    return result;
                }
            }

            public int Result
            {
                get
                {
                    if (!_counted) return 0;
                    int marks = _marks.Sum() - _marks.Min() - _marks.Max();
                    int jump = 60 + (_distance - _target) * 2;
                    return marks + jump;
                }

            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                _counted = false;
                _target = 120;
            }
            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || marks.Length == 0) return;
                _distance = distance;
                _counted = true;
                _marks = new int[marks.Length];
                _target = target;
                for (int i = 0; i < _marks.Length; i++)
                {
                    _marks[i] = marks[i];
                }
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = i; j < array.Length; j++)
                    {
                        if (array[i].Result < array[j].Result)
                        {
                            (array[i], array[j]) = (array[j], array[i]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_marks} {_distance} ");
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

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
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

            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i< _participants.Length; i++)
                {
                    if (!_participants[i].Counted)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_standard} {_participants}");
            }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100)
            {
            }
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150)
            {
            }
        }
    }
}
