using System.Data;
using System.Data.Common;
using System.Data.Odbc;

namespace Task_Management_API.DL
{
    public class ConnClass
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ConnClass> _logger;
        private readonly string _connectionString;

        public ConnClass(IConfiguration config, ILogger<ConnClass> logger)
        {
            _config = config;
            _logger = logger;
            _connectionString = _config.GetConnectionString("WebApiDatabase");
        }

        private OdbcConnection CreateConnection()
        {
            return new OdbcConnection(_connectionString);
        }

        public async Task<OdbcDataReader> ExecuteDataReaderAsync(string sql, params OdbcParameter[] parameters)
        {
            try
            {
                var connection = CreateConnection();
                await connection.OpenAsync();

                using (var command = new OdbcCommand(sql, connection))
                {
                    command.CommandTimeout = 60000;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return (OdbcDataReader)await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing data reader: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<DataSet> ExecuteDataSetAsync(string sql, params OdbcParameter[] parameters)
        {
            var ds = new DataSet();
            try
            {
                using (var connection = CreateConnection())
                {
                    await connection.OpenAsync();

                    using (var adapter = new OdbcDataAdapter(sql, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                adapter.SelectCommand.Parameters.Add(parameter);
                            }
                        }
                        adapter.Fill(ds);
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing dataset: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string sql, params OdbcParameter[] parameters)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new OdbcCommand(sql, connection))
                    {
                        command.CommandTimeout = 60000;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        return await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing non-query: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<int> ExecuteQueryAsync(string sql, params OdbcParameter[] parameters)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new OdbcCommand(sql, connection))
                    {
                        command.CommandTimeout = 60000;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing insert query: {Message}", ex.Message);
                throw;
            }
        }

        public void Dispose()
        {
            // Implement disposal logic if necessary
        }

    }
}

