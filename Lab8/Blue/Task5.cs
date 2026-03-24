using System;
using System.Linq;
   
namespace Lab8.Blue.Task5
{
    public class Sportsman(string name, string surname)
    {
        private readonly string name = name;
        private readonly string surname = surname;
        private int place = 0;
        private bool placeSet = false;

        public string Name => this.name;
        public string Surname => this.surname;
        public int Place => this.place;

        public void SetPlace(int place)
        {
            if (this.placeSet) { return; }
            this.place = place;
            this.placeSet = true;
        }

        public void Print() => Console.WriteLine(this.ToString()); // этот тоже никому не ужин

        public override string ToString() =>
            $"Sportsman{{Name: \"{this.name}\", Surname: \"{this.surname}\", Place: {this.place}}}";
    }

    public abstract class Team(string name)
    {
        private readonly string name = name;
        private Sportsman[] sportsmen = new Sportsman[6];
        private int count = 0;

        public string Name => this.name;

        public Sportsman[] Sportsmen
        {
            get
            {
                var result = new Sportsman[this.count];
                Array.Copy(this.sportsmen, result, this.count);
                return result;
            }
        }

        public int SummaryScore
        {
            get
            {
                int sum = 0;
                for (int i = 0; i < this.count; i += 1)
                {
                    int p = this.sportsmen[i].Place;
                    if (p >= 1 && p <= 5) { sum += 6 - p; }
                }
                return sum;
            }
        }

        public int TopPlace
        {
            get
            {
                if (this.count == 0) return 18;
                int min = int.MaxValue;
                for (int i = 0; i < this.count; i += 1)
                {
                    int p = this.sportsmen[i].Place;
                    if (p > 0 && p < min)
                        min = p;
                }
                return min == int.MaxValue ? 18 : min;
            }
        }

        public void Add(Sportsman sportsman)
        {
            if (sportsman == null) { return; }
            for (int i = 0; i < this.count; i += 1)
            {
                if (this.sportsmen[i].Name == sportsman.Name &&
                    this.sportsmen[i].Surname == sportsman.Surname)
                    return;
            }
            if (this.count >= this.sportsmen.Length)
            {
                int newLen = this.sportsmen.Length == 0 ? 6 : this.sportsmen.Length * 2;
                Array.Resize(ref this.sportsmen, newLen);
            }
            this.sportsmen[this.count] = sportsman;
            this.count += 1;
        }

        public void Add(Sportsman[] sportsmen)
        {
            if (sportsmen == null) { return; }
            foreach (var s in sportsmen)
                this.Add(s);
        }

        public static void Sort(Team[] teams)
        {
            if (teams == null) { return; }
            Array.Sort(teams, (a, b) =>
            {
                int c = b.SummaryScore.CompareTo(a.SummaryScore);
                if (c != 0) return c;
                return a.TopPlace.CompareTo(b.TopPlace);
            });
        }

        public static Team GetChampion(Team[] teams)
        {
            if (teams == null || teams.Length == 0) { return null; }
            Team champion = teams[0];
            double best = champion.GetTeamStrength();
            for (int i = 1; i < teams.Length; i += 1)
            {
                double cur = teams[i].GetTeamStrength();
                if (cur > best)
                {
                    best = cur;
                    champion = teams[i];
                }
            }
            return champion;
        }

        protected abstract double GetTeamStrength();

        public void Print() => Console.WriteLine(this.ToString()); // тоже не ужин

        public override string ToString() =>
            $"Team{{Name: \"{this.name}\", Sportsmen: [{string.Join(", ", this.Sportsmen.Select(s => s.Name))}], SummaryScore: {this.SummaryScore}, TopPlace: {this.TopPlace}}}";
    }

    public class ManTeam(string name) : Team(name)
    {
        protected override double GetTeamStrength()
        {
            var sportsmen = this.Sportsmen;
            if (sportsmen.Length == 0) { return 0; }
            double sum = 0;
            foreach (var s in sportsmen)
                sum += s.Place;
            double avg = sum / sportsmen.Length;
            return 100.0 / avg;
        }
    }

    public class WomanTeam(string name) : Team(name)
    {
        protected override double GetTeamStrength()
        {
            var sportsmen = this.Sportsmen;
            if (sportsmen.Length == 0) { return 0; }
            double sumPlaces = 0;
            double productPlaces = 1;
            foreach (var s in sportsmen)
            {
                sumPlaces += s.Place;
                productPlaces *= s.Place;
            }
            if (productPlaces == 0) { return 0; }
            return 100.0 * (sumPlaces * sportsmen.Length) / productPlaces;
        }
    }
}