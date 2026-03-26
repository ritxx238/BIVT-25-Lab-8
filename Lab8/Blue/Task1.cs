using System.Security.Cryptography.X509Certificates;
using static Lab8.Blue.Task1;

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
            public virtual int CountVotes(Response[] responses)
            {
                int local_votes = 0;
                foreach (Response response in responses)
                {
                    if (_name == response.Name)
                    {
                        local_votes++;
                    }
                }

                for (int i = 0; i < responses.Length; i++)
                {
                    if (_name == responses[i].Name)
                    {
                        responses[i]._votes = local_votes;
                    }
                }
                _votes = local_votes;

                return Votes;
            }
            public virtual void Print()
            {
                Console.WriteLine($"Name: {Name}\n Votes: {Votes}\n");
            }

        }
        public class HumanResponse : Response
        {
            private string _surename;
            public string Surname => _surename;
            public HumanResponse(string name, string surname) : base(name)
            {
                _surename = surname;
            }
            public override int CountVotes(Response[] responses)
            {
                {
                    int local_votes = 0;
                    foreach (Response response in responses)
                    {
                        if (response is HumanResponse human && Name == human.Name & _surename == human.Surname)
                        {
                            local_votes++;
                        }
                    }

                    for (int i = 0; i < responses.Length; i++)
                    {
                        if (responses[i] is HumanResponse human && Name == human.Name & _surename == human.Surname)
                        {
                            human._votes = local_votes;
                        }
                    }

                    return _votes;
                }
            
            }
            public override void Print()
            {
                Console.WriteLine($"Name: {Name}\n Surname: {Surname}\n Votes: {Votes}\n");
            }
        }
    }
}
