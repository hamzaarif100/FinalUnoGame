using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FinalUnoGame
{
    public class PlayingCards
    {
        #region ENUMS
        public enum CardColor
        {
            Red = 1,
            Blue = 2,
            Yellow = 3,
            Green = 4
        }

        public enum CardValue
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Skip = 10,
            DrawTwo = 11,
            Reverse = 12
        }
        #endregion

        public const string CARDS_PATH = "./images/cards";

        public CardColor Color { get; set; }
        public CardValue Value { get; set; }
        public int Score { get; set; }

        public Boolean isFlipped {get; set; }
        public BitmapImage cardImage { get; set; }

        public PlayingCards(CardColor color, CardValue value, Boolean isFlipped)
        {
            this.Color = color;
            this.Value = value;
            this.isFlipped = isFlipped;
            this.cardImage = GetCardImagePath(color, value, isFlipped);
        }
        //no parameter constructor
        public PlayingCards()
        {

        }
        //THis method utilizes the helper class method GetImage to get the path of the image.
        public BitmapImage GetCardImagePath(CardColor color, CardValue value, Boolean isFlipped)
        {
            if (isFlipped == true)
            {
                return Helper.GetImage(CARDS_PATH + "/back.PNG");
            }
            else
            {
                return Helper.GetImage(CARDS_PATH + "/" + color.ToString() + (int)value + ".PNG");
            }
        }
        public string DisplayValue
        {
            get
            {
                
                    return Value.ToString();
                
                return Color.ToString() + " " + Value.ToString();
            }
        }

    }
}
