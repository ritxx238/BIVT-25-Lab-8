namespace Lab8.Purple
{
    public class Task3
    {
        public struct Participant
        {
            private string _name; private string _surname;
            private double[] _marks; private int[] _places;
            private int _top; private double _total; private int _score;

            public string Name => _name; public string Surname => _surname;
            public double[] Marks => (double[])_marks.Clone(); public int[] Places => (int[])_places.Clone();
            public int TopPlace => _places.Min(); public double TotalMark => _marks.Sum();
            public int Score
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _places.Length; i++)
                        sum += _places[i];
                    return sum;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name; _surname = surname;
                _marks = new double[7]; _places = new int[7];
                _top = 0; _total = 0; _score = 0;
            }
            public void Evaluate(double result)
            {
                if (_marks == null || _marks.Length != 7) return;
                for (int i = 0; i < _marks.Length; i++)
                    if (Math.Abs(_marks[i] - 0.0) < 0.0001)
                    {
                        _marks[i] = result;
                        return;
                    }
            }
            public static void SetPlaces(Participant[] participants)
            {
                for (int k = 0; k < 7; k++)
                {
                    double[] mat = new double[participants.Length];
                    int n = mat.Length;
                    for (int i = 0; i < n; i++)
                        mat[i] = participants[i].Marks[k];
                    for (int a = 0; a < n - 1; a++)
                    {
                        for (int b = 0; b < n - a - 1; b++)
                            if (mat[b] < mat[b + 1])
                                (mat[b], mat[b + 1]) = (mat[b + 1], mat[b]);
                    }
                    for (int j = 0; j < n; j++)
                        for (int l = 0; l < n; l++)
                            if (mat[j] == participants[l].Marks[k])
                                participants[l]._places[k] = j + 1;
                }
            }
            public static void Sort(Participant[] array)
            {
                for (int k = 0; k < array.Length - 1; k++)
                {
                    for (int i = 0; i < array.Length - k - 1; i++)
                    {
                        if (array[i].Score > array[i + 1].Score)
                            (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        else if (array[i].Score == array[i + 1].Score)
                        {
                            if (array[i].TopPlace < array[i + 1].TopPlace)
                                (array[i], array[i + 1]) = (array[i + 1], array[i]);
                            else if (array[i].TopPlace == array[i + 1].TopPlace)
                            {
                                if (array[i].TotalMark < array[i + 1].TotalMark)
                                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                            }
                        }
                    }
                }
            }
            public void Print()
            { Console.Write($"Name: {_name} Surname: {_surname} Score: {_places.Sum()} TopPlace: {_places.Min()} TotalMark: {_marks.Sum()}"); }

        }
        public abstract class Skating 
        {
            protected Participant[] _participants; protected double[] _moods; protected int k;
            public Participant[] Participants => _participants;
            public double[] Moods => (double[])_moods.Clone();
            public Skating(double[] moods)
            {
                _participants = Array.Empty<Participant>(); _moods = new double[7];
                for (int i=0;i<7;i++)  _moods[i] = moods[i]; 
                ModificateMood(); k = 0;
            }
            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {
                if (marks == null || marks.Length != 7 || k >= _participants.Length) return;
                for (int i = 0; i < marks.Length; i++)
                    _participants[k].Evaluate(marks[i] * _moods[i]);
                k++;
            }
            public void Add(Participant participant)
            { 
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }
            public void Add(Participant[] participant)
            {
                foreach (var i in participant)
                    Add(i);
            }
        }
        public class FigureSkating : Skating
        { 

            public FigureSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                for (int i = 0; i < 7; i++)
                    _moods[i] += (double)(i+1) / 10;
            }
        }
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                for (int i = 0; i < 7; i++)
                    _moods[i] += (double)_moods[i] / 100 * (i+1);
            }
        }




    }
}
