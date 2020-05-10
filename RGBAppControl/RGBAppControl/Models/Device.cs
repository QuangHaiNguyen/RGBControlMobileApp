using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RGBAppControl.Models
{
    [Table("Device")]
    public class Device
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255), Unique]
        public string Name { get; set; }
        
        [MaxLength(255)]
        public string IP { get; set; }

        [MaxLength(255)]
        public string Port { get; set; }

        [MaxLength(255)]
        public string Num_Of_Led { get; set; }
    }
}
