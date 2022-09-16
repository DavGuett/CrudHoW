using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CrudHoW.Pages.Funcionarios
{
    public class EditarModel : PageModel
    {
        public Funcionario funcionario = new Funcionario();
        public String mensagemErro = "";
        public String mensagemSucesso = "";
        public void OnGet()
        {
            String id = Request.Query["Id"];

            String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT * FROM Funcionarios WHERE id=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            funcionario.Id = reader.GetInt32(0);
                            funcionario.Name = reader.GetString(1);
                            funcionario.Email = reader.GetString(2);
                            funcionario.Setor = reader.GetString(3);
                        }
                    }


                    command.ExecuteNonQuery();
                }
            }
        }

        public void OnPost()
        {
            funcionario.Id = Convert.ToInt32(Request.Form["Id"]);
            funcionario.Name = Request.Form["nome"];
            funcionario.Email = Request.Form["email"];
            funcionario.Setor = Request.Form["setor"];

            if (funcionario.Id.ToString().Length == 0 || funcionario.Name.Length == 0 || funcionario.Email.Length == 0 || funcionario.Setor.Length == 0)
            {
                mensagemErro = "Todos os campos são obrigatórios";
                return;
            }
           
           
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Funcionarios " +
                        "SET name=@Name, email=@Email, setor=@Setor " +
                        "WHERE id=@Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", funcionario.Name);
                        command.Parameters.AddWithValue("@Email", funcionario.Email);
                        command.Parameters.AddWithValue("@Setor", funcionario.Setor);
                        command.Parameters.AddWithValue("@Id", funcionario.Id);

                        command.ExecuteNonQuery();
                    }
                }
            Response.Redirect("/Funcionarios/Index");
        }
    }
}
