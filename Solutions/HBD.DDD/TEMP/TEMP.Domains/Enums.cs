using HBD.EfCore.Abstractions.Attributes;

namespace TEMP.Domains;

public enum LegalType
{
    PrivateLimited = 0,
    Partnership = 1
}

[SequenceEnum]
[Flags]
public enum Sequences
{
    [Sequence(typeof(int), FormatString = "T{DateTime:yyMMdd}{1:00000}", Max = 99999)]
    Membership = 1
}

public enum ProfileType
{
    Personal = 0,
    Business = 1
}