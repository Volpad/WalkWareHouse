using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseApp
{
    [Table(Name = "WharehouseChanges")]
    public class WharehouseChanges
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false)] public DateTime time { get; set; }
        [Column] public string yarntype { get; set; }
        [Column] public string colorno { get; set; }
        [Column] public string fstlst { get; set; }
        [Column] public Nullable<int> boxnum { get; set; }


    }
}
