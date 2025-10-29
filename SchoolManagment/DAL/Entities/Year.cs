

using System.ComponentModel.DataAnnotations;

namespace SchoolDLL.Entities
{
    public class Year
    {


        public int Id { get; set; }


        public string Name { get; set; } = string.Empty;


        public bool isActive { get; set; } = true;

    }
}
