using System.Collections.Generic;
using MTCG.Entity;

namespace MTCG.Interface
{
    public interface IPackage
    {
        public PackageEntity Entity { get; set; }
        public List<CardEntity> Open(UserEntity user);
    }
}