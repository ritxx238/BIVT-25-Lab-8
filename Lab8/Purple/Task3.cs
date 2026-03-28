using System.Linq;

namespace Lab8.Purple
{
    public class Task3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _topplace;
            private double _totalmark;
            private bool _counted;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks =>
                (_marks == null || _marks.Length == 0) ? null : (double[]) _marks.Clone();
            public int[] Places => 
                (_places == null || _places.Length == 0) ? null : (int[]) _places.Clone();
            public int TopPlace => _places.Min();
            public double TotalMark => _marks.Sum();
            public int Score => (_places == null) ? 0 : _places.Sum();
            internal bool Counted => _counted;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _counted = false;
            }

            int c = 0;
            public void Evaluate(double result)
            {
                _marks[c++] = result;
                _counted = true;
            }
            
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                for (int judge = 0; judge < 7; judge++)
                {
                    Array.Sort(participants, (a, b) => b._marks[judge].CompareTo(a._marks[judge]));

                    for (int i = 0; i < participants.Length; i++)
                    {
                        participants[i]._places[judge] = i + 1;
                        if (judge == 0 || (i + 1) < participants[i]._topplace) //устанавливаем сначала наибольшее место на 0 судье,
                                                                               //а потом сравниваем текущее место с установленным наибольшим
                        {
                            participants[i]._topplace = i + 1; 
                        }
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        bool shouldSwap = false;
                        
                        if (array[j].Score > array[j+1].Score) shouldSwap = true;
                        else if (array[j].Score == array[j+1].Score)
                        {
                            if (array[j].TopPlace > array[j+1].TopPlace) shouldSwap = true;
                            else if (array[j].TopPlace == array[j+1].TopPlace)
                            {
                                double sum1 = 0.0, sum2 = 0.0;
                                for (int k = 0; k < array[j].Marks.Length; k++)
                                {
                                    sum1 += array[j].Marks[k];
                                    sum2 += array[j+1].Marks[k];
                                }
                                if (sum1 < sum2)
                                    shouldSwap = true;
                            }
                        }
                        if (shouldSwap) (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }


            public void Print()
            {
                Console.WriteLine($"Participant: {_surname} {_name}");
                Console.WriteLine($"Total Score: {Score}");
                Console.WriteLine($"Places: {string.Join(", ", _places)}");
            }
        }

        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;
            
            public Participant[] Participants => _participants;
            public double[] Moods => _moods;

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[7];
                Array.Copy(moods, _moods, moods.Length);
                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                if (marks == null || marks.Length == 0) return;
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (!_participants[i].Counted)
                    {
                        for (int j = 0; j < _moods.Length; j++)
                        {
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        }
                        break;
                    }
                }
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }

            public void Add(Participant[] participants)
            {
                int OldLength = _participants.Length;
                Array.Resize(ref _participants, OldLength + participants.Length);
                Array.Copy(participants, 0, _participants, OldLength, participants.Length);
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods)
            {
            }

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += (i + 1) / 10.0;
                }
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods)
            {
            }

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] *= (1 + (i + 1) / 100.0);
                }
            }
        }
        
    }
}
