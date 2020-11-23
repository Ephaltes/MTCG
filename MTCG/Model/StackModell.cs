using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MTCG.Interface;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class StackModell : IStack
    {
        private readonly List<ICard> _stackList;
        private readonly List<ICard> _lockedCardList;

        public StackModell()
        {
            _stackList = new List<ICard>();
            _lockedCardList = new List<ICard>();
        }

        public bool Add(ICard card)
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

        public bool Add(List<ICard> cards)
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
        public List<ICard> GetStack()
        {
            return _stackList;
        }

    }
}
