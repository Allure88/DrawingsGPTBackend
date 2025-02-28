namespace DrawingsGPTBackend.Domain.Bodies.Views;

public record ViewsRequest(BoundingBoxBody BoundingBox, DrawingsOptionsBody DrawingsOptions, bool IsAssembly);
