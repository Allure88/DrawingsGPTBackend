namespace DrawingsGPTBackend.Domain.Bodies.Views;

public class ViewsResponce
{
    public List<BaseViewBody> BaseViews { get; set; }
    public List<ProjectViewBody> ProjectViews { get; set; }
    public Format Format { get; set; }
}
