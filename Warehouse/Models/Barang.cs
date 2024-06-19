using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;

public class Barang
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("kode_barang")]
    public string KodeBarang { get; set; }

    [Required]
    [Column("nama_barang")]
    public string NamaBarang { get; set; }

    [Required]
    [Column("harga_barang")]
    public int HargaBarang { get; set; }

    [Required]
    [Column("jumlah_barang")]
    public int JumlahBarang { get; set; }

    [Required]
    [Column("expired_date")]
    public DateTime ExpiredDate { get; set; }

    [Required]
    [ForeignKey("Gudang")]
    [Column("gudang_id")]
    public int GudangId { get; set; }

    public Gudang Gudang { get; set; }
}