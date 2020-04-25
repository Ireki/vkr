using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace vkr.Model
{
    public partial class Practical
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public string Fio { get; set; }
        public string PracticeBase { get; set; }
        public string HumanSettlement { get; set; }
        public DateTime StartOfPractice { get; set; }
        public DateTime EndOfPractice { get; set; }
        public string FormOfPractice { get; set; }
        public string Payment { get; set; }
        public string Director { get; set; }
        public DateTime? DateDeleted { get; set; }
        [NotMapped]
        public bool CheckDeduct { get; set; }
    }
}
