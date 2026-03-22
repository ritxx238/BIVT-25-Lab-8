using System;
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
            private int _target;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;

            public int[] Marks
            {
                get
                {
                    if (_marks == null || _marks.Length == 0)
                        return new int[0];
                    int[] copy = new int[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int Result
            {
                get
                {
                    if (_distance == 0) return 0;
                    if (_marks == null || _marks.Length < 3) return 0;

                    int[] copy = (int[])_marks.Clone();
                    Array.Sort(copy);

                    int sum = 60;
                    for (int i = 1; i < copy.Length - 1; i++)
                        sum += copy[i];

                    if (_target > 0)
                        sum += (_distance - _target) * 2;

                    return Math.Max(0, sum);
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _target = 0;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                _distance = distance;
                _target = target;
                _marks = marks != null ? (int[])marks.Clone() : new int[0];
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                var sorted = array.OrderByDescending(x => x.Result).ToArray();
                Array.Copy(sorted, array, array.Length);
            }

            public void Print() => Console.WriteLine($"{Name} {Surname}: {Result}");
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            protected Participant[] _participants;

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
                if (participants == null) return;
                foreach (var p in participants) Add(p);
            }

            public void Jump(int distance, int[] marks)
            {
                if (_participants == null) return;

                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Distance == 0)
                    {
                        ref var participant = ref _participants[i];
                        participant.Jump(distance, marks, _standard);
                        break;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name}");
                if (_participants != null)
                {
                    foreach (var p in _participants)
                        p.Print();
                }
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
