using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class StackModell
    {
        private readonly List<CardModell> _stackList;
        private readonly List<CardModell> _lockedCardList;

        public StackModell()
        {
            _stackList = new List<CardModell>();
            _lockedCardList = new List<CardModell>();
        }

        public bool Add(CardModell card)
        {
            try
            {
                _stackList.Add(card);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Add(List<CardModell> cards)
        {
            try
            {
                foreach (var card in cards)
                {
                    _stackList.Add(card);
                }
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
            return _stackList;
        }

    }
}
