using Microsoft.Data.Sqlite;
using ProductManagement.Data.Repositories;
using ProductManagement.Domain.Entities;
using TestesProjetoxUnit.Helper;

namespace TestesProjetoxUnit
{
    public class UnitTest1
    {
        private ProdutoSQLiteRepository CreateRepository(SqliteConnection con)
        {
            return new ProdutoSQLiteRepository(con);
        }


        [Fact]
        public void DeveAdicioarProduto()
        {
            using var con = SQLiteTestHelper.CreateInMemoryDataBase();
            var repo = CreateRepository(con);

            var produto = new Produto { Nome = "Café", Preco = 2.5m };

            repo.Adicionar(produto);
            var lista = repo.ObterTodos();

            Assert.Single(lista);//verifica se a lista tem exacamente 1 elemento
            Assert.Equal("Café", lista[0].Nome);
        }
        [Fact]
        public void DeveObterProdutoPorNome()
        {
            using var con = SQLiteTestHelper.CreateInMemoryDataBase();
            var repo = CreateRepository(con);

            var produto = new Produto { Nome = "leite", Preco = 0.99m };

            repo.Adicionar(produto);

            var prodPesquisa = repo.ObterPorNome("leite");

            Assert.NotNull(prodPesquisa);
            Assert.Equal("leite", produto.Nome);

        }
        [Fact]
        public void Deve_Remover_Produto()
        {
            using var con = SQLiteTestHelper.CreateInMemoryDataBase();
            var repo = CreateRepository(con);

            var produto = new Produto { Nome = "leite", Preco = 0.99m };

            repo.Adicionar(produto);

            var produtoNaBaseDados = repo.ObterPorNome("leite");

            Assert.NotNull(produtoNaBaseDados);
            bool removido = repo.Remover(produtoNaBaseDados.Id);

            Assert.True(removido);
            Assert.Empty(repo.ObterTodos());

        }
        [Fact]
        public void DeveVerificarExistenciaPorNome()
        {
            using var con = SQLiteTestHelper.CreateInMemoryDataBase();
            var repo = CreateRepository(con);

            var produto = new Produto { Nome = "leite", Preco = 0.99m };

            repo.Adicionar(produto);
            Assert.True(repo.ExistePorNome("leite"));
            Assert.False(repo.ExistePorNome("bolo"));

        }
    }
}
