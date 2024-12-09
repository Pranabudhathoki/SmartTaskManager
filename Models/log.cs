using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTaskManager.Models
{
    public class Log
    {
        [Key]
        public int LogID { get; set; }

        [Required]
        public string Action { get; set; }

        public string ErrorMessage { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}