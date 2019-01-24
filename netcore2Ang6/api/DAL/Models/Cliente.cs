using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage="*")]
        [MaxLength(100), StringLength(100)]
        [Display(Name ="Nome")]
        public string Nome { get; set; }

        //[RegularExpression("([0-9]+)")]
        //public int Idade { get; set; }

        [MaxLength(15), StringLength(15)]
        public string Sexo { get; set; }

        [MaxLength(100), StringLength(100)]
        public string Endereco { get; set; }

        [MaxLength(10), StringLength(10)]
        public string Cep { get; set; }

        public DateTime DataNascimento { get; set; }

        [MaxLength(15), StringLength(15)]
        public string CPF { get; set; }

        [MaxLength(30), StringLength(30)]
        public string Profissao { get; set; }

        [MaxLength(16), StringLength(16)]
        public string TelCel { get; set; }

        [MaxLength(16), StringLength(16)]
        public string TelRes { get; set; }

        [MaxLength(15), StringLength(15)]
        public string EstadoCivil { get; set; }

        [MaxLength(50), StringLength(50)]
        public string Email { get; set; }

        [MaxLength(50), StringLength(50)]
        public string Instagram { get; set; }

        public string Observacao { get; set; }

        public DateTime? DataUltimoServico { get; set; }

        public bool Bloqueado { get; set; }

        //fk
        [Display(Name = "Midia")]
        [Required(ErrorMessage = "*")]
        public int? MidiaId { get; set; }
        [ForeignKey("MidiaId")]
        public virtual Midia Midia { get; set; }

    }
}
