using ProductManagement.Domain.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Data.Repositories
{
    public class ProdutoRepository:IProdutoRepository
    {
        private List<Produto> _produtos;

        private int _proximoId;

        public ProdutoRepository()
        {
            _produtos = new List<Produto>();
            _proximoId = 1;
        }

        public void Adicionar(Produto produto)
        {
            produto.Id = _proximoId;
            _proximoId++;

            _produtos.Add(produto);

        }
        public List<Produto> ObterTodos()
        {
            return _produtos;
        }

        public Produto? ObterPorNome(string nome)
        { foreach(Produto p in _produtos)
            {
                if(p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return p;
                }
            }
            return null;
        }

        public bool Remover(int id)
        {
            Produto? produto = null;
            foreach(Produto p in _produtos)
            {
                if(p.Id == id)
                {
                    produto = p;
                    break;
                }
            }
            if (produto != null)
            {
                _produtos.Remove(produto);
                return true;
            }
            return false;

        }

        public bool ExistePorNome(string nome)
        {
            foreach(Produto p in _produtos)
            {
                if(p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
