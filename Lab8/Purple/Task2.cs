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
            public int[] Marks => _marks.ToArray();

            public int Result
            {
                get
                {
                    if (_distance == 0 || _marks == null || _marks.Length == 0) 
                        return 0;

                    int minMark = _marks[0];
                    int maxMark = _marks[0];
                    int sumMarks = 0;

                    foreach (var m in _marks)
                    {
                        sumMarks += m;
                        if (m < minMark) minMark = m;
                        if (m > maxMark) maxMark = m;
                    }
                    int styleScore = sumMarks - minMark - maxMark;
                    int distanceScore = 60 + (_distance - _target) * 2;

                    int total = styleScore + distanceScore;
                    return total < 0 ? 0 : total;
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
                _marks = new int[5];
                for (int i = 0; i < Math.Min(5, marks.Length); i++)
                {
                    _marks[i] = marks[i];
                }
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 1; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;
                    while (j >= 0 && array[j].Result < key.Result)
                    {
                        array[j + 1] = array[j];
                        j--;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_surname} {_name}: {Result}");
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
                foreach (var p in participants) Add(p);
            }

            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Distance == 0)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }

            public virtual void Print()
            {
                Console.WriteLine($"{_name} ({_standard}m):");
                foreach (var p in _participants) p.Print();
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