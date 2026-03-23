using System;

namespace Lab8.Purple
{
    public class Task2
    {
        public struct Participant
        {
            private string _name; private string _surname;
            private int _dist; private int[] _marks; private int _result;
            private int _target;

            public string Name => _name; public string Surname => _surname;
            public int Distance => _dist; public int[] Marks => (int[])_marks.Clone();
            public int Result
            {
                get
                {
                    if (_marks==null || _dist==0) return 0;
                    int sum = 0, min = int.MaxValue, max = int.MinValue;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] < min) min = _marks[i];
                        if (_marks[i] > max) max = _marks[i];
                        sum += _marks[i];
                    }
                    int range = 60+(_dist - _target) * 2;
                    _result = Math.Max(range + sum - min - max, 0);
                    return _result;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name; _surname = surname; _dist = 0; _marks = new int[5];
            }
            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || marks.Length != 5) return;
                _dist = distance; _target = target;
                _marks = (int[])marks.Clone();

            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length - 1; i++)
                    for (int j = 0; j < array.Length - i - 1; j++)
                        if (array[j].Result < array[j + 1].Result)
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
            }
            public void Print()
            { Console.Write($"Name: {_name} Surname: {_surname} Distance: {_dist} Marks: {string.Join(" ", _marks)} Result: {_result}"); }
        }
        public abstract class SkiJumping
        {
            protected string _name; protected int _standard;
            protected Participant[] _participants; protected int _jump;
            public string Name => _name; public int Standard => _standard; public Participant[] Participants => _participants;
            public SkiJumping(string name, int standard)
            {
                _name = name; _standard = standard; 
                _participants = Array.Empty<Participant>(); _jump = 0;
            }
            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }
            public void Add(Participant[] participants)
            {
                foreach (Participant participant in participants)
                    Add(participant);
            }
            public void Jump(int distance, int[] marks)
            {
                if (marks == null || marks.Length == 0 || _jump >=_participants.Length) return;
                var participant = _participants[_jump];
                participant.Jump(distance, marks, _standard);
                _participants[_jump++] = participant;
            }
            public void Print()
            { foreach (var i in _participants)   i.Print(); }
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
