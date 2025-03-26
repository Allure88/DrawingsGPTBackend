namespace DrawingsGPTBackend.Domain.Bodies.Views;

public class ViewsResponce(List<ViewBody> views, Format format, bool succes, string message)
{
    public List<ViewBody> Views { get; } = views;
    public bool Succes { get; } = succes;
    public string Message { get; } = message;
    public Format Format { get; } = format;
}
