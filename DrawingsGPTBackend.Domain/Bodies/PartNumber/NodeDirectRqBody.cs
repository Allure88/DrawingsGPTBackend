namespace DrawingsGPTBackend.Domain.Bodies.PartNumber;

public class NodeDirectRqBody//(string partNumberExist, string fullFileName, NodeDirectRqBody)
{
    public string PartNumberFull { get; set; } = string.Empty;
    public string FullFileName { get; set; } = string.Empty;

    public bool IsDetail => Childs.Count == 0;
    public List<NodeDirectRqBody> Childs { get; set; } = [];
    //public NodeDirectRqBody? Parent { get; } = parent;
    public int Level { get; set; }
    public int? SettedNumber { get; set; }
}
