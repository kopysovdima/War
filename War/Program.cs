using System;
using System.Collections.Generic;

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            PlatoonCreater platoonCreator = new PlatoonCreater();
            Platoon platoonCountryRed = platoonCreator.Create(5);
            Platoon platoonCountryBlue = platoonCreator.Create(5);

            Battlefield battlefield = new Battlefield();
            battlefield.Work(platoonCountryRed, platoonCountryBlue);
        }
    }
}
