using DrawingsGPTBackend.API.Models.Responces;
using DrawingsGPTBackend.Application.UseCases.FitViews;
using DrawingsGPTBackend.Domain;
using DrawingsGPTBackend.Domain.Bodies;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsGPTBackend.API.Controllers
{
    public class ViewsdisplacementController(ViewsInteractor viewsInteractor) : Controller
    {
        [Route("/Views")]
        public ActionResult<BaseResponse> Post([FromBody] ViewsRequest request)
        {

           ( List<BaseViewBody> baseviews, List<ProjectViewBody>projectViews, Format format) = viewsInteractor.FitViews(request.BoundingBox, request.DrawingsOptions, request.IsAssembly);
            ViewsResponce viewsResponce = new() {BaseViews  = baseviews,ProjectViews = projectViews, Format = format };

            BaseResponse baseResponse = new(viewsResponce)
            {
                Success = true,
                Code = System.Net.HttpStatusCode.OK,
            };
            return baseResponse.ToActionResult(this);
        }
    }
}
