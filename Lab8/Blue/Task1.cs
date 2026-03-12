

namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            private string _name;
            protected int _votes;
            public Response(string name)
            {
                if (name != null)
                {
                    _name = name;
                }
                else
                {
                    _name = "";
                }
                _votes = 0; 
            }
            public int Votes => _votes;
            public string Name => _name;

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null)
                {
                    return 0;
                }


                int count = 0;
                string CurrName = Name;

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == CurrName)
                    {
                        count++;
                    }
                }
                _votes = count;
                return _votes;

            }

            public virtual void Print()
            {
                Console.WriteLine("Бибибиб");
            }

            

        }
        public class HumanResponse : Response
        {
            private string _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                if (surname != null)
                {
                    _surname = surname;
                }
                else
                {
                    _surname = "";
                }
            }
            public string Surname => _surname;

            public override int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;

                int count = 0;
                string CurrName = Name;
                string CurrSurname = Surname;

                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse hummRes && hummRes.Name == CurrName && hummRes.Surname== CurrSurname)
                    {
                        count++;
                    }
                }
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i] is HumanResponse hummRes &&
                        hummRes.Name == CurrName && hummRes.Surname == CurrSurname)
                    {
                        hummRes._votes = count;
                    }
                }

                _votes = count;
                return _votes;
            }

            public override void Print()
            {
                Console.WriteLine("Гигигиг");
            }
            

        }
    }
}