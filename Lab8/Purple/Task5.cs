using System;
using System.Linq;

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
                if (responses == null) return 0;
                int count = 0;
                foreach (var r in responses)
                {
                    switch (questionNumber)
                    {
                        case 1: if (r.Animal == _animal) count++; break;
                        case 2: if (r.CharacterTrait == _characterTrait) count++; break;
                        case 3: if (r.Concept == _concept) count++; break;
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
            public Response[] Responses => _responses.ToArray();

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] answers)
            {
                Response resp = new Response(answers[0], answers[1], answers[2]);
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = resp;
            }

            private struct AnswerStat
            {
                public string Value;
                public int Count;
                public int FirstIndex;
            }

            public string[] GetTopResponses(int question)
            {
                string[] raw = new string[_responses.Length];
                for (int i = 0; i < _responses.Length; i++)
                {
                    if (question == 1) raw[i] = _responses[i].Animal;
                    else if (question == 2) raw[i] = _responses[i].CharacterTrait;
                    else if (question == 3) raw[i] = _responses[i].Concept;
                }

                AnswerStat[] stats = new AnswerStat[_responses.Length];
                int uniqueCount = 0;

                for (int i = 0; i < raw.Length; i++)
                {
                    if (string.IsNullOrEmpty(raw[i])) continue;
                    int foundIdx = -1;
                    for (int k = 0; k < uniqueCount; k++)
                    {
                        if (stats[k].Value == raw[i]) { foundIdx = k; break; }
                    }

                    if (foundIdx == -1)
                    {
                        stats[uniqueCount] = new AnswerStat { Value = raw[i], Count = 1, FirstIndex = i };
                        uniqueCount++;
                    }
                    else stats[foundIdx].Count++;
                }

                for (int i = 0; i < uniqueCount - 1; i++)
                {
                    for (int j = 0; j < uniqueCount - i - 1; j++)
                    {
                        if (stats[j].Count < stats[j + 1].Count || (stats[j].Count == stats[j + 1].Count && stats[j].FirstIndex > stats[j + 1].FirstIndex))
                        {
                            var temp = stats[j];
                            stats[j] = stats[j + 1];
                            stats[j + 1] = temp;
                        }
                    }
                }

                int size = Math.Min(uniqueCount, 5);
                string[] result = new string[size];
                for (int i = 0; i < size; i++) result[i] = stats[i].Value;
                return result;
            }

            public void Print()
            {
                Console.WriteLine(_name);
                foreach (var r in _responses) r.Print();
            }
        }

        public class Report
        {
            private Research[] _researches;
            private static int _idCounter;

            public Research[] Researches => _researches;

            static Report()
            {
                _idCounter = 1;
            }

            public Report()
            {
                _researches = new Research[0];
            }

            public Research MakeResearch()
            {
                string date = DateTime.Now.ToString("MM/yy");
                string name = $"No_{_idCounter}_{date}";
                _idCounter++;

                Research newResearch = new Research(name);
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[_researches.Length - 1] = newResearch;

                return newResearch;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                int totalCapacity = 0;
                foreach (var res in _researches) 
                {
                    if (res.Responses != null) totalCapacity += res.Responses.Length;
                }

                string[] allAnswers = new string[totalCapacity];
                int idx = 0;
                int totalValid = 0;

                foreach (var res in _researches)
                {
                    var responses = res.Responses;
                    if (responses == null) continue;
                    foreach (var r in responses)
                    {
                        string val = "";
                        if (question == 1) val = r.Animal;
                        else if (question == 2) val = r.CharacterTrait;
                        else if (question == 3) val = r.Concept;

                        if (!string.IsNullOrEmpty(val))
                        {
                            allAnswers[idx++] = val;
                            totalValid++;
                        }
                    }
                }


                var uniqueAnswers = new string[totalValid];
                var counts = new int[totalValid];
                int uniqueCount = 0;

                for (int i = 0; i < idx; i++)
                {
                    string current = allAnswers[i];
                    if (current == null) continue;
                    int found = -1;
                    for (int k = 0; k < uniqueCount; k++)
                    {
                        if (uniqueAnswers[k] == current) { found = k; break; }
                    }

                    if (found == -1)
                    {
                        uniqueAnswers[uniqueCount] = current;
                        counts[uniqueCount] = 1;
                        uniqueCount++;
                    }
                    else counts[found]++;
                }

                (string, double)[] result = new (string, double)[uniqueCount];
                for (int i = 0; i < uniqueCount; i++)
                {
                    double percent = (counts[i] * 100.0) / totalValid;
                    result[i] = (uniqueAnswers[i], percent);
                }

                return result;
            }
        }
    }
}