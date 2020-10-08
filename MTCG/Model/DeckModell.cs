using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class DeckModell
    {
        private readonly List<CardModell> _deckList;

        public DeckModell()
        {
            _deckList = new List<CardModell>();
        }
        public bool Add(CardModell card)
        {
            try
            {
                _deckList.Add(card);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<CardModell> GetDeck()
        {
            return _deckList;
        }

    }
}
