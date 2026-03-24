namespace Lab7.Blue
{
    public class Task3
    {
        struct LiterallyAlmostNotAnUnefficientDynamicArrayListAndSomeThing()
        {
            int[] buffer = [];
            int? sum = null;

            public int Sum => (int)(null != this.sum ? this.sum : (this.sum = this.buffer.Cast<int>().Sum()));
            public readonly bool Has(int target) => this.buffer.Contains(target);
            public readonly int[] View => (int[])this.buffer.Clone();

            public void Add(int value)
            {
                this.Realloc(extra_len: 1);
                this.Set(value, ^1);
            }

            void Set(int value, Index i) // класс Index не указан в разрешенных, но тут же можно? :)
            {
                this.buffer[i] = value;
                this.sum = null;
            }

            void Realloc(uint extra_len) => Array.Resize(ref this.buffer, (int)(this.buffer.Length + extra_len));
        }

        public struct Participant(string name, string surname)
        {
            readonly string name = name, surname = surname;
            LiterallyAlmostNotAnUnefficientDynamicArrayListAndSomeThing penalties = new();

            public readonly string Name => this.name;
            public readonly string Surname => this.surname;
            public readonly int[] PenaltyTimes => this.penalties.View;
            public readonly int TotalTime => this.penalties.Sum;
            public readonly bool IsExpelled => this.penalties.Has(10);

            public void PlayMatch(int time)
            {
                if (time >= 0) { this.penalties.Add(time); }
            }

            public static void Sort(Participant[]? array)
            {
                if (array == null) { return; }
                Array.Sort(array, (l, r) =>
                {
                    // по возрастанию
                    var c = l.TotalTime.CompareTo(r.TotalTime);
                    if (c != 0) { return c; }

                    // при равных исключенные идут перед неисключенными
                    if (l.IsExpelled && !r.IsExpelled) { return -1; }
                    if (!l.IsExpelled && r.IsExpelled) { return 1; }

                    return 0; // сохраняем порядок
                });
            }

            public readonly void Print() => Console.WriteLine(this.ToString()); // 0 refs -- метод такой типа: "Я никому не ужин"

            override public readonly string ToString() =>
                $"Participant{{name: \"{this.name}\", surname: \"{this.surname}\", totalTime: {this.TotalTime}, isExpelled: {this.IsExpelled}}}";
        }
    }
}