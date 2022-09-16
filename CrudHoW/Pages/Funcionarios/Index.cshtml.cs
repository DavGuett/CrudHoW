using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CrudHoW.Pages.Funcionarios
{
    public class IndexModel : PageModel
    {
        public List<Funcionario> listarFuncionarios = new List<Funcionario>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Funcionario funcionario = new Funcionario();
                                funcionario.Id = reader.GetInt32(0);
                                funcionario.Name = reader.GetString(1);
                                funcionario.Email = reader.GetString(2);
                                funcionario.Setor = reader.GetString(3);

                               listarFuncionarios.Add(funcionario);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: ", e.ToString());
            }
        }
    }

    public class Funcionario
    {
        public int Id;
        public string Name;
        public string Email;
        public string Setor;
    }
}
