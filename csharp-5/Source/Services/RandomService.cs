using System;

namespace Codenation.Challenge.Services
{
    public class RandomService: IRandomService
    {
        public int RandomInteger(int max)
        {
            Random random = new System.Random();
            return random.Next(max);
        }
    }
}