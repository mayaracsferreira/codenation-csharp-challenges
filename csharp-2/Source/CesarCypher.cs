using System;
using System.Linq;

namespace Codenation.Challenge
{
    public class CesarCypher : ICrypt, IDecrypt
    {

        public string Crypt(string message)
        {
            return CryptMecanism(message, 3);
        }

        public string Decrypt(string cryptedMessage)
        {
            return CryptMecanism(cryptedMessage, -3);
        }

        public string CryptMecanism(string message, int positionToSkip)
        {
            string alfa = "abcdefghijklmnopqrstuvwxyz";
            string cypherMessage = "";            

            if (message != null)
            {
                message = message.ToLower();
                foreach (var symbol in message)
                {
                    if (symbol == ' ' || char.IsDigit(symbol))
                    {
                        cypherMessage += symbol;
                    }
                    else if (alfa.Contains(symbol))
                    {
                        var index = alfa.IndexOf(symbol) + positionToSkip;
                        if (index > alfa.Length - 1)
                        {
                            index = index % alfa.Length;
                        }
                        else if (index < 0)
                        {
                            index = (alfa.Length) + index;
                        }

                        cypherMessage += alfa[index];
                    }                    
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                }
                return cypherMessage;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

    }
}
