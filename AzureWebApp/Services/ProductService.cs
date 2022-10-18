using AzureWebApp.Models;
using System.Data.SqlClient;

namespace AzureWebApp.Services
{
    public class ProductService
    {
        private static string db_source = "";
        private static string db_user = "";
        private static string db_password = "";
        private static string db_database = "";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder
            {
                DataSource = db_source,
                UserID = db_user,
                Password = db_password,
                InitialCatalog = db_database
            };

            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> products = new List<Product>();
            string query = "SELECT PRODUCTID, PRODUCTNAME, QUANTITY FROM PRODUCTS";

            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;
        }

    }
}
