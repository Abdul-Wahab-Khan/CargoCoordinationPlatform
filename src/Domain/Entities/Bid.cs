using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoCoordinationPlatform.Domain.Entities;

public class Bids : BaseAuditableEntity
{
    public int LoadId { get; set; }
    public Loads? Load { get; set; }
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
}