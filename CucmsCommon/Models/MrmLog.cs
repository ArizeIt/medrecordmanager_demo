using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CucmsCommon.Models
{
    public class MrmLog
    {
        [Key]
        public int Id {  get; set; }

        public DateTime Date {  get; set; }
        public string Thread { get; set; } = string.Empty;
        public string Level {  get; set; } = string.Empty;
        public string Logger {  get; set; } = string.Empty;
        public string Message {  get; set; } = string.Empty;
        public string Exception {  get; set; } = string.Empty;
    }
}
