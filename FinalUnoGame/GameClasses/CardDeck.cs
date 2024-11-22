using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalUnoGame.PlayingCards;

namespace FinalUnoGame.GameClasses
{
    public class CardDeck
    {
        public List<PlayingCards> Cards { get; set; }
        //Creating the cards, 
        public CardDeck()
        {
            Cards = new List<PlayingCards>();
            #region Create the cards

            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                
                    //Creates the starter game deck by adding one copy of every card 1-9 for each uno card color
                    foreach (CardValue val in Enum.GetValues(typeof(CardValue)))
                    {
                        switch (val)
                        {
                            case CardValue.One:
                            case CardValue.Two:
                            case CardValue.Three:
                            case CardValue.Four:
                            case CardValue.Five:
                            case CardValue.Six:
                            case CardValue.Seven:
                            case CardValue.Eight:
                            case CardValue.Nine:
                            //add one copy of each number of the four colors
                                Cards.Add(new PlayingCards()
                                {
                                    Color = color,
                                    Value = val,
                                    Score = (int)val
                                });
                                
                                break;
                            case CardValue.Skip:
                            case CardValue.Reverse:
                            case CardValue.DrawTwo:
                                //Add one copy per color of Skip, Reverse, and Draw Two
                                Cards.Add(new PlayingCards()
                                {
                                    Color = color,
                                    Value = val,
                                    Score = 20
                                });
                                
                                break;

                            //Add a copy of zero for each color
                            case CardValue.Zero:
                                Cards.Add(new PlayingCards()
                                {
                                    Color = color,
                                    Value = val,
                                    Score = 0
                                });
                                break;
                        }
                    }
                
                
            }
        }

        //returns one card from the card deck
        public PlayingCards TakeCard()
        {
            var card = Cards.FirstOrDefault();
            Cards.Remove(card);

            return card;
        }
        //returns the specified number of cards from the card deck
        public List<PlayingCards> TakeCards(int numberOfCards)
        {
            var cards = Cards.Take(numberOfCards);

            //var takeCards = cards as Card[] ?? cards.ToArray();
            var takeCards = cards as List<PlayingCards> ?? cards.ToList();
            Cards.RemoveAll(takeCards.Contains);

            return takeCards;
        }
        #endregion }
        //Shuffles the card deck 
        public void Shuffle() {
            Random r = new Random();

            List<PlayingCards> cards = Cards;

            for (int i = cards.Count - 1; i > 0; --i)
            {
                int k = r.Next(i + 1);
                PlayingCards temp = cards[i];
                cards[i] = cards[k];
                cards[k] = temp;
            }
        }
        //Draw cards method
        public List<PlayingCards> Draw(int count) {
            var drawnCards = Cards.Take(count).ToList();

            //Remove the drawn cards from the draw pile
            Cards.RemoveAll(x => drawnCards.Contains(x));

            return drawnCards;
        }
    }
}
