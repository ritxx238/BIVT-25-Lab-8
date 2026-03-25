using System.Linq;

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
            private int _result;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;

            public int[] Marks
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return null;
                    int[] copy = new int[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int Result => _result;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _result = 0;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || marks.Length != 5) return;
                _distance = distance;
                _marks = marks;
                int distScore;
                if (_distance == target)
                {
                    distScore = 60;
                }
                else if (_distance > target)
                {
                    distScore = 60 + (_distance - target) * 2;
                }
                else
                {
                    distScore = 60 - (target - _distance) * 2;
                }
                _result = _marks.Sum() - _marks.Max() - _marks.Min() + distScore;
                if (_result < 0) _result = 0;
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
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
                Console.WriteLine($"Participant: {_surname} {_name}");
                Console.WriteLine($"Distance: {_distance}");
                Console.WriteLine($"Result: {Result}");
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;
            private int _indexOfParticipant;
            
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
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants)
            {
                int oldLength = _participants.Length;
                Array.Resize(ref _participants, _participants.Length + participants.Length);
                Array.Copy(participants, 0, _participants, oldLength, participants.Length);
            }

            public void Jump(int distance, int[] marks)
            {
                _participants[_indexOfParticipant].Jump(distance, marks, _standard);
                _indexOfParticipant++;
            }

            public void Print()
            {
                Console.WriteLine($"Contest's name: {_name}");
                Console.WriteLine($"Contest's standart: {_standard}");
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
