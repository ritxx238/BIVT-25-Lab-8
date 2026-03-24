using System;
using System.Linq;

namespace Lab8.Blue.Task4
{
    public abstract class Team(string name)
    {
        private readonly string name = name;
        private int[] scores = [];

        public string Name => this.name;

        public int[] Scores => (int[])this.scores.Clone();

        public int TotalScore
        {
            get
            {
                int sum = 0;
                foreach (var s in this.scores) sum += s;
                return sum;
            }
        }

        public void PlayMatch(int result)
        {
            var newScores = new int[this.scores.Length + 1];
            Array.Copy(this.scores, newScores, this.scores.Length);
            newScores[this.scores.Length] = result;
            this.scores = newScores;
        }

        public void Print() => Console.WriteLine(this.ToString());

        public override string ToString() =>
            $"Team{{name: {this.name}, totalScore: {this.TotalScore}}}";
    }

    public class ManTeam(string name) : Team(name) { }

    public class WomanTeam(string name) : Team(name) { }

    public class Group(string name)
    {
        private readonly string name = name;
        private readonly Team?[] manTeams = new Team?[12];
        private readonly Team?[] womanTeams = new Team?[12];

        public string Name => this.name;

        public Team?[] ManTeams => this.manTeams;

        public Team?[] WomanTeams => this.womanTeams;

        public void Add(Team team)
        {
            if (team is ManTeam)
            {
                for (int i = 0; i < this.manTeams.Length; i += 1)
                {
                    if (this.manTeams[i] == null)
                    {
                        this.manTeams[i] = team;
                        return;
                    }
                }
            }
            else if (team is WomanTeam)
            {
                for (int i = 0; i < this.womanTeams.Length; i += 1)
                {
                    if (this.womanTeams[i] == null)
                    {
                        this.womanTeams[i] = team;
                        return;
                    }
                }
            }
        }

        public void Add(Team[] teams)
        {
            if (teams == null) return;
            foreach (var t in teams)
            {
                this.Add(t);
            }
        }

        public void Sort()
        {
            var menList = this.manTeams.Where(t => t != null).Select((t, idx) => (t, idx)).ToList();
            var sortedMen = menList.OrderByDescending(x => x.t!.TotalScore).ThenBy(x => x.idx).Select(x => x.t).ToArray();
            for (int i = 0; i < sortedMen.Length; i += 1)
                this.manTeams[i] = sortedMen[i];
            for (int i = sortedMen.Length; i < this.manTeams.Length; i += 1)
                this.manTeams[i] = null;

            var womenList = this.womanTeams.Where(t => t != null).Select((t, idx) => (t, idx)).ToList();
            var sortedWomen = womenList.OrderByDescending(x => x.t!.TotalScore).ThenBy(x => x.idx).Select(x => x.t).ToArray();
            for (int i = 0; i < sortedWomen.Length; i += 1)
                this.womanTeams[i] = sortedWomen[i];
            for (int i = sortedWomen.Length; i < this.womanTeams.Length; i += 1)
                this.womanTeams[i] = null;
        }

        public static Group Merge(Group group1, Group group2, int size)
        {
            var result = new Group("Финалисты");
            int half = size / 2;

            var men1 = TakeFirstNonNull(group1.manTeams, half);
            var men2 = TakeFirstNonNull(group2.manTeams, half);
            var mergedMen = MergeSorted(men1, men2, size);
            for (int i = 0; i < mergedMen.Length; i += 1)
                result.manTeams[i] = mergedMen[i];

            var women1 = TakeFirstNonNull(group1.womanTeams, half);
            var women2 = TakeFirstNonNull(group2.womanTeams, half);
            var mergedWomen = MergeSorted(women1, women2, size);
            for (int i = 0; i < mergedWomen.Length; i += 1)
                result.womanTeams[i] = mergedWomen[i];

            return result;
        }

        private static Team?[] TakeFirstNonNull(Team?[] arr, int count)
        {
            var result = new Team?[count];
            int taken = 0;
            for (int i = 0; i < arr.Length && taken < count; i += 1)
            {
                if (arr[i] != null)
                {
                    result[taken] = arr[i];
                    taken += 1;
                }
            }
            return result;
        }

        private static Team?[] MergeSorted(Team?[] arr1, Team?[] arr2, int total)
        {
            var result = new Team?[total];
            int i = 0, j = 0, k = 0;
            while (k < total && i < arr1.Length && j < arr2.Length)
            {
                if (arr1[i] == null) i += 1;
                else if (arr2[j] == null) j += 1;
                else if (arr1[i]!.TotalScore >= arr2[j]!.TotalScore)
                {
                    result[k] = arr1[i];
                    i += 1;
                    k += 1;
                }
                else
                {
                    result[k] = arr2[j];
                    j += 1;
                    k += 1;
                }
            }
            while (k < total && i < arr1.Length)
                if (arr1[i] != null) { result[k] = arr1[i]; k += 1; i += 1; }
                else i += 1;
            while (k < total && j < arr2.Length)
                if (arr2[j] != null) { result[k] = arr2[j]; k += 1; j += 1; }
                else j += 1;
            return result;
        }

        public void Print() => Console.WriteLine(this.ToString());

        public override string ToString() =>
            $"Group{{name: {this.name}, manTeamsCount: {CountNonNull(this.manTeams)}, womanTeamsCount: {CountNonNull(this.womanTeams)}}}";

        private static int CountNonNull(Team?[] arr)
        {
            int c = 0;
            for (int i = 0; i < arr.Length; i += 1)
                if (arr[i] != null) c += 1;
            return c;
        }
    }
}