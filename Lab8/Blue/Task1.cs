namespace Lab8.Blue;
public class Task1
{
    public class Response
    {
        protected string _name;
        protected int _votes = 0;
        public string Name => _name;
        public int Votes => _votes;

        bool Eq(Response some) => some != null &&  _name == some.Name ;
        
        public virtual void CountVotes(Response[] responses)
        {
            int count = 0;
            if (responses == null) return;
            foreach (var response in responses)
            {
                if ( Eq(response) ) 
                {
                    count++;
                }
            }

            foreach (var response in responses)
            {
                if ( Eq(response) )
                {
                    response._votes = count;
                }
            }


        }

        public virtual void Print()
        {
            Console.WriteLine($"Name: {_name}, Votes: {_votes}");
        }
        
        public Response(string name) {
                _name = name;
        }
    }



    

    public class HumanResponse : Response
    {
        private string _surname;
        public string Surname => _surname;

        public HumanResponse(string Name, string Surname) : base(Name)
        {
            _surname = Surname;
        }

        bool Eq(HumanResponse some) => _name == some.Name && _surname == some.Surname;
        
        public override void CountVotes(Response[] responses)
        {
            int count = 0;
            if (responses == null) return;
            foreach (var response in responses)
            {
                if (response is HumanResponse human)
                {
                    if ( Eq(human) )
                    {
                        count++;
                    }
                }
            }

            foreach (var response in responses)
            {
                if (response is HumanResponse human)
                {
                    if ( Eq(human) )
                    {
                        human._votes = count;
                    }
                }
            }
        }
        public override void Print()
        {
            Console.WriteLine($"Name: {_name}, Surname: {_surname}, Votes: {_votes}");
        }

    }

}
