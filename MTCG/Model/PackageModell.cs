using System;
using System.Collections.Generic;

namespace MTCG.Model.BaseClass
{
    public class PackageModell
    {
        public int Count { get; set; }

        public PackageModell()
        {
            //logic set Package count from DB if not in database set count to 1
            Count = 1;
        }

        public List<CardModell> Open()
        {
            List<CardModell> retVal = new List<CardModell>();

            if (Count < 1)
                return retVal;
            
            Random random = new Random();
            for (int i = 0; i < Constant.MAXCARDSPERPACKAGE; i++)
            {
                int r = random.Next(Constant.AVAILABLECARDS.Length);
                retVal.Add(Constant.AVAILABLECARDS[r]);
            }

            Count--;
            return retVal;
        }
    }
}