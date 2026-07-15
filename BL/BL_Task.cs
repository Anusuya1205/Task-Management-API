using System.Data;
using System.Text.Json;

namespace Task_Management_API.BL
{
    public class BL_Task
    {
        private readonly IConfiguration v_config;
        private readonly BL_Class _blClass;
        private readonly ILogger<BL_Task> _logger;
        public BL_Task(BL_Class blClass, ILogger<BL_Task> logger, IConfiguration config)
        {
            _blClass = blClass;
            _logger = logger;
            v_config = config;
        }


        // if the id is not null it will go the condition otherwise it will show all the record
        public async Task<List<MOD_Task>> GetTasks(string id)
        {
            string SQL;

            SQL = @" select * from tasks  where 1=1 ";

            if (id != "null")
            {
                SQL = SQL + " and id = '{0}' ";
                SQL = string.Format(SQL, id);
            }
            SQL = SQL + " order by due_date ";
            SQL = string.Format(SQL);

            DataSet ds = await _blClass.FetchDataAsync(SQL);
            DataTable dt = ds.Tables[0];

            List<MOD_Task> clsTask = new List<MOD_Task>();
            foreach (DataRow dr in dt.Rows)
            {
                clsTask.Add(new MOD_Task
                {
                    id = Convert.ToInt32(dr["id"]),
                    title = dr["title"]?.ToString(),
                    description = dr["description"]?.ToString(),
                    status = dr["status"].ToString(),
                    due_date = Convert.ToDateTime(dr["due_date"]),
                    created_dt = Convert.ToDateTime(dr["created_dt"]),
                    updated_dt = dr["updated_dt"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(dr["updated_dt"])

                });

            }
            return clsTask;
        }

        public async Task<string> CreateTask(MOD_Task cls)
        {

            string v_result = "success", SQL;
            int v_compile = 0;

            try
            {


                #region "Update JSON Data Null"

                foreach (var prop in typeof(MOD_Task).GetProperties())
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        var value = prop.GetValue(cls) as string;
                        if (value == null || value == "")
                        {
                            prop.SetValue(cls, "NULL");
                        }
                    }
                }
                string updatedJson = JsonSerializer.Serialize(cls, new JsonSerializerOptions { WriteIndented = true });
                cls = JsonSerializer.Deserialize<MOD_Task>(updatedJson)!;

                #endregion


                if (cls.due_date < DateTime.Now)
                {
                    v_result = " Due date should be a Future Date";
                }
                else {

                    SQL = @"INSERT INTO public.tasks(
	                    title, description, due_date)
	                    VALUES ('{0}', '{1}', '{2}')";
                    SQL = string.Format(SQL,
                            cls.title, cls.description, cls.due_date
                        );
                    SQL = SQL.Replace("'NULL'", "NULL");
                    v_compile = await _blClass.ExecuteQueryAsync(SQL);

                }
            }
            catch (Exception e)
            {
                v_result = e.Message.ToString();
            }

            return v_result;
        }


        public async Task<string> UpdatetTask(MOD_Task cls)
        {
            string v_result = "success", SQL;
            int v_compile = 0;
            try
            {

                #region "Update JSON Data Null"

                foreach (var prop in typeof(MOD_Task).GetProperties())
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        var value = prop.GetValue(cls) as string;
                        if (value == null || value == "")
                        {
                            prop.SetValue(cls, "NULL");
                        }
                    }
                }
                string updatedJson = JsonSerializer.Serialize(cls, new JsonSerializerOptions { WriteIndented = true });
                cls = JsonSerializer.Deserialize<MOD_Task>(updatedJson)!;

                #endregion

                if (cls.due_date < DateTime.Now)
                {
                    v_result = " Due date should be a Future Date";
                }
                else
                {

                    SQL = @" UPDATE public.tasks 
                         SET  
                         title = '{1}', description = '{2}', due_date = '{3}' , status = '{4}',updated_dt = CURRENT_DATE
                         where id = '{0}'";
                    SQL = string.Format(SQL,
                        cls.id, cls.title, cls.description, cls.due_date, cls.status
                        );
                    SQL = SQL.Replace("'NULL'", "NULL");
                    v_compile = await _blClass.ExecuteQueryAsync(SQL);
                }
            }
            catch (Exception e)
            {
                v_result = e.Message.ToString();
            }

            return v_result;
        }


        public async Task<string> DeleteTask(string id)
        {
            string v_result = "success", SQL;
            int v_compile = 0;
            try
            {


                SQL = " Delete from tasks where id = '{0}'";
                SQL = string.Format(SQL, id);


                v_compile = await _blClass.ExecuteQueryAsync(SQL);
            }
            catch (Exception e)
            {
                v_result = e.Message.ToString();
            }

            return v_result;
        }



        public async Task ExpireTasks()
        {
            string sql = @"
        UPDATE tasks
        SET status = 'Expired',
            updated_dt = CURRENT_DATE
        WHERE due_date < CURRENT_TIMESTAMP
        AND status <> 'Expired'";

            await _blClass.ExecuteQueryAsync(sql);
        }




    }
}
