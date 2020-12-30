using System;
using System.Collections.Generic;

namespace MTCG.Entity
{
    public class PackageEntity
    {
        public List<CardEntity> CardsInPackage;
        public string Name;
        public string Id { get; set; }
        public int Amount { get; set; } = 1;
    }
}