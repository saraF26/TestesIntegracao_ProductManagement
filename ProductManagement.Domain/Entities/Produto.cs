using System.Reflection.Metadata;
using System.Security.Principal;

namespace ProductManagement.Domain.Entities
{
    public class Produto
    {
        //modelo de dados do sistema

        public int Id { get; set; }
        public string Nome { get; set; } 
        public decimal Preco { get; set; }

        public Produto()
        {
            Nome = string.Empty;//defenir como vazio para evitar null
        }
    }
}
