using Microsoft.Data.Sqlite;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ProductManagement.Data.Repositories
{
    public class ProdutoSQLiteRepository:IProdutoRepository
    {
        private string _connectionString = "Data Source = produtos.db";

        //adicionar para utilizar na base dados in_memory:

        private readonly SqliteConnection _connetion;

        //criação da tabela

        public ProdutoSQLiteRepository()
        {
            _connetion = new SqliteConnection(_connectionString);
           _connetion.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Produtos (
Id INTEGER PRIMARY KEY AUTOINCREMENT,
Nome TEXT NOT NULL,
Preco REAL NOT NULL);";

            using var cmd = new SqliteCommand(sql, _connetion);
            cmd.ExecuteNonQuery();
        }
        //*************************************************************************

        //adicionar Construtor para testes (usa a connetion que vem de fora)
        public ProdutoSQLiteRepository(SqliteConnection connection)
        {
            _connetion = connection;
            string sql = @"CREATE TABLE IF NOT EXISTS Produtos (
Id INTEGER PRIMARY KEY AUTOINCREMENT,
Nome TEXT NOT NULL,
Preco REAL NOT NULL);";

            using var cmd = new SqliteCommand(sql, _connetion);
            cmd.ExecuteNonQuery();
        }

        //*************************************************************************

        //implementação do smetodos da interface

        public void Adicionar(Produto produto)
        {
            //using var con = new SqliteConnection(_connectionString);
            //con.Open();

            string sql = @"INSERT INTO Produtos (Nome,Preco) VALUES(@n,@p)";

            using var cmd = new SqliteCommand(sql, _connetion);

            cmd.Parameters.AddWithValue("@n", produto.Nome);
            cmd.Parameters.AddWithValue("@p", produto.Preco);

            cmd.ExecuteNonQuery();


        }
        public List<Produto> ObterTodos()
        {
            List<Produto> lista = new();

            //using var con = new SqliteConnection(_connectionString);
            //con.Open();

            string sql = @"SELECT Id, Nome, Preco FROM  Produtos";

            using var cmd = new SqliteCommand(sql, _connetion);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Produto
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Preco=reader.GetDecimal(2)
                });
            }
            return lista;

        }

        public Produto? ObterPorNome(string nome)
        {
            //using var con = new SqliteConnection(_connectionString);
            //con.Open();

            string sql = @"SELECT Id, Nome, Preco FROM  Produtos
WHERE Nome=@n";

            using var cmd = new SqliteCommand(sql, _connetion);
            cmd.Parameters.AddWithValue("@n", nome);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Produto
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Preco = reader.GetDecimal(2)
                };
            }
            return null;         
            
        }

        public bool Remover(int id)
        {
            //using var con = new SqliteConnection(_connectionString);
            //con.Open();

            string sql = @"DELETE FROM Produtos WHERE Id = @id";

            using var cmd = new SqliteCommand(sql, _connetion);
            cmd.Parameters.AddWithValue("@id", id);
            int linhas = cmd.ExecuteNonQuery();
            return linhas > 0;
        }
        public bool ExistePorNome(string nome)
        {
            //using var con = new SqliteConnection(_connectionString);
            //con.Open();

            string sql = @"SELECT COUNT(*) FROM Produtos WHERE Nome=@n";
            using var cmd = new SqliteCommand(sql, _connetion);
            cmd.Parameters.AddWithValue("@n", nome);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }
    }
}
