using DrawingsGPTBackend.API.Models.Responces;
using DrawingsGPTBackend.Application.UseCases.PlaceDimensions;
using DrawingsGPTBackend.Domain.Bodies.Dimensions;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsGPTBackend.API.Controllers
{
    public class DimensionsController(DimensionsInteractor dimensionInteractor) : Controller
    {
        [Route("/Dimensions")]
        public ActionResult<BaseResponse> Post([FromBody] DimensionsRequest request)
        {

            DimensionsResponce viewsResponce = dimensionInteractor.PlaceCommonDimensions(request);




            BaseResponse baseResponse = new(viewsResponce)
            {
                Success = true,
                Code = System.Net.HttpStatusCode.OK,
            };
            return baseResponse.ToActionResult(this);
        }
    }
}
