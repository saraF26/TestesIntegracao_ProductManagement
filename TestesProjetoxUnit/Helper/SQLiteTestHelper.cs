using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestesProjetoxUnit.Helper
{
    public class SQLiteTestHelper
    {

        //Criação de uma base de dados limpa,
        // in-memory (não cria ficheiro)
        //com tabela já pronta a usar

        public static SqliteConnection CreateInMemoryDataBase()
        {
            var connetion = new SqliteConnection("DataSource=:memory:");

            connetion.Open();

            //criar a tabela

            var cmd = connetion.CreateCommand();
            cmd.CommandText = @"CREATE TABLE Produtos(Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL, Preco REAL NOT NULL);";

            cmd.ExecuteNonQuery();

            return connetion;

            //este return devolve a ligação aberta e com a tabela criada

        }
    }
}
