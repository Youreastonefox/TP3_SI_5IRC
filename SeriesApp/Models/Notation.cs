using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeriesApp.Models;

public partial class Notation
{
    public int UtilisateurId { get; set; }

    public int SerieId { get; set; }

    public int Note { get; set; }

    public virtual Utilisateur UtilisateurNotant { get; set; } = null!;

    public virtual Serie SerieNotee { get; set; } = null!;
}