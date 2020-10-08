using System;
using System.Collections.Generic;
using System.Text;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    class DeckModell
    {
        public List<CardModell> DeckList;

        public DeckModell()
        {
            DeckList = new List<CardModell>();
        }
    }
}
