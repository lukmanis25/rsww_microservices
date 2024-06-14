﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tours.Core.Exceptions;

public class InvalidAggregateIdException : DomainException
{
    public override string Code { get; } = "invalid_aggregate_id";
    public Guid Id { get; }

    public InvalidAggregateIdException(Guid id) : base($"Invalid aggregate id: {id}")
        => Id = id;
}

