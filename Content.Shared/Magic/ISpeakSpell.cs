﻿using Content.Shared.Chat;

namespace Content.Shared.Magic;

public interface ISpeakSpell // The speak n spell interface
{
    /// <summary>
    /// Localized string spoken by the caster when casting this spell.
    /// </summary>
    public string? Speech { get; }

    [DataField]
    public InGameICChatType ChatType { get; }
}
