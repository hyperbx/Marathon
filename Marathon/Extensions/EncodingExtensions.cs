using System.Text;

namespace Marathon.Extensions
{
    public static class EncodingExtensions
    {
        extension(Encoding)
        {
            public static Encoding ShiftJIS
            {
                get
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    return Encoding.GetEncoding("shift-jis");
                }
            }
        }
    }
}
