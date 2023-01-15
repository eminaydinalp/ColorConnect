using System;
using System.Collections.Generic;
using System.Linq;
using Rentire.Core;
using UnityEngine;

namespace _GAME.__Scripts.Controller
{
    [System.Serializable]
    public class WhichColor
    {
        public TypeColor colorName;
            
        public Color liquidColor;
        public Color targetColor;
        public Color textColor;
    }

    public enum TypeColor
    {
        Empty,
        Red,
        Green,
        Blue,
        Yellow,
        Black,
        Pink,
        Orange,
        Purple,
        Turquoise,
        Brown,
        Grey,
        Burgundy
    }
    public class ColorController : Singleton<ColorController>
    {
        public List<WhichColor> whichColors = new List<WhichColor>();
        
        public WhichColor GetColor(TypeColor typeColor)
        {
            var selectedItem = whichColors.FirstOrDefault(x => x.colorName == typeColor);

            return selectedItem;
        }
    }
}