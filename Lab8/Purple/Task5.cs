namespace Lab8.Purple
{
    public class Task5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }
            internal string ReturnResponse(int number)
            {
                switch (number)
                {
                    case 1:
                        return this.Animal;
                    case 2:
                        return this.CharacterTrait;
                    case 3:
                        return this.Concept;
                    default:
                        return null;
                }
            }
            public int CountVotes(Response[] responses, int questionNumber)
            {
                int SameVote = 0;
                if (this.ReturnResponse(questionNumber) != null)
                {
                    foreach (Response response in responses)
                    {
                        if (this.ReturnResponse(questionNumber).CompareTo(response.ReturnResponse(questionNumber)) == 0)
                            SameVote++;
                    }
                }
                return SameVote;
            }
            public void Print()
            {
                Console.WriteLine($"{_animal}  {_characterTrait}  {_concept}");
            }

        }
        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[] Responses => _responses;

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }
            public void Add(string[] answers)
            {
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[^1] = new Response(answers[0], answers[1], answers[2]);
            }
            public string[] GetTopResponses(int question)
            {
                string[] TopResponses = new string[0];
                for (int i = 0; i < _responses.Length; i++)
                {
                    for (int j = i; j > 0 && _responses[j].CountVotes(_responses, question) > _responses[j - 1].CountVotes(_responses, question); j--)
                    {
                        (_responses[j], _responses[j - 1]) = (_responses[j - 1], Responses[j]);
                    }
                }
                foreach (Response response in _responses)
                {
                    bool uniqe = true;
                    foreach (string answer in TopResponses)
                    {
                        if (response.ReturnResponse(question) == null || response.ReturnResponse(question).CompareTo(answer) == 0)
                        { uniqe = false; break; }
                    }
                    if (response.ReturnResponse(question) != null && uniqe && TopResponses.Length < 5)
                    {
                        Array.Resize(ref TopResponses, TopResponses.Length + 1);
                        TopResponses[^1] = response.ReturnResponse(question);
                    }
                }
                return TopResponses;
            }
            public void Print()
            {

            }
        }
        public class Report
        {
            private Research[] _researches;
            private static int _global;
            public Research[] Researches => _researches;
            static Report()
            {
                _global = 1;
            }
            public Report()
            {
                _researches = new Research[0];
            }
            public Research MakeResearch()
            {
                Research newresearch = new Research($"No_{_global++}_{DateTime.Now.Month.ToString().PadLeft(2, '0')}/{(DateTime.Now.Year % 100).ToString().PadLeft(2, '0')}");
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[^1] = newresearch;
                return newresearch;
            }
            public (string, double)[] GetGeneralReport(int question)
            {
                (string, double)[] report = new (string, double)[0];
                Response[] responses = new Response[0];
                foreach( Research research in _researches )
                {
                    foreach (Response response in research.Responses )
                    {
                        if (response.ReturnResponse(question) != null)
                        {
                            Array.Resize(ref responses, responses.Length + 1);
                            responses[^1] = response;
                        }
                    }
                }
                Response[] uniqe = new Response[0];
                foreach (Response response in responses)
                {
                    bool repeat = false;
                    foreach (Response item in uniqe)
                    {
                        if (item.ReturnResponse(question).CompareTo(response.ReturnResponse(question))==0)
                        {
                            repeat = true;
                        }
                    }
                    if (repeat == false)
                    {
                        Array.Resize(ref uniqe, uniqe.Length + 1);
                        uniqe[^1] = response;
                    }
                }
                
                foreach (Response response in uniqe)
                {
                    Array.Resize(ref report, report.Length + 1);
                    report[^1] = (response.ReturnResponse(question),(double)response.CountVotes(responses,question)/responses.Length*100);
                }
                return report.OrderByDescending(x => x.Item2).ToArray();
            }
        }
    }
}
