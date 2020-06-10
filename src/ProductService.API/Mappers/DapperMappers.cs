using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroservice.API.Mappers
{
    public class DapperMappers
    {
        public static void Config()
        {
            Dapper.SqlMapper.AddTypeHandler(new GuidTypeHandler());
            Dapper.SqlMapper.AddTypeHandler(new DecimalTypeHandler());
            Dapper.SqlMapper.RemoveTypeMap(typeof(Guid));
            Dapper.SqlMapper.RemoveTypeMap(typeof(Guid?));
            Dapper.SqlMapper.RemoveTypeMap(typeof(Decimal));
            Dapper.SqlMapper.RemoveTypeMap(typeof(Decimal?));

        }
    }

    public class GuidTypeHandler : Dapper.SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(System.Data.IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }

        public override Guid Parse(object value)
        {
            return new Guid((string)value);
        }
    }

    public class DecimalTypeHandler : Dapper.SqlMapper.TypeHandler<decimal>
    {
        public override void SetValue(System.Data.IDbDataParameter parameter, decimal val)
        {
            parameter.Value = val.ToString();
        }

        public override decimal Parse(object value)
        {
            if (value.GetType() == typeof(double))
            {
                return new decimal((double)value);
            }
            else
            {
                return new decimal((Int64)value);
            }
        }
    }

}
