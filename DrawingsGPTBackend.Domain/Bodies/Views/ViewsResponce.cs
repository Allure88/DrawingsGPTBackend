namespace DrawingsGPTBackend.Domain.Bodies.Views;

public class ViewsResponce
{
    public List<ViewBody> Views { get; set; } = null!;
    public Format Format { get; set; }
}
