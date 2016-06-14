using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear.OTP
{
    /// <summary>
    /// Encodes arbitrary byte arrays as case-insensitive base-32 strings. 
    /// The implementation is slightly different than in RFC 4648. During encoding, padding is not added, and during decoding 
    /// the last incomplete chunk is not taken into account
    /// </summary>
    public class Base32Encoding
    {
        private const int SHIFT = 5;
        private const int MASK = 31;

        public static byte[] ToBytes(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
            }

            input = input.TrimEnd('=').ToUpper();
            int byteCount = input.Length * SHIFT / 8;
            byte[] returnArray = new byte[byteCount];

            int buffer = 0;
            int next = 0;
            int bitsLeft = 0;

            foreach (char c in input)
            {
                buffer <<= SHIFT;
                buffer |= CharToValue(c) & MASK;
                bitsLeft += SHIFT;
                if (bitsLeft >= 8)
                {
                    returnArray[next++] = (byte)(buffer >> (bitsLeft - 8));
                    bitsLeft -= 8;
                }

                int cValue = CharToValue(c);
            }

            return returnArray;
        }

        public static string ToString(byte[] input)
        {
            if (input == null || input.Length == 0)
            {
                throw new ArgumentNullException("input");
            }

            if (input.Length >= (1 << 28))
            {
                // The computation below will fail, so don't do it.
                throw new System.ArgumentException();
            }

            int outputLength = (input.Length * 8 + SHIFT - 1) / SHIFT;
            StringBuilder result = new StringBuilder(outputLength);

            int buffer = input[0];
            int next = 1;
            int bitsLeft = 8;
            while (bitsLeft > 0 || next < input.Length)
            {
                if (bitsLeft < SHIFT)
                {
                    if (next < input.Length)
                    {
                        buffer <<= 8;
                        buffer |= (input[next++] & 0xff);
                        bitsLeft += 8;
                    }
                    else
                    {
                        int pad = SHIFT - bitsLeft;
                        buffer <<= pad;
                        bitsLeft += pad;
                    }
                }
                int index = MASK & (buffer >> (bitsLeft - SHIFT));
                bitsLeft -= SHIFT;
                result.Append(ValueToChar(index));
            }

            return result.ToString();
        }

        /// <summary>
        /// Convert uppercase character A-Z and 2-7 to int value
        /// </summary>
        /// <param name="c">the character to convert</param>
        /// <returns>0 for A till Z 26 for 2 till 7</returns>
        protected static int CharToValue(char c)
        {
            int value = (int)c;

            if (value <= 'Z' && value >= 'A')
            {
                return value - 'A';
            }
            if (value <= '7' && value >= '2')
            {
                return value - 24;
            }

            throw new ArgumentException("Character is not a Base32 character.", "" + c);
        }

        /// <summary>
        /// Convert value to char A-Z and 2-7
        /// </summary>
        /// <param name="b">the value to convert</param>
        /// <returns>the corresponding character</returns>
        private static char ValueToChar(int b)
        {
            if (b < 26)
            {
                return (char)(b + 'A');
            }

            if (b < 32)
            {
                return (char)(b + 24);
            }

            throw new ArgumentException("Byte is not a value Base32 value.", "b");
        }

    }    
}
