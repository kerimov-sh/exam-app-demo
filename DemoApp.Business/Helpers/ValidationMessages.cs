namespace DemoApp.Business.Helpers;

public static class ValidationMessages
{
    public static string GetOutOfRangeMessage(string fieldName, int min, int max)
        => $"{AsFormattedFieldName(fieldName)} - {Math.Min(min, max)} və {Math.Max(min, max)} arasında olmalıdır.";

    public static string GetMaximumLengthMessage(string fieldName, int maximumLength)
        => $"{AsFormattedFieldName(fieldName)} - maksimum simvol sayı {maximumLength} olmalıdır.";

    public static string GetStaticLengthMessage(string fieldName, int length)
        => $"{AsFormattedFieldName(fieldName)} - {length} simvoldan ibarət olmalıdır.";

    private static string AsFormattedFieldName(string fieldName)
        => $"'{fieldName}'";
}