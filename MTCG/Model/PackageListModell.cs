using System;
using System.Collections.Generic;
using System.Linq;
using MTCG.Entity;
using MTCG.Interface;

namespace MTCG.Model.BaseClass
{
    public class PackageListModell : IPackageList
    {
        public List<IPackage> PackageModellList { get; }

        public PackageListModell()
        {
            PackageModellList = new List<IPackage>();
        }

        public bool AddPackageToList(IPackage entity)
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

        public List<CardEntity> Open(string packageId = "")
        {
            IPackage packageToOpen = null;
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