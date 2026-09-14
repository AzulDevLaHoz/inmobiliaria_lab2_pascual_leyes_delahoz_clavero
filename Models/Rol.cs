using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    
    public class Rol
    {
        [Key]
        public int IdRol {get;set;}

        [Required]
        public String Nombre{get;set;}="";
    }
}