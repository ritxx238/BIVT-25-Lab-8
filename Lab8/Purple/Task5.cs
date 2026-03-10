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

            public int CountVotes(Response[] responses, int questionNumber)
            {
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (questionNumber == 1)
                    {
                        if (responses[i].Animal == _animal) count++;
                    }
                    if (questionNumber == 2)
                    {
                        if (responses[i].CharacterTrait == _characterTrait) count++;
                    }
                    if (questionNumber == 3)
                    {
                        if (responses[i].Concept == _concept) count++;
                    }
                }

                return count;
            }

            public void Print()
            {
                Console.WriteLine($"{_animal} {_characterTrait} {_concept}");
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[] Responses
            {
                get
                {
                    var responses = new Response[_responses.Length];
                    Array.Copy(_responses, responses, responses.Length);
                    return responses;
                }
            }

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] answers)
            {
                Array.Resize(ref _responses, _responses.Length + 1);
                var response = new Response(answers[0], answers[1], answers[2]);
                _responses[^1] = response;
            }


            public string[] GetTopResponses(int question)
            {
                if (question > 3 || question < 1 || _responses == null || _responses.Length == 0)
                {
                    return new string[0];
                }
                int countResponses;
                string resp = "";
                string[] responses = new string[0];
                int count;
                string[] allResponses = new string[0];

                if (question == 1)
                {
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        if (_responses[i].Animal != "" && _responses[i].Animal != null)
                        {
                            Array.Resize(ref allResponses, allResponses.Length + 1);
                            allResponses[^1] = _responses[i].Animal;
                        }
                    }
                }
                else if (question == 2)
                {
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        if (_responses[i].CharacterTrait != "" && _responses[i].CharacterTrait != null)
                        {
                            Array.Resize(ref allResponses, allResponses.Length + 1);
                            allResponses[^1] = _responses[i].CharacterTrait;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _responses.Length; i++)
                    {
                        if (_responses[i].Concept != "" && _responses[i].Concept != null)
                        {
                            Array.Resize(ref allResponses, allResponses.Length + 1);
                            allResponses[^1] = _responses[i].Concept;
                        }
                    }
                }

                for (int len = 0; len < 5; len++)
                {
                    count = 0;
                    resp = "";
                    foreach (string response in allResponses)
                    {
                        countResponses = allResponses.Count(x => x == response);
                        if (responses.Contains(response) == false && countResponses > count)
                        {
                            count = countResponses;
                            resp = response;
                        }
                    }
                    if (resp != "" && resp != null)
                    {
                        Array.Resize(ref responses, responses.Length + 1);
                        responses[^1] = resp;
                    }
                    else
                    {
                        break;
                    }
                }
                return responses;
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_responses}");
            }
        }

        public class Report
        {
            private Research[] _researches;
            private static int _id;

            public Research[] Researches => _researches;

            static Report()
            {
                _id = 1;
            }

            public Report()
            {
                _researches = new Research[0];
            }

            public Research MakeResearch()
            {
                DateTime date = DateTime.Now;
                string formattedDate = date.ToString("MM/yy");
                string name = "No_" + _id + "_" + formattedDate;
                Research research = new Research(name);
                _id++;
                Array.Resize(ref _researches, _researches.Length+1);
                _researches[^1] = research;
                return research;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                (string, double)[] array = new (string, double)[0];
                string ans = "";
                string[] allDifferentResponses = new string[0];
                int cntmx;
                string[] allResponses = new string[0];

                // Формируем массивы всех ответов на вопрос и всех уникальных ответов на вопрос
                if (question == 1)
                {
                    for (int j = 0; j<_researches.Length; j++)
                    {
                        for (int i = 0; i < _researches[j].Responses.Length; i++)
                        {
                            if (_researches[j].Responses[i].Animal != "" && _researches[j].Responses[i].Animal != null)
                            {
                                Array.Resize(ref allResponses, allResponses.Length + 1);
                                allResponses[^1] = _researches[j].Responses[i].Animal;
                            }
                        }
                    }
                    foreach (string answer in allResponses)
                    {
                        if (allDifferentResponses.Contains(answer) == false)
                        {
                            ans = answer;
                            Array.Resize(ref allDifferentResponses, allDifferentResponses.Length + 1);
                            allDifferentResponses[^1] = ans;
                        }
                    }
                }
                else if (question == 2)
                {
                    for (int j = 0; j < _researches.Length; j++)
                    {
                        for (int i = 0; i < _researches[j].Responses.Length; i++)
                        {
                            if (_researches[j].Responses[i].CharacterTrait != "" && _researches[j].Responses[i].CharacterTrait != null)
                            {
                                Array.Resize(ref allResponses, allResponses.Length + 1);
                                allResponses[^1] = _researches[j].Responses[i].CharacterTrait;
                            }
                        }
                    }
                    foreach (string answer in allResponses)
                    {
                        if (allDifferentResponses.Contains(answer) == false)
                        {
                            ans = answer;
                            Array.Resize(ref allDifferentResponses, allDifferentResponses.Length + 1);
                            allDifferentResponses[^1] = ans;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < _researches.Length; j++)
                    {
                        for (int i = 0; i < _researches[j].Responses.Length; i++)
                        {
                            if (_researches[j].Responses[i].Concept != "" && _researches[j].Responses[i].Concept != null)
                            {
                                Array.Resize(ref allResponses, allResponses.Length + 1);
                                allResponses[^1] = _researches[j].Responses[i].Concept;
                            }
                        }
                    }
                    foreach (string answer in allResponses)
                    {
                        if (allDifferentResponses.Contains(answer) == false)
                        {
                            ans = answer;
                            Array.Resize(ref allDifferentResponses, allDifferentResponses.Length + 1);
                            allDifferentResponses[^1] = ans;
                        }
                    }
                }
                
                //Находим процентах конкретных ответов на вопрос
                for (int i = 0; i< allDifferentResponses.Length; i++)
                {
                    int count = 0;
                    for (int j = 0; j< allResponses.Length; j++)
                    {
                        if (allResponses[j] == allDifferentResponses[i]) count++;
                    }
                    Array.Resize(ref array, array.Length + 1);
                    double percent = (double)count/allResponses.Length*100;
                    array[^1] = (allDifferentResponses[i], percent);
                }
                
                return array;
            }
        }
    }
}
