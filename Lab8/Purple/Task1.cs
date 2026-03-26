using System.Globalization;

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
                    if (_coefs == null) 
                        return default(double[]);
                    double[] Coefs = new double[_coefs.Length];
                    Array.Copy(_coefs, Coefs, Coefs.Length);
                    return Coefs;
                }
            }
            
            //делаем копию массива чтобы работать с ней и (чтоб нельзя было извне поменять)

            public int[,] Marks
            {
                get
                {
                    if (_marks == null) 
                        return default(int[,]);
                    int[,] marks = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, marks, _marks.Length);
                    return marks;
                }
            }
            
            public double TotalScore
            {
                get
                {
                    double totalScore = 0;
                    
                    for (int jump = 0; jump < 4; jump++) 
                    {
                        int[] tempArray = new int[7];
                        for (int judge = 0; judge < 7; judge++)
                        {
                            tempArray[judge] = _marks[jump, judge]; //закинули строку для последующей обрезки лишних оценок
                        }
                        Array.Sort(tempArray);
                        
                        int currentScore = 0;
                        for (int judge = 1; judge < 6; judge++)
                        {
                            currentScore += tempArray[judge];
                        }

                        totalScore += currentScore*_coefs[jump];
                    }
                    
                    return totalScore;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4];
                _marks = new int[4, 7];
                for (int i = 0; i < 4; i++)
                    _coefs[i] = 2.5;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs.Length != 4)
                    return;
                for (int i = 0; i < 4; i++)
                {
                    _coefs[i] = coefs[i];
                }
            }
            public void Jump(int[] marks)
            {
                if (marks.Length != 7)
                    return;
                
                int number = 0;
                
                for (int jump = 0; jump < 4; jump++) //проверяем на пустую сторку и ищем первую непустую
                {
                    bool isEmpty = true;
                    for (int judge = 0; judge < 7; judge++)
                    {
                        if (_marks[jump, judge] != 0)
                        {
                            isEmpty = false;
                            break;
                        }
                    }
                
                    if (isEmpty)
                    {
                        for (int judge = 0; judge < 7; judge++)
                        {
                            _marks[jump, judge] = marks[judge]; //перекидываем значения в первую пустую строку
                        }
                        break;
                    }
                }
                
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
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
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname} ,коэффициент: {string.Join(',',_coefs)}");
                for (int jump = 0; jump < 4; jump++) 
                {
                    Console.WriteLine($"Прыжок {jump+1} ");
                    for (int judge = 0; judge < 7; judge++)
                    {
                        Console.WriteLine($"{_marks[jump, judge]} ");
                    }
                    Console.WriteLine($"Общий результат: {TotalScore}");
                }
                
            }




        }
        
        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _count;
            
            public string Name => _name;

            public Judge(string name, int[] marks)
            {
                _marks = marks;
                _name = name;
                _count = 0;
            }

            public int CreateMark()
            {
                int result = _marks[_count++];
                
                if (_count >= _marks.Length)
                    _count = 0;
                
                return result;
            }
            
            public void Print()
            {
                Console.WriteLine($"Имя: {_name}, оценки: {string.Join(',',_marks)}");
            }
        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges => _judges;
            public Participant[] Participants => _participants;

            public Competition(Judge[] judges)
            {
                _judges = judges;
                _participants = Array.Empty<Participant>();
            }

            public void Evaluate(Participant jumper)
            {
                int indexParticipant = Array.IndexOf(_participants, jumper); //искали участника в списках
                if (indexParticipant < 0)
                    return;
                int[] marks = new int[_judges.Length];
                for (int i = 0; i < _judges.Length; i++) //создали его оценки
                {
                    marks[i] = _judges[i].CreateMark();
                }
                
                jumper.Jump(marks); //передаем оценки
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
                Evaluate(_participants[^1]);
            }

            public void Add(Participant[] participant)
            {
                foreach (Participant p in participant)
                    Add(p);
            }

            public void Sort() => Participant.Sort(_participants);
        }
    }
}