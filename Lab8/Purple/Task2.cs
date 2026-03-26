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
                    if (_marks == null) 
                        return default(int[]);
                    int[] Marks = new int[_marks.Length];
                    Array.Copy(_marks, Marks, Marks.Length);
                    return Marks;
                }
            }

            public double Result
            {
                get
                {
                    if (_distance == 0 || _marks == null || _marks.Length == 0) 
                        return 0;
                    
                    
                    int[] sortedMarks = new int[5];
                    Array.Copy(_marks, sortedMarks, 5);
                    Array.Sort(sortedMarks);
                    int _judgeScore = sortedMarks[1] +  sortedMarks[2] + sortedMarks[3];
                    
                    int _distanceScore = 60 + (_distance - _target)*2;

                    return _judgeScore + _distanceScore;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                for (int i = 0; i < 5; i++)
                    _marks[i] = 0;
            }
            
            public void Jump(int distance, int[] marks, int target)
            {
                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[i] = marks[i];
                }
                _distance = distance;
                _target = target;
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Result < array[j + 1].Result)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname} ,дистанция: {_distance}");
                for (int jump = 0; jump < 4; jump++) 
                {
                    Console.WriteLine($"Результат: {Result}");
                }
                
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
                _participants = Array.Empty<Participant>();
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }

            public void Add(Participant[] participants)
            {
                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }

            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Result == 0)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }
            
            public void Print()
            {
                Console.WriteLine($"Имя: {_name}, норматив: {_standard} , " +
                                  $"участники: {string.Join(", ", _participants)})");
            }
        }

        public class JuniorSkiJumping: SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100)
            {
                
            }
        }

        public class ProSkiJumping: SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) 
            {
                
            }
        }
    }
}