using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class StackModell
    {
        public List<CardModell> StackList;
        public List<CardModell> LockedCardList;

        public StackModell()
        {
            StackList = new List<CardModell>();
            LockedCardList = new List<CardModell>();
        }

        public bool Add(CardModell card)
        {
            try
            {
                StackList.Add(card);
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
