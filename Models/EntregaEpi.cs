using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace projetoepi.Models;

public partial class EntregaEpi
{
    public int CodEntrega { get; set; }

    public DateOnly? DataValidade { get; set; }

    public int CodColaborador { get; set; }

    public int CodEpi { get; set; }

    public DateOnly DataEntrega { get; set; }

[JsonIgnore]
    public virtual Colaborador? CodColaboradorNavigation { get; set; }

[JsonIgnore]
    public virtual CadastroEpi? CodEpiNavigation { get; set; }
}
