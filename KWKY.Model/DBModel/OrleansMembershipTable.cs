using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KWKY.Model.DBModel
{
    public partial class OrleansMembershipTable
    {
        [Key]
        [StringLength(150)]
        public string DeploymentId { get; set; }
        [Key]
        [StringLength(45)]
        public string Address { get; set; }
        [Key]
        public int Port { get; set; }
        [Key]
        public int Generation { get; set; }
        [Required]
        [StringLength(150)]
        public string SiloName { get; set; }
        [Required]
        [StringLength(150)]
        public string HostName { get; set; }
        public int Status { get; set; }
        public int? ProxyPort { get; set; }
        [StringLength(8000)]
        public string SuspectTimes { get; set; }
        [Column(TypeName = "datetime2(3)")]
        public DateTime StartTime { get; set; }
        [Column(TypeName = "datetime2(3)")]
        public DateTime IAmAliveTime { get; set; }

        [ForeignKey(nameof(DeploymentId))]
        [InverseProperty(nameof(OrleansMembershipVersionTable.OrleansMembershipTable))]
        public virtual OrleansMembershipVersionTable Deployment { get; set; }
    }
}
