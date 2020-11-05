﻿using System;
using System.Collections.Generic;
using MTCG.Model.BaseClass;

namespace MTCG.Entity
{
    public class PackageEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Amount { get; set; }
        public List<CardModell> CardsInPackage;
    }
}