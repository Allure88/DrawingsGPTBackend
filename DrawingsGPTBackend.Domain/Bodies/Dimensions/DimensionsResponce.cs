namespace DrawingsGPTBackend.Domain.Bodies.Dimensions
{
    public class DimensionsResponce(bool success, string message, List<DimensionBody> dimensions)
    {
        public bool Success { get; } = success;
        public List<DimensionBody> Dimensions { get; } = dimensions;
        public string Message { get; } = message;
    }

}
