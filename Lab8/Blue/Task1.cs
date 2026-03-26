namespace Lab8.Blue
{
    public class Task1
    {
        public class Response
        {
            protected string _Name;
            protected int _Votes=0;
            public string Name => _Name;
            public int Votes => _Votes;
            
            public virtual void CountVotes(Response[] responses)
            {
                int count=0;
                if(responses==null)return;
                foreach( var response in responses)
                {
                    if (response != null && response.Name == this.Name)
                    {
                        count++;
                    }
                }
                
                foreach( var response in responses)
                {
                    if (response != null && response.Name == this.Name)
                    {
                        response._Votes=count;
                    }
                }

                
            }
            
            public virtual void Print()
            {
                Console.WriteLine($"Name: {_Name}, Votes: {_Votes}");
            }

            public Response(string Name)
            {
                _Name = Name;
            }
        }
        
        public class HumanResponse : Response
        {
            private string _Surname;
            public string Surname => _Surname;
            
            public HumanResponse(string Name, string Surname) : base(Name)
            {
                _Surname = Surname;
            }
            public override void CountVotes(Response[] responses)
            {
                int count=0;
                if(responses==null)return;
                foreach(var response in responses)
                {
                    if(response is HumanResponse human )
                    {
                        if(human.Name==this.Name && human.Surname==this.Surname)
                        {
                            count++;
                        }
                    }
                }

                foreach(var response in responses)
                {
                    if(response is HumanResponse human )
                    {
                        if(human.Name==this.Name && human.Surname==this.Surname)
                        {
                            human._Votes=count;
                        }
                    }
                }
            }
            public override void Print()
            {
                Console.WriteLine($"Name: {_Name}, Surname: {_Surname}, Votes: {_Votes}");
            }
            
        }
    }
}