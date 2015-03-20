using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AeroGear.OTP
{
    public class Totp
    {
        private readonly string secret;
        private readonly Clock clock;
        private const int DELAY_WINDOW = 1;

        /// <summary>
        /// Initialize an OTP instance with the shared secret generated on Registration process
        /// </summary>
        /// <param name="secret"> Shared secret </param>
        public Totp(string secret)
        {
            this.secret = secret;
            clock = new Clock();
        }

        /// <summary>
        /// Initialize an OTP instance with the shared secret generated on Registration process
        /// </summary>
        /// <param name="secret"> Shared secret </param>
        /// <param name="clock">  Clock responsible for retrieve the current interval </param>
        public Totp(string secret, Clock clock)
        {
            this.secret = secret;
            this.clock = clock;
        }

        /// <summary>
        /// Prover - To be used only on the client side
        /// Retrieves the encoded URI to generated the QRCode required by Google Authenticator
        /// </summary>
        /// <param name="name"> Account name </param>
        /// <returns> Encoded URI </returns>
        public virtual string uri(string name)
        {
            return string.Format("otpauth://totp/{0}?secret={1}", Uri.EscapeUriString(name), secret);
        }

        /// <summary>
        /// Retrieves the current OTP
        /// </summary>
        /// <returns> OTP </returns>
        public virtual string now()
        {
            return leftPadding(hash(secret, clock.CurrentInterval));
        }

        /// <summary>
        /// Verifier - To be used only on the server side
        /// <p/>
        /// Taken from Google Authenticator with small modifications from </summary>
        /// {<seealso cref= <a href="http://code.google.com/p/google-authenticator/source/browse/src/com/google/android/apps/authenticator/PasscodeGenerator.java?repo=android#212">PasscodeGenerator.java</a>}
        /// <p/>
        /// Verify a timeout code. The timeout code will be valid for a time
        /// determined by the interval period and the number of adjacent intervals
        /// checked.
        /// </seealso>
        /// <param name="otp"> Timeout code </param>
        /// <returns> True if the timeout code is valid
        ///         <p/>
        ///         Author: sweis@google.com (Steve Weis) </returns>
        public virtual bool verify(string otp)
        {

            long code = long.Parse(otp);
            long currentInterval = clock.CurrentInterval;

            int pastResponse = Math.Max(DELAY_WINDOW, 0);

            for (int i = pastResponse; i >= 0; --i)
            {
                int candidate = generate(this.secret, currentInterval - i);
                if (candidate == code)
                {
                    return true;
                }
            }
            return false;
        }

        private int generate(string secret, long interval)
        {
            return hash(secret, interval);
        }

        private int hash(string secret, long interval)
        {
            byte[] data = Base32Encoding.ToBytes(secret);
            var hash = new HMACSHA1(data).ComputeHash(BitConverter.GetBytes(interval).Reverse().ToArray());
            return bytesToInt(hash);
        }

        private int bytesToInt(byte[] hash)
        {
            // put selected bytes into result int
            int offset = hash.Last() & 0x0f;

            int binary = ((hash[offset] & 0x7f) << 24) 
                | ((hash[offset + 1] & 0xff) << 16) 
                | ((hash[offset + 2] & 0xff) << 8) 
                | (hash[offset + 3] & 0xff);

            return binary % 1000000;
        }

        private string leftPadding(int otp)
        {
            return string.Format("{0:D6}", otp);
        }
    }
}
