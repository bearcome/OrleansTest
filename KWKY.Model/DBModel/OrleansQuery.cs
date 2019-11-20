using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KWKY.Model.DBModel
{
    public partial class OrleansQuery
    {
        [Key]
        [StringLength(64)]
        public string QueryKey { get; set; }
        [Required]
        [StringLength(8000)]
        public string QueryText { get; set; }
    }
}
