namespace DrawingsGPTBackend.Domain.Bodies;

public record ViewsRequest(BoundingBoxBody BoundingBox, DrawingsOptionsBody DrawingsOptions, bool IsAssembly);
