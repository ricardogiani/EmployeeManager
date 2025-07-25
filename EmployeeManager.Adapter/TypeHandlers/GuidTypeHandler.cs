using System.Data;
using Dapper;
using Npgsql;

namespace EmployeeManager.Adapter.TypeHandlers
{
    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            // Npgsql pode retornar UUIDs como string ou como Guid dependendo da versão
            // e como o dado foi lido. Tenta converter de string primeiro.
            if (value is string sValue)
            {
                return new Guid(sValue);
            }
            if (value is Guid gValue)
            {
                return gValue;
            }
            throw new ArgumentException($"Cannot convert value of type {value?.GetType().Name ?? "null"} to Guid.");
        }

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value;
            ((NpgsqlParameter)parameter).NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid;
        }
    }
}