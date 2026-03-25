using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Business2Business.Common.ErrorLoggers.Models
{
    [Table("ErrorMessage")]
    public class ErrorMessage
    {
        [Key]
        [Column("ErrorMessageId")]
        public int ErrorMessageId { get; set; }
        [Column("Controller")]
        public string? Controller { get; set; }
        [Column("Action")]
        public string? Action { get; set; }
        [Column(" Message")]
        public string? Message { get; set; }
        [Column("UserId")]
        public Guid UserId { get; set; }
        [Column("DateActioned")]
        public DateTime DateActioned { get; set; }

    }
}
