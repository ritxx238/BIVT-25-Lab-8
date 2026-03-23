namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            private string _name;
            protected int _votes;
        
            public string Name => _name;
            public int Votes => _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public virtual int CountVotes(Response [] responses)
            {
                int count = 0;
                for (int i = 0; i < responses.Length; i++) if (responses[i].Name == _name) count++;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == _name)
                    {
                        Response nr = responses[i]; nr._votes = count; responses[i] = nr;
                    }
                }
                _votes = count;
                return count;
            }
            public virtual void Print() { }
        }

        public class HumanResponse : Response
        {
            private string _surname;
            public string Surname => _surname;
            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }
            public override int CountVotes(Response[] responses)
            {
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse humanResponse)
                    {
                        if (humanResponse.Name == this.Name && humanResponse.Surname == this.Surname) count++;
                    }
                }
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse humanResponse)
                    {
                        if (humanResponse.Name == this.Name && humanResponse.Surname == this.Surname) humanResponse._votes = count;
                    }
                }
                _votes = count;
                return count;
            }
            public override void Print() { }
        }
    }
}
