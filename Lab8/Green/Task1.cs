using System.Runtime.CompilerServices;

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
            protected double Norm;
            private static int _passed;

            public string Surname => _surname;
            public string Group => _group;
            public string Trainer => _trainer;
            public double Result => _result;
            public static int PassedTheStandard => _passed;

            public bool HasPassed
            {
                get
                {
                    if (_result == 0)
                    {
                        return false;
                    }

                    return _result <= Norm;
                }
            }

            public Participant(string surname, string group, string trainer)
            {
                _surname = surname;
                _group = group;
                _trainer = trainer;
            }

            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                List<Participant> result = new List<Participant>();
                
                for (int i = 0; i < participants.Length; i++)
                {
                    if ((participants[i].Trainer == trainer) && (participants[i].GetType() == participantType))
                    {
                        result.Add(participants[i]);
                    }
                }
                return result.ToArray();;
            }

            public void Run(double result)
            {
                if (_result == 0)
                {
                    _result = result;

                    if (result <= Norm)
                    {
                        _passed++;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine("Surname:" + Surname);
                Console.WriteLine("Trainer:" + Trainer);
                Console.WriteLine("Group:" + Group);
                Console.WriteLine("Result:" + Result);
                string res;
                if (HasPassed == true)
                {
                    res = "да";
                }
                else
                {
                    res = "нет";
                }

                Console.WriteLine("Ïðîøëà íîðìàòèâ:" + res);
            }
        }

        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                Norm = 12;
            }
        }
        
        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                Norm = 90;
            }
        }
    }
}
