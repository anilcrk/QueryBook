using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.SqlStoredProcedure
{
    public class SqlDbManager
    {

        public enum SqlConnectionMode
        {
            DEFAULT = 0,
            COMPANY,
            NORTHWİND,
            QUERYBOOK

        }
        public enum ExecuteType
        {
            ExecuteReader,
            ExecuteNonQuery,
            ExecuteScalar
        }

        public enum ParameterDirection
        {
            Input = 1,
            Output = 2,
            InputOutput = 3,
            ReturnValue = 6
        }
        private string ConnectionString
        {
            get
            {
                string connection;
                switch (ConnectionMode)
                {
                    case SqlConnectionMode.DEFAULT:
                        connection = ConfigurationManager.ConnectionStrings["Default"].ConnectionString; ;
                        break;
                    case SqlConnectionMode.NORTHWİND:
                        connection = ConfigurationManager.ConnectionStrings["NortwindContext"].ConnectionString; ;
                        break;
                    case SqlConnectionMode.QUERYBOOK:
                        connection = ConfigurationManager.ConnectionStrings["QueryBookContext"].ConnectionString; ;
                        break;

                    default:
                        connection = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                        break;
                }

                return connection;
            }
        }

        private SqlDbManager()
        {

        }

        public static SqlDbManager Instance()
        {
            return new SqlDbManager();
        }

        public SqlConnectionMode ConnectionMode { get; set; }

        private SqlConnection Connection { get; set; }

        private SqlCommand Command { get; set; }

        public List<DbQueryParameter> OutParameters { get; private set; }

        public int TotalRows { get; set; }

        private void Open(SqlConnectionMode connectionMode)
        {
            try
            {
                ConnectionMode = connectionMode;

                //var connect= new StackExchange.Profiling.Data.ProfiledDbConnection(new SqlConnection(ConnectionString), MiniProfiler.Current);
                Connection = new SqlConnection(ConnectionString);
                // Connection = connect.InnerConnection as SqlConnection;
                Connection.Open();
            }
            catch (DataException ex)
            {

                Close();
            }
        }

        private void Close()
        {
            if (Connection != null)
            {
                Connection.Close();
            }
        }

        // executes stored procedure with DB parameteres if they are passed
        public object ExecuteProcedure(string procedureName, ExecuteType executeType, IEnumerable<DbQueryParameter> parameters)
        {
            object returnObject = null;

            if (Connection == null) return null;
            if (Connection.State != ConnectionState.Open) return null;

            Command = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = Connection,
                CommandText = procedureName
            };

            // pass stored procedure parameters to command
            if (parameters != null)
            {
                Command.Parameters.Clear();

                foreach (var dbParameter in parameters)
                {
                    var parameter = new SqlParameter
                    {
                        ParameterName = "@" + dbParameter.Name,
                        Direction = dbParameter.Direction,
                        Value = dbParameter.Value
                    };
                    //if (dbParameter.Value is DateTime)
                    //    parameter.Value = Convert.ToDateTime(dbParameter.Value).ToOADate();
                    //else
                    Command.Parameters.Add(parameter);
                }
            }

            switch (executeType)
            {
                case ExecuteType.ExecuteReader:
                    returnObject = Command.ExecuteReader();
                    break;
                case ExecuteType.ExecuteNonQuery:
                    returnObject = Command.ExecuteNonQuery();
                    break;
                case ExecuteType.ExecuteScalar:
                    returnObject = Command.ExecuteScalar();
                    break;
            }

            return returnObject;
        }

        // updates output parameters from stored procedure
        private void UpdateOutParameters()
        {
            if (Command.Parameters.Count <= 0) return;

            OutParameters = new List<DbQueryParameter>();
            OutParameters.Clear();

            for (var i = 0; i < Command.Parameters.Count; i++)
            {
                //if (Command.Parameters[i].Direction == ParameterDirection.Output)
                //{
                //    OutParameters.Add(new DbQueryParameter(Command.Parameters[i].ParameterName,
                //        Command.Parameters[i].Value,
                //        ParameterDirection.Output));
                //}
            }
        }

        // executes scalar query stored procedure without parameters
        public T ExecuteSingle<T>(string procedureName, SqlConnectionMode connectionMode) where T : new()
        {
            return ExecuteSingle<T>(procedureName, null, connectionMode);
        }

        // executes scalar query stored procedure and maps result to single object
        public T ExecuteSingle<T>(string procedureName, List<DbQueryParameter> parameters, SqlConnectionMode connectionMode) where T : new()
        {
            Open(connectionMode);
            var tempObject = new T();

            try
            {
                var reader = (IDataReader)ExecuteProcedure(procedureName, ExecuteType.ExecuteReader, parameters);


                if (reader.Read())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        try
                        {
                            SetObjectValue(reader.GetName(i), reader.GetValue(i), ref tempObject);
                        }
                        catch (Exception ex)
                        {
                            var name = reader.GetName(i);
                            //Helpers.LogHelper.Insert($"SQL:{procedureName} parameter {name}", ex);
                            //LogHelper.Error(procedureName + " parameters:" + GetErrorMessage(parameters) + " result  :" + reader.GetName(i) + " exception " + ex);
                        }
                    }
                }
                reader.Close();
                UpdateOutParameters();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }



            Close();

            return tempObject;
        }

        // executes list query stored procedure without parameters
        public List<T> ExecuteList<T>(string procedureName, SqlConnectionMode connectionMode) where T : new()
        {
            return ExecuteList<T>(procedureName, null, connectionMode);
        }

        // executes list query stored procedure and maps result generic list of objects
        public List<T> ExecuteList<T>(string procedureName, List<DbQueryParameter> parameters, SqlConnectionMode connectionMode) where T : new()
        {
            var listObjects = new List<T>();
            Open(connectionMode);

            try
            {
                var reader = (IDataReader)ExecuteProcedure(procedureName, ExecuteType.ExecuteReader, parameters);
                while (reader.Read())
                {
                    var listItemObject = new T();

                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        try
                        {
                            SetObjectValue(reader.GetName(i), reader.GetValue(i), ref listItemObject);
                        }
                        catch (Exception ex)
                        {
                            var name = reader.GetName(i);
                            //Helpers.LogHelper.Insert($"SQL:{procedureName} parameter {name}", ex);
                            //LogHelper.Error(procedureName + " parameters:" + GetErrorMessage(parameters) + " result  :" + reader.GetName(i) + " exception " + ex);
                        }

                    }

                    listObjects.Add(listItemObject);
                }

                reader.Close();

                UpdateOutParameters();
            }
            catch (Exception ex)
            {
                //Helpers.LogHelper.Insert($"SQL:{procedureName}", ex);
                //LogHelper.Error(procedureName + " exception " + ex);
            }
            Close();

            return listObjects;
        }

        public string GetErrorMessage(List<DbQueryParameter> parameters)
        {

            StringBuilder builder = new StringBuilder();
            if (parameters == null) return builder.ToString();
            foreach (var item in parameters)
            {
                builder.AppendFormat("{0}:{1}", item.Name, item.Value);
            }
            return builder.ToString();
        }

        // executes non query stored procedure with parameters
        public int ExecuteNonQuery(string procedureName, List<DbQueryParameter> parameters, SqlConnectionMode connectionMode)
        {
            Open(connectionMode);

            int returnValue = 0;
            try
            {
                returnValue = (int)ExecuteProcedure(procedureName, ExecuteType.ExecuteNonQuery, parameters);
            }
            catch (Exception ex)
            {
                //Helpers.LogHelper.Insert($"SQL:{procedureName} parameter error", ex);
                //LogHelper.Error(procedureName + " parameters:" + GetErrorMessage(parameters) + " exception " + ex);
            }

            UpdateOutParameters();

            Close();

            return returnValue;
        }

        public int ExecuteScalar(string procedureName, List<DbQueryParameter> parameters, SqlConnectionMode connectionMode)
        {
            Open(connectionMode);

            int returnValue = 0;
            try
            {
                returnValue = (int)ExecuteProcedure(procedureName, ExecuteType.ExecuteScalar, parameters);
            }
            catch (Exception ex)
            {
                //LogHelper.Error(procedureName + " parameters:" + GetErrorMessage(parameters) + " exception " + ex);
            }

            UpdateOutParameters();

            Close();

            return returnValue;
        }

        public void ExecuteBulkInsert(DataTable _table, SqlConnectionMode connectionMode)
        {
            Open(connectionMode);
            try
            {
                using (SqlBulkCopy s = new SqlBulkCopy(Connection))
                {
                    s.DestinationTableName = _table.TableName;

                    foreach (var column in _table.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());

                    s.WriteToServer(_table);
                }
            }
            catch (Exception ex)
            {

                //LogHelper.Error(ex);
            }

            Close();
        }

        private void SetObjectValue<T>(string name, object value, ref T itemObject)
        {
            if (value == DBNull.Value) return;
            var typeofT = typeof(T);
            if (typeofT.IsClass)
            {
                if (name == "TotalRows")
                {
                    TotalRows = Convert.ToInt32(value);
                    return;
                }


                if (name.Contains("."))
                {
                    string[] propertyNames = name.Split(new[] { '.' });
                    PropertyInfo nestedObjectProperty = typeofT.GetProperty(propertyNames[0]);
                    if (nestedObjectProperty != null && nestedObjectProperty.PropertyType.IsClass)
                    {
                        object subObjectInstance = nestedObjectProperty.GetValue(itemObject) ?? Activator.CreateInstance(nestedObjectProperty.PropertyType);

                        PropertyInfo propertyInfoOfNestedObject = subObjectInstance.GetType().GetProperty(propertyNames[1]);


                        if (propertyInfoOfNestedObject.CanWrite)
                            propertyInfoOfNestedObject.SetValue(subObjectInstance, value, null);

                        if (nestedObjectProperty.CanWrite)
                            nestedObjectProperty.SetValue(itemObject, subObjectInstance, null);

                    }
                }
                else
                {
                    var propertyInfo = typeofT.GetProperty(name);
                    if (propertyInfo == null) return;



                    if (propertyInfo.CanWrite)
                        propertyInfo.SetValue(itemObject, value, null);
                }

            }
            else
            {
                if (typeof(T).GetInterface("IConvertible") == typeof(IConvertible))
                    itemObject = (T)Convert.ChangeType(value, typeofT);
                else
                    itemObject = (T)value;
            }
        }

        public DataTable ExecuteQuery(string query, SqlConnectionMode model)
        {
            //switch (ConnectionMode)
            //{
            //    case SqlConnectionMode.BIGPARA:
            //        Open(SqlConnectionMode.BIGPARA);
            //        break;
            //    case SqlConnectionMode.DBMATRIKS:
            //        Open(SqlConnectionMode.DBMATRIKS);
            //        break;
            //    default:
            //        Open(SqlConnectionMode.BIGPARA);
            //        break;
            //}


            var sqlDataAdapter = new SqlDataAdapter(query, ConnectionString);
            var dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            sqlDataAdapter.Dispose();
            Close();
            return dt;
        }


        public int ExecuteNonQuery(string query, SqlConnectionMode connectionMode)
        {
            switch (connectionMode)
            {
                case SqlConnectionMode.DEFAULT:
                    Open(SqlConnectionMode.DEFAULT);
                    break;

                default:
                    Open(SqlConnectionMode.DEFAULT);
                    break;
            }
            Open(connectionMode);

            if (Connection == null) return -1;
            if (Connection.State != ConnectionState.Open) return -1;

            Command = new SqlCommand(query, Connection)
            {
                CommandType = CommandType.Text
            };
            int returnValue = 0;
            try
            {
                returnValue = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Helpers.LogHelper.Insert($"SQL:{query}", ex);
                //LogHelper.Error(query + " exception " + ex);
            }

            Command.Dispose();
            Close();

            return returnValue;


        }

        public List<T> ExecuteProcedureWithDataAdapter<T>(string procedureName, IEnumerable<DbQueryParameter> parameters,SqlConnectionMode connectionMode) where T : class
        {
            List<T> returnObject = null;
            Open(connectionMode);
            if (Connection == null) return null;
            if (Connection.State != ConnectionState.Open) return null;

            Command = new SqlCommand(procedureName, Connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // pass stored procedure parameters to command
            if (parameters != null)
            {
                Command.Parameters.Clear();

                foreach (var dbParameter in parameters)
                {
                    var parameter = new SqlParameter
                    {
                        ParameterName = "@" + dbParameter.Name,
                        Direction = dbParameter.Direction
                    };
                    //if (dbParameter.Value is DateTime)
                    //    parameter.Value = Convert.ToDateTime(dbParameter.Value).ToOADate();
                    //else
                    parameter.Value = dbParameter.Value;
                    Command.Parameters.Add(parameter);
                }
            }

            var da = new SqlDataAdapter();
            var dt = new DataTable();
            try
            {

                da.SelectCommand = Command;
                da.Fill(dt);
                returnObject = ConvertToList<T>(dt);
            }
            catch (Exception x)
            {
                //Helpers.LogHelper.Insert($"SQL:{procedureName}", x);
                //LogHelper.Error(procedureName + " exception " + x);
            }
            finally
            {
                Command.Dispose();
                da.Dispose();


            }
            return returnObject;
        }


        public DataSet ExecuteProcedureDataSets(string procedureName, IEnumerable<DbQueryParameter> parameters, SqlConnectionMode connectionMode)
        {
            Open(connectionMode);

            procedureName = procedureName.Trim();
            if (Connection == null) return null;
            if (Connection.State != ConnectionState.Open) return null;

            Command = new SqlCommand(procedureName, Connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
            {
                Command.Parameters.Clear();

                foreach (var dbParameter in parameters)
                {
                    var parameter = new SqlParameter
                    {
                        ParameterName = "@" + dbParameter.Name,
                        Direction = dbParameter.Direction
                    };
                    parameter.Value = dbParameter.Value;
                    Command.Parameters.Add(parameter);
                }
            }

            var da = new SqlDataAdapter();
            var dataSet = new DataSet();
            try
            {

                da.SelectCommand = Command;
                da.Fill(dataSet);

            }
            catch (Exception x)
            {

                throw new Exception();
            }
            finally
            {
                Command.Dispose();
                da.Dispose();
                Close();
            }
            return dataSet;
        }
        public void WriteSql(DataTable table, string tableName, SqlConnectionMode connectionMode)
        {

            Open(connectionMode);
            if (Connection == null) return;
            if (Connection.State != ConnectionState.Open) return;
            var rowArray = table.Select();

            using (var bulkCopy = new SqlBulkCopy(Connection))
            {
                bulkCopy.DestinationTableName = tableName;

                foreach (var c in table.Columns.OfType<DataColumn>())
                {
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(c.ColumnName, c.ColumnName));
                }


                bulkCopy.WriteToServer(rowArray);


            }
            Close();

        }

        private List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();

            var properties = typeof(T).GetProperties();

            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();

                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                        pro.SetValue(objT, row[pro.Name]);
                }

                return objT;
            }).ToList();

        }

    }

    public class DbQueryParameter
    {
        public string Name { get; set; }
        public ParameterDirection Direction { get; set; }
        public object Value { get; set; }

        public DbQueryParameter(string paramName, object paramValue)
        {
            Name = paramName;
            Direction = ParameterDirection.Input;
            Value = paramValue;
        }

        public DbQueryParameter(string paramName, object paramValue, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            Name = paramName;
            Direction = paramDirection;
            Value = paramValue;
        }
    }
}

