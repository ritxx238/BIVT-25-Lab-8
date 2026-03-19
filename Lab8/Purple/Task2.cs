namespace Lab8.Purple
{
    public class Task2
    {
        public struct Participant
        {

            // приватные поля Name, Surname, Distance и Marks

            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _target;

            // свойства 
            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;

            public int[] Marks
            {
                get
                {
                    int[] marks = new int[_marks.Length];
                    for (int i = 0; i < marks.Length; i++)
                    {
                        marks[i] = _marks[i];
                    }
                    return marks;
                }
            }

            public int Result
            {
                get
                {
                    int mx = -1000;
                    int mn = 10000;
                    int sum = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] > mx)
                        {
                            mx = _marks[i];
                        }
                        if (_marks[i] < mn)
                        {
                            mn = _marks[i];
                        }
                        sum += _marks[i];
                    }
                    sum -= mx;
                    sum -= mn;
                    if (_distance > _target)
                    {
                        sum = sum + 60 + ((_distance - _target) * 2);
                    }
                    else if (_distance < _target)
                    {
                        sum = sum + 60 - ((_target - _distance) * 2);
                    }
                    if (sum < 0)
                    {
                        sum = 0;
                    }
                    return sum;
                }

            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
            }
            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || marks.Length != 5) return;
                _distance = distance;
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
                Console.WriteLine(_name);
                Console.WriteLine(_surname);
            }
        }
        public abstract  class SkiJumping
        {
            // для названия соревнования, норматива дистанции и списка участников.
            protected string _name;
            protected int _standard;
            protected Participant[] _parts;
            protected int _count;

            // Создать свойства Name, Standard и Participants для чтения приватных полей.
            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _parts;

            // Конструктор должен получать название соревнования и норматив дистанции, а также инициализировать массив участников.
            public SkiJumping(string name, int standart)
            {
                _name = name;
                _standard = standart;
                _parts = new Participant[0];
                _count = 0;
            }
            // Создать методы Add, которые получают одного или несколько участников и добавляют их в хранимый массив участников.
            public void Add(Participant participant)
            {
                 Array.Resize(ref _parts, _parts.Length + 1);
                _parts[_parts.Length-1] = participant;
            
            }
            public void Add(Participant[] participants)
            {
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
//            Создать метод public void Jump(int distance, int[] marks), который вызывает у первого ещё не
//прыгнувшего спортсмена метод Jump и передает ему полученные данные.Расчет дополнительных
//баллов должен зависеть от норматива соревнования.
            public void Jump(int distance, int[] marks)
            {
                _parts[_count].Jump(distance, marks, _standard);
                _count++;


            }
            public void Print()
            {
                Console.WriteLine(_name);
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
