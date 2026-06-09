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

        //criação da tabela

        public ProdutoSQLiteRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Produtos (
id INTEGER PRIMARY KEY AUTOINCREMENT,
Nome TEXT NOT NULL,
Preco REAL NOT NULL);";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
        //implementação do smetodos da interface

        public void Adicionar(Produto produto)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"INSERT INTO Produtos (Nome,Preco) VALUES(@n,@p)";

            using var cmd = new SqliteCommand(sql, con);

            cmd.Parameters.AddWithValue("@n", produto.Nome);
            cmd.Parameters.AddWithValue("@p", produto.Preco);

            cmd.ExecuteNonQuery();


        }
        public List<Produto> ObterTodos()
        {
            List<Produto> lista = new();

            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Nome, Preco FROM  Produtos";

            using var cmd = new SqliteCommand(sql, con);

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
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Nome, Preco FROM  Produtos
WHERE Nome=@n";

            using var cmd = new SqliteCommand(sql, con);
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
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"DELETE FROM Produtos WHERE Id = @id";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            int linhas = cmd.ExecuteNonQuery();
            return linhas > 0;
        }
        public bool ExistePorNome(string nome)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT COUNT(*) FROM Produtos WHERE Nome=@n";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", nome);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }
    }
}
