using EFBricks.API.Models;

namespace EFBricks.Service.DatabricksAPI
{
    public interface IDatabricksApiService
    {
        public IAsyncEnumerable<BronzeAssetDTO> getBronzeAssets(string sql);
        public string sanitiseSQLSelect(string sql);
    }
}