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
        private List<CardModell> _tempIngameCards;

        public DeckModell()
        {
            _deckList = new List<CardModell>();
            _tempIngameCards = new List<CardModell>();
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
        
        public bool AddTemp(CardModell card)
        {
            try
            {
                _deckList.Add(card);
                _tempIngameCards.Add(card);
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

        public bool Remove(CardModell card)
        {
            return _deckList.Remove(card);
        }

        public bool RemoveTempCards()
        {
            try
            {
                foreach (var card in _tempIngameCards)
                {
                    _deckList.Remove(card);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
