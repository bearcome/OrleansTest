using KWKY.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KWKY.Model.DBModel
{
    public partial class DemoTable: IDBModel
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int Age { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Birthday { get; set; }


        public void SetId (string guid)
        {
            ID = guid;
        }
        public string GetId ()
        {
            return ID;
        }
    }
}
