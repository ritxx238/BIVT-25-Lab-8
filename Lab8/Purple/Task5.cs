namespace Lab8.Purple
{
    public class Task5
    {
         public struct Response
        {
            private string _animal;
            private string _charactertrait;
            private string _concept;
            public string Animal => _animal;
            public string CharacterTrait => _charactertrait;
            public string Concept => _concept;

            public Response(string animal, string charactertrait, string concept)
            {
                _animal = animal;
                _charactertrait = charactertrait;
                _concept = concept;
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || questionNumber > 3 || questionNumber < 1) return default(int);
                int count = 0;
                if (questionNumber == 1)
                {
                    foreach (Response r in responses)
                    {
                        if (_animal == r._animal)
                        {
                            count++;
                        }
                    }
                }
                else if (questionNumber == 2)
                {
                    foreach (Response r in responses)
                    {
                        if (_charactertrait == r._charactertrait)
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    foreach (Response r in responses)
                    {
                        if (_concept == r._concept)
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
            public void Print(){}
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
                if (answers == null) return;
                Array.Resize(ref _responses,_responses.Length+1);
                _responses[_responses.Length - 1] = new Response(answers[0], answers[1], answers[2]);
            }

            public string[] GetTopResponses(int question)
            {
                if (question > 3 || question < 1) return null;
                string[] answers = null;
                if (question == 1)
                {
                    Response[] responses = new Response[_responses.Length];
                    int count = 0;
                    foreach (Response r in _responses)
                    {
                        if (r.Animal == null) continue;
                        int n = 0;
                        for (int i = 0; i < count; i++)
                        {
                            if (r.Animal == responses[i].Animal)
                            {
                                n = 1;
                                break;
                            }
                        }

                        if (n == 0)
                        {
                            responses[count] = r;
                            count++;
                        }
                    }

                    int i1 = 0;
                    while (i1 < count)
                    {
                        if (i1 == 0 || responses[i1].CountVotes(_responses, question) <=
                            responses[i1 - 1].CountVotes(_responses, question))
                        {
                            i1++;
                        }
                        else
                        {
                            Response tmp = responses[i1];
                            responses[i1] = responses[i1 - 1];
                            responses[i1 - 1] = tmp;
                            i1--;
                        }
                    }

                    if (count > 5) count = 5;
                    answers = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        answers[i] = responses[i].Animal;
                    }
                }
                else if (question == 2)
                {
                    Response[] responses = new Response[_responses.Length];
                    int count = 0;
                    foreach (Response r in _responses)
                    {
                        if (r.CharacterTrait == null) continue;
                        int n = 0;
                        for (int i = 0; i < count; i++)
                        {
                            if (r.CharacterTrait == responses[i].CharacterTrait)
                            {
                                n = 1;
                                break;
                            }
                        }

                        if (n == 0)
                        {
                            responses[count] = r;
                            count++;
                        }
                    }

                    int i1 = 0;
                    while (i1 < count)
                    {
                        if (i1 == 0 || responses[i1].CountVotes(_responses, question) <=
                            responses[i1 - 1].CountVotes(_responses, question))
                        {
                            i1++;
                        }
                        else
                        {
                            Response tmp = responses[i1];
                            responses[i1] = responses[i1 - 1];
                            responses[i1 - 1] = tmp;
                            i1--;
                        }
                    }

                    if (count > 5) count = 5;
                    answers = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        answers[i] = responses[i].CharacterTrait;
                    }
                }
                else
                {
                    Response[] responses = new Response[_responses.Length];
                    int count = 0;
                    foreach (Response r in _responses)
                    {
                        if (r.Concept == null) continue;
                        int n = 0;
                        for (int i = 0; i < count; i++)
                        {
                            if (r.Concept == responses[i].Concept)
                            {
                                n = 1;
                                break;
                            }
                        }

                        if (n == 0)
                        {
                            responses[count] = r;
                            count++;
                        }
                    }

                    int i1 = 0;
                    while (i1 < count)
                    {
                        if (i1 == 0 || responses[i1].CountVotes(_responses, question) <=
                            responses[i1 - 1].CountVotes(_responses, question))
                        {
                            i1++;
                        }
                        else
                        {
                            Response tmp = responses[i1];
                            responses[i1] = responses[i1 - 1];
                            responses[i1 - 1] = tmp;
                            i1--;
                        }
                    }

                    if (count > 5) count = 5;
                    answers = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        answers[i] = responses[i].Concept;
                    }
                }

                return answers;
            }
            public void Print(){}
        }

        public class Report
        {
            private Research[] _researches;
            private static int _count;
            private DateTime _datetime;
            public Research[] Researches => _researches;

            static Report()
            {
                _count = 1;
            }

            public Report()
            {
                _researches = new Research[0];
                _datetime = new DateTime();
            }

            public Research MakeResearch()
            {
                Research research = new Research($"No_{_count}_{_datetime.Month}/{_datetime.Year}");
                _count++;
                Array.Resize(ref _researches, _researches.Length+1);
                _researches[_researches.Length - 1] = research;
                return research;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                string[] responses = new string[0];
                for (int i = 0; i < _researches.Length; i++)
                {
                    GetResponses(_researches[i].Responses, ref responses, question);
                }

                double[] average = new double[responses.Length];
                for (int i = 0; i < responses.Length; i++)
                {
                    for (int j = 0; j < _researches.Length; j++)
                    {
                        average[i] += Count(_researches[j].Responses, responses[i], question);
                    }
                }

                double total = 0; 
                for (int i = 0; i < average.Length; i++)
                {
                    total += average[i];
                }

                for (int i = 0; i < average.Length; i++)
                {
                    average[i] /= total;
                }
                
                (string, double)[] ans = new (string, double)[responses.Length];
                for (int i = 0; i < ans.Length; i++)
                {
                    ans[i] = (responses[i], average[i] * 100);
                }

                return ans;
            }

            private void GetResponses(Response[] responses, ref string[] array, int question)
            {
                for (int i = 0; i < responses.Length; i++)
                {
                    bool meet = false;
                    for (int j = 0; j < array.Length; j++)
                    {
                        if (question == 1)
                        {
                            if (responses[i].Animal == null) continue;
                            if (responses[i].Animal == array[j])
                            {
                                meet = true;
                                break;
                            }
                        }
                        else if (question == 2)
                        {
                            if (responses[i].CharacterTrait == null) continue;
                            if (responses[i].CharacterTrait == array[j])
                            {
                                meet = true;
                                break;
                            }
                        }
                        else
                        {
                            if (responses[i].Concept == null) continue;
                            if (responses[i].Concept == array[j])
                            {
                                meet = true;
                                break;
                            }
                        }
                    }

                    if (!meet)
                    {
                        if (question == 1)
                        {
                            if (responses[i].Animal == null) continue;
                            Array.Resize(ref array, array.Length + 1);
                            array[array.Length - 1] = responses[i].Animal;
                        }
                        else if (question == 2)
                        {
                            if (responses[i].CharacterTrait == null) continue;
                            Array.Resize(ref array, array.Length + 1);
                            array[array.Length - 1] = responses[i].CharacterTrait;
                        }
                        else
                        { 
                            if (responses[i].Concept == null) continue;
                            Array.Resize(ref array, array.Length + 1);
                            array[array.Length - 1] = responses[i].Concept;
                        }
                    }
                }
            }

            private int Count(Response[] responses, string response, int question)
            {
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (question == 1)
                    {
                        if (responses[i].Animal == response)
                        {
                            count++;
                        }
                    }
                    else if (question == 2)
                    {
                        if (responses[i].CharacterTrait == response)
                        {
                            count++;
                        }
                    }
                    else
                    {
                        if (responses[i].Concept == response)
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
        }
    }
}
