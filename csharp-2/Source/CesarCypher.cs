using System;
using System.Text.RegularExpressions;

namespace Codenation.Challenge
{
    public class CesarCypher : ICrypt, IDecrypt
    {
        public string Crypt(string message)
        {
            if (message != null)
            {
                if (message != "")
                {
                    return Encipher(message, 3);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public string Decrypt(string cryptedMessage)
        {
            if (cryptedMessage != null)
            {
                if (cryptedMessage != "")
                {
                    return Decipher(cryptedMessage, 3);
                }
                else
                {
                    return "";
                }

            }
            else
            {
                throw new ArgumentNullException();
            }
        }


        private static char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
                return ch;

            char offset = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - offset) % 26) + offset);
        }

        public static string Encipher(string input, int key)
        {
            string output = string.Empty;

            Regex regex = new Regex("^[a-zA-Z0-9 ]*$");


            foreach (char ch in input)
                output += Cipher(ch, key);
            if (!regex.IsMatch(input))
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                return output.ToLower();
            }

        }

        public static string Decipher(string input, int key)
        {
            Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
            if (!regex.IsMatch(input))
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                return Encipher(input, 26 - key).ToLower();
            }

        }

    }
}