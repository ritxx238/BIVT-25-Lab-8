namespace Lab7.Blue
{
    public class Task4
    {
        struct AlmostListOfInts
        {
            int[] buffer = [];
            int? sum = null;

            public AlmostListOfInts() { }
            public int Sum => (int)(null != this.sum ? this.sum : (this.sum = this.buffer.Cast<int>().Sum()));

            public readonly bool Has(int target) => buffer.Contains(target);
            public readonly int[] View => (int[])this.buffer.Clone();
            public void Add(int value)
            {
                Realloc(extra_len: 1);
                buffer[^1] = value;
                sum = null;
            }

            void Realloc(int extra_len) => Array.Resize(ref this.buffer, this.buffer.Length + extra_len);
        }

        public struct Team(string name)
        {
            readonly string name = name;
            AlmostListOfInts scores = new();

            public readonly string Name => name;
            public readonly int[] Scores => scores.View;
            public readonly int TotalScore => scores.Sum;

            public void PlayMatch(int result) => scores.Add(result);
            public readonly void Print() => Console.WriteLine(ToString());
            public override readonly string ToString() =>
                $"Team{{Name: \"{name}\", Scores: [{string.Join(", ", Scores)}], TotalScore: {TotalScore}}}";
        }

        public struct Group(string name)
        {
            readonly string name = name;
            readonly Team[] teams = new Team[12];
            int count = 0;

            public readonly string Name => name;
            public readonly Team[] Teams => (Team[])teams.Clone();

            public void Add(Team t)
            {
                if (count >= 12) { return; }
                teams[count] = t;
                count += 1;
            }

            public void Add(Team[]? t)
            {
                for (var i = 0; t != null && i < t.Length && count < 12; i += 1)
                {
                    teams[count] = t[i];
                    count += 1;
                }
            }

            public readonly void Sort() => Array.Sort(teams, (l, r) => r.TotalScore.CompareTo(l.TotalScore));

            public readonly void Print() => Console.WriteLine(ToString());

            public override readonly string ToString() =>
                $"Group{{Name: \"{name}\", Teams: [{string.Join(", ", Teams.Select(t => t.Name))}]}}";

            public static Group Merge(Group a, Group b, int size)
            {
                var combined = new Team[12];
                var idx = 0;
                for (var i = 0; i < 6; i++) { combined[idx++] = a.teams[i]; }
                for (var i = 0; i < 6; i++) { combined[idx++] = b.teams[i]; }

                Array.Sort(combined, (l, r) => r.TotalScore.CompareTo(l.TotalScore));

                var ret = new Group("Финалисты");
                for (var i = 0; i < idx && ret.count < size; i++) { ret.Add(combined[i]); }
                return ret;
            }
        }
    }
}