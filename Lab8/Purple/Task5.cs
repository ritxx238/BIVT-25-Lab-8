using System.Linq.Expressions;
using System.Runtime;
using System.Runtime.InteropServices.JavaScript;
using System.Xml.Linq;

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

            private string GetAnswer(int questionNumber) => questionNumber switch
            {
                1 => _animal,
                2 => _characterTrait,
                3 => _concept,
                _ => string.Empty
            };

            public int CountVotes(Response[] responses, int questionNumber)
            {
                int count = 0;

                string answer = GetAnswer(questionNumber);

                foreach (var response in responses)
                {
                    if (response.GetAnswer(questionNumber) == answer)
                        count++;
                }

                return count;
            }

            public void Print()
            {
                Console.WriteLine($"{Animal}, {CharacterTrait}, {Concept}");
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
                _responses = [];
            }

            public void Add(string[] answers)
            {
                Response response = new Response(answers[0], answers[1], answers[2]);
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = response;
            }

            private static string GetAnswer(Response responce, int questionNumber) => questionNumber switch
            {
                1 => responce.Animal,
                2 => responce.CharacterTrait,
                3 => responce.Concept,
                _ => string.Empty
            }; //для перебора всех ответов в Research
            
            public string[] GetTopResponses(int question)
            {
                return _responses.Select(r => GetAnswer(r, question))
                    .Where(a => a != null) // не пустой
                    .GroupBy(answer => answer)//группы с одинаковыми строками
                    .Select(g => new
                    {
                        Answer = g.Key, //сам ответ от группы
                        Count = g.Count(),// количество повторений ответа
                    })
                    .OrderByDescending(x => x.Count)//сортировка по количеству повторений
                    .Select(x => x.Answer)
                    .Take(5)
                    .ToArray();
            }

            public void Print()
            {
                Console.WriteLine($"{Name}, {string.Join(", ", _responses)}");
            }
        }

        public class Report
        {
            private Research[] _researches;
            private static int _questionNumber;
            
            public Research[] Researches => _researches;

            static Report()
            {
                _questionNumber = 1;
            }

            public Report()
            {
                _researches = [];
            }

            public Research MakeResearch()
            {
                string name = $"No_{_questionNumber++}_{DateTime.Now.ToString("MM/yy")}";
                Research research = new Research(name);
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[^1] = research;
                
                return research;
            }

            private string GetAnswer(Response response, int questionNumber) => questionNumber switch
            {
                1 => response.Animal,
                2 => response.CharacterTrait,
                3 => response.Concept,
                _ => string.Empty
            };

            public (string, double)[] GetGeneralReport(int question)
            {
                string[] answers = _researches.SelectMany(r => r.Responses)//в исследованиях берем ответы
                    .Select(r => GetAnswer(r, question))//в ответах ищем такой ответ под этим номером
                    .Where(a => !string.IsNullOrEmpty(a))//проверка строки на пустоту
                    .ToArray();

                int answersCount = answers.Length;

                (string, double)[] result = answers.GroupBy(a => a)
                    .Select(g => (g.Key, g.Count() * 100.0 / answersCount))
                    .ToArray();
                
                return result;
            }
        }
    }
}