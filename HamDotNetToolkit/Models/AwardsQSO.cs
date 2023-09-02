using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamDotNetToolkit
{
    public class AwardsQSO
    {
    public enum Status { Confirmed, None, Requested, Invalid, Granted}
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UploadedDtg { get;set; }
        public DateTime RecvDtg { get; set; }
        public DateTime ConfirmedDate { get; set; }
       
    }
}
