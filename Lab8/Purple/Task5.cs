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
                    if (questionNumber == 1 && responses[i].Animal == _animal)
                    {
                        count++;
                    }
                    else if (questionNumber == 2 && responses[i].CharacterTrait == _characterTrait)
                    {
                        count++;
                    }
                    else if (questionNumber == 3 && responses[i].Concept == _concept)
                    {
                        count++;
                    }
                }
                return count;
            }
            public void Print()
            {
                Console.WriteLine($"Animal: {_animal}, CharacterTrait: {_characterTrait}, Concept: {_concept}");
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
                if (answers == null) return;
                Array.Resize(ref _responses, _responses.Length+1);
                Response response = new Response(answers[0], answers[1], answers[2]);
                _responses[^1] = response;
            }
            public string[] GetTopResponses(int question)
            {
                string[] uniqueAnswers = new string[_responses.Length];
                int[] counts = new int[_responses.Length];
                int uniqueCount = 0;

                for (int i = 0; i < _responses.Length; i++)
                {
                    string answer = null;
                    if (question == 1) answer = _responses[i].Animal;
                    else if (question == 2) answer = _responses[i].CharacterTrait;
                    else if (question == 3) answer = _responses[i].Concept;
                    if (string.IsNullOrEmpty(answer)) continue;
                    
                    int index = -1;
                    for (int j = 0; j < uniqueCount; j++)
                    {
                        if (uniqueAnswers[j] == answer)
                        {
                            index = j;
                            break;
                        }
                    }
                    if (index == -1)
                    {
                        uniqueAnswers[uniqueCount] = answer;
                        counts[uniqueCount] = 1;
                        uniqueCount++;
                    }
                    else
                    {
                        counts[index] = counts[index] + 1;
                    }
                }

                for (int i = 0; i < uniqueCount - 1; i++)
                {
                    for (int j = 0; j < uniqueCount - 1 - i; j++)
                    {
                        if (counts[j] < counts[j + 1])
                        {
                            (counts[j], counts[j + 1]) = (counts[j + 1], counts[j]);
                            (uniqueAnswers[j], uniqueAnswers[j + 1]) = (uniqueAnswers[j + 1], uniqueAnswers[j]);
                        }
                    }
                }
                
                int resultLength;
                if (uniqueCount < 5)
                {
                    resultLength = uniqueCount;
                }
                else
                {
                    resultLength = 5;
                }

                string[] result = new string[resultLength];
                for (int i = 0; i < resultLength; i++)
                {
                    result[i] = uniqueAnswers[i];
                }

                return result;
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_responses}");
            }
        }

        public class Report
        {
            private Research[] _researches;
            private static int _number;

            public Research[] Researches => _researches;

            static Report()
            {
                _number = 1;
            }

            public Report()
            {
                _researches = new Research[0];
            }

            public Research MakeResearch()
            {
                int number = _number++;
                string month = DateTime.Now.Month.ToString("00");
                string year = (DateTime.Now.Year % 100).ToString("00");
                Research research = new Research($"No_{number}_{month}/{year}");
                Array.Resize(ref _researches, _researches.Length+1);
                _researches[^1] = research;
                return research;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                string[] uniqueAnswers = new string[0];
                int[] counts = new int[0];
                int totalAnswers = 0;
                string answer = null;
                for (int i = 0; i < _researches.Length; i++)
                {
                    Response[] responses = _researches[i].Responses;
                    for (int j = 0; j < responses.Length; j++)
                    {
                        if (question == 1) answer = responses[j].Animal;
                        else if (question == 2) answer = responses[j].CharacterTrait;
                        else if (question == 3) answer = responses[j].Concept;
                        if (string.IsNullOrEmpty(answer)) continue;
                        totalAnswers++;

                        int index = -1;
                        for (int k = 0; k < uniqueAnswers.Length; k++)
                        {
                            if (uniqueAnswers[k] == answer)
                            {
                                index = k;
                                break;
                            }
                        }

                        if (index == -1)
                        {
                            Array.Resize(ref uniqueAnswers, uniqueAnswers.Length+1);
                            Array.Resize(ref counts, counts.Length+1);

                            uniqueAnswers[^1] = answer;
                            counts[^1]=1;
                        }
                        else
                        {
                            counts[index]++;
                        }
                    }
                }
                (string, double)[] result = new (string, double)[uniqueAnswers.Length];
                for (int i = 0; i < uniqueAnswers.Length; i++)
                {
                    result[i] = (uniqueAnswers[i], (counts[i] * 100.0) / totalAnswers);
                }

                return result;
            }
        }
    }
}
