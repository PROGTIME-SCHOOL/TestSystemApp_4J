using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemApp_4J.Models.ViewModels
{
    public class ViewModelResultUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string TestName { get; set; }
        public int Score { get; set; }
        public int MaxScores { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
    }
}
