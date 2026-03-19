namespace Lab8.Purple
{
    public class Task3
    {
        public struct Participant
        {
            //Name, Surname, Marks, Places, TopPlace и TotalMark поля
            private string _name;
            private string _surname;
            private double[] _marks;// оценок за прыжки
            private int[] _places;// массива мест, полученных спортсменом у судей,
            private int _topplace;//наивысшего места
            private double _totalmark;//суммы полученных мест

            //свойства 
            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    double[] marks = new double[7];
                    for (int i = 0; i < marks.Length; i++)
                    {
                        marks[i] = _marks[i];
                    }
                    return marks;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) return null;
                    int[] places = new int[7];
                    for (int i = 0; i < places.Length; i++)
                    {
                        places[i] = _places[i];
                    }
                    return places;
                }
            }

            public int TopPlace
            {
                get
                {

                    int mn = 1000;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        if (_places[i] < mn)
                        {
                            mn = _places[i];
                        }
                    }
                    _topplace = mn;

                    return _topplace;
                }
            }
            public double TotalMark
            {
                get
                {
                    double sum = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        sum += _marks[i];
                    }

                    return sum;
                }
            }
            public int Score
            {
                get
                {
                    int itog = 0;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        itog += _places[i];
                    }
                    return itog;
                }
            }
            // конструктор 
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _topplace = 0;
                _totalmark = 0;
            }
            int k = 0;
            //который добавляет оценку очередного судьи в массиd
            public void Evaluate(double result)
            {
                if (k < 7)
                {
                    _marks[k] = result;
                }

                k++;

            }
            public static void SetPlaces(Participant[] participants)
            {
                double[,] matrix = new double[participants.Length, 7];
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = participants[i]._marks[j];
                    }

                }


                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    for (int j1 = 0; j1 < matrix.GetLength(0); j1++)
                    {


                        for (int i = 1; i < matrix.GetLength(0); i++)
                        {
                            if (matrix[i, j] > matrix[i - 1, j])
                            {
                                (matrix[i, j], matrix[i - 1, j]) = (matrix[i - 1, j], matrix[i, j]);
                            }
                        }
                    }
                }


                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    for (int i1 = 0; i1 < matrix.GetLength(0); i1++)
                    {
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            if (participants[i1]._marks[j] == matrix[i, j])
                            {
                                participants[i1]._places[j] = i + 1;

                            }


                        }
                    }

                }

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    for (int i1 = 0; i1 < matrix.GetLength(0); i1++)
                    {
                        for (int i = 1; i < matrix.GetLength(0); i++)
                        {
                            if (matrix[i, 6] < matrix[i - 1, 6])
                            {
                                (matrix[i, j], matrix[i - 1, j]) = (matrix[i - 1, j], matrix[i, j]);
                            }
                        }
                    }
                }

            }


            public static void Sort(Participant[] array)
            {

                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].Score > array[j].Score)
                        {
                            (array[j], array[j - 1]) = (array[j - 1], array[j]);
                        }
                        else if (array[j - 1].Score == array[j].Score)
                        {
                            if (array[j - 1].TotalMark < array[j].TotalMark)
                            {
                                (array[j], array[j - 1]) = (array[j - 1], array[j]);
                            }
                        }
                    }
                }

            }


            public void Print()
            {

                Console.WriteLine(_name);
                Console.WriteLine(_surname);
                Console.WriteLine(Score);
                Console.WriteLine(_topplace);
                Console.WriteLine(_totalmark);
                for (int i = 0; i < _marks.Length; i++)
                {
                    Console.Write(_marks[i] + " ");
                }
                Console.WriteLine();
                for (int i = 0; i < _places.Length; i++)
                {
                    Console.Write(_places[i] + " ");
                }
            }
        }

        public abstract class Skating
        {
            // полями для списка участников и массива настроений судей(вещественные значения) с уровнем доступа protected.
            protected Participant[] _participants;
            protected double[] _moods;
            protected int _count;

            public Participant[] Participants => _participants;
            public double[] Moods
            {
                get
                {
                    double[] moods = new double[_moods.Length];
                    for (int i = 0; i < _moods.Length; i++)
                    {
                        moods[i] = _moods[i];
                    }
                    return moods;
                }

            }

            // Конструктор должен получать массив настроений судей, заполнять данные для каждого судьи (их 7) и сразу менять данные массива, вызывая метод ModificateMood.
            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[moods.Length];
                for (int i = 0; i < 7; i++)
                {
                    _moods[i] = moods[i];
                }

                _count = 0;
                ModificateMood();
                // доделать
            }
            protected abstract void ModificateMood();
            //  Создать метод public void Evaluate(double[] marks), который будет вызывать у первого ещё не
            //выступившего спортсмена метод Evaluate и передавать ему полученные оценки, умноженные на
            //настроение соответствующего судьи.
            public void Evaluate(double[] marks)
            {

                for (int i = 0; i < 7; i++)
                {
                    _participants[_count].Evaluate((double)marks[i] * _moods[i]);
                }
                _count++;
               
            }
            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }
            public void Add(Participant[] participant)
            {
                for (int i = 0; i < participant.Length; i++)
                {
                    Add(participant[i]);
                }
            }
            // Создать классы–наследники от Skating: FigureSkating и IceSkating.

        }
        // Их конструкторы получают массив настроений и передают его базовому классу.
        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods)
            {

            }
            protected override void ModificateMood()
            {
                for (int i = 0; i < 7; i++)
                {
                    _moods[i] += (double)(i + 1) / 10;
                }
            }


        }
        // Переопределить метод ModificateMood в классе FigureSkating следующим образом: к
        //настроению добавляется порядковый номер судьи, деленный на 10.
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods)
            {

            }
            protected override void ModificateMood()
            {
                for (int i = 0; i < 7; i++)
                {
                    _moods[i] *= (((double)(i + 101) / 100));
                }
            }
        }
        // Переопределить метод ModificateMood в классе IceSkating следующим образом: настроение
        //судьи увеличивается на i%, где i – порядковый номер судьи.

    }
}
