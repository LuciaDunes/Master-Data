using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Master_Data.Pages.Products
{
    public class EditModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String product_id = Request.Query["product_id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Master-Data;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM product WHERE product_id = @product_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@product_id", product_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                productInfo.product_id = "" + reader.GetInt32(0);
                                productInfo.marking = reader.GetString(1);
                                productInfo.product_name = reader.GetString(2);
                                productInfo.machine_name = reader.GetString(3);
                                productInfo.description = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            successMessage = "New data successfully been added";
        }
    }
}
