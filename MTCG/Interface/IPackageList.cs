using System.Collections.Generic;
using MTCG.Entity;

namespace MTCG.Interface
{
    public interface IPackageList
    {
        public List<IPackage> Packages { get; }
        public List<CardEntity> Open(UserEntity userEntity,string packageId = "");
    }
}