using System;

namespace Academia.Domain.Entities
{
    public class Presenca
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }
        public DateTime DataPresenca { get; set; }
    }
}