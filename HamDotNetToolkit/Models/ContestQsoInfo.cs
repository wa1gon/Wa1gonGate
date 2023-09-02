using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamDotNetToolkit
{
    public class ContestQsoInfo
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? WorkedExch { get; set; } = string.Empty;
        public string? YourExch { get; set;} = string.Empty;
        public string? ClubMemberNum { get; set; } = null;
        public string? ClubElite{ get; set; } = null;

    }
}
