using System.Security.Cryptography.X509Certificates;
using static Lab8.Purple.Task1;

namespace Lab8.Purple
{
    public class Task1
    {
        public class Participant
        {
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
                    double[] result = new double[4];
                    for (int i = 0; i < result.Length; i++)
                    {
                        result[i] = _coefs[i];
                    }
                    return result;
                }
            }
            public int[,] Marks
            {
                get
                {
                    int[,] result = new int[4, 7];
                    for (int i = 0; i < result.GetLength(0); i++)
                    {
                        for (int j = 0; j < result.GetLength(1); j++)
                        {
                            result[i, j] = _marks[i, j];
                        }
                    }
                    return result;
                }
            }

            public double TotalScore
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;
                    double sumTotal = 0;
                    for (int k = 0; k < _coefs.Length; k++)
                    {
                        int sum = 0;
                        int min = _marks[k, 0];
                        int minIndex = 0;
                        int max = _marks[k, 0];
                        int maxIndex = 0;
                        // Находим минимальную и максимальную оценку
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            if (_marks[k, j] < min)
                            {
                                min = _marks[k, j];
                                minIndex = j;
                            }
                            if (_marks[k, j] > max)
                            {
                                max = _marks[k, j];
                                maxIndex = j;
                            }
                        }
                        // Находим сумму оценок без минимальной и максимальной
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            if (j != minIndex && j != maxIndex)
                            {
                                sum += _marks[k, j];
                            }
                        }
                        sumTotal += sum * _coefs[k]; // Оценка за все прыжки спортсмена
                    }
                    return sumTotal;
                }

            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[,]{
                    { 0, 0, 0, 0, 0, 0, 0},
                    { 0, 0, 0, 0, 0, 0, 0},
                    { 0, 0, 0, 0, 0, 0, 0},
                    { 0, 0, 0, 0, 0, 0, 0}
                };
            }
            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || coefs.Length == 0) return;
                _coefs = new double[coefs.Length];
                for (int i = 0; i < coefs.Length; i++)
                {
                    _coefs[i] = coefs[i];
                }
            }
            int count = 0;
            public void Jump(int[] marks)
            {
                if (marks == null || marks.Length == 0) return;
                for (int j = 0; j < marks.Length; j++)
                {
                    _marks[count, j] = marks[j];
                }
                count++;
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = i; j < array.Length; j++)
                    {
                        if (array[i].TotalScore < array[j].TotalScore)
                        {
                            (array[i], array[j]) = (array[j], array[i]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_marks} {_coefs}");
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _index;

            public string Name => Name;

            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = new int[marks.Length];
                _index = 0;
                Array.Copy(marks, _marks, marks.Length);
            }
            public int CreateMark()
            {
                int result = 0;
                if (_index == _marks.Length)
                {
                    _index = 0;
                }
                result = _marks[_index++];
                return result;
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {String.Join(" ", _marks)}");
            }
        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;
            public Judge[] Judges
            {
                get
                {
                    Judge[] judges = new Judge[_judges.Length];
                    Array.Copy(_judges, judges, _judges.Length);
                    return judges;
                }
            }
            public Participant[] Participants
            {
                get
                {
                    Participant[] participants = new Participant[_participants.Length];
                    Array.Copy(_participants, participants, _participants.Length);
                    return participants;
                }
            }

            public Competition(Judge[] judges)
            {
                _judges = new Judge[judges.Length];
                Array.Copy(judges, _judges, _judges.Length);
                _participants = new Participant[0];
            }

            public void Evaluate(Participant jumper)
            {
                int[] marks = new int[jumper.Marks.GetLength(1)];
                for (int i = 0; i<jumper.Marks.GetLength(1); i++)
                {
                    marks[i] = _judges[i].CreateMark();
                }
                jumper.Jump(marks);
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                Evaluate(participant);
                _participants[^1] = participant;

            }
            public void Add(Participant[] participants)
            {
                int len = _participants.Length;
                int count = 0;
                Array.Resize(ref _participants, _participants.Length + participants.Length);
                for (int i = len; i < _participants.Length; i++)
                {
                    Evaluate(participants[count]);
                    _participants[i] = participants[count++];
                }
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
    }
}
