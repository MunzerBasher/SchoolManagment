using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ClassFeesTable
    {
        public int Id { get; set; }

        public string YearName { get; set; } = string.Empty;


        public string SemesterName { get; set; } = string.Empty;

        public string LevelName {  get; set; } = string.Empty;

        public string ClassName { get; set; } = string.Empty;

        public decimal Amount { get; set; }
    }

    
    public class AddClassFees
    {

        public int Id { get; set; }

        public int ClassId { get; set; }

        public int FeesTypeId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreateAt { get; set; }

    }


}
