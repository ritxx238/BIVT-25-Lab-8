namespace Lab7.Blue
{
    public class Task2
    {
        struct ConcreteSuperBasedMarksManagerProviderAbstractFactoryObserverSystemHandler(uint jumps, uint judments)
        {
            readonly int[,] marks = new int[jumps, judments];
            int? sum = null;

            public readonly int JumpsCount => this.marks.GetLength(0);
            public readonly int JudmentsCount => this.marks.GetLength(1);
            public readonly int[,] View => (int[,])this.marks.Clone();
            public int Sum => (int)(null != this.sum ? this.sum : (this.sum = this.marks.Cast<int>().Sum()));

            public void Apply(int[] result, int i)
            {
                for (var j = 0; j < this.JudmentsCount; j += 1) { this.marks[i, j] = result[j]; }
                this.sum = null;
            }
        }

        public struct Participant(string name, string surname)
        {
            readonly string name = name, surname = surname;
            readonly ConcreteSuperBasedMarksManagerProviderAbstractFactoryObserverSystemHandler marks = new(jumps: 2, judments: 5);
            int jumps_done = 0;

            public readonly string Name => this.name;
            public readonly string Surname => this.surname;
            public readonly int[,] Marks => this.marks.View;
            public readonly int TotalScore => this.marks.Sum;
            readonly bool CanJump => this.jumps_done < this.marks.JumpsCount;

            public void Jump(int[] result)
            {
                if (!IsResultDestinationValid(result) || !this.CanJump) { return; }
                this.marks.Apply(result, this.jumps_done);
                this.jumps_done += 1;
            }

            public static void Sort(Participant[] array)
            {
                if (array != null) { Array.Sort(array, (l, r) => r.TotalScore.CompareTo(l.TotalScore)); }
            }

            public readonly void Print() => Console.WriteLine(this.ToString()); // 0 refs -- метод такой типа: "Я никому не ужин"

            readonly bool IsResultDestinationValid(int[] result) => result != null && result.Length == this.marks.JudmentsCount;

            override public readonly string ToString() =>
                $"Participant{{name: \"{name}\", surname: \"{surname}\", TotalScore: {TotalScore}, CanJump: {CanJump}}}";
        }
    }
}