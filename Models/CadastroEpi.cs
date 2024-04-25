using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace projetoepi.Models;

public partial class CadastroEpi
{
    public int CodEpi { get; set; }

    public string? NomeEpi { get; set; }

    public string? DescricaoUso { get; set; }

[JsonIgnore]
    public virtual ICollection<EntregaEpi> EntregaEpis { get; } = new List<EntregaEpi>();
}
