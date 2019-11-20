using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KWKY.Model.DBModel
{
    public partial class Storage
    {
        public int GrainIdHash { get; set; }
        public long GrainIdN0 { get; set; }
        public long GrainIdN1 { get; set; }
        public int GrainTypeHash { get; set; }
        [Required]
        [StringLength(512)]
        public string GrainTypeString { get; set; }
        [StringLength(512)]
        public string GrainIdExtensionString { get; set; }
        [Required]
        [StringLength(150)]
        public string ServiceId { get; set; }
        public byte[] PayloadBinary { get; set; }
        [Column(TypeName = "xml")]
        public string PayloadXml { get; set; }
        public string PayloadJson { get; set; }
        [Column(TypeName = "datetime2(3)")]
        public DateTime ModifiedOn { get; set; }
        public int? Version { get; set; }
    }
}
