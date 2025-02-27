namespace DrawingsGPTBackend.Domain.Bodies;

public class ViewsResponce
{
    public List<BaseViewBody> BaseViews { get; set; }
    public List<ProjectViewBody> ProjectViews { get; set; }
    public Format Format { get; set; }
}
