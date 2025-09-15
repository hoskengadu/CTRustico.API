using System;
using System.Collections.Generic;

namespace Academia.Domain.Entities
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public int PlanoId { get; set; }
        public Plano? Plano { get; set; }
        public ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();
    }
}