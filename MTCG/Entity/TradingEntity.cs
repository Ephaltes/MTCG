using System;
using Newtonsoft.Json;

namespace MTCG.Entity
{
    public class TradingEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public CardEntity CardToTrade { get; set; }
        public CardType WantCardType { get; set; }
        public double WantMinDamage { get; set; }
        public Race WantRace { get; set; }
        public ElementType WantElementType { get; set; }
        [JsonIgnore]
        public UserEntity UserEntity;

    }
}