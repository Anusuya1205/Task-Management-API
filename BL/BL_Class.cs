using System.Data;
using Task_Management_API.DL;

namespace Task_Management_API.BL
{
    public class BL_Class
    {
        private readonly ConnClass _connClass;
        private readonly ILogger<BL_Class> _logger;

        public BL_Class(ConnClass connClass, ILogger<BL_Class> logger)
        {
            _connClass = connClass;
            _logger = logger;
        }

        public async Task<DataSet> FetchDataAsync(string sql)
        {
            var ds = new DataSet();
            try
            {
                ds = await _connClass.ExecuteDataSetAsync(sql);

                return ds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching and processing data: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<int> ExecuteQueryAsync(string SQL)
        {
            int _result = 0;

            try
            {
                _result = await _connClass.ExecuteQueryAsync(SQL);
                return _result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Inserted data: {Message}", ex.Message);
                throw;
            }

        }




    }
}
