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
                    if (_marks == null) return null;
                    int[] marks = new int[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
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
                    if (_marks == null || _distance == 0) return default(int);
                    int score = 0;
                    int imax = 0;
                    for (int i = 1; i < _marks.Length; i++)
                    {
                        if (_marks[i] > _marks[imax])
                        {
                            imax = i;
                        }
                    }

                    int imin = 0;
                    for (int i = 1; i < _marks.Length; i++)
                    {
                        if (_marks[i] < _marks[imin])
                        {
                            imin = i;
                        }
                    }

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (i == imax || i == imin) continue;
                        score += _marks[i];
                    }

                    int b = 60;
                    if ((_distance - _target) > 0)
                    {
                        b += (_distance - _target) * 2;
                    }
                    else
                    {
                        b -= (_target - _distance) * 2;
                    }
                    
                    score += b;
                    return score;
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
                _distance = distance;
                _target = target;
                if (marks == null || marks.Length != _marks.Length) return;
                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[i] = marks[i];
                }
                
            }
            
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                int i = 0;
                while (i < array.Length)
                {
                    if (i == 0 || array[i].Result <= array[i - 1].Result)
                    {
                        i++;
                    }
                    else
                    {
                        Participant tmp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = tmp;
                        i--;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name: {Name},\nSurname: {Surname},\nResult: {Result}");
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;
            private int _participantNo;
            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
                _participantNo = 0;
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants,_participants.Length+1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null) return;
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }

            public void Jump(int distance, int[] marks)
            {
                if (marks == null || _participants == null) return;
                if (_participantNo < _participants.Length)
                {
                    _participants[_participantNo].Jump(distance,marks,_standard);
                    _participantNo++;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name: {Name},\nStandard: {Standard},\nParticipants:");
                for (int i = 0; i < _participants.Length; i++)
                {
                    _participants[i].Print();
                }
                
            }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping():base("100m",100){}
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping():base("150m",150){}
        }
    }
}
