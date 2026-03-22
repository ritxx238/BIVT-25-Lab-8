using System.Runtime;
using System.Xml.Linq;

namespace Lab8.Purple
{
    public class Task5
    {
      public class Report
      {
	private Research[] _researches;
	private static int _researchCount;

	public Research[] Researches => _researches;

	static Report()
	{
	  _researchCount = 1;
	}

	public Report()
	{
	  _researches = new Research[0];
	}

	public Research MakeResearch()
	{
	  Research r = new Research($"No_{_researchCount++}_{DateTime.Today.Month}/{DateTime.Today.Year % 100}");
	  _researches = _researches.Append(r).ToArray();
	  return r;
	}

	public (string, double)[] GetGeneralReport(int question)
	{
	  int total = 0;
	  var dict = new Dictionary<string, int>();

	  for (int i = 0; i < _researches.Length; i++)
	  {
	    Response[] responses = _researches[i].Responses;

	    for (int j = 0; j < responses.Length; j++)
	    {
	      string response = "";

	      switch (question){
		case 1:
		  response = responses[j].Animal;
		  break;
		case 2:
		  response = responses[j].CharacterTrait;
		  break;
		case 3:
		  response = responses[j].Concept;
		  break;
	      }

	      if (response == null) continue; 

	      total++;
	      dict[response] = dict.ContainsKey(response) ? dict[response] + 1 : 1;
	    }
	  }

	  return dict.Select(x => (x.Key, x.Value * 100.0 / total)).ToArray();
	}
      }

      public struct Response
      {
	private string _animal;
	private string _characterTrait;
	private string _concept;

	public string Animal => _animal;
	public string CharacterTrait => _characterTrait;
	public string Concept => _concept;

	public Response (string a, string ch, string c)
	{
	  _animal = a;
	  _characterTrait = ch;
	  _concept = c;
	}

	private bool compare(Response l, Response r)
	{
	  return (l.Concept == r.Concept && l.Animal == r.Animal && l.CharacterTrait == r.CharacterTrait);
	}
	  

	public int CountVotes(Response[] responses, int questionNumber)
	{
	  int s = 0;
	  for (int i = 0; i < responses.Length; i++)
	  {
	    switch (questionNumber){
	      case 1:
		if (responses[i].Animal == this.Animal)
		{
		  s+=1;
		}
		break;
	      case 2:
		if (responses[i].CharacterTrait == this.CharacterTrait)
		{
		  s+=1;
		}
		break;
	      case 3:
		if (responses[i].Concept == this.Concept)
		{
		  s+=1;
		}
		break;
	    }
	  }
	  return s;
	}

	public void Print()
	{
	  Console.Write($"Animal: {Animal}\nCharacterTrait: {CharacterTrait}\nConcept: {Concept}\n");
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

	private void Append(ref Response[] rs, Response r)
	{
	  Response[] buff = new Response[rs.Length+1];
	  for (int i = 0; i < rs.Length; i++)
	  {
	    buff[i] = rs[i];
	  }
	  buff[rs.Length] = r;
	  rs = buff;
	}

	public void Add(string[] answers)
	{
	  Append(ref _responses, new Response(answers[0], answers[1], answers[2]));
	}

	public string[] GetTopResponses(int question)
	{
	  Dictionary<string, int> data = new Dictionary<string, int>();
	  for (int i = 0; i < _responses.Length; i++)
	  {
	    switch (question){
	      case 1:
		if (Responses[i].Animal == null) {break;}
		data[Responses[i].Animal] = 0;
		break;
	      case 2:
		if (Responses[i].CharacterTrait == null) break;
		data[Responses[i].CharacterTrait] = 0;
		break;
	      case 3:
		if (Responses[i].Concept == null) break;
		data[Responses[i].Concept] = 0;
		break;
	    }
	  }
	  for (int i = 0; i < _responses.Length; i++)
	  {
	    switch (question){
	      case 1:
		if (Responses[i].Animal == null) {break;}
		data[Responses[i].Animal]++;
		break;
	      case 2:
		if (Responses[i].CharacterTrait == null) break;
		data[Responses[i].CharacterTrait]++;
		break;
	      case 3:
		if (Responses[i].Concept == null) break;
		data[Responses[i].Concept]++;
		break;
	    }
	  }

	  //if (data.ContainsKey("Тануки"))throw new Exception(data["Панда"].ToString());
	  data = data.OrderByDescending(data => data.Value).ToDictionary<string, int>();

	  string[] answer = new string[Math.Min(5, data.Count)];
	  int done = 0;
	  foreach (KeyValuePair<string, int> a in data){
	    if (done >= 5) break;
	    answer[done++] = a.Key;
	  }
/*
	  int[] counts = new int[Responses.Length];

	  for (int i = 0; i < Responses.Length; i++){
	    for (int j = 0; j < Responses.Length; j++){
	      switch (question){
		case 1:
		  if (Responses[i].Animal == null) break;
		  if (Responses[j].Animal == null) break;
		  if (Responses[i].Animal == Responses[j].Animal) counts[i]++;
		  break;
		case 2:
		  if (Responses[i].CharacterTrait == null) break;
		  if (Responses[j].CharacterTrait == null) break;
		  if (Responses[i].CharacterTrait == Responses[j].CharacterTrait) counts[i]++;
		  break;
		case 3:
		  if (Responses[i].Concept == null) break;
		  if (Responses[j].Concept == null) break;
		  if (Responses[i].Concept == Responses[j].Concept) counts[i]++;
		  break;
	      }
	    }
	  }

	  Response[] temp = new Response[0];
	  temp = Responses.ToArray();

	  int pos = 1;
	  while (pos < counts.Length)
	  {
	    if (counts[pos] <= counts[pos-1])
	    {
	      pos++;
	    }
	    else
	    {
	      (counts[pos], counts[pos-1]) = (counts[pos-1], counts[pos]);
	      (temp[pos], temp[pos-1]) = (temp[pos-1], temp[pos]);
	      if (pos > 1)
	      {
		pos--;
	      }
	    }
	  }
	  
	  string[] ans = new string[0];
	  int found = 0;
	  int ind = 0;
	  string last = "";
	  while (found < 5 && ind < temp.Length)
	  {
	    string cur = "";
	    switch (question){
	      case 1:
		if (Responses[ind].Animal == null) break;
		cur = Responses[ind].Animal;
		break;
	      case 2:
		if (Responses[ind].CharacterTrait == null) break;
		cur = Responses[ind].CharacterTrait;
		break;
	      case 3:
		if (Responses[ind].Concept == null) break;
		cur = Responses[ind].Concept;
		break;
	    }
	    if (cur == null) {ind++; continue;}
	    if (cur != last){
	      ans = ans.Append(cur).ToArray();
	      last = cur;
	      found++;
	    }
	    ind++;
	  }

*/
	  return answer;
	}

	public void Print()
	{
	  Console.Write($"Name: {Name}\nReponses:\n\n");
	  foreach (Response r in Responses)
	  {
	    r.Print();
	  }
	}
      }

      
    }
}
