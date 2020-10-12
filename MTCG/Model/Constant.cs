using System;
using System.Collections.Generic;
using System.Text;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class Constant
    {
        public const int MAXCARDSINDECK = 4;
        public const int MAXCARDSPERPACKAGE = 5;
        public static readonly CardModell[] AVAILABLECARDS = new CardModell[]
        {
            new BaseDragonModell(), new BaseGoblinModell(), new BaseKnightModell(),new BaseKrakenModell(), new BaseOrcModell(), new BaseWizardModell(),
            new BaseFireElveModell(), new BaseFireSpellModell(), new BaseNormalSpellModell(), new BaseWaterSpellModell()
        }; 
    }
}
