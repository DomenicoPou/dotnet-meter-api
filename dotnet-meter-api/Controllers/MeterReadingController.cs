using dotnet_meter_api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Microsoft.AspNetCore.Http;
using dotnet_meter_api.Models.Responses;
using System.ComponentModel.DataAnnotations;

namespace dotnet_meter_api.Controllers
{
    /// <summary>
    /// The main controller for meter reading
    /// Note: I would have this be routed with the commented code bellow, just to make it more readable to any (inhouse, integration) developer.
    /// </summary>
    //[Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : Controller
    {

        private readonly DatabaseContext _databaseContext;

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MeterReadingController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpPost("meter-reading-uploads")]
        public async Task<ActionResult> UploadMeterReadings(
            [Required]
            IFormFile CsvFileUpload
            )
        {
            if (CsvFileUpload == null || !CsvFileUpload.FileName.EndsWith(".csv"))
                return BadRequest(new ErroredJsonResponse("Please attach a valid CSV file"));

            return Ok();
        }


        [HttpPost("add-customers")]
        public async Task<ActionResult> AddCustomers(
            [Required]
            IFormFile CsvFileUpload
            )
        {
            if (CsvFileUpload == null || !CsvFileUpload.FileName.EndsWith(".csv"))
                return BadRequest(new ErroredJsonResponse("Please attach a valid CSV file"));

            return Ok();
        }
    }
}
