using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using EFBricks.API.Models;

namespace EFBricks.API.Entities
{
    [Table("water_demos.demo_app.bronze_asset")]

    public class BronzeAssetEntity
    {
        [Key]
        public int asset_id {get;set;}

        public string asset_type {get;set;}

        public int site_id  {get; set;}

        public string site_name  {get; set;}

        public double longitude  {get; set;}

        public double latitude  {get; set;}

        public string created_date {get; set;}

        public static BronzeAssetDTO createFromDataAPI(List<string> data)
            {
                if (data.Count != 7) {
                    throw new ArgumentException("Not 7 data points in array to create entity");
                }
                return new BronzeAssetDTO
                        {
                            asset_id = Convert.ToInt32(data[0]),
                            asset_type = data[1],
                            created_date = Convert.ToString(data[2]),
                            latitude = Convert.ToDouble(data[3]),
                            longitude = Convert.ToDouble(data[4]),
                            site_id = Convert.ToInt32(data[5]),
                            site_name = data[6]
                        };
            }        

    }

   
}