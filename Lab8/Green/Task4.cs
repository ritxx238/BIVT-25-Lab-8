namespace Lab8.Green
{
    public class Task4
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _jumps;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Jumps
            {
                get
                {
                    double[] copy = new double[_jumps.Length];

                    for (int i = 0; i < _jumps.Length; i++)
                    {
                        copy[i] = _jumps[i];
                    }

                    return copy;
                }
            }

            public double BestJump
            {
                get
                {
                    double best = 0;
                    for (int i = 0; i < _jumps.Length; i++)
                    {
                        if (_jumps[i] > best)
                        {
                            best = _jumps[i];
                        }
                    }

                    return best;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _jumps = new double[3];
            }

            public void Jump(double result)
            {
                for (int i = 0; i < _jumps.Length; i++)
                {
                    if (_jumps[i] == 0)
                    {
                        _jumps[i] = result;
                        return;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i].BestJump < array[j].BestJump)
                        {
                            (array[i], array[j]) = (array[j], array[i]);
                        }
                    }
                }
            }

            public void Print()
            {
                return;
            }
        }
        public abstract class Discipline
        {
            private string _name;

            protected Participant[] _participants;

            public string Name => _name;

            public Participant[] Participants
            {
                get
                {
                    Participant[] copy = new Participant[_participants.Length];
                    Array.Copy(_participants, copy, _participants.Length);
                    return copy;
                }
            }

            protected Discipline(string name)
            {
                _name = name;
                _participants = new Participant[0];
            }
            public void Add(Participant participant)
            {
                Participant[] newArray = new Participant[_participants.Length + 1];
                Array.Copy(_participants, newArray, _participants.Length);
                newArray[_participants.Length] = participant;
                _participants = newArray;
            }

            public void Add(Participant[] participants)
            {
                Participant[] newArray = new Participant[_participants.Length + participants.Length];
                Array.Copy(_participants, newArray, _participants.Length);
                Array.Copy(participants, 0, newArray, _participants.Length, participants.Length);
                _participants = newArray;
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }

            public abstract void Retry(int index);

            public void Print()
            {
                return;
            }
        }

        public class LongJump : Discipline
        {
            public LongJump() : base("Long jump")
            {
            }

            public override void Retry(int index)
            {
                Participant p = _participants[index];
                double best = p.BestJump;

                Participant newParticipant = new Participant(p.Name, p.Surname);

                newParticipant.Jump(best);

                _participants[index] = newParticipant;
            }
        }

        public class HighJump : Discipline
        {
            public HighJump() : base("High jump")
            {
            }

            public override void Retry(int index)
            {
                Participant p = _participants[index];

                double[] jumps = p.Jumps;

                Participant newParticipant = new Participant(p.Name, p.Surname);

                for (int i = 0; i < 2 && i < jumps.Length && jumps[i] != 0; i++)
                {
                    newParticipant.Jump(jumps[i]);
                }

                _participants[index] = newParticipant;
            }
        }
    }
}
