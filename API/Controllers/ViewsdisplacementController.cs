using DrawingsGPTBackend.API.Models.Responces;
using DrawingsGPTBackend.Application.UseCases.FitViews;
using DrawingsGPTBackend.Domain.Bodies.Views;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsGPTBackend.API.Controllers
{
    public class ViewsdisplacementController(ViewsInteractor viewsInteractor) : Controller
    {
        [Route("/Views")]
        public ActionResult<BaseResponse> Post([FromBody] ViewsRequest request)
        {

            ViewsResponce viewsResponce = viewsInteractor.FitViews(request.BoundingBox, request.DrawingsOptions, request.IsAssembly);

            BaseResponse baseResponse = new(viewsResponce)
            {
                Success = true,
                Code = System.Net.HttpStatusCode.OK,
            };
            return baseResponse.ToActionResult(this);
        }
    }
}
