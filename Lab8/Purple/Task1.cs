using System.Text.RegularExpressions;
using static Lab8.Purple.Task1;

namespace Lab8.Purple
{
    public class Task1
    {
        public class Participant
        {

            //Name, Surname, Coefs и Marks
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    double[] coefs = new double[_coefs.Length];
                    for (int i = 0; i < coefs.Length; i++)
                    {
                        coefs[i] = _coefs[i];
                    }
                    return coefs;
                }
            }
            public int[,] Marks
            {
                get
                {
                    int[,] marks = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < marks.GetLength(1); j++)
                        {
                            marks[i, j] = _marks[i, j];
                        }
                    }
                    return marks;
                }
            }
            public double TotalScore
            {
                get
                {
                    double itog = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        double mx = -1000;
                        double mn = 1000;
                        double sum = 0;
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            if (_marks[i, j] > mx)
                            {
                                mx = _marks[i, j];
                            }
                            if (_marks[i, j] < mn)
                            {
                                mn = _marks[i, j];
                            }
                            sum += _marks[i, j];
                        }
                        sum -= mn;
                        sum -= mx;
                        itog += sum * _coefs[i];

                    }
                    return itog;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
            }
            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || coefs.Length != 4) return;
                for (int i = 0; i < coefs.Length; i++)
                {
                    _coefs[i] = coefs[i];
                }
            }
            int k = 0;
            public void Jump(int[] marks)
            {
                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[k, i] = marks[i];
                }
                k++;
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].TotalScore < array[j].TotalScore)
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
                Console.WriteLine(TotalScore);
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        Console.WriteLine(_marks[i, j] + " ");
                    }
                }
            }


        }
        public class Judge
        {
            private string _name;
            private int[] _coefs;
            private int _count;

            // свойство 
            public string Name => _name;

            // конструктор 
            public Judge(string name, int[] coefs)
            {
                _name = name;
                _coefs = coefs;
                _count = 0;
            }
            public int CreateMark()
            {
                //if (_coefs != null || _coefs.Length != 0)
                //{
                int coefss = _coefs[_count];
                _count++;
                if (_count == _coefs.Length)
                {
                    _count = 0;
                }
                return coefss;
                //}
            }
            public void Print()
            {
                Console.WriteLine(_name);
            }


        }

        public class Competition
        {
            private Participant[] _participants;
            private Judge[] _judges;
            // свойства 
            public Participant[] Participants => _participants;
            public Judge[] Judges => _judges;


            // консттруктор
            public Competition(Judge[] judge1)
            {
                _participants = new Participant[0];
                _judges = judge1;
            }
            //
            public void Evaluate(Participant jumper)
            {
                int[] marks = new int[_judges.Length];
                for (int i = 0; i < marks.Length; i++)
                {
                    marks[i] = _judges[i].CreateMark();
                }
                jumper.Jump(marks);
            }
            public void Add(Participant participant)
            {
                
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
                Evaluate(participant);

            }
            public void Add(Participant[] participant)
            {

                for (int i = 0; i < participant.Length; i++)
                {
                    Add(participant[i]);
                }
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }

    }
    }

