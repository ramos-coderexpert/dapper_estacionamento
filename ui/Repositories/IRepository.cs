namespace Dapper_estacionamento.Repositories

{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> ObterTodos();
        T ObterPorId(int id);
        void Inserir(T entidade);
        void Atualizar(T entidade);
        void Excluir(int id);
    }
}