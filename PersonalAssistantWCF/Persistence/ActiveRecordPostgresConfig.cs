using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using System.Collections.Generic;
using System.Reflection;

namespace PersonalAssistantWCF.Persistence
{
    public class ActiveRecordPostgresConfig
    {
        private const string _ConnectionString = "Server=localhost;Port=5432;DataBase=assistant_db;User ID=postgres;Password=1201;";

        private string _ActiveRecordClassesAssemblyName { get; set; }
        private bool _ShowSql { get; set; }

        public ActiveRecordPostgresConfig(string activeRecordClassesAssemblyName, bool showSql)
        {
            _ActiveRecordClassesAssemblyName = activeRecordClassesAssemblyName;
            _ShowSql = showSql;
        }

        public void Initialize()
        {
            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(Assembly.Load(_ActiveRecordClassesAssemblyName));
            ActiveRecordStarter.ResetInitializationFlag();
            ActiveRecordStarter.Initialize(assemblies.ToArray(), _GetConfigSource());
        }

        public void CreateSchema()
        {
            ActiveRecordStarter.CreateSchema();
        }

        public void GenerateCreationScripts(string filePath)
        {
            ActiveRecordStarter.GenerateCreationScripts(filePath);
        }

        public void DropSchema()
        {
            ActiveRecordStarter.DropSchema();
        }

        private InPlaceConfigurationSource _GetConfigSource()
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add("connection.driver_class", "NHibernate.Driver.NpgsqlDriver");
            properties.Add("dialect", "NHibernate.Dialect.PostgreSQLDialect");
            properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            properties.Add("proxyfactory.factory_class", "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle");
            properties.Add("show_sql", _ShowSql.ToString());
            properties.Add("connection.connection_string", _ConnectionString);
            InPlaceConfigurationSource source = new InPlaceConfigurationSource();
            source.Add(typeof(ActiveRecordBase), properties);
            return source;
        }
    }
}




