using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        // isolar o acesso aos dados da lógica de negócio

        public void Adicionar(Produto produto);

        public List<Produto> ObterTodos();

        public Produto? ObterPorNome(string nome); //? Nullable reference type

        public bool Remover(int id);

        public bool ExistePorNome(string nome);
    }
}
