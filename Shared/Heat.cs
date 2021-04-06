using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stal.Shared
{
    [Table("HEATS", Schema = "public")]
    public class Heat
    {
        [Key, Column("HEAT_ID")]
        public int Id { get; set; }

        [Column("HEAT_NO")]
        public int Number { get; set; }

        [Column("HEAT_DATE")]
        public DateTime Date { get; set; }
    }
}
