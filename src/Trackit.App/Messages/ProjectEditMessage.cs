﻿namespace Trackit.App.Messages;

public record ProjectEditMessage
{
    public required Guid ProjectId { get; init; }
}