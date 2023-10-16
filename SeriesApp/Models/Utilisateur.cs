using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesApp.Models;

public partial class Utilisateur
{
    public int UtilisateurId { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Mobile { get; set; }

    public string? Mail { get; set; }

    public string? Pwd { get; set; }

    public string? Rue { get; set; }

    public string? CodePostal { get; set; }

    public string? Ville { get; set; }

    public string? Pays { get; set; }

    public float? Latitude { get; set; }

    public float? Longitude { get; set; }

    public DateTime DateCreation { get; set; }

    public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();

}