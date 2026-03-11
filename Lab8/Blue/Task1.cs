namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            readonly string _name;
            protected int _votes;

            public string Name => _name;
            public int Votes => _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public virtual void CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0)
                    return;

                int count = 0;

                foreach (var r in responses)
                {
                    if (r.Name == this.Name)
                        count++;
                }

                foreach (var r in responses)
                {
                    if (r.Name == this.Name)
                        r._votes = count;
                }
            }

            public virtual void Print()
            {
                return;
            }
        }

        public class HumanResponse : Response
        {
            readonly string _surname;

            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override void CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0)
                    return;

                int count = 0;

                foreach (var candidate in responses)
                {
                    if (candidate is HumanResponse hr)
                    {
                        if (hr.Name == this.Name && hr.Surname == this.Surname)
                            count++;
                    }
                }

                foreach (var candidate in responses)
                {
                    if (candidate is HumanResponse hr)
                    {
                        if (hr.Name == this.Name && hr.Surname == this.Surname)
                            hr._votes = count;
                    }
                }
            }

            public override void Print()
            {
                return;
            }
        }

    }
}