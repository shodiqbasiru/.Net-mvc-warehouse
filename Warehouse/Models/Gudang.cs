using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;

public class Gudang
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("kode_gudang")]
    [DisplayName("Kode Gudang")]
    public string KodeGudang { get; set; }
    
    [Required]
    [Column("nama_gudang")]
    [DisplayName("Nama Gudang")]
    public string NamaGudang { get; set; }
    
    [Required]
    [Column("alamat_gudang")]
    [DisplayName("Alamat Gudang")]
    public string AlamatGudang { get; set; }
    
    public ICollection<Barang> ListBarang { get; set; }
}