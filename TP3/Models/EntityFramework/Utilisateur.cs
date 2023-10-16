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
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Le nom ne peut pas être vide.")]
    public string? Nom { get; set; }

    [Column("utl_prenom")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Le prénom ne peut pas être vide.")]
    public string? Prenom { get; set; }

    [Column("utl_mobile", TypeName="char(10)")]
    [StringLength(10)]
    [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le mobile doit contenir 10 chiffres.")]
    public string? Mobile { get; set; }

    [Required(ErrorMessage="L'adresse mail est requise.")]
    [Column("utl_mail")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La longueur d’un email doit être comprise entre 6 et 100 caractères.")]
    [EmailAddress(ErrorMessage="Mauvais format d'adresse mail.")]
    public string? Mail { get; set; }

    [Column("utl_pwd")]
    [StringLength(64)]
    [Required(ErrorMessage="Le mot de passe est requis.")]
    [RegularExpression(@"^(?=.*[A-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@-_'=*!?])\S{6,10}$", ErrorMessage = "Le mot de passe doit contenir entre 6 et 10 caractères et au moins 1 majuscule, 1 chiffre et 1 caractère spécial")]
    public string? Pwd { get; set; }

    [Column("utl_rue")]
    [StringLength(200)]
    public string? Rue { get; set; }

    [Column("utl_cp", TypeName="char(50)")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Le code postale doit contenir au moins 5 chiffres.")]
    public string? CodePostal { get; set; }

    [Column("utl_ville")]
    [StringLength(50)]
    public string? Ville { get; set; }

    [Column("utl_pays")]
    [StringLength(50)]
    public string? Pays { get; set; }

    [Column("utl_latitude")]
    [RegularExpression(@"^-?([1-8]?[1-9]|[1-9]0)\.{1}\d{1,6}", ErrorMessage = "Mauvais format de latitude.")]
    public float? Latitude { get; set; }

    [Column("utl_longitude")]
    [RegularExpression(@"^-?([1]?[1-7][1-9]|[1]?[1-8][0]|[1-9]?[0-9])\.{1}\d{1,6}", ErrorMessage = "Mauvais format de longitude.")]
    public float? Longitude { get; set; }

    [Column("utl_datecreation", TypeName = "date")]
    [Required(ErrorMessage="La date de création est requise")]
    public DateTime DateCreation { get; set; }

    [InverseProperty("UtilisateurNotant")]
    public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();

}