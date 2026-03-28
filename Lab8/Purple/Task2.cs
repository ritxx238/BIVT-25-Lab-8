namespace Lab8.Purple
{
    public struct Task2
    {
        public struct Participant
        {
            //поля
            private string _name;
            private string _surname;
            private int _distance;
            private int _target;
            private int[] _marks;
            private bool _done;

            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            internal int Target => _target;
            internal bool Done => _done;
            public int[] Marks => (int[])_marks.Clone();
            public int Result
            {
                get
                {
                    if (!_done) return 0;
                    int n = _marks.Length;
                    int total_sum = 0;

                    total_sum = _marks.Sum() - _marks.Min() - _marks.Max();
                    total_sum += 60 + (_distance - _target) * 2;
                    return Math.Max(0, total_sum); 
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                _distance = 0;
                _target = 0;
                _done = false;
            }
            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || marks.Length != 5) return;
                _distance = distance;
                _marks = new int[marks.Length];
                _target = target;
                _done = true;
                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[i] = marks[i];
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                int n = array.Length;

                for (int i = 0; i < n-1; i++)
                {
                    for (int j = 0; j < n-1-i; j++)
                    {
                        if (array[j].Result < array[j+1].Result)
                            (array[j], array[j+1]) = (array[j+1], array[j]);
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"name: {_name}, surname: {_surname}, Result: {Result}");
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
            public void Add(Participant p)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = p;
            }
            public void Add(Participant[] parts)
            {

                foreach (Participant participant in parts)
                {
                    Add(participant);

                }
            }
            public void Jump(int distance, int[] marks)
            {
                if (marks == null || marks.Length !=5) return;
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (!_participants[i].Done)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name}");
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
