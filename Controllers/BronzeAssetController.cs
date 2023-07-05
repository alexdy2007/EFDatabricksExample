using Microsoft.AspNetCore.Mvc;
using EFBricks.Service.DatabricksAPI;
using EFBricks.API.Entities;
using EFBricks.API.Database;
using Microsoft.EntityFrameworkCore;

namespace EFBricks.API.Controllers;

[Route("api/bronze")]
[ApiController]
public class BronzeAssetController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IDatabricksApiService _databricksApiService;

    private readonly EFBricksDbContext _context;


    public BronzeAssetController(ILogger<BronzeAssetController> logger, IDatabricksApiService databricksApiService, EFBricksDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _databricksApiService = databricksApiService ?? throw new ArgumentNullException(nameof(_databricksApiService));
        _context = context ?? throw new ArgumentNullException(nameof(_context));
    }    
    // 

    [HttpGet(Name = "bronzeasset")]
    public IActionResult BronzeAsset()
    {
        _logger.LogInformation("Hit API");

        // TODO MAKE REPOSITORY PATTERN INSTEAD OF LOGIC IN CONTROLLER
        var sql = _context.BronzeAsset.Where(b => b.asset_id==20).ToQueryString();
        var sqlClean = _databricksApiService.sanitiseSQLSelect(sql);
        var bronzeAssetData = _databricksApiService.getBronzeAssets(sqlClean);
        _logger.LogInformation("API request made");
        return Ok(bronzeAssetData);
    }
}