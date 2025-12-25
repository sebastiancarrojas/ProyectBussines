using PersonalProyect.Core;
using PersonalProyect.DTOs;
using PersonalProyect.DTOs.SalesDetails;

namespace PersonalProyect.Services.Abtractions
{
    public interface ISaleDetail
    {

        public Task<Response<List<SaleDetailModalDTO>>> GetSaleDetailsBySaleIdAsync(Guid SaleId);
    }
}
