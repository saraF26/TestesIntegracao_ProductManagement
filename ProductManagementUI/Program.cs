using ProductManagement.Data.Repositories;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Business.Services;
using SQLitePCL;
namespace ProductManagementUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ProdutoRepository repo = new ProdutoRepository();
            IProdutoRepository repo = new ProdutoSQLiteRepository();
            ProductService servico = new ProductService(repo);

            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("1- Adicionar produto");
                Console.WriteLine("2 - Listar produtos");
                Console.WriteLine("3 - Procurar Produto");
                Console.WriteLine("4 - Remover Produto");
                Console.WriteLine("0 - para sair");

                string opcao = Console.ReadLine();

                if(opcao == "1")
                {
                    Console.WriteLine("Nome: ");
                    string nome = Console.ReadLine();

                    Console.WriteLine("Preço: ");
                    decimal preco = decimal.Parse(Console.ReadLine());

                    try
                    {
                        servico.AdicionarProduto(nome, preco);
                        Console.WriteLine("produto adicionado com sucesso");
                    }catch(Exception ex)
                    {
                        Console.WriteLine("Erro:"+ ex.Message);
                    }

                }
                else if (opcao == "2")
                {
                    var lista = servico.ListarTodos();
                    foreach(var p in lista)
                    {
                        Console.WriteLine($"{p.Id} - {p.Nome} - {p.Preco}");
                    }
                }
                else if(opcao == "3")//procurar por nome
                {
                    Console.WriteLine("Nome: ");
                    string nome = Console.ReadLine();

                    var produto = servico.ProcurarProduto(nome);

                    if(produto == null)
                    {
                        Console.WriteLine("Não encontrado");
                    }else
                    {
                        Console.WriteLine($"{produto.Id}- {produto.Nome} - {produto.Preco}");
                    }

                }
                else if (opcao == "4")
                {
                    Console.WriteLine("ID do produto.");
                    int id = int.Parse(Console.ReadLine());

                    try
                    {
                        servico.RemoverProduto(id);
                        Console.WriteLine("produto removido com sucesso");
                    }catch(Exception ex)
                    {
                        Console.WriteLine("erro: "+ex.Message);
                    }
                }
                else if(opcao == "0")
                {
                    continuar = false;
                }
            }

        }
    }
}
