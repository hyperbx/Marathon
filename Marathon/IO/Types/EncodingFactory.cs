using System.Text;

namespace Marathon.IO.Types
{
    public static class EncodingFactory
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
