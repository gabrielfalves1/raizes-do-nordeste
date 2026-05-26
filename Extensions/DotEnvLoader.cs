using System.Text;

namespace raizes_do_nordeste.Extensions
{
    public static class DotEnvLoader
    {
        public static void Load(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            foreach (var rawLine in File.ReadAllLines(path, Encoding.UTF8))
            {
                var line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                {
                    continue;
                }

                var separatorIndex = line.IndexOf('=');
                if (separatorIndex <= 0)
                {
                    continue;
                }

                var key = line[..separatorIndex].Trim();
                var value = line[(separatorIndex + 1)..].Trim();

                if (value.Length >= 2)
                {
                    var isQuoted =
                        (value.StartsWith('\"') && value.EndsWith('\"')) ||
                        (value.StartsWith('\'') && value.EndsWith('\''));

                    if (isQuoted)
                    {
                        value = value[1..^1];
                    }
                }

                if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(key)))
                {
                    Environment.SetEnvironmentVariable(key, value);
                }
            }
        }
    }
}
