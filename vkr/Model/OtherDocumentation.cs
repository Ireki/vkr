using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkr.Model
{
    public class OtherDocumentation
    {
        public int Id { get; set; }
        public string TypeDocumentation { get; set; }
        public DateTime DateDeposit { get; set; }
        public int ShelfLife { get; set; }
        public string Location { get; set; }
        public DateTime? DateDeleted { get; set; }


        [NotMapped]
        public bool CheckDeduct { get; set; }
    }
}
