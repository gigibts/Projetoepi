using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace projetoepi.Models;

public partial class Colaborador
{
    public int CodColaborador { get; set; }

    public string Nome { get; set; } = null!;

    public int Ctps { get; set; }

    public decimal Cpf { get; set; }

    public int Telefone { get; set; }

    public DateOnly DataAdmissao { get; set; }

    public string Email { get; set; } = null!;

[JsonIgnore]
    public virtual ICollection<EntregaEpi> EntregaEpis { get; } = new List<EntregaEpi>();
}
