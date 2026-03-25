using static Lab8.Blue.Task2;

namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks = new int[2, 5]; // матрица оценок по двум прыжкам
            private int _jumpCount; // счётчик показывает какой сейчас прыжок(первый или второй)

            // свойства
            public string Name => _name;
            public string Surname => _surname;
            // создание копии массива с оценками, чтобы не изменять исходные данные оценок
            public int[,] Marks
            {
                get
                {
                    return (int[,])_marks.Clone(); // возвращаем копию
                }
            }
            public double TotalScore // общая сумма баллов
            {
                get
                {
                    double sum = 0;
                    for (int i = 0; i < 2; i++)
                        for (int j = 0; j < 5; j++)
                            sum += _marks[i, j];
                    return sum;
                }
            }
            //конструктор
            public Participant(string name, string surname) // фамилия, имя, матрица из нулей
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
            }

            //добавление оценок за прыжок
            public void Jump(int[] result)
            {
                if (_jumpCount >= 2) return;
                if (result == null) return;
                if (result.Length != 5) return;

                for (int i = 0; i < 5; i++)
                {
                    _marks[_jumpCount, i] = result[i];
                }

                _jumpCount++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                    return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {TotalScore}");
            }
        }
        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;
            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants
            {
                get
                {
                    Participant[] copy = new Participant[_participants.Length];
                    for (int i = 0; i < _participants.Length; i++)
                    {
                        copy[i] = _participants[i];
                    }
                    return copy;
                }
            }
            public abstract double[] Prize { get; }

            // конструктор протект
            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant) // добавляем одного
            {
                Participant[] newArray = new Participant[_participants.Length + 1];

                for (int i = 0; i < _participants.Length; i++)
                    newArray[i] = _participants[i];

                newArray[_participants.Length] = participant;

                _participants = newArray;
            }

            public void Add(Participant[] participants) // добавляем нескольких
            {
                if (participants == null) return;

                for (int i = 0; i < participants.Length; i++)
                    Add(participants[i]);
            }
        }

            public class WaterJump3m : WaterJump
            {
                public override double[] Prize
                {
                    get
                    {
                        if (Participants.Length < 3) // проверка на больше чем 3 участника
                        {
                            return null;
                        }

                        return [Bank * 0.5, Bank * 0.3, Bank * 0.2]; // возвращаем победителей
                    }
                }

                public WaterJump3m(string name, int bank) : base(name, bank) { }
            }


            public class WaterJump5m : WaterJump
            {
                public override double[] Prize
                {
                    get
                    {
                        if (Participants.Length < 3) // проверка на 3 участника
                        {
                            return null;
                        }

                        int half = (Participants.Length - 1) / 2;   // индекс последнего получившего приз(среднего)

                        if (half > 10) // если их больше 10 то 10
                        {
                            half = 10;
                        }
                        if (half < 3) // если их меньше 3 то 3
                        {
                            half = 3;
                        }

                        double[] prizes = new double[half + 1];

                        for (int i = 0; i <= half; i++)
                        {
                            prizes[i] = Bank * (20 / (Participants.Length / 2)) / 100;
                        }

                        prizes[0] += Bank * 0.4;
                        prizes[1] += Bank * 0.25;
                        prizes[2] += Bank * 0.15;

                        return prizes;
                    }
                }

                public WaterJump5m(string name, int bank) : base(name, bank) { }
            }
        
    }
}
