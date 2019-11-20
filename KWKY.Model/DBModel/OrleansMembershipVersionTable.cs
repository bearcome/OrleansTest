using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KWKY.Model.DBModel
{
    public partial class OrleansMembershipVersionTable
    {
        public OrleansMembershipVersionTable()
        {
            OrleansMembershipTable = new HashSet<OrleansMembershipTable>();
        }

        [Key]
        [StringLength(150)]
        public string DeploymentId { get; set; }
        [Column(TypeName = "datetime2(3)")]
        public DateTime Timestamp { get; set; }
        public int Version { get; set; }

        [InverseProperty("Deployment")]
        public virtual ICollection<OrleansMembershipTable> OrleansMembershipTable { get; set; }
    }
}
