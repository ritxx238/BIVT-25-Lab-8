namespace Lab8.Purple
{
    public class Task2
    {
        public struct Participant
        {
            //Поля
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _target;
            //Свойства
            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks => _marks.ToArray();

            public int Result
            {
                get
                {
                    int sum = 60, minMark = 100, maxMark = -100;
                    foreach (int mark in _marks)
                    {
                        sum += mark;
                        minMark = Math.Min(minMark, mark);
                        maxMark = Math.Max(maxMark, mark);
                    }
                    sum -= minMark + maxMark;
                    sum += (_distance - _target) * 2;
                    sum = Math.Max(sum, 0);
                    return sum;
                }
            }
            //Конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _target = 30;
            }
            public void Jump(int distance, int[] marks, int target)
            {
                _target = target;
                _distance = distance;
                for (int i = 0; i < 5; i++)
                {
                    _marks[i] = marks[i];
                }
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = i; j > 0 && array[j].Result > array[j - 1].Result; j--)
                    {
                        (array[j], array[j - 1]) = (array[j - 1], array[j]);
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {this.Result}");
            }
        }
        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;
            private int _jumpernumber;
            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;
            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
                _jumpernumber = 0;
            }
            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length+1);
                _participants[^1] = participant;
            }
            public void Add(Participant[] participants)
            {
                foreach (Participant participant in participants)
                {
                    this.Add(participant);
                }
            }
            public void Jump(int distance, int[] marks)
            {
                if (_participants.Length > _jumpernumber)
                    _participants[_jumpernumber++].Jump(distance, marks, _standard);
            }
            public void Print()
            {

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
