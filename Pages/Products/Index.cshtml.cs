using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Master_Data.Pages.Products
{
    public class IndexModel : PageModel
    {
        public List<ProductInfo> listProducts = new List<ProductInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Master-Data;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM product";
                    using (SqlCommand command = new SqlCommand (sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo productInfo = new ProductInfo();
                                productInfo.product_id = "" + reader.GetInt32(0);
                                productInfo.marking = reader.GetString(1);
                                productInfo.product_name = reader.GetString(2);
                                productInfo.machine_name = reader.GetString(3);
                                productInfo.description = reader.GetString(4);

                                listProducts.Add(productInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine ("Exception: " + ex.ToString());
            }
        }
    }

    public class ProductInfo
    {
        public String product_id;
        public String marking;
        public String product_name;
        public String machine_name;
        public String description;
    }
}
