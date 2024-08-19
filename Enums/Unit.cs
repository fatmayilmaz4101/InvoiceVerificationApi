using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.Enums
{
    public enum Unit
    {
        Pc = 1,
        M,
        Sqm,
        Lm,
        Pair,
        Box,
        Roll
    }
}
public static class EnumExtensions
{
    public static string ToEnumString(this Unit unit) => unit.ToString();
    public static string ToEnumString(this Currency currency) => currency.ToString();
    public static string ToEnumString(this InvoiceCurrency invoiceCurrency) => invoiceCurrency.ToString();

}


