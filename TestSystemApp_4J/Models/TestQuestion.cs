using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemApp_4J.Models
{
    public class TestQuestion
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Test")]
        public int TestId { get; set; }
        public Test Test { get; set; }


        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}