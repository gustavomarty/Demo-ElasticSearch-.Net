using System;
using System.Collections.Generic;

namespace DemoElasticApplication.EntityFramework.Models
{
    public partial class AgendaContatos2
    {
        public int Id { get; set; }
        public int IdEspecialidade { get; set; }
        public string Nome { get; set; }
        public decimal? Celular { get; set; }
        public string Email { get; set; }
    }
}
