using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Lab8Test.Blue
{
   [TestClass]
   public sealed class Task4
   {
       record InputTeam(string Name, int[] Scores);
       record InputGroup(string Name, InputTeam[] Teams);
       record OutputTeam(string Name, int TotalScore);
       private InputGroup[] _inputMan;
       private InputGroup[] _inputWoman;
       private OutputTeam[] _outputMan;
       private OutputTeam[] _outputWoman;
       private Lab8.Blue.Task4.Team[] _manTeams;
       private Lab8.Blue.Task4.Team[] _womanTeams;
       private Lab8.Blue.Task4.Group[] _groups;

       [TestInitialize]
       public void LoadData()
       {
           var folder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
           folder = Path.Combine(folder, "Lab8Test", "Blue");

           var input = JsonSerializer.Deserialize<JsonElement>(
               File.ReadAllText(Path.Combine(folder, "input.json")))!;
           var output = JsonSerializer.Deserialize<JsonElement>(
               File.ReadAllText(Path.Combine(folder, "output.json")))!;

           var task4Input = input.GetProperty("Task4").Deserialize<InputGroup[]>()!;

           _inputMan = task4Input.Where(g => g.Name.StartsWith("ManGroup")).ToArray();
           _inputWoman = task4Input.Where(g => g.Name.StartsWith("WomanGroup")).ToArray();

           _outputMan = output.GetProperty("Task4Man").Deserialize<OutputTeam[]>()!;
           _outputWoman = output.GetProperty("Task4Woman").Deserialize<OutputTeam[]>()!;

           _manTeams = _inputMan.SelectMany(g => g.Teams).Select(t => (Lab8.Blue.Task4.Team)new Lab8.Blue.Task4.ManTeam(t.Name)).ToArray();
           _womanTeams = _inputWoman.SelectMany(g => g.Teams).Select(t => (Lab8.Blue.Task4.Team)new Lab8.Blue.Task4.WomanTeam(t.Name)).ToArray();

           _groups = new Lab8.Blue.Task4.Group[task4Input.Length];
           for (int i = 0; i < _inputMan.Length; i++)
           {
               _groups[i] = new Lab8.Blue.Task4.Group(_inputMan[i].Name);
           }
           for (int i = 0; i < _inputWoman.Length; i++)
           {
               _groups[i + _inputMan.Length] = new Lab8.Blue.Task4.Group(_inputWoman[i].Name);
           }
       }
       [TestMethod]
       public void Test_00_OOP()
       {
           var type = typeof(Lab8.Blue.Task4.Team);
           Assert.IsTrue(type.IsAbstract, "Team должен быть абстрактным классом");
           Assert.IsTrue(type.IsClass);
           Assert.AreEqual(0, type.GetFields(BindingFlags.Public | BindingFlags.Instance).Length);
           Assert.IsTrue(type.GetProperty("Name")?.CanRead ?? false, "Нет свойства Name");
           Assert.IsTrue(type.GetProperty("Scores")?.CanRead ?? false, "Нет свойства Scores");
           Assert.IsTrue(type.GetProperty("TotalScore")?.CanRead ?? false, "Нет свойства TotalScore");
           Assert.IsFalse(type.GetProperty("Name")?.CanWrite ?? false, "Свойство Name должно быть только для чтения");
           Assert.IsFalse(type.GetProperty("Scores")?.CanWrite ?? false, "Свойство Scores должно быть только для чтения");
           Assert.IsFalse(type.GetProperty("TotalScore")?.CanWrite ?? false, "Свойство TotalScore должно быть только для чтения");
           Assert.IsNotNull(type.GetMethod("PlayMatch", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(int) }, null), "Нет публичного метода PlayMatch(int result)");
           Assert.IsNotNull(type.GetMethod("Print", BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null), "Нет публичного метода Print()");
           Assert.AreEqual(0, type.GetFields().Count(f => f.IsPublic));
           Assert.AreEqual(3, type.GetProperties().Count(f => f.PropertyType.IsPublic));
           Assert.AreEqual(9, type.GetMethods().Count(f => f.IsPublic));
           var manType = typeof(Lab8.Blue.Task4.ManTeam);
           Assert.IsTrue(manType.IsClass);
           Assert.AreEqual(type, manType.BaseType);
           Assert.IsNotNull(manType.GetConstructor(new[] { typeof(string) }));
           Assert.AreEqual(0, manType.GetFields().Count(f => f.IsPublic));
           Assert.AreEqual(3, manType.GetProperties().Count(f => f.PropertyType.IsPublic));
           Assert.AreEqual(1, manType.GetConstructors().Count(f => f.IsPublic));
           Assert.AreEqual(9, manType.GetMethods().Count(f => f.IsPublic));
           var womanType = typeof(Lab8.Blue.Task4.WomanTeam);
           Assert.IsTrue(womanType.IsClass);
           Assert.AreEqual(type, womanType.BaseType);
           Assert.IsNotNull(womanType.GetConstructor(new[] { typeof(string) }));
           Assert.AreEqual(0, womanType.GetFields().Count(f => f.IsPublic));
           Assert.AreEqual(3, womanType.GetProperties().Count(f => f.PropertyType.IsPublic));
           Assert.AreEqual(1, womanType.GetConstructors().Count(f => f.IsPublic));
           Assert.AreEqual(9, womanType.GetMethods().Count(f => f.IsPublic));
           type = typeof(Lab8.Blue.Task4.Group);
           Assert.IsFalse(type.IsValueType, "Group должен быть классом");
           Assert.IsTrue(type.IsClass);
           Assert.AreEqual(0, type.GetFields(BindingFlags.Public | BindingFlags.Instance).Length);
           Assert.IsTrue(type.GetProperty("Name")?.CanRead ?? false, "Нет свойства Name");
           Assert.IsTrue(type.GetProperty("ManTeams")?.CanRead ?? false, "Нет свойства ManTeams");
           Assert.IsTrue(type.GetProperty("WomanTeams")?.CanRead ?? false, "Нет свойства WomanTeams");
           Assert.IsFalse(type.GetProperty("Name")?.CanWrite ?? false, "Свойство Name должно быть только для чтения");
           Assert.IsFalse(type.GetProperty("ManTeams")?.CanWrite ?? false, "Свойство ManTeams должно быть только для чтения");
           Assert.IsFalse(type.GetProperty("WomanTeams")?.CanWrite ?? false, "Свойство WomanTeams должно быть только для чтения");
           Assert.IsNotNull(type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(string) }, null), "Нет публичного конструктора Group(string name)");
           Assert.IsNotNull(type.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(Lab8.Blue.Task4.Team) }, null), "Нет публичного метода Add(Team team)");
           Assert.IsNotNull(type.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(Lab8.Blue.Task4.Team[]) }, null), "Нет публичного метода Add(Team[] teams)");
           Assert.IsNotNull(type.GetMethod("Sort", BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null), "Нет публичного метода Sort()");
           Assert.IsNotNull(type.GetMethod("Merge", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(Lab8.Blue.Task4.Group), typeof(Lab8.Blue.Task4.Group), typeof(int) }, null), "Нет публичного статического метода Merge(Group group1, Group group2, int size)");
           Assert.IsNotNull(type.GetMethod("Print", BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null), "Нет публичного метода Print()");
           Assert.AreEqual(0, type.GetFields().Count(f => f.IsPublic));
           Assert.AreEqual(3, type.GetProperties().Count(f => f.PropertyType.IsPublic));
           Assert.AreEqual(1, type.GetConstructors().Count(f => f.IsPublic));
           Assert.AreEqual(12, type.GetMethods().Count(f => f.IsPublic));
       }
       [TestMethod]
       public void Test_02_InitTeams()
       {
           CheckManTeams(scoresExpected: false);
           CheckWomanTeams(scoresExpected: false);
       }
       [TestMethod]
       public void Test_03_PlayMatches()
       {
           PlayManMatches();
           PlayWomanMatches();
           CheckManTeams(scoresExpected: true);
           CheckWomanTeams(scoresExpected: true);
       }
       [TestMethod]
       public void Test_04_CreateGroups()
       {
           CheckGroups(filled: false);
       }
       [TestMethod]
       public void Test_05_FillGroups()
       {
           PlayManMatches();
           PlayWomanMatches();
           FillGroups();
           for (int gi = 0; gi < 2; gi++)
           {
               CheckGroups(filled: true, expectedManCount: 12, expectedWomanCount: 12, groupIndex: gi);
           }
           for (int gi = 2; gi < 4; gi++)
           {
               CheckGroups(filled: true, expectedManCount: 12, expectedWomanCount: 12, groupIndex: gi);
           }
       }
       [TestMethod]
       public void Test_06_FillManyGroups()
       {
           PlayManMatches();
           PlayWomanMatches();
           FillManyGroups();
           for (int gi = 0; gi < 2; gi++)
           {
               CheckGroups(filled: true, expectedManCount: 12, expectedWomanCount: 12, groupIndex: gi);
           }
           for (int gi = 2; gi < 4; gi++)
           {
               CheckGroups(filled: true, expectedManCount: 12, expectedWomanCount: 12, groupIndex: gi);
           }
       }
       [TestMethod]
       public void Test_07_FillGroups()
       {
           PlayManMatches();
           PlayWomanMatches();
           FillGroups();
           FillManyGroups();
           FillGroups();
           for (int gi = 0; gi < 2; gi++)
           {
               CheckGroups(filled: true, expectedManCount: 12, expectedWomanCount: 12, groupIndex: gi);
           }
           for (int gi = 2; gi < 4; gi++)
           {
               CheckGroups(filled: true, expectedManCount: 12, expectedWomanCount: 12, groupIndex: gi);
           }
       }
       [TestMethod]
       public void Test_08_SortGroups()
       {
           PlayManMatches();
           PlayWomanMatches();
           FillGroups();
           foreach (var g in _groups) g.Sort();
           for (int gi = 0; gi < 2; gi++)
           {
               CheckGroups(filled: true, expectedManCount: 12, expectedWomanCount: 12, sorted: true, groupIndex: gi);
           }
           for (int gi = 2; gi < 4; gi++)
           {
               CheckGroups(filled: true, expectedManCount: 12, expectedWomanCount: 12, sorted: true, groupIndex: gi);
           }
       }
       [TestMethod]
       public void Test_09_MergeFinalists()
       {
           PlayManMatches();
           PlayWomanMatches();
           FillGroups();
           foreach (var g in _groups) g.Sort();
           var mergedMan = Lab8.Blue.Task4.Group.Merge(_groups[0], _groups[1], 12);
           var mergedWoman = Lab8.Blue.Task4.Group.Merge(_groups[2], _groups[3], 12);
           var merged = new Lab8.Blue.Task4.Group("Финалисты");
           merged.Add(mergedMan.ManTeams);
           merged.Add(mergedWoman.WomanTeams);
           Assert.AreEqual("Финалисты", merged.Name);
           Assert.AreEqual(12, merged.ManTeams.Length);
           Assert.AreEqual(12, merged.WomanTeams.Length);
           for (int i = 0; i < _outputMan.Length; i++)
           {
               Assert.IsNotNull(merged.ManTeams[i]);
               Assert.AreEqual(_outputMan[i].Name, merged.ManTeams[i].Name);
               Assert.AreEqual(_outputMan[i].TotalScore, merged.ManTeams[i].TotalScore);
           }
           for (int i = 0; i < _outputWoman.Length; i++)
           {
               Assert.IsNotNull(merged.WomanTeams[i]);
               Assert.AreEqual(_outputWoman[i].Name, merged.WomanTeams[i].Name);
               Assert.AreEqual(_outputWoman[i].TotalScore, merged.WomanTeams[i].TotalScore);
           }
       }
       private void PlayManMatches()
       {
           int idx = 0;
           foreach (var g in _inputMan)
           {
               foreach (var t in g.Teams)
               {
                   foreach (var s in t.Scores)
                       _manTeams[idx].PlayMatch(s);
                   idx++;
               }
           }
       }
       private void PlayWomanMatches()
       {
           int idx = 0;
           foreach (var g in _inputWoman)
           {
               foreach (var t in g.Teams)
               {
                   foreach (var s in t.Scores)
                       _womanTeams[idx].PlayMatch(s);
                   idx++;
               }
           }
       }
       private void FillGroups()
       {
           int idx = 0;
           for (int gi = 0; gi < _groups.Length/2; gi++)
           {
               var men = _manTeams.Skip(idx).Take(_inputMan[gi].Teams.Length).ToArray();
               _groups[gi].Add(men);
               _groups[gi+2].Add(men);
               idx += _inputMan[gi].Teams.Length;
               var women = _womanTeams.Where((team, index) => index % 2 == gi).ToArray();
               _groups[gi].Add(women);
               _groups[gi + 2].Add(women);
           }
       }
       private void FillManyGroups()
       {
           FillGroups();
       }
       private void CheckManTeams(bool scoresExpected)
       {
           int idx = 0;
           foreach (var g in _inputMan)
           {
               foreach (var t in g.Teams)
               {
                   var team = _manTeams[idx];
                   Assert.AreEqual(t.Name, team.Name);
                   if (scoresExpected)
                       Assert.AreEqual(t.Scores.Sum(), team.TotalScore);
                   else
                       Assert.AreEqual(0, team.TotalScore);
                   idx++;
               }
           }
       }
       private void CheckWomanTeams(bool scoresExpected)
       {
           int idx = 0;
           foreach (var g in _inputWoman)
           {
               foreach (var t in g.Teams)
               {
                   var team = _womanTeams[idx];
                   Assert.AreEqual(t.Name, team.Name);
                   if (scoresExpected)
                       Assert.AreEqual(t.Scores.Sum(), team.TotalScore);
                   else
                       Assert.AreEqual(0, team.TotalScore);
                   idx++;
               }
           }
       }
       private void CheckGroups(bool filled, int expectedManCount = 12, int expectedWomanCount = 0, bool sorted = false, int groupIndex = 0)
       {
           var group = _groups[groupIndex];
           Assert.AreEqual(groupIndex < _inputMan.Length ? _inputMan[groupIndex].Name : _inputWoman[groupIndex - _inputMan.Length].Name, group.Name);
           Assert.AreEqual(12, group.ManTeams.Length);
           Assert.AreEqual(12, group.WomanTeams.Length);
           var manTeams = group.ManTeams.Where(t => t != null).ToArray();
           var womanTeams = group.WomanTeams.Where(t => t != null).ToArray();
           if (!filled)
           {
               Assert.AreEqual(0, manTeams.Length);
               Assert.AreEqual(0, womanTeams.Length);
           }
           else
           {
               Assert.AreEqual(expectedManCount, manTeams.Length);
               Assert.AreEqual(expectedWomanCount, womanTeams.Length);
           }
           if (sorted)
           {
               for (int j = 1; j < manTeams.Length; j++)
                   Assert.IsTrue(manTeams[j - 1].TotalScore >= manTeams[j].TotalScore,
                   $"ManTeams в группе {group.Name} не отсортированы по TotalScore");
               for (int j = 1; j < womanTeams.Length; j++)
                   Assert.IsTrue(womanTeams[j - 1].TotalScore >= womanTeams[j].TotalScore,
                   $"WomanTeams в группе {group.Name} не отсортированы по TotalScore");
           }
       }
   }
}
