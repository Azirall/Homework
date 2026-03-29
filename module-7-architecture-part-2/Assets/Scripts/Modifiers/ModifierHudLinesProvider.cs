using System.Collections.Generic;

public sealed class ModifierHudLinesProvider
{
    public IReadOnlyList<string> GetLines(IReadOnlyList<GameModifierConfig> configs)
    {
        var lines = new List<string>();
        if (configs == null)
            return lines;

        foreach (var config in configs)
        {
            if (config == null)
                continue;

            var line = config.GetHudLine();
            if (!string.IsNullOrWhiteSpace(line))
                lines.Add(line);
        }

        return lines;
    }
}
