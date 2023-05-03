using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BaseEntitie
    {
        [Key]
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public bool IsDelete { get; set; }
    }
}