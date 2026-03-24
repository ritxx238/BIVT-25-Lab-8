using System;
using System.Linq;

namespace Lab8.Blue.Task3
{
    public class Participant(string n, string s)
    {
        private readonly string name = n;
        private readonly string surname = s;
        protected int[] penalties = [];

        public string Name => this.name;
        public string Surname => this.surname;
        public int[] Penalties => (int[])this.penalties.Clone();

        public int Total => this.penalties.Sum();

        public virtual bool IsExpelled => this.penalties.Any(p => p >= 10);

        public virtual void PlayMatch(int time)
        {
            if (time < 0) { return; }
            var buf = new int[this.penalties.Length + 1];
            Array.Copy(this.penalties, buf, this.penalties.Length);
            buf[this.penalties.Length] = time;
            this.penalties = buf;
        }

        public static void Sort(Participant[] array)
        {
            if (array == null) { return; }
            Array.Sort(array, (a, b) =>
            {
                var c = a.Total.CompareTo(b.Total);
                if (c != 0) return c;
                if (a.IsExpelled && !b.IsExpelled) return -1;
                if (!a.IsExpelled && b.IsExpelled) return 1;
                return 0;
            });
        }

        public void Print() => Console.WriteLine(this.ToString()); // и этот никому не ужин D:

        public override string ToString() =>
            $"Participant{{name: {this.name}, surname: {this.surname}, total: {this.Total}, isExpelled: {this.IsExpelled}}}";
    }

    public class BasketballPlayer : Participant
    {
        public BasketballPlayer(string name, string surname) : base(name, surname) { }

        public override bool IsExpelled
        {
            get
            {
                var matches = this.penalties.Length;
                if (matches == 0) { return false; }
                return (this.penalties.Count(p => p == 5) > matches * 0.1) || (this.penalties.Sum() > 2 * matches);
            }
        }

        public override void PlayMatch(int time)
        {
            if (time < 0 || time > 5) { return; }
            base.PlayMatch(time);
        }
    }

    public class HockeyPlayer : Participant
    {
        private static int players = 0;
        private static int totalTime = 0;

        public HockeyPlayer(string name, string surname) : base(name, surname)
        {
            players += 1;
        }

        public override bool IsExpelled
        {
            get
            {
                if (this.penalties.Any(p => p >= 10)) return true;

                if (players == 0) return false;
                var avg = totalTime / (double)players;
                var threshold = avg * 0.1;
                return this.Total > threshold;
            }
        }

        public override void PlayMatch(int time)
        {
            base.PlayMatch(time);
            if (time > 0) totalTime += time;
        }
    }
}