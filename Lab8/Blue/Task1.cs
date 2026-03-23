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
            }
            
            public virtual int CountVotes(Response[] responses)
            {
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (_name == responses[i]._name)
                    {
                        count++;
                    }
                }
                
                for (int i = 0; i < responses.Length; i++)
                {
                    if (_name == responses[i]._name)
                    {
                        responses[i]._votes = count;
                    }
                }
                
                return count;
            }
            
            public virtual void Print()
            {
                Console.WriteLine("Name: " + _name +  ", Votes: " + _votes);
            }
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
                    // Используем преобразование типов для доступа к полям HumanResponse
                    if (responses[i] is HumanResponse humanResp)
                    {
                        if (Name == humanResp.Name && _surname == humanResp._surname)
                        {
                            count++;
                        }
                    }
                }
                
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse humanResp)
                    {
                        if (Name == humanResp.Name && _surname == humanResp._surname)
                        {
                            humanResp._votes = count;
                        }
                    }
                }
                
                return count;
            }
            
            public override void Print()
            {
                Console.WriteLine($"Name: {Name}, Surname: {_surname}, Votes: {_votes}");
            }
            
        }
    }
}
