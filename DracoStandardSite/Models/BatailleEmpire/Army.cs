namespace BatailleEmpire.Data.Models;

public class Army
{
    public int ArmyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Nation { get; set; } = string.Empty;
    public string? Period { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<DivisionType> DivisionTypes { get; set; } = new List<DivisionType>();
}

public class DivisionType
{
    public int DivisionTypeId { get; set; }
    public int ArmyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? CommanderRating { get; set; }
    public int CommanderCost { get; set; }
    public int MinCount { get; set; }
    public int MaxCount { get; set; } = 99;
    public string? Notes { get; set; }

    public Army Army { get; set; } = null!;
    public ICollection<DivisionUnitOption> UnitOptions { get; set; } = new List<DivisionUnitOption>();
}

public class UnitType
{
    public int UnitTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;   // Infantry/Cavalry/Artillery/Skirmisher
    public string? Morale { get; set; }                    // Elite/Veteran/Regular/Militia
    public string? ManeuverClass { get; set; }
    public string? Size { get; set; }                      // Small/Normal/Large
    public int BudgetCost { get; set; }
    public string? SpecialAbilities { get; set; }

    public ICollection<DivisionUnitOption> DivisionOptions { get; set; } = new List<DivisionUnitOption>();
}

public class DivisionUnitOption
{
    public int OptionId { get; set; }
    public int DivisionTypeId { get; set; }
    public int UnitTypeId { get; set; }
    public int MinUnits { get; set; }
    public int MaxUnits { get; set; } = 10;
    public bool IsRequired { get; set; }

    public DivisionType DivisionType { get; set; } = null!;
    public UnitType UnitType { get; set; } = null!;
}

public class ArmyBuild
{
    public int BuildId { get; set; }
    public string UserId { get; set; } = string.Empty;     // ASP.NET Identity user ID
    public int ArmyId { get; set; }
    public string BuildName { get; set; } = string.Empty;
    public int TotalPoints { get; set; }
    public string BuildJson { get; set; } = string.Empty;  // Serialised DivisionBuild list
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? ShareToken { get; set; }

    public Army Army { get; set; } = null!;
}
