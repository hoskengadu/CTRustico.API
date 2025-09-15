using System.Collections.Generic;

namespace Academia.Domain.Entities
{
    public class Plano
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
    }
}