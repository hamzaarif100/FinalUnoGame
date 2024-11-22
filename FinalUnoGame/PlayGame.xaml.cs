using FinalUnoGame.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FinalUnoGame
{
    /// <summary>
    /// Interaction logic for PlayGame.xaml
    /// </summary>
    public partial class PlayGame : Window
    {
        //Declare Playing Cards Lists to store deck of cards for player and AI
        List<PlayingCards> playerdeck;
        List<PlayingCards> playerPhysicalDeck;
        List<PlayingCards> computerDeck;
        List<PlayingCards> computerPhysicalDeck;
        //List to generate a discard Pile card and new Cards
        List<PlayingCards> discardPile;
        List<PlayingCards> newCard;

        bool cardHasBeenDragged = false;
        bool hasMatchingCard = true;
        bool playerTurn = true;
        bool computerHasMatchingCard = true;

        int cardNumPlayed;

        public PlayGame()
        {
            InitializeComponent();

            //disable all cards until that getCards button is clicked
            card1.IsEnabled = false;
            card2.IsEnabled = false;
            card3.IsEnabled = false;
            card4.IsEnabled = false;
            card5.IsEnabled = false;
            card6.IsEnabled = false;
            card7.IsEnabled = false;
            card8.IsEnabled = false;
            card9.IsEnabled = false;
            card10.IsEnabled = false;

            bGenerateNewCard.IsEnabled = false;
        }

        private void bGetCards_Click(object sender, RoutedEventArgs e)
        {

      
            //Enable all cards
            card1.IsEnabled = true;
            card2.IsEnabled = true;
            card3.IsEnabled = true;
            card4.IsEnabled = true;
            card5.IsEnabled = true;
            card6.IsEnabled = true;
            card7.IsEnabled = true;
            card8.IsEnabled = true;
            card9.IsEnabled = true;
            card10.IsEnabled = true;

            //Instantiate deck object of Card deck class
            var deck = new CardDeck();
            //Shuffles the deck
            deck.Shuffle();

            //adds 10 cards to player deck
            playerdeck = deck.TakeCards(10);
            //makes another copy of player deck
            playerPhysicalDeck = new List<PlayingCards>(playerdeck);

            //adds 10 cards to AI deck
            computerDeck = deck.TakeCards(10);
            //makes another copy of AI deck
            computerPhysicalDeck = new List<PlayingCards>(computerDeck);

            //add 1 card to discard pile to start of the game
            discardPile = deck.TakeCards(1);

            //Gets the image for each player card in player deck list
            foreach (PlayingCards card in playerdeck)
            {
                card.cardImage = card.GetCardImagePath(card.Color, card.Value, false);

            }
            //Gets the image for each AI card in computer deck list
            foreach (PlayingCards card in computerDeck)
            {
                card.cardImage = card.GetCardImagePath(card.Color, card.Value, true);

            }
            //Gets the image for the card in discard pile deck
            foreach (PlayingCards card in discardPile)
            {
                card.cardImage = card.GetCardImagePath(card.Color, card.Value, false);
            }
            
            cardPlayed.Source = discardPile[0].cardImage;

                //Sets the card image source for each card in player hands to the card image of that particular card
                card1.Source = playerdeck[0].cardImage;
                card2.Source = playerdeck[1].cardImage;
                card3.Source = playerdeck[2].cardImage;
                card4.Source = playerdeck[3].cardImage;
                card5.Source = playerdeck[4].cardImage;
                card6.Source = playerdeck[5].cardImage;
                card7.Source = playerdeck[6].cardImage;
                card8.Source = playerdeck[7].cardImage;
                card9.Source = playerdeck[8].cardImage;
                card10.Source = playerdeck[9].cardImage;

                //Sets the card image source for each card in computers hands to the card image of that particular card
                aicard1.Source = computerDeck[0].cardImage;
                aicard2.Source = computerDeck[1].cardImage;
                aicard3.Source = computerDeck[2].cardImage;
                aicard4.Source = computerDeck[3].cardImage;
                aicard5.Source = computerDeck[4].cardImage;
                aicard6.Source = computerDeck[5].cardImage;
                aicard7.Source = computerDeck[6].cardImage;
                aicard8.Source = computerDeck[7].cardImage;
                aicard9.Source = computerDeck[8].cardImage;
                aicard10.Source = computerDeck[9].cardImage;

            //Disable the start game button
            bGetCards.IsEnabled = false;
        }

        //Method that is called when it is computer turn
        private void computerTurn()
        {
            textBox.Text = "Computer Turn";
            int index = 0;
            //Set a time that executes every 0.75 seconds giving the perception that the computer player is thinking
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.75) };
            timer.Start();
            timer.Tick += (senders, args) =>
            {
                //Loop through all cards in computer deck
                foreach (PlayingCards card in computerDeck)
                {
                    //if the card being looped through matches the card in the middle by either color or value, then set computerHasMatching card to true and find index
                    //of the following card
                    if (discardPile[0].Color == card.Color || discardPile[0].Value == card.Value)
                    {
                        index = computerDeck.FindIndex(a => a.Color == discardPile[0].Color || a.Value == discardPile[0].Value);
                        computerHasMatchingCard = true;


                        break;
                    }
                    //else set computerHasMatchingCard to false
                    computerHasMatchingCard = false;
                }
                //If computer has Matching card is true
                if (computerHasMatchingCard)
                {
                        //insert the card that matched the discard pile into the discard pile at 0
                        discardPile.Insert(0, computerDeck[index]);
                        //Removes the card that was just played from the computer deck 
                        computerDeck.RemoveAll(p => p.Color == discardPile[0].Color && p.Value == discardPile[0].Value);
                        
                        discardPile[0].cardImage = discardPile[0].GetCardImagePath(discardPile[0].Color, discardPile[0].Value, false);
                        cardPlayed.Source = discardPile[0].cardImage;

                        //for loop that runs through each computer deck card and checks if it matches the discard pile card, if so replaces the computer deck card image with a
                        //blank image
                        for (int i = 0; i < 10; i++)
                        {
                            if (computerPhysicalDeck[i].cardImage == discardPile[0].cardImage)
                            {
                                computerPhysicalDeck[i].cardImage = Helper.GetImage("./images/cards/blank.PNG");
                            }

                            aicard1.Source = computerPhysicalDeck[0].cardImage;
                            aicard2.Source = computerPhysicalDeck[1].cardImage;
                            aicard3.Source = computerPhysicalDeck[2].cardImage;
                            aicard4.Source = computerPhysicalDeck[3].cardImage;
                            aicard5.Source = computerPhysicalDeck[4].cardImage;
                            aicard6.Source = computerPhysicalDeck[5].cardImage;
                            aicard7.Source = computerPhysicalDeck[6].cardImage;
                            aicard8.Source = computerPhysicalDeck[7].cardImage;
                            aicard9.Source = computerPhysicalDeck[8].cardImage;
                            aicard10.Source = computerPhysicalDeck[9].cardImage;
                        }
                        //If computer deck is empty, then computer wins and game is over.
                        if (computerDeck.Count == 0)
                        {
                            MessageBox.Show("Computer Wins");
                            this.Close();
                        
                    }
                }
                else //Computer has no matching card
                {
                    MessageBox.Show("computer has no matching card so it generates new card.");
                    
                    var deck = new CardDeck();

                    deck.Shuffle();

                    //add 1 card to new Card list
                    newCard = deck.TakeCards(1);

                    //Insert the new card just generated into discardPile at the location 0
                    discardPile.Insert(0, newCard[0]);
                    
                    newCard[0].cardImage = newCard[0].GetCardImagePath(newCard[0].Color, newCard[0].Value, false);
                    cardPlayed.Source = discardPile[0].cardImage;

                    //check if any card from the computer deck matches the new card that was just generated
                    foreach (PlayingCards card in computerDeck)
                    {
                        //if the card being looped through matches the card in the middle by either color or value, then set computerHasMatching card to true and find index
                        //of the following card
                        if (discardPile[0].Color == card.Color || discardPile[0].Value == card.Value)
                        {
                            index = computerDeck.FindIndex(a => a.Color == discardPile[0].Color || a.Value == discardPile[0].Value);
                            
                            computerHasMatchingCard = true;


                            break;
                        }
                        computerHasMatchingCard = false;
                    }
                    //calls computer turn method again if computer now has a matching card
                    if (computerHasMatchingCard)
                    {
                        
                        computerTurn();
                    }
                }

                //Computer turn ends, so we set playerTurn to true
                playerTurn = true;
                textBox.Text = "Your Turn";
                //Stops the timer
                timer.Stop();
                //enable generate card if player has no playable card
                foreach (PlayingCards card in playerPhysicalDeck)
                {
                    if (discardPile[0].Color == card.Color || discardPile[0].Value == card.Value)
                    {
                        hasMatchingCard = true;


                        break;
                    }
                    hasMatchingCard = false;
                }
                if (hasMatchingCard == false)
                {
                    bGenerateNewCard.IsEnabled = true;

                }
                else
                {
                    bGenerateNewCard.IsEnabled = false;
                }
            };
        }
        private void cardPlayed_Drop(object sender, DragEventArgs e)
        {
            //If playerTurn is true
            if (playerTurn)
            {
                //if the card that was just played by the player matches the card in the middle by either color or value
                if (discardPile[0].Color.Equals(playerdeck[cardNumPlayed].Color) || discardPile[0].Value == playerdeck[cardNumPlayed].Value)
                {
                    Image img = e.Source as Image;
                    img.Source = (BitmapSource)e.Data.GetData(DataFormats.Text);
                    cardHasBeenDragged = true;
                    //Insert the card played into the discard pile
                    discardPile.Insert(0, playerdeck[cardNumPlayed]);
                    //Remove that was just played from the playerPhysicalDeck 
                    playerPhysicalDeck.RemoveAll(p => p.Color == discardPile[0].Color && p.Value == discardPile[0].Value);
                    //Set player turn to false
                    playerTurn = false;
                }
            }
            //If player turn is over, check if the playerDeck is equal to 0, if true player wins, if not then call the computer turn method
            if (playerTurn == false)
            {
                if (playerPhysicalDeck.Count == 0)
                {
                    MessageBox.Show("You Win");
                    this.Close();
                }
                computerTurn();
            }
        }
        //Runs when Generate new card button is clicked
        private void bGenerateNewCard_Click(object sender, RoutedEventArgs e)
        {
            var deck = new CardDeck();

            deck.Shuffle();

            //add a card to newCard list
            newCard = deck.TakeCards(1);

            //Insert that new card just generated into discardPile
            discardPile.Insert(0, newCard[0]);
            newCard[0].cardImage = newCard[0].GetCardImagePath(newCard[0].Color, newCard[0].Value, false);
            cardPlayed.Source = discardPile[0].cardImage;

            //disable the GenerateNewCard Button
            bGenerateNewCard.IsEnabled = false;
            //check if any card from the player deck matches the new card that was just generated
            foreach (PlayingCards card in playerPhysicalDeck)
            {
                //If it does then set hasMatchingCard to true, else set it to false
                if (discardPile[0].Color == card.Color || discardPile[0].Value == card.Value)
                {
                    hasMatchingCard = true;
                    break;
                }
                hasMatchingCard = false;
            }
            //if player doesn't have a matching card then playerTurn is false, else Player turn is true
            if (hasMatchingCard == false)
            {
                playerTurn = false;
                computerTurn();

            }
            else
            {
                playerTurn = true;
                textBox.Text = "Your Turn";
            }
        }
        //mouse left button down methods for the 10 player cards
        private void card1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cardNumPlayed = 0;
            Image img = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text, img.Source);
            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            if (cardHasBeenDragged)
            {
                card1.Source = Helper.GetImage("./images/cards/blank.PNG");
                card1.IsEnabled = false;
            }
            cardHasBeenDragged = false;
        }

        private void card2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cardNumPlayed = 1;
            Image img = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text, img.Source);

            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            if (cardHasBeenDragged)
            {
                card2.Source = Helper.GetImage("./images/cards/blank.PNG");
                card2.IsEnabled = false;
            }
            cardHasBeenDragged = false;
        }

        private void card3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cardNumPlayed = 2;
            Image img = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text, img.Source);

            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            if (cardHasBeenDragged)
            {
                card3.Source = Helper.GetImage("./images/cards/blank.PNG");
                card3.IsEnabled = false;
            }
            cardHasBeenDragged = false;
        }

        private void card4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cardNumPlayed = 3;
            Image img = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text, img.Source);

            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            if (cardHasBeenDragged)
            {
                card4.Source = Helper.GetImage("./images/cards/blank.PNG");
                card4.IsEnabled = false;
            }
            cardHasBeenDragged = false;




        }

        private void card5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cardNumPlayed = 4;
            Image img = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text, img.Source);

            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            if (cardHasBeenDragged)
            {
                card5.Source = Helper.GetImage("./images/cards/blank.PNG");
                card5.IsEnabled = false;
            }
            cardHasBeenDragged = false;





        }

        private void card6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cardNumPlayed = 5;
            Image img = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text, img.Source);

            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            if (cardHasBeenDragged)
            {
                card6.Source = Helper.GetImage("./images/cards/blank.PNG");
                card6.IsEnabled = false;
            }
            cardHasBeenDragged = false;



        }

        

        private void card7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cardNumPlayed = 6;
            Image img = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text, img.Source);

            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            if (cardHasBeenDragged)
            {
                card7.Source = Helper.GetImage("./images/cards/blank.PNG");
                card7.IsEnabled = false;
            }
            cardHasBeenDragged = false;
        }

        private void card8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cardNumPlayed = 7;
            Image img = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text, img.Source);

            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            if (cardHasBeenDragged)
            {
                card8.Source = Helper.GetImage("./images/cards/blank.PNG");
                card8.IsEnabled = false;
            }
            cardHasBeenDragged = false;
        }

        private void card9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cardNumPlayed = 8;
            Image img = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text, img.Source);

            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            if (cardHasBeenDragged)
            {
                card9.Source = Helper.GetImage("./images/cards/blank.PNG");
                card9.IsEnabled = false;
            }
            cardHasBeenDragged = false;
        }

        private void card10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cardNumPlayed = 9;
            Image img = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text, img.Source);

            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            if (cardHasBeenDragged)
            {
                card10.Source = Helper.GetImage("./images/cards/blank.PNG");
                card10.IsEnabled = false;
            }
            cardHasBeenDragged = false;
        }
    }
}