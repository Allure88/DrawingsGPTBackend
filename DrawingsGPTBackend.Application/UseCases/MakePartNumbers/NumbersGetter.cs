using System.Text.RegularExpressions;

namespace DrawingsGPTBackend.Application.UseCases.MakePartNumbers
{
    public class NumbersGetter
    {
        private class AssemblyInfo
        {
            public int Number { get; set; }
            public List<int> DetailNumbers { get; set; } = [0];

            public void AddToDetails(int detailNum)
            {
                if (!DetailNumbers.Contains(detailNum))
                {
                    DetailNumbers.Add(detailNum);
                    DetailNumbers.Sort();
                }
            }

        }
        private List<AssemblyInfo> Assemblies { get; set; } = [];

        internal void Init(HashSet<string> allExistPartNumbers, string head, int mainAssemblyNum)
        {
            Assemblies.Add(new() { Number = mainAssemblyNum });
            foreach (string existedPN in allExistPartNumbers)
            {
                bool succsess = TryToParsePartNumber(head, existedPN, out int assemblyNumber, out int detailNumber);
                if (succsess)
                {
                    AssemblyInfo? assembly = Assemblies.FirstOrDefault(a => a.Number == assemblyNumber);
                    assembly ??= new() { Number = assemblyNumber };

                    assembly.AddToDetails(detailNumber);
                }
            }
            Assemblies = [.. Assemblies.OrderBy(a => a.Number)];
        }

        private bool TryToParsePartNumber(string head, string existedPN, out int assemblyNumber, out int detailNumber)
        {
            string correctedPN = existedPN.Replace(head, "HEAD");
            if (correctedPN != existedPN)
            {
                string pattern = @"^HEAD\.(\d{2})\.(\d{3})$";
                Regex regex = new(pattern);
                Match match = regex.Match(correctedPN);
                if (match.Success)
                {
                    string aa = match.Groups[1].Value;
                    string bbb = match.Groups[2].Value;
                    assemblyNumber = Convert.ToInt32(aa);
                    detailNumber = Convert.ToInt32(bbb);
                    return true;
                }
            }
            assemblyNumber = 0;
            detailNumber = 0;
            return false;
        }

        internal int CreateNewAssemblyAndGetNumber(int? settedNumber)
        {
            if (settedNumber != null)
            {
                if (!Assemblies.Any(a => a.Number == settedNumber.Value))
                {
                    Assemblies.Add(new() { Number = settedNumber.Value });
                }
                return settedNumber.Value;
            }

            int number = Assemblies[0].Number;
            int count = Assemblies.Count;

            if (Assemblies[count - 1].Number == number + count - 1)
            {
                number = Assemblies[count - 1].Number + 1;
                Assemblies.Add(new() { Number = number });
                return number;
            }
            else
            {
                for (int i = Assemblies[0].Number + 1; i <= Assemblies.Count; i++)
                {
                    if (Assemblies[i].Number > i)
                    {
                        Assemblies.Add(new() { Number = i });
                        Assemblies = [.. Assemblies.OrderBy(a => a.Number)];
                        return i;
                    }
                }
                number = number + Assemblies.Count;
                Assemblies.Add(new() { Number = number });
                return number;
            }
        }


        internal int? CreateDetailAndGetNumber(int assemblyNumber)
        {
            AssemblyInfo? assemblyInfo = Assemblies.FirstOrDefault(a => a.Number == assemblyNumber);
            if (assemblyInfo is null) return null;

            var list = assemblyInfo.DetailNumbers;
            int number = list[0];
            int count = list.Count;

            if (list[count - 1] == number + count - 1)
            {
                number = list[count - 1] + 1;
                assemblyInfo.AddToDetails(number);
                return number;
            }
            else
            {
                for (int i = number + 1; i <= list.Count; i++)
                {
                    if (list[i] > i)
                    {
                        assemblyInfo.AddToDetails(i);
                        return i;
                    }
                }
                number = number + list.Count;
                assemblyInfo.AddToDetails(number);
                return number;
            }

        }
    }
}
