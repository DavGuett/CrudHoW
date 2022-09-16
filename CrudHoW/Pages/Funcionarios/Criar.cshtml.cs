using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CrudHoW.Pages.Funcionarios
{
    public class CriarModel : PageModel
    {
        public Funcionario funcionario = new Funcionario();
        public string mensagemErro = "";
        public string mensagemSucesso = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            funcionario.Name = Request.Form["nome"];
            funcionario.Email = Request.Form["email"];
            funcionario.Setor = Request.Form["setor"];

            if (funcionario.Name.Length == 0 || funcionario.Email.Length == 0 || funcionario.Setor.Length == 0)
            {
                mensagemErro = "Todos os campos são obrigatórios";
                return;
            }

            String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "INSERT INTO Funcionarios " +
                    "(name, email, setor) VALUES " +
                    "(@Name, @Email, @Setor);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", funcionario.Name);
                    command.Parameters.AddWithValue("@Email", funcionario.Email);
                    command.Parameters.AddWithValue("@Setor", funcionario.Setor);

                    command.ExecuteNonQuery();
                }
            }

            mensagemSucesso = "Funcionário cadastrado com sucesso";

            Response.Redirect("/Funcionarios/Index");
        }
    }
}
