﻿namespace Trackit.App.Messages;

public record ActivityEditMessage
{
    public required Guid ActivityId { get; init; }
}