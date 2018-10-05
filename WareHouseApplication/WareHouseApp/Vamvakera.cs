using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseApp
{

    [Table(Name = "Wharehouse")]
    public class Vamvakera
    {

        [Column(IsPrimaryKey = true, IsDbGenerated = false)] public string ColourNo { get; set; }
        [Column] public string ColourName { get; set; }
        [Column] public Nullable<int> B140first { get; set; }
        [Column] public Nullable<int> B140last { get; set; }
        [Column] public Nullable<int> B120first { get; set; }
        [Column] public Nullable<int> B120last { get; set; }
        [Column] public Nullable<int> O130first { get; set; }
        [Column] public Nullable<int> O130last { get; set; }
        [Column] public Nullable<int> B260first { get; set; }
        [Column] public Nullable<int> B260last { get; set; }
        [Column] public Nullable<int> B250first { get; set; }
        [Column] public Nullable<int> B250last { get; set; }
        [Column] public Nullable<int> B240first { get; set; }
        [Column] public Nullable<int> B240last { get; set; }
        [Column] public Nullable<int> B234first { get; set; }
        [Column] public Nullable<int> B234last { get; set; }
        [Column] public Nullable<int> B280first { get; set; }
        [Column] public Nullable<int> B280last { get; set; }
        [Column] public Nullable<bool> Inactive { get; set; }

/*
        [Column] public Int64 Rank { get; set; }
        [Column] public string ColorCode { get; set; }
        [Column] public string ColorGRdescription { get; set; }
        [Column] public Int64 RankS { get; set; }
        [Column] public string SizeDescription { get; set; }
        [Column] public int Warehouse { get; set; }
        [Column] public int InOrder { get; set; }
        [Column] public int Remainder { get; set; }
        [Column] public string Expr1 { get; set; }

    */
    }
}
