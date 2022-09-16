using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudHoW.Pages.Funcionarios
{
    public class EditarModel : PageModel
    {
        public Funcionario funcionario = new Funcionario();
        public String mensagemErro = "";
        public String mensagemSucesso = "";
        public void OnGet()
        {
        }
    }
}
