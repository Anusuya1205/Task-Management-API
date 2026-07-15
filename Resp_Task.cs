namespace Task_Management_API
{
    public class Resp_Task
    {
        public string? Status { get; set; }
        public int Status_Code { get; set; }
        public string? Message { get; set; }
        public List<MOD_Task>? Data { get; set; }
    }
}
