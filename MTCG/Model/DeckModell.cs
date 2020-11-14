using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MTCG.Model.BaseClass;

namespace MTCG.Model
{
    public class DeckModell
    {
        public List<CardModell> DeckList { get; set; }

        public DeckModell()
        {
            DeckList = new List<CardModell>();
        }
    }
}
