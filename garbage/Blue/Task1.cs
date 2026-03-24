namespace Lab7.Blue
{
    public class Task1
    {
        public struct Response(string name, string surname)
        {
            readonly string name = name, surname = surname;
            int votes = 0; // Имеют ли смысл отрицательные голоса?

            public readonly string Name => name;
            public readonly string Surname => surname;
            public readonly int Votes => votes;

            public int CountVotes(Response[] responses)
            {
                this.votes = responses.Count(Same);

                for (int i = 0; i < responses.Length; i += 1)
                {
                    ref var r = ref responses[i];
                    if (Same(r)) { r.votes = this.votes; }
                }

                return this.votes;
            }

            public readonly void Print() => Console.WriteLine(this.ToString()); // 0 refs -- метод такой типа: "Я никому не ужин"

            readonly bool Same(Response other) => this.name == other.name && this.surname == other.surname;

            override public readonly string ToString() => 
                $"Response{{name: \"{name}\", surname: \"{surname}\", votes: {votes}}}";
        }
    }
}