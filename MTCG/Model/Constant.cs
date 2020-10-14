using System;
using System.Collections.Generic;
using System.Text;
using MTCG.Model.BaseClass;
using MTCG.Model.MonsterTypes.Dragon;
using MTCG.Model.MonsterTypes.FireElf;
using MTCG.Model.MonsterTypes.Goblin;
using MTCG.Model.MonsterTypes.Knight;
using MTCG.Model.MonsterTypes.Kraken;
using MTCG.Model.MonsterTypes.Orc;
using MTCG.Model.MonsterTypes.Wizard;
using MTCG.Model.SpellCards.Fire;
using MTCG.Model.SpellCards.Normal;
using MTCG.Model.SpellCards.Water;

namespace MTCG.Model
{
    public class Constant
    {
        public const int MAXCARDSINDECK = 4;
        public const int MAXCARDSPERPACKAGE = 5;
        public static readonly CardModell[] AVAILABLECARDS = new CardModell[]
        {
            new RedDragon(),  new WingEggElf(), new GoblinLackey(), new GalaxyKnight(), new DemonKraken(), new OrcWarrior(), new FireWizard(), 
            new Fireball(), new Light(), new WaterGun()
        };

        public const double SPELLMULTIPLIER = 2;
    }
}
