using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.PriceLists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.PriceListsDtos;

namespace Sazmanyar.Transactions.Controllers.PriceListsController
{
    public class PriceListController : BaseController
    {
        private readonly IPriceListService _priceListService;
        public PriceListController(IPriceListService priceListService)
        {
            _priceListService = priceListService;
        }

       [HttpPost("ImportExcelFile")]
        public async Task<IActionResult> ImportExcelFile(IFormFile file)
        {
           
            if (file == null || file.Length == 0)
            {
                return BadRequest("فایلی آپلود نشده!!!");
            }

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            // Reset stream position to beginning
            stream.Position = 0;

            // Pass the stream to service layer
            var result = await _priceListService.AddPriceListByExcel(stream);

            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        
        {
            var options = new JsonSerializerOptions
            {
                // ReferenceHandler = ReferenceHandler.Preserve,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                MaxDepth = 32
            };
            var result = await _priceListService.GetAllPriceLists();
            return Ok(JsonSerializer.Serialize(result.Item1, options));
         //   return Ok(result.Item1);
        }
        [HttpGet("GetAllYear")]
        public async Task<IActionResult> GetAllYear()

        {
            var result = await _priceListService.GetAllYears();
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] PriceListDtos obj)

        {
            var result = await _priceListService.Add(obj);
            return Ok(new { ID = result.ID,isSuccess=result.isSuccess});
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] PriceListDtos obj)

        {
            var result = await _priceListService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)

        {
            var result = await _priceListService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess } );
        }
    }
}
