using System;
using System.Collections.Generic;
using System.Linq;
using MTCG.Entity;

namespace MTCG.Model.BaseClass
{
    public class PackageListModell
    {
        public readonly List<PackageModell> PackageModellList;

        public PackageListModell()
        {
            PackageModellList = new List<PackageModell>();
        }

        public bool AddPackageToList(PackageModell entity)
        {
            try
            {
                PackageModellList.Add(entity);       
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<CardModell> Open(string packageId = "")
        {
            PackageModell packageToOpen = null;
            if (string.IsNullOrWhiteSpace(packageId))
            {
                var random = new Random();
                int index = random.Next(PackageModellList.Count);
                packageToOpen = PackageModellList[index];
            }
            else
            {
                packageToOpen = PackageModellList.FirstOrDefault(x => x.Id == packageId);
                if (packageToOpen == null)
                    return null;
            }
            
            var ret = packageToOpen.Open();

            if (packageToOpen.PackageAmount <= 0)
            {
                PackageModellList.Remove(packageToOpen);
            }
            
            return ret;
        }
    }
}