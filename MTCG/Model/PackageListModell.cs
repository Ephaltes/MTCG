using System;
using System.Collections.Generic;
using System.Linq;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;

namespace MTCG.Model
{
    public class PackageListModell : IPackageList
    {
        private readonly IDatabase _database;
        public List<IPackage> Packages
        {
            get => GetPackageList();
        }

        protected List<IPackage> GetPackageList()
        {
            return _database.GetPackages()?.OrderByDescending(x=>x.Entity.Amount).ToList();
        }
        public PackageListModell(IDatabase database)
        {
            _database = database;
        }

        public List<CardEntity> Open(UserEntity userEntity, string packageId = "")
        {
            return Packages.FirstOrDefault(x => x.Entity.Id == packageId)?.Open(userEntity);
        }
    }
}