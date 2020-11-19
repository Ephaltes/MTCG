using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MTCG.Interface;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class DeckModell : IDeck
    {
        public List<ICard> DeckList { get; set; }

        public DeckModell()
        {
            DeckList = new List<ICard>();
        }
    }
}
