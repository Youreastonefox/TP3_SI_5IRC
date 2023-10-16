using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TP3.Models.EntityFramework;

[Table("t_e_serie_ser")]
public partial class Serie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ser_id")]
    public int SerieId { get; set; }

    [Column("ser_titre")]
    [StringLength(100)]
    [Required(ErrorMessage="Le titre est requis.")]
    public string? Titre { get; set; }

    [Column("ser_resume")]
    public string? Resume { get; set; }

    [Column("ser_nbsaisons")]
    public int? NbSaisons { get; set; }

    [Column("ser_nbepisodes")]
    public int? NbEpisodes { get; set; }

    [Column("ser_anneecreation")]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Mauvais format de date.")]
    public int? AnneeCreation { get; set; }

    [Column("ser_network")]
    [StringLength(50)]
    public string? Network { get; set; }

    [InverseProperty("SerieNotee")]
    public virtual ICollection<Notation> NotesSerie { get; set; } = new List<Notation>();
}
