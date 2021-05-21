/// <summary>
/// ASCII digits to binary value conversions
/// </summary>
namespace BK_MeterLogger
{
    public static class Conversions
    {
        public static int AsciiHexByteToInt(byte hex)
        {
            int value = (int)hex;

            value -= (value <= '9' ? '0' : '7');

            return value;
        }

        public static int TwoAsciiHexBytesToInt(byte c, byte d)
        {
            int value = AsciiHexByteToInt(c);
            value <<= 4;
            value |= AsciiHexByteToInt(d);

            return value;
        }

        public static int FourAsciiHexBytesToInt(byte c, byte d, byte e, byte f)
        {
            int value = AsciiHexByteToInt(c);
            value <<= 4;
            value |= AsciiHexByteToInt(d);
            value <<= 4;
            value |= AsciiHexByteToInt(e);
            value <<= 4;
            value |= AsciiHexByteToInt(f);

            return value;
        }

        public static decimal AsciiDecimalByteToDecimal(byte c)
        {
            decimal value = 0M;

            if (c >= '0' && c <= '9')
            {
                value = (decimal) (c - '0');
            }

            return value;
        }

        public static decimal FourAsciiDecimalBytesToDecimal(byte c, byte d, byte e, byte f)
        {
            decimal value = AsciiDecimalByteToDecimal(c);
            value *= 10M;
            value += AsciiDecimalByteToDecimal(d);
            value *= 10M;
            value += AsciiDecimalByteToDecimal(e);
            value *= 10M;
            value += AsciiDecimalByteToDecimal(f);

            return value;
        }
    }
}