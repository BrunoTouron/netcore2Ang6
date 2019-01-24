using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Midia
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Descricao { get; set; }

        //public ICollection<Cliente> Clientes { get; set; }

        //public Midia()
        //{
        //    Clientes = new List<Cliente>();
        //}

    }
}
