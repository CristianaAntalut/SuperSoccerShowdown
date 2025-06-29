namespace SuperSoccerShowdown.Common.Utilities;

public static class RandomGenerator
{
    public static int GenerateUniqueRandomNumber(int minValue, int maxValue)
    {
        if (maxValue < minValue)
            throw new ArgumentException("incorrect range.");

        Random random = new Random();
        return random.Next(minValue, maxValue);
    }

    private static readonly char[] _chars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
    public static string GenerateUniqueRandomString(int length)
    {
        if (length < 1)
            throw new ArgumentException("Length must be at least 1.", nameof(length));

        var random = new Random();
        return new string(Enumerable.Range(0, length)
            .Select(_ => _chars[random.Next(_chars.Length)])
            .ToArray());
    }
}
