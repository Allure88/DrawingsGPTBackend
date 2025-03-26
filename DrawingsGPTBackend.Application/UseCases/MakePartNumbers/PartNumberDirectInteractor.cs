using DrawingsGPTBackend.Domain.Bodies.PartNumber;

namespace DrawingsGPTBackend.Application.UseCases.MakePartNumbers
{
    public class PartNumberDirectInteractor(NumbersGetter numbersGetter)
    {
        public PartNumberResponce MakePartNumbers(PartNumberRequest request)
        {
            string head = string.Concat(request.Options.ConditionCode, request.Options.LayoutCode);

            HashSet<string> allExistedPartNumbers = [];
            TraverseToExistedPartNumbers(request.NodeDirectRqBody, ref allExistedPartNumbers);

            numbersGetter.Init(allExistedPartNumbers, head, request.Options.AssemblyNumber);

            var topNode = request.NodeDirectRqBody;
            topNode.SettedNumber = request.Options.AssemblyNumber;
            List<NodeDirectRqBody> assemblies = GetOrderedAssemblies(topNode);

            Dictionary<string, string> pathPartNumberMap = SetPartNumbers(assemblies, head);
            return new() { PathPartNumberMap = pathPartNumberMap };
        }

        private Dictionary<string, string> SetPartNumbers(List<NodeDirectRqBody> assemblies, string head)
        {
            Dictionary<string, string> pathPartNumberMap = [];
            foreach (var assembly in assemblies)
            {
                if (!assembly.PartNumberFull.Contains('.'))
                {
                    int number = numbersGetter.CreateNewAssemblyAndGetNumber(assembly.SettedNumber);

                    string numberStr = number < 10
                        ? string.Concat('0', number.ToString())
                        : number.ToString();
                    assembly.PartNumberFull = string.Concat(head, '.', numberStr, ".000СБ");
                    assembly.SettedNumber = number;

                    if (!pathPartNumberMap.TryGetValue(assembly.FullFileName, out _))
                        pathPartNumberMap.Add(assembly.FullFileName, assembly.PartNumberFull);
                }
                if (assembly.SettedNumber != null)
                {
                    foreach (var detail in assembly.Childs)
                    {
                        if (detail.IsDetail)
                        {

                            if (!detail.PartNumberFull.Contains('.'))
                            {
                                string assemblyNumStr = "";
                                if (assembly.SettedNumber.Value < 100)
                                    assemblyNumStr += "0";
                                if (assembly.SettedNumber.Value < 10)
                                    assemblyNumStr += "0";
                                assemblyNumStr = string.Concat(assemblyNumStr, assembly.SettedNumber.Value.ToString());

                                int? detailNum = numbersGetter.CreateDetailAndGetNumber(assembly.SettedNumber.Value);
                                if (detailNum != null)
                                {
                                    string numberStr = detailNum.Value < 10
                                          ? string.Concat('0', detailNum.Value.ToString())
                                          : detailNum.Value.ToString();
                                    detail.PartNumberFull = string.Concat(head, '.', assemblyNumStr, '.', numberStr);
                                    detail.SettedNumber = detailNum;

                                    if (!pathPartNumberMap.TryGetValue(detail.FullFileName, out _))
                                        pathPartNumberMap.Add(detail.FullFileName, detail.PartNumberFull);
                                }
                            }
                        }
                    }
                }
            }
            return pathPartNumberMap;
        }

        private List<NodeDirectRqBody> GetOrderedAssemblies(NodeDirectRqBody topNode)
        {
            List<NodeDirectRqBody> assemblies = [topNode];
            TraverseToGetAssemblies(ref assemblies, topNode.Childs);
            assemblies = [.. assemblies.OrderBy(a => a.Level)];
            return assemblies;
        }

        private void TraverseToGetAssemblies(ref List<NodeDirectRqBody> assemblies, List<NodeDirectRqBody> childs)
        {
            foreach (var node in childs)
            {
                if (!node.IsDetail && !assemblies.Any(a => a.FullFileName == node.FullFileName))
                {
                    assemblies.Add(node);
                    TraverseToGetAssemblies(ref assemblies, node.Childs);
                }
            }
        }

        private void TraverseToExistedPartNumbers(NodeDirectRqBody topNode, ref HashSet<string> allExistedPartNumbers)
        {
            HashSet<string> result = [];
            foreach (var node in topNode.Childs)
            {
                if (!string.IsNullOrEmpty(node.PartNumberFull))
                {
                    result.Add(node.PartNumberFull);
                    allExistedPartNumbers.Add(node.PartNumberFull);
                }
                if (!node.IsDetail)
                    TraverseToExistedPartNumbers(node, ref allExistedPartNumbers);
            }
        }
    }
}
