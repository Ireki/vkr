using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace vkr.Model
{
    public partial class Theses
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public string ProtocolNumber { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Theme { get; set; }
        public string Director { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public DateTime? DateDeleted { get; set; }
        [NotMapped]
        public bool CheckDeduct { get; set; }
    }
}
