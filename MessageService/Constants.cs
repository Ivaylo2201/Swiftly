using System.Text.RegularExpressions;

namespace MessageService;

public static partial class Constants
{
    public static partial class Patterns
    {
        [GeneratedRegex(@"(?s){1:(?<SenderBic>\w+)\}.*?\{2:(?<ReceiverBic>\w+)\}.*?:20:(?<TransactionReference>[^\r\n]+).*?:23B:(?<BankOperationCode>[^\r\n]+).*?:32A:(?<ValueDate>\d{6})(?<OutgoingCurrencyCode>[A-Z]{3})(?<OutgoingAmount>[\d,]+).*?:33B:(?<IncomingCurrencyCode>[A-Z]{3})(?<IncomingAmount>[\d,]+).*?:50K:(?<Sender>.*?):52A:(?<Issuer>.*?)(?:\:53B:(?<Correspondent>.*?))?:59:(?<Receiver>.*?):70:(?<Reason>[^\r\n]+).*?:71A:(?<Charges>[^\r\n]+)", RegexOptions.Compiled | RegexOptions.Singleline)]
        public static partial Regex MessageParseRegex();
    
        [GeneratedRegex("^/[A-Za-z0-9]{2}[A-Za-z0-9]*$", RegexOptions.Compiled | RegexOptions.Singleline)]
        public static partial Regex AccountNumberParseRegex();
    }
}