using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

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
                _name = (name == null) ? "" : name;
                _votes = 0;
            }

            virtual public int CountVotes(Response[] responses)
            {
                int count = 0;

                //  считаем голоса для этого чела

                for (int i = 0; i < responses.Length; i++)
                {
                    if (_name == responses[i]._name)
                    {
                        count++;
                    }
                }

                // меняем для всего него

                for (int i = 0; i < responses.Length; i++)
                {
                    if (_name == responses[i]._name)
                    {
                        responses[i]._votes = count;
                    }
                }

                return count;
            }
            virtual public void Print()
            {
                Console.WriteLine($"{_name}: {_votes}");
            }
        }

        public class HumanResponse : Response
        {
            private string _surname;
            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = (surname == null) ? "" : surname;
            }

            override public int CountVotes(Response[] responses)
            {
                int count = 0;

                //  считаем голоса для этого чела

                for (int i = 0; i < responses.Length; i++)
                {
                    if (Name == responses[i].Name && _surname == ((HumanResponse)responses[i])._surname)
                    {
                        count++;
                    }
                }

                // меняем для всего него

                for (int i = 0; i < responses.Length; i++)
                {
                    if (Name == responses[i].Name && _surname == ((HumanResponse)responses[i])._surname)
                    {
                        ((HumanResponse)responses[i])._votes = count;
                    }
                }

                return count;
            }

            override public void Print()
            {
                Console.WriteLine($"{Name} {_surname}: {_votes}");
            }
        }
    }

    
}

