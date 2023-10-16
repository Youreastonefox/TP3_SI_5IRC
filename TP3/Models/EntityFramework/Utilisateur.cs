using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TP3.Models.EntityFramework;

[Table("t_e_utilisateur_utl")]
public partial class Utilisateur
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("utl_id")]
    public int UtilisateurId { get; set; }

    [Column("utl_nom")]
    [StringLength(50)]
    public string? Nom { get; set; }

    [Column("utl_prenom")]
    [StringLength(50)]
    public string? Prenom { get; set; }

    [Column("utl_mobile", TypeName="char(100)")]
    [StringLength(100)]
    [RegularExpression(@"^((\+)33|0)[1-9](\d{2}){4}$", ErrorMessage = "Le numéro de téléphone doit être au format 0X XX XX XX XX ou +33 X XX XX XX XX")]
    public string? Mobile { get; set; }

    [Required]
    [Column("utl_mail")]
    [EmailAddress]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La longueur d’un email doit être comprise entre 6 et 100 caractères.")]
    public string? Mail { get; set; }

    [Column("utl_pwd")]
    [StringLength(64)]
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{{8,}}$", ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères, une majuscule, une minuscule, un chiffre et un caractère spécial.")]
    public string? Pwd { get; set; }

    [Column("utl_rue")]
    [StringLength(200)]
    public string? Rue { get; set; }

    [Column("utl_cp", TypeName="char(50)")]
    [StringLength(50)]
    public string? CodePostal { get; set; }

    [Column("utl_ville")]
    [StringLength(50)]
    public string? Ville { get; set; }

    [Column("utl_pays")]
    [StringLength(50)]
    public string? Pays { get; set; }

    [Column("utl_latitude")]
    public float? Latitude { get; set; }

    [Column("utl_longitude")]
    public float? Longitude { get; set; }

    [Column("utl_datecreation", TypeName = "date")]
    [Required]
    public DateTime DateCreation { get; set; }

    [InverseProperty("UtilisateurNotant")]
    public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();

}