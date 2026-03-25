using static Lab8.Purple.Task5;

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
                if (responses == null || responses.Length == 0)
                    return 0;

                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (questionNumber == 1)
                    {
                        if (_animal == responses[i].Animal) count++;
                    }
                    else if (questionNumber == 2)
                    {
                        if (_characterTrait == responses[i].CharacterTrait) count++;
                    }
                    else if (questionNumber == 3)
                    {
                        if (_concept == responses[i].Concept) count++;
                    }
                }

                return count;
            }

            public void Print()
            {
                Console.WriteLine($"question 1: {_animal}");
                Console.WriteLine($"question 2: {_characterTrait}");
                Console.WriteLine($"question 3: {_concept}");
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
                if (answers == null || answers.Length == 0)
                    return;

                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = new Response(answers[0], answers[1], answers[2]);
            }
            public string[] GetTopResponses(int question)
            {
                if (_responses == null || _responses.Length == 0) return new string[0];

                string[] allAnswers = new string[_responses.Length];
                int count = 0;
                for (int i = 0; i < _responses.Length; i++)
                {
                    string answer = "";
                    if (question == 1) answer = _responses[i].Animal;
                    else if (question == 2) answer = _responses[i].CharacterTrait;
                    else if (question == 3) answer = _responses[i].Concept;

                    if (answer != null && answer != "")
                    {
                        allAnswers[count] = answer;
                        count++;
                    }
                }

                if (count == 0) return new string[0];

                string[] uniqueAnswers = new string[count];
                int uniqueIndex = 0;
                int[] counts = new int[count];

                for (int i = 0; i < count; i++)
                {
                    string current = allAnswers[i];
                    bool isNew = true;

                    for (int j = 0; j < uniqueAnswers.Length; j++)
                    {
                        if (uniqueAnswers[j] == current)
                        {
                            isNew = false;
                            counts[j]++;
                            break;
                        }
                    }

                    if (isNew)
                    {
                        uniqueAnswers[uniqueIndex] = current;
                        counts[uniqueIndex] = 1;
                        uniqueIndex++;
                    }
                }

                for (int i = 0; i < counts.Length; i++)
                {
                    for (int j = 1; j < counts.Length; j++)
                    {
                        if (counts[j - 1] < counts[j])
                        {
                            int temp1 = counts[j];
                            counts[j - 1] = counts[j];
                            counts[j] = temp1;

                            string temp2 = uniqueAnswers[j - 1];
                            uniqueAnswers[j - 1] = uniqueAnswers[j];
                            uniqueAnswers[j] = temp2;
                        }
                    }
                }

                int length;
                if (uniqueIndex < 5) length = uniqueIndex;
                else
                    length = 5;

                string[] result = new string[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = uniqueAnswers[i];
                }

                return result;
            }

            public void Print()
            {
                Console.WriteLine($"Research: {Name}");
                foreach (var response in _responses)
                    response.Print();
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
                DateTime time = DateTime.Now;
                string name = $"No_{_number}_{time:MM}/{time:yy}";
                _number++;
                
                Research research = new Research(name);
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[_researches.Length - 1] = research;

                return research;
            }
            public (string, double)[] GetGeneralReport(int question)
            {
                if (_researches == null || _researches.Length == 0) return new (string, double)[0];

                int totalAnswers = 0;
                for (int i = 0; i < _researches.Length; i++)
                {
                    totalAnswers += _researches[i].Responses.Length;
                }
                string[] allAnswers = new string[totalAnswers];

                int count = 0;
                foreach (var research in _researches)
                {
                    foreach (var response in research.Responses)
                    {
                        string answer = "";
                        if (question == 1) answer = response.Animal;
                        else if (question == 2) answer = response.CharacterTrait;
                        else if (question == 3) answer = response.Concept;

                        if (answer != null && answer != "")
                        {
                            allAnswers[count] = answer;
                            count++;
                        }
                    }
                }

                if (count == 0) return new (string, double)[0];

                string[] uniqueAnswers = new string[count];
                int uniqueIndex = 0;
                int[] counts = new int[count];
                for (int i = 0; i < count; i++)
                {
                    string current = allAnswers[i];
                    bool isNew = true;

                    for (int j = 0; j < uniqueAnswers.Length; j++)
                    {
                        if (uniqueAnswers[j] == current)
                        {
                            isNew = false;
                            counts[j]++;
                            break;
                        }
                    }

                    if (isNew)
                    {
                        uniqueAnswers[uniqueIndex] = current;
                        counts[uniqueIndex] = 1;
                        uniqueIndex++;
                    }
                }

                (string, double)[] GeneralReport = new (string, double)[uniqueIndex];
                for (int i = 0; i < uniqueIndex; i++)
                {
                    double percent = (double)counts[i] / count * 100.0;
                    GeneralReport[i] = (uniqueAnswers[i], percent);
                }

                return GeneralReport;
            }
        }
    }
}
