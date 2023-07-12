using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Master_Data.Pages.Products
{
    public class AddModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            productInfo.product_id = Request.Form["product_id"];
            productInfo.marking = Request.Form["marking"];
            productInfo.product_name = Request.Form["product_name"];
            productInfo.machine_name = Request.Form["machine_name"];
            productInfo.description = Request.Form["description"];

            if (productInfo.product_id.Length == 0 || productInfo.marking.Length == 0 || 
                productInfo.product_name.Length == 0 || productInfo.machine_name.Length == 0)
            {
                errorMessage = "Fill all the empty forms";
                return;
            }

            //Add data

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Master-Data;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO product" +
                                 "(product_id, marking, product_name, machine_name, description) VALUES" +
                                 "(@product_id, @marking, @product_name, @machine_name, @description);";

                    using (SqlCommand command = new SqlCommand (sql, connection))
                    {
                        command.Parameters.AddWithValue("@product_id", productInfo.product_id);
                        command.Parameters.AddWithValue("@marking", productInfo.marking);
                        command.Parameters.AddWithValue("@product_name", productInfo.product_name);
                        command.Parameters.AddWithValue("@machine_name", productInfo.machine_name);
                        command.Parameters.AddWithValue("@description", productInfo.description);

                        command.ExecuteNonQuery();
                    }
                }

            } 
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            productInfo.product_id = ""; productInfo.marking = ""; productInfo.product_name = "";
            productInfo.machine_name = ""; productInfo.description = "";
            successMessage = "New data successfully been added";
        }
    }
}
