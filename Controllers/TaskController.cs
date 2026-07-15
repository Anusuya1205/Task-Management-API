using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Management_API.BL;

namespace Task_Management_API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly IConfiguration v_config;
        private readonly BL_Task _blTask;
        private readonly ILogger<TaskController> _logger;
        public TaskController(IConfiguration _config, BL_Task blTask, ILogger<TaskController> logger)
        {
            v_config = _config;
            _blTask = blTask;
            _logger = logger;
        }


        // [Authorize]
        [HttpGet]
        [Route("api/GetTasks/{id}")]
        public async Task<IActionResult> GetTasks(string id)
        {
            List<MOD_Task> lstData = await _blTask.GetTasks(id);
            if (lstData != null)
            {
                Resp_Task clsRes = new Resp_Task();
                clsRes.Status = "True";
                clsRes.Status_Code = StatusCodes.Status200OK;
                clsRes.Message = "Data Retrived Succesfully";
                clsRes.Data = lstData;
                return Ok(clsRes);
            }
            else
            {

                Resp_Task clsResp = new Resp_Task();
                clsResp.Status_Code = StatusCodes.Status202Accepted;
                clsResp.Message = "no Records";
                clsResp.Status = "False";
                return Ok(clsResp);
            }
        }


        // [Authorize]
        [HttpPost]
        [Route("api/CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] MOD_Task cls)
        {
            string v_result = await _blTask.CreateTask(cls);
            if (v_result == "success")
            {
                MOD_Gnrl_Response clsResp = new MOD_Gnrl_Response();
                clsResp.message = "Record Created Successfully";
                clsResp.status = "True";
                clsResp.status_code = StatusCodes.Status200OK;
                return Ok(clsResp);

            }
            else
            {
                MOD_Gnrl_Response clsResp = new MOD_Gnrl_Response();
                clsResp.message = " Error while Creating the record : " + v_result;
                clsResp.status = "False";
                clsResp.status_code = StatusCodes.Status200OK;
                return StatusCode(200, clsResp);
            }



        }


        // [Authorize]
        [HttpPut]
        [Route("api/UpdatetTask")]
        public async Task<IActionResult> UpdatetTask([FromBody] MOD_Task cls)
        {
            string v_result = await _blTask.UpdatetTask(cls);
            if (v_result == "success")
            {
                MOD_Gnrl_Response clsResp = new MOD_Gnrl_Response();
                clsResp.message = "Record Updated Successfully";
                clsResp.status = "True";
                clsResp.status_code = StatusCodes.Status200OK;
                return Ok(clsResp);

            }
            else
            {
                MOD_Gnrl_Response clsResp = new MOD_Gnrl_Response();
                clsResp.message = " Error while Updating the record : " + v_result;
                clsResp.status = "False";
                clsResp.status_code = StatusCodes.Status200OK;
                return StatusCode(200, clsResp);
            }
        }



        ///  [Authorize]
        [HttpDelete]
        [Route("api/DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            string v_result = await _blTask.DeleteTask(id);
            if (v_result == "success")
            {
                MOD_Gnrl_Response clsResp = new MOD_Gnrl_Response();
                clsResp.message = "Record Deleted Successfully";
                clsResp.status = "True";
                clsResp.status_code = StatusCodes.Status200OK;
                return Ok(clsResp);

            }
            else
            {
                MOD_Gnrl_Response clsResp = new MOD_Gnrl_Response();
                clsResp.message = " Error while Deleting the record : " + v_result;
                clsResp.status = "False";
                clsResp.status_code = StatusCodes.Status200OK;
                return StatusCode(200, clsResp);
            }
        }

    }
}
