namespace DialogueEditor.Extensions;

public static class StringExtensions
{
	public static string Truncate(this string? value, int maxLength)
	{
		if (string.IsNullOrEmpty(value)) return string.Empty;
		return value.Length <= maxLength ? value : value.Substring(0, maxLength);
	}

	public static string TruncateAndAdd(this string? value, int maxLength, string addition)
	{
		var truncated = value.Truncate(maxLength);
		var result = truncated;

		if (!string.IsNullOrEmpty(truncated) && truncated != value)
		{
			result = truncated + addition;
		}

		return result;
	}
}
