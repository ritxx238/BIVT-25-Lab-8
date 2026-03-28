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
            public int CountVotes(Response[] responses, int questionNumber)
            {
                int cnt = 0;
                switch (questionNumber)
                {
                    case 1:
                        {
                            foreach (var res in responses)
                            {
                                if (_animal == res.Animal) cnt++;
                            }
                            break;
                        }
                    case 2:
                        {
                            foreach (var res in responses)
                            {
                                if (_characterTrait == res.CharacterTrait) cnt++;
                            }
                            break;
                        }
                    case 3:
                        {
                            foreach (var res in responses)
                            {
                                if (_concept == res.Concept) cnt++;
                            }
                            break;
                        }
                }

                return cnt;
            }
            public void Print()
            {
                Console.WriteLine($"animal: {_animal}, character trait: {_characterTrait}, concept: {_concept}");
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
                Response r = new Response(answers[0], answers[1], answers[2]);
                if (answers == null || answers.Length != 3) return;
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = r;
            }
            public string[] GetTopResponses(int question)
            {
                if (_responses == null || _responses.Length == 0) return new string[0];
                string[] all_ans = new string[_responses.Length];
                switch (question)
                {
                    case 1:
                        for (int i = 0; i < _responses.Length; i++)
                        {
                            all_ans[i] = _responses[i].Animal;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < _responses.Length; i++)
                        {
                            all_ans[i] = _responses[i].CharacterTrait;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < _responses.Length; i++)
                        {
                            all_ans[i] = _responses[i].Concept;
                        }
                        break;
                }
                string[] no_repeat = all_ans.Distinct().Where(x => !string.IsNullOrEmpty(x)).ToArray();

                int n = no_repeat.Length;
                int[] reps = new int[n];
                int k = 0;
                foreach (string s in no_repeat)
                {
                    reps[k++] = all_ans.Count(w => w == s);
                }
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (reps[j + 1] > reps[j])
                        {
                            (no_repeat[j + 1], no_repeat[j]) = (no_repeat[j], no_repeat[j + 1]);
                            (reps[j + 1], reps[j]) = (reps[j], reps[j + 1]);

                        }
                    }
                }
                int size = Math.Min(5, n);
                string[] res = new string[size];
                Array.Copy(no_repeat, res, size);
                return res;

            }
            public void Print()
            {
                Console.WriteLine($"name: {_name}");
                foreach (var s in _responses)
                {
                    s.Print();
                }
            }


        }

        public class Report
        {
            private Research[] _researches;
            private static int _num;
            public Research[] Researches => _researches;
            static Report()
            {
                _num = 1;
            }
            public Report()
            {
                _researches = new Research[0];
            }
            public Research MakeResearch()
            {
                var newRes = new Research($"No_{_num++}_{DateTime.Now.Month}/{DateTime.Now.Year}");
                Array.Resize(ref _researches, _researches.Length + 1);
                _researches[_researches.Length - 1] = newRes;
                return newRes;
            }
            public (string, double)[] GetGeneralReport(int question)
            {

                // _researches - масссив исследований, у массива - массив анкет
                string ans = null;
                string[] all_ans = new string[0];
                int k = 0;
                for (int i = 0; i < _researches.Length; i++)
                {
                    Response[] responses = _researches[i].Responses; //-массив анкет
                    for (int j = 0; j < responses.Length; j++)
                    {
                        if (question == 1) ans = responses[j].Animal;
                        if (question == 2) ans = responses[j].CharacterTrait;
                        if (question == 3) ans = responses[j].Concept;
                        if (string.IsNullOrEmpty(ans)) continue;
                        Array.Resize(ref all_ans, all_ans.Length + 1);
                        all_ans[k++] = ans;

                    }
                }
                string[] no_repeat = all_ans.Distinct().ToArray();
                int[] repeat_counts = new int[no_repeat.Length];
                int m = 0;
                (string, double)[] result = new (string, double)[no_repeat.Length];
                foreach (string s in no_repeat)
                {
                    repeat_counts[m++] = all_ans.Count(w => w == s);
                }
                int total = all_ans.Length;
                for (int i = 0; i < no_repeat.Length; i++)
                {
                    result[i] = (no_repeat[i], (double) repeat_counts[i] / total * 100.0);
                }
                return result;
            }
        }
    }

}
