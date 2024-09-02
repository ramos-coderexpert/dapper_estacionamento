using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace Dapper_estacionamento.Repositories

{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbConnection _connection;
        private readonly string _nometabela;

        public Repository(IDbConnection connection)
        {
            _connection = connection;
            _nometabela = ObterNomeTabela();
        }

        public IEnumerable<T> ObterTodos()
        {
            var sql = $"SELECT * FROM {_nometabela}";
            return _connection.Query<T>(sql);
        }

        public T ObterPorId(int id)
        {
            var sql = $"SELECT * FROM {_nometabela} WHERE Id = @Id";
            return _connection.QueryFirstOrDefault<T>(sql, new { Id = id });
        }

        public void Inserir(T entidade)
        {
            var campos = ObterCamposInsert(entidade);
            var valores = ObterValoresInsert(entidade);
            var sql = $"INSERT INTO {_nometabela} ({campos}) VALUES ({valores})";
            _connection.Execute(sql, entidade);
        }

        public void Atualizar(T entidade)
        {
            var campos = ObterCamposUpdate(entidade);
            var sql = $"UPDATE {_nometabela} SET {campos} WHERE Id = @Id";
            _connection.Execute(sql, entidade);
        }

        public void Excluir(int id)
        {
            var sql = $"DELETE FROM {_nometabela} WHERE Id = @Id";
            _connection.Execute(sql, new { Id = id });
        }

        private string ObterNomeTabela()
        {
            //Obtém o nome da tabela usando reflexão
            var tipo = typeof(T);
            var atributoTabela = tipo.GetCustomAttribute<TableAttribute>();

            if (atributoTabela != null)
                return atributoTabela.Name;

            return $"{tipo.Name}s";
        }

        private string ObterCamposInsert(T entidade)
        {
            //Obtém os nomes dos campos usando reflexão
            var tipo = typeof(T);
            var propriedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance) // Filtra as propriedades que não tem IgnoreInDapper
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreInDapperAttribute)));

            var nomesCampos = propriedades.Select(p => { var colunaName = p.GetCustomAttribute<ColumnAttribute>()?.Name; return colunaName ?? p.Name; });

            return string.Join(", ", nomesCampos);
        }

        private string ObterValoresInsert(T entidade)
        {
            //Cria valores com os nomes correspondentes aos campos
            var tipo = typeof(T);
            var propriedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance) // Filtra as propriedades que não tem IgnoreInDapper
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreInDapperAttribute)));

            var nomesCampos = propriedades.Select(p => { var colunaName = p.GetCustomAttribute<ColumnAttribute>()?.Name; return colunaName ?? p.Name; });

            return string.Join(", ", nomesCampos);
        }

        private string ObterCamposUpdate(T entidade)
        {
            var tipo = typeof(T);
            var propriedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance) // Filtra as propriedades que não tem IgnoreInDapper
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreInDapperAttribute)));

            var nomesCampos = propriedades.Select(p => { var colunaName = p.GetCustomAttribute<ColumnAttribute>()?.Name; return colunaName ?? p.Name; });

            return string.Join(", ", nomesCampos);
        }
    }
}