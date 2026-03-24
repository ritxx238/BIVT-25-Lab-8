using System;

namespace Lab8.Blue.Task2
{
    public struct Participant(string n, string s)
    {
        private readonly string name = n;
        private readonly string surname = s;
        private readonly MarksStorage marks = new(2, 5);
        private int jumps_done = 0;

        private struct MarksStorage(uint jumps, uint judges)
        {
            private readonly int[,] marks = new int[jumps, judges];
            private int? sum = null;

            public readonly int JumpsCount => this.marks.GetLength(0);
            public readonly int JudgesCount => this.marks.GetLength(1);
            public readonly int[,] View => (int[,])marks.Clone();
            public int Sum => sum ??= this.marks.Cast<int>().Sum();

            public void Apply(int[] result, int jumpIndex)
            {
                for (int j = 0; j < this.JudgesCount; j += 1)
                    this.marks[jumpIndex, j] = result[j];
                this.sum = null;
            }
        }

        public readonly string Name => this.name;
        public readonly string Surname => this.surname;
        public readonly int[,] Marks => this.marks.View;
        public readonly int TotalScore => this.marks.Sum;
        private readonly bool CanJump => this.jumps_done < this.marks.JumpsCount;

        public void Jump(int[] result)
        {
            if (result == null || result.Length != this.marks.JudgesCount || !this.CanJump) { return; }
            this.marks.Apply(result, this.jumps_done);
            this.jumps_done += 1;
        }

        public static void Sort(Participant[] array)
        {
            if (array == null) { return; }
            Array.Sort(array, (a, b) => b.TotalScore.CompareTo(a.TotalScore));
        }

        public readonly void Print() => Console.WriteLine(this.ToString()); // 0 refs --- Я никому не ужин

        public override readonly string ToString() =>
            $"Participant{{name: {this.name}, surname: {this.surname}, TotalScore: {this.TotalScore}}}";
    }

    public abstract class WaterJump(string n, int b)
    {
        private readonly string name = n;
        private readonly int bank = b;
        private Participant[] participants = [];

        public string Name => this.name;
        public int Bank => this.bank;
        public Participant[] Participants => this.participants;

        public abstract float[] Prize { get; }

        public void Add(Participant p)
        {
            var buf = new Participant[this.participants.Length + 1];
            Array.Copy(this.participants, buf, this.participants.Length);
            buf[participants.Length] = p;
            this.participants = buf;
        }

        public void Add(Participant[] ps)
        {
            if (ps == null || ps.Length == 0) { return; }
            var buf = new Participant[this.participants.Length + ps.Length];
            Array.Copy(this.participants, buf, this.participants.Length);
            Array.Copy(ps, 0, buf, this.participants.Length, ps.Length);
            this.participants = buf;
        }
    }

    public class WaterJump3m(string name, int bank) : WaterJump(name, bank)
    {
        public override float[] Prize
        {
            get
            {
                var sorted = GetSortedParticipants();
                if (sorted.Length < 3) { return null; }

                float total = Bank;
                return
                [
                    total * 0.5f,
                    total * 0.3f,
                    total * 0.2f
                ];
            }
        }

        private Participant[] GetSortedParticipants()
        {
            var copy = (Participant[])Participants.Clone();
            Array.Sort(copy, (a, b) => b.TotalScore.CompareTo(a.TotalScore));
            return copy;
        }
    }

    public class WaterJump5m(string name, int bank) : WaterJump(name, bank)
    {
        public override float[] Prize
        {
            get
            {
                var sorted = GetSortedParticipants();
                var n = sorted.Length;
                if (n < 3) { return null; }

                var count_above = n / 2;
                if (count_above < 3) { return null; }

                float total = Bank;
                float extra_percent = 20.0f / count_above;
                float extra = total * extra_percent / 100.0f;

                float[] prizes = new float[count_above];

                for (int i = 0; i < count_above; i += 1)
                {
                    float base_amount = 0;
                    if (i == 0) base_amount = total * 0.4f;
                    else if (i == 1) base_amount = total * 0.25f;
                    else if (i == 2) base_amount = total * 0.15f;
                    prizes[i] = base_amount + extra;
                }

                return prizes;
            }
        }

        private Participant[] GetSortedParticipants()
        {
            var copy = (Participant[])this.Participants.Clone();
            Array.Sort(copy, (a, b) => b.TotalScore.CompareTo(a.TotalScore));
            return copy;
        }
    }
}