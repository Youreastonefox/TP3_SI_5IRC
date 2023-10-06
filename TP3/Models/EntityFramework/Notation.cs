using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TP3.Models.EntityFramework;

[Table("t_j_notation_not")]
public partial class Notation
{
    [Column("utl_id")]
    public int UtilisateurId { get; set; }

    [Column("ser_id")]
    public int SerieId { get; set; }

    [Column("not_note")]
    public int Note { get; set; }

    [ForeignKey(nameof(Utilisateur))]
    [InverseProperty("NotesUtilisateur")]
    public virtual Utilisateur UtilisateurNotant { get; set; } = null!;

    [ForeignKey(nameof(Serie))]
    [InverseProperty("NotesSerie")]
    public virtual Serie SerieNotee { get; set; } = null!;
}