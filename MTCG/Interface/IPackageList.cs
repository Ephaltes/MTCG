using System.Collections.Generic;

namespace MTCG.Interface
{
    public interface IPackageList
    {
        public List<IPackage> PackageModellList { get; }

        public bool AddPackageToList(IPackage entity);

        public List<ICard> Open(string packageId = "");
    }
}