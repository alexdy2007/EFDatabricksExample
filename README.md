##Setup

Fill out appsettings.json with
1) datarbicks sql warehoust hostname
2) PAT token generated from databricks user, service principle, or PAT via MS Graph API auth
3) Fill out fake path, dbName to generate sqllite db (Will not be used need a context object to generate sql)
4) Can find warehouse id using following api https://{databricks workspace url}/api/2.0/sql/warehouses/
5) Create Entity to replace BronzeAsset for one in your database
6) Add endpoint to DatabricksServiceAPI to make/parse response