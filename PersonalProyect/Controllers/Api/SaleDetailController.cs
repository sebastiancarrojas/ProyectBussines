using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailController : ApiController
    {
        // Inyertar dependencias necesarias
        private readonly ISaleDetail _saleDetailService;
        public SaleDetailController(ISaleDetail saleDetailService)
        {
            _saleDetailService = saleDetailService;
        }

    }
}
