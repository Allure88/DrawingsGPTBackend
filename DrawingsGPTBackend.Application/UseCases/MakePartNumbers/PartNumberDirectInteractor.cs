using DrawingsGPTBackend.Domain.Bodies.PartNumber;

namespace DrawingsGPTBackend.Application.UseCases.MakePartNumbers
{
    public class PartNumberDirectInteractor(NumbersGetter numbersGetter)
    {
        public PartNumberResponce MakePartNumbers(PartNumberRequest request)
        {
            string head = string.Concat(request.Options.ConditionCode, request.Options.LayoutCode);

            HashSet<string> allExistedPartNumbers = [];
            TraverseToExistedPartNumbers(request.NodeDirectRqBody.Childs, ref allExistedPartNumbers);

            numbersGetter.Init(allExistedPartNumbers, head, request.Options.AssemblyNumber);

            var topNode = request.NodeDirectRqBody;
            List<NodeDirectRqBody> assemblies = GetOrderedAssemblies(topNode);

            Dictionary<string, string> pathPartNumberMap = SetPartNumbers(assemblies, head);
            return new() { PathPartNumberMap = pathPartNumberMap };
        }

        private Dictionary<string, string> SetPartNumbers(List<NodeDirectRqBody> assemblies, string head)
        {
            Dictionary<string, string> pathPartNumberMap = [];
            foreach (var assembly in assemblies)
            {
                if (!string.IsNullOrEmpty(assembly.PartNumberFull))
                {
                    int number = numbersGetter.CreateNewAssemblyAndGetNumber();
                    string numberStr = number < 10
                        ? string.Concat('0', number.ToString())
                        : number.ToString();
                    assembly.PartNumberFull = string.Concat(head, '.', numberStr, ".000СБ");
                }
                if (assembly.SettedNumber != null)
                {
                    foreach (var detail in assembly.Childs)
                    {
                        if (!string.IsNullOrEmpty(detail.PartNumberFull))
                        {
                            string assemblyNumStr = assembly.SettedNumber.Value < 10
                                      ? string.Concat('0', assembly.SettedNumber.Value.ToString())
                                      : assembly.SettedNumber.Value.ToString();

                            int? detailNum = numbersGetter.CreateDetailAndGetNumber(assembly.SettedNumber.Value);
                            if (detailNum != null)
                            {
                                string numberStr = detailNum.Value < 10
                                      ? string.Concat('0', detailNum.Value.ToString())
                                      : detailNum.Value.ToString();
                                detail.PartNumberFull = string.Concat(head, '.', assemblyNumStr, '.', numberStr);
                            }
                        }
                    }
                }
            }
            return pathPartNumberMap;
        }

        private List<NodeDirectRqBody> GetOrderedAssemblies(NodeDirectRqBody topNode)
        {
            List<NodeDirectRqBody> assemblies = [];
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

        private void TraverseToExistedPartNumbers(List<NodeDirectRqBody> childs, ref HashSet<string> input)
        {
            HashSet<string> result = [];
            foreach (var node in childs)
            {
                if (!string.IsNullOrEmpty(node.PartNumberFull))
                    result.Add(node.PartNumberFull);
                if (!node.IsDetail)
                    TraverseToExistedPartNumbers(node.Childs, ref input);
            }
        }
    }
}
