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
            public int[] Marks => _marks.ToArray();
            public int Result
            {
                get
                {
                    if (_marks == null || _marks.Length == 0 || _distance == 0 || _target == 0) return 0;

                    int[] marks = (int[]) _marks.Clone();
                    for (int i = 0; i < marks.Length; i++)
                    {
                        for (int j = 1; j < marks.Length; j++)
                        {
                            if (marks[j - 1] > marks[j])
                            {
                                (marks[j - 1], marks[j]) = (marks[j], marks[j - 1]);
                            }
                        }
                    }

                    int sum = 0;
                    for (int i = 1; i < marks.Length - 1; i++)
                    {
                        sum += marks[i];
                    }

                    int distancePoints = (_distance - _target) * 2 + 60;

                    return sum + distancePoints;
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
                if (marks == null || marks.Length != 5) return;

                _distance = distance;
                _marks = marks;
                _target = target;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].Result < array[j].Result)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Participant: {Name} {Surname}");
                Console.WriteLine($"Result: {Result}");
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
            public void Jump(int distance, int[] marks)
            {
                if (_participants == null || _participants.Length == 0) return;

                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Distance == 0)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Name: {_name}");
                Console.WriteLine($"Standard: {_standard}");
                foreach(var participant in _participants)
                {
                    Console.Write($"{participant} ");
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
