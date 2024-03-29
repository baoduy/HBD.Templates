﻿using HBDStack.EfCore.Abstractions.Attributes;

namespace MediatR.Domains.Share;

[SqlSequence]
public enum Sequences
{
    [Sequence(typeof(int), FormatString = "T{DateTime:yyMMdd}{1:00000}", Max = 99999)]
    Membership = 1
}