using KWKY.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitCommonTest
{
    public class RSA2HelperTest
    {
        [Theory]
        [InlineData("MIIEowIBAAKCAQEAscSh8XvxdlZQGtnLOzPJsYYTc7zq9GMQBxarpVarcxdii7AxTjDMfiF5Os+Q5PjhsKevep5yl3YVdNZv4kM3bjaj8xon0q+RpXb76a40kzf10+sczrvXfxYxHR8GkRPmEOopDiqd//LlQbiGUiatjSjnxSdJM2hRQgyT+MF0Qbqsk5YSra98QqPWOyjhV8djFFtKtDAh5KbnfY61uwi4UBbeNhxbE8ngarwlZBISW74S3zzSQvLlnGs26PahEj6MKPtB38qU0fOJJUQGaqMoAv4GsVjT8T/dcTgssqpZ/cJXc2VcOIRbnVqD/Xb+Gyc4mLAxciNVcrbl2XSwgXj4rQIDAQABAoIBABW+R1CSKGahCAtAuGr1WqYsEBUH9nUN6Nf1HemLQE1Dfvo3AtDdhyWmUn2vBbn79x70Y7JLzxhXiShRUj4Kmq0QYd1KRbSvZNEoTIe4lxWqspbJGyzuLN2OiVNWiCcWl/1Zpsvkyo6T1xbCyyshL/kkF7U1qO5ww/+gKjky8FVZm69Dhm1fo1i7X7RWxxiobJbiGpPkF+ahYXiIcN8v/KbCLm/5xNsmJkX/ME+OFqTI00U4oNJSrISzppwMXXcySa9vz+XSSF5DOggU2jM8fjzKlFCnLa5PvZVWuE9dCUzkWJgCOUR9Vu3QUJRbsOpBrsJuOkbCoWH7IFjSJcD5FMECgYEA2h4WOEkdf0iYcHDfnXF9L1KK74/VZjoHqXNQFW7TjOwe5LxL7cpahDfXvwkUAl+bbFM9S3Dk9RVb6Dgx20TgSjd/7FnqmHBxFiKkIoi2Ond5g90m1fluj3+wecAZGRIuIG2SIpH8VP9N1bbyrJz559r2RdZWTKrALLL2at+R8GMCgYEA0KSI3nO0tjHIMKhayMWaQhYqwKKbwv0ao+tE9/E55rQ6vyfLrH222gXAbg9rNro8dAW97bA34HOjITka0vlR90kREmeoWNm65ybwjUTXa3XGZFKM/9Ys5pBOi2huvcrzl7bJ/KE155o9yTVDQXZt6/J0BYJZzd8RuxF2pyEsV68CgYEAlH7rTcKpbGC2N8cA1qXPPoqJwHNCK+jSEpFJMm5TZJvUpJF1eBzN5zpFje/WSY9htywsjNH9bsXt3nlzp/6KXLzQQM8uoj3bH+DRshk1RzTT6+yCZib9GIEsWpdn/uObt8QhO5v8GLn/+cImEVKF6U3vYLh95JBTaZslnLk9GFUCgYBaVZKo9eHdLIqnfozn9p5GVMCn7P+RmfJp3FhdHYVbPNdeokVN5pQ/q1PYkzZxWIsKFN5Zxf07YfXUUZwqkiYJ8gnBNT4vUf3ZTQPDtnWqojCGiTNhJYMFRJ+tPkcMR43WNx8XEVl8cEymDDiVZEhNawkOxZRKTXWoz/PWgsdnIwKBgFCl38S634m2AaBeayVt86wKL2PrrYWmRBhPzwW2u+bc6sO/j0bYZ/ErZw4t9X8eq3nZWbxrL/BKoxMp0bwhuTaZGP+ihn3/xpkEKZ6g6mqGm3/jTEILvkKYgK2woWiIvGUonJ1yN1uIGDZzpvRJ7gf0GfPozw8yYOGRghBAZ/6w",

            "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAscSh8XvxdlZQGtnLOzPJsYYTc7zq9GMQBxarpVarcxdii7AxTjDMfiF5Os+Q5PjhsKevep5yl3YVdNZv4kM3bjaj8xon0q+RpXb76a40kzf10+sczrvXfxYxHR8GkRPmEOopDiqd//LlQbiGUiatjSjnxSdJM2hRQgyT+MF0Qbqsk5YSra98QqPWOyjhV8djFFtKtDAh5KbnfY61uwi4UBbeNhxbE8ngarwlZBISW74S3zzSQvLlnGs26PahEj6MKPtB38qU0fOJJUQGaqMoAv4GsVjT8T/dcTgssqpZ/cJXc2VcOIRbnVqD/Xb+Gyc4mLAxciNVcrbl2XSwgXj4rQIDAQAB")]
        public void Test1(string privateKey, string publicKey)
        {

            RSA2Helper rsa = new RSA2Helper(privateKey,publicKey);
            string message = "abc";

            string messageEncrypted = rsa.Encrypt(message);
            string messageDecrypted = rsa.Decrypt(messageEncrypted);
            string sign = rsa.Sign(message);

            bool verified = rsa.Verify(message,sign);

            Assert.Equal(message, messageDecrypted);
            Assert.True(verified);
        }

        [Theory]
        [InlineData("MIIEowIBAAKCAQEAscSh8XvxdlZQGtnLOzPJsYYTc7zq9GMQBxarpVarcxdii7AxTjDMfiF5Os+Q5PjhsKevep5yl3YVdNZv4kM3bjaj8xon0q+RpXb76a40kzf10+sczrvXfxYxHR8GkRPmEOopDiqd//LlQbiGUiatjSjnxSdJM2hRQgyT+MF0Qbqsk5YSra98QqPWOyjhV8djFFtKtDAh5KbnfY61uwi4UBbeNhxbE8ngarwlZBISW74S3zzSQvLlnGs26PahEj6MKPtB38qU0fOJJUQGaqMoAv4GsVjT8T/dcTgssqpZ/cJXc2VcOIRbnVqD/Xb+Gyc4mLAxciNVcrbl2XSwgXj4rQIDAQABAoIBABW+R1CSKGahCAtAuGr1WqYsEBUH9nUN6Nf1HemLQE1Dfvo3AtDdhyWmUn2vBbn79x70Y7JLzxhXiShRUj4Kmq0QYd1KRbSvZNEoTIe4lxWqspbJGyzuLN2OiVNWiCcWl/1Zpsvkyo6T1xbCyyshL/kkF7U1qO5ww/+gKjky8FVZm69Dhm1fo1i7X7RWxxiobJbiGpPkF+ahYXiIcN8v/KbCLm/5xNsmJkX/ME+OFqTI00U4oNJSrISzppwMXXcySa9vz+XSSF5DOggU2jM8fjzKlFCnLa5PvZVWuE9dCUzkWJgCOUR9Vu3QUJRbsOpBrsJuOkbCoWH7IFjSJcD5FMECgYEA2h4WOEkdf0iYcHDfnXF9L1KK74/VZjoHqXNQFW7TjOwe5LxL7cpahDfXvwkUAl+bbFM9S3Dk9RVb6Dgx20TgSjd/7FnqmHBxFiKkIoi2Ond5g90m1fluj3+wecAZGRIuIG2SIpH8VP9N1bbyrJz559r2RdZWTKrALLL2at+R8GMCgYEA0KSI3nO0tjHIMKhayMWaQhYqwKKbwv0ao+tE9/E55rQ6vyfLrH222gXAbg9rNro8dAW97bA34HOjITka0vlR90kREmeoWNm65ybwjUTXa3XGZFKM/9Ys5pBOi2huvcrzl7bJ/KE155o9yTVDQXZt6/J0BYJZzd8RuxF2pyEsV68CgYEAlH7rTcKpbGC2N8cA1qXPPoqJwHNCK+jSEpFJMm5TZJvUpJF1eBzN5zpFje/WSY9htywsjNH9bsXt3nlzp/6KXLzQQM8uoj3bH+DRshk1RzTT6+yCZib9GIEsWpdn/uObt8QhO5v8GLn/+cImEVKF6U3vYLh95JBTaZslnLk9GFUCgYBaVZKo9eHdLIqnfozn9p5GVMCn7P+RmfJp3FhdHYVbPNdeokVN5pQ/q1PYkzZxWIsKFN5Zxf07YfXUUZwqkiYJ8gnBNT4vUf3ZTQPDtnWqojCGiTNhJYMFRJ+tPkcMR43WNx8XEVl8cEymDDiVZEhNawkOxZRKTXWoz/PWgsdnIwKBgFCl38S634m2AaBeayVt86wKL2PrrYWmRBhPzwW2u+bc6sO/j0bYZ/ErZw4t9X8eq3nZWbxrL/BKoxMp0bwhuTaZGP+ihn3/xpkEKZ6g6mqGm3/jTEILvkKYgK2woWiIvGUonJ1yN1uIGDZzpvRJ7gf0GfPozw8yYOGRghBAZ/6w",

            "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAscSh8XvxdlZQGtnLOzPJsYYTc7zq9GMQBxarpVarcxdii7AxTjDMfiF5Os+Q5PjhsKevep5yl3YVdNZv4kM3bjaj8xon0q+RpXb76a40kzf10+sczrvXfxYxHR8GkRPmEOopDiqd//LlQbiGUiatjSjnxSdJM2hRQgyT+MF0Qbqsk5YSra98QqPWOyjhV8djFFtKtDAh5KbnfY61uwi4UBbeNhxbE8ngarwlZBISW74S3zzSQvLlnGs26PahEj6MKPtB38qU0fOJJUQGaqMoAv4GsVjT8T/dcTgssqpZ/cJXc2VcOIRbnVqD/Xb+Gyc4mLAxciNVcrbl2XSwgXj4rQIDAQAB")]
        public void Test2 (string privateKey, string publicKey)
        {

            RSA2Helper rsa = new RSA2Helper(privateKey,publicKey);
            string message = "abcasdasdasdadasdasdasdF0Qbqz张张张张sk5YSra98QqPWOyjhV8djFFtKtDAh5KbnfY61uwi4UBbeNhxbE8ngarwlZBISW74S3zzSQvLlnGs26PahEj6MKPtB38qU0fOJJUF0Qbqsk5YSra98QqPWOyjhV8djFFtKtDAh5KbnfY61uwi4UBbeNhxbE8ngarwlZBISW74S3zzSQvLlnGs26PahEj6MKPtB38qU0fOJJUasdads";

            string messageEncrypted = rsa.Encrypt(message);
            string messageDecrypted = rsa.Decrypt(messageEncrypted);
            string sign = rsa.Sign(message);

            bool verified = rsa.Verify(message,sign);

            Assert.Equal(message, messageDecrypted);
            Assert.True(verified);
        }
    }
}
