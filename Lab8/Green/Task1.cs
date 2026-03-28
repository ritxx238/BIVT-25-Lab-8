using System.Xml.Linq;

namespace Lab8.Green
{
    public class Task1
    {
        public abstract class Participant
        {

            private string _surname;
            private string _group;
            private string _trainer;
            private double _result;

            protected double _standard;
            private static int _passed;


            static public int PassedTheStandard => _passed;

            public string Surname => _surname;
            public string Group => _group;
            public string Trainer => _trainer;
            public double Result => _result;

            public bool HasPassed
            {
                get
                {
                    if (_result == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (_result <= _standard)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }


            static Participant()
            {

                _passed = 0;
            }

            public Participant(string surname, string group, string trainer)
            {
                _surname = surname;
                _group = group;
                _trainer = trainer;
                _result = 0;
            }


            public void Run(double result)
            {
                if (_result == 0)
                {
                    _result = result;
                    if (_result <= _standard)
                    {
                        _passed++;
                    }
                }
            }

            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType,
                string trainer)
            {
                int count = 0;

                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].GetType() == participantType && participants[i].Trainer == trainer)
                    {
                        count++;
                    }
                }

                Participant[] result = new Participant[count];

                int index = 0;

                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].GetType() == participantType && participants[i].Trainer == trainer)
                    {
                        result[index] = participants[i];
                        index++;
                    }
                }

                return result;
            }

            public void Print()
            {
                return;
            }
        }

        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standard = 12;
            }
        }

        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standard = 90;
            }
        }
    }
}


