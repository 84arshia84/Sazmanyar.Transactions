using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.Errors
{
    [Table("ErrorLogger", Schema = "TAM")]
    public class ErrorLogger
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string FunctionName { get; set; }
        public string Message {  get; set; }
        public string Line {  get; set; }
    }
}
