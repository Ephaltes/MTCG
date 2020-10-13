using MTCG.Model.BaseClass;

namespace MTCG.Model.SpellCards.Fire
{
    public class Fireball:BaseFireSpellModell
    {
        public Fireball()
        {
            Description = "Story Fireball";
            Name = "Fireball";
            Damage = 1;
        }
        
        public override double CalculateDamge(CardModell enemyCard)
        {
            throw new System.NotImplementedException();
        }
    }
}