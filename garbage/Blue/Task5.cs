namespace Lab7.Blue
{
    public class Task5
    {
        public struct Sportsman(string name, string surname)
        {
            readonly string name = name, surname = surname;
            int place = 0;
            bool place_set = false;

            public readonly string Name => this.name;
            public readonly string Surname => this.surname;
            public readonly int Place => this.place;

            public void SetPlace(int place)
            {
                if (this.place_set) { return; }
                this.place = place;
                this.place_set = true;
            }

            public readonly void Print() => Console.WriteLine(this.ToString());
            public override readonly string ToString() =>
                $"Sportsman{{Name: \"{this.name}\", Surname: \"{this.surname}\", Place: {this.place}}}";
        }

        public struct Team(string name)
        {
            Sportsman[] sportsmen = [];
            readonly string name = name;
            int count = 0;

            public readonly string Name => this.name;
            public readonly Sportsman[] Sportsmen => this.sportsmen.AsSpan(new Range(0, this.count)).ToArray();
            readonly IEnumerable<int> SelectPlaces => this.Sportsmen.Select(s => s.Place);
            public readonly int TotalScore => this.SelectPlaces.Sum(p => p > 5 ? 0 : 6 - p);
            public readonly int TopPlace => this.SelectPlaces.Where(p => p > 0).DefaultIfEmpty(0).Min();

            public void Add(Sportsman s)
            {
                if (this.Sportsmen.Contains(s)) { return; }

                if (this.count >= this.sportsmen.Length)
                {
                    Array.Resize(ref this.sportsmen, this.sportsmen.Length == 0 ? 6 : this.sportsmen.Length << 1);
                }

                this.sportsmen[this.count] = s;
                this.count += 1;
            }

            public void Add(Sportsman[]? ss)
            {
                if (ss != null) { foreach (var s in ss) { this.Add(s); } }
            }

            public static void Sort(Team[] teams)
            {
                if (null == teams) { return; }
                Array.Sort(teams, (a, b) =>
                {
                    var c = b.TotalScore.CompareTo(a.TotalScore);
                    return c != 0 ? c : a.TopPlace.CompareTo(b.TopPlace);
                });
            }

            public readonly void Print() => Console.WriteLine(this.ToString());

            public override readonly string ToString() =>
                $"Team{{Name: \"{this.name}\", Sportsmen: [{string.Join(", ", this.Sportsmen.Select(s => s.Name))}], TotalScore: {this.TotalScore}, TopPlace: {this.TopPlace}}}";
        }
    }
}