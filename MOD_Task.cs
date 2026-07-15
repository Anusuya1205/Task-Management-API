using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Task_Management_API
{
    public class MOD_Task
    {
        public int? id { get; set; }

        [Required]
        public string? title { get; set; }

        [Required]
        public string? description { get; set; }

        [Required]
        public DateTime? due_date { get; set; }

        public string? status { get; set; }
        public DateTime? created_dt { get; set; }
        public DateTime? updated_dt { get; set; }
    }
}
