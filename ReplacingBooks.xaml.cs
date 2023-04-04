using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading; // import the threading name space to use the dispatcher time

namespace PROG7312POE_PARTONE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ReplacingBooks : Window
    {

        // This method was adapted from Moo ICT
        // https://www.youtube.com/watch?v=n4s1QJPbLog
        // Moo ICT

        List<string> SafeSpaceList = new List<string>();
        List<int> RandomNumber = new List<int>();

        // This method was adapted from WPF tutorial 
        // https://wpf-tutorial.com/misc/dispatchertimer/
        // WPF Tutorial


        Boolean Lose = false;
        DispatcherTimer gameTimer = new DispatcherTimer(); // create a new instance of the dispatcher timer called game timer

        bool goLeft, goRight, goDown, goUp; // 4 boolean created to move player in 4 direction
        bool noLeft, noRight, noDown, noUp; // 4 more boolean created to stop player moving in that direction

        int speed = 8; // player speed

        Rect LibrarianHitBox; // player hit box, this will be used to check for collision between player to walls and books

 
        int score = 0; // score keeping integer



        public ReplacingBooks()
        {
            InitializeComponent();

            // This method was adapted from stack overflow
            // https://stackoverflow.com/questions/24180084/applying-style-with-visibility-visible-to-a-hidden-control-doesnt-work
            // Sheridan
            // https://stackoverflow.com/users/249281/sheridan

            TextTen.Visibility = Visibility.Hidden;
            TextNine.Visibility = Visibility.Hidden;
            TextEight.Visibility = Visibility.Hidden;
            TextSeven.Visibility = Visibility.Hidden;
            TextSix.Visibility = Visibility.Hidden;
            TextFive.Visibility = Visibility.Hidden;
            TextFour.Visibility = Visibility.Hidden;
            TextThree.Visibility = Visibility.Hidden;
            TextTwo.Visibility = Visibility.Hidden;
            TextOne.Visibility = Visibility.Hidden;

            NumTen.Visibility = Visibility.Hidden;
            NumNine.Visibility = Visibility.Hidden;
            NumEight.Visibility = Visibility.Hidden;
            NumSeven.Visibility = Visibility.Hidden;
            NumSix.Visibility = Visibility.Hidden;
            NumFive.Visibility = Visibility.Hidden;
            NumFour.Visibility = Visibility.Hidden;
            NumThree.Visibility = Visibility.Hidden;
            NumTwo.Visibility = Visibility.Hidden;
            NumOne.Visibility = Visibility.Hidden;


            // This method was adapted from stack overflow 
            // https://stackoverflow.com/questions/33879065/generating-a-random-number-and-putting-it-in-a-textblock
            // Salah Akbari
            // https://stackoverflow.com/users/2946329/salah-akbari
            

            Random NumberGenerator = new Random();
            RandomNumber.Add(NumberGenerator.Next(1000));
            do
            {
                int CurrentRandomNumber = NumberGenerator.Next(1000);
                if (!RandomNumber.Contains(CurrentRandomNumber))
                {
                    RandomNumber.Add(CurrentRandomNumber);

                }

            } while (RandomNumber.Count < 10);

            foreach (var item in RandomNumber)
            {
                Debug.WriteLine(item);
            }


            TextEleven.Text = Text_Formatter();
            TextTwelve.Text = Text_Formatter();
            TextThirteen.Text = Text_Formatter();
            TextFourteen.Text = Text_Formatter();
            TextFifteen.Text = Text_Formatter();
            TextSixteen.Text = Text_Formatter();
            TextSeventeen.Text = Text_Formatter();
            TextEighteen.Text = Text_Formatter();
            TextNineteen.Text = Text_Formatter();
            TextTwenty.Text = Text_Formatter();

            GameSetUp(); // run the game set up function
        }



        // This method was adapted from the prescibed textbook c# data structures and alogorithms 
        // https://libraryconnect.iie.ac.za/client/en_US/iie/search/detailnonmodal/ent:$002f$002fSD_ILS$002f0$002fSD_ILS:22013/one?qu=Algorithmus.&qf=SUBJECT%09Subject%09C%23%09C%23&ps=300
        // Marcin Jamro
        

        //This function generates random decimal numbers 
        static System.Random Number_Generator = new System.Random();
        public static decimal GetRandomNumber(int minNum, int maxNum, int decimalNum)
        {
            return System.Math.Round(Convert.ToDecimal(Number_Generator.NextDouble() * (maxNum- minNum) + minNum), decimalNum);

        }


        static System.Random Alphabet_Generator = new System.Random();

        public static string GetTextFormatter()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(chars.Select(c => chars[Alphabet_Generator.Next(chars.Length)]).Take(3).ToArray());
        }





        public static string Text_Formatter()
        {

            string Number = GetRandomNumber(100, 1000, 3).ToString();
            string Alphabet = GetTextFormatter();
            string Decimal = Number + "." + Alphabet;

            return Decimal;


        }




        private string TextFormatter(String RandomNumber)
        {
            string Number = RandomNumber.ToString();
            string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random AlphabetGenerator = new Random();

            while (Number.Length < 3)
            {
                Number = "0" + Number;
            }
            Number = Number + ".";


            do
            {
                int RandNumber = AlphabetGenerator.Next(26);
                Number = Number + Alphabet.Substring(RandNumber, 1);
            } while (Number.Length < 7);

            return Number;
        }

        // This method was adapted from stack overflow
        // https://stackoverflow.com/questions/12782822/using-onkeydown-and-onpreviewkeydown-in-a-canvas
        // H.B.
        // https://stackoverflow.com/users/546730/h-b
        // Nacho 1984
        // https://stackoverflow.com/users/1703821/nacho1984

        // This method was also adapted from microsoft 
        // https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.canvas?view=windowsdesktop-6.0
        // Microsoft

        // This method was adapted from Moo ICT
        // https://www.youtube.com/watch?v=n4s1QJPbLog
        // Moo ICT
        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            // this is the key down event

            if (e.Key == Key.Left && noLeft == false)
            {
                // if the left key is down and the boolean noLeft is set to false
                goRight = goUp = goDown = false; // set rest of the direction booleans to false
                noRight = noUp = noDown = false; // set rest of the restriction boolean to false

                goLeft = true; // set go left true

                Librarian.RenderTransform = new RotateTransform(-180, Librarian.Width / 2, Librarian.Height / 2); // rotate the librarian image to face left
            }

            if (e.Key == Key.Right && noRight == false)
            {
                // if the right key pressed and no right boolean is false
                noLeft = noUp = noDown = false; // set rest of the direction boolean to false
                goLeft = goUp = goDown = false; // set rest of the restriction boolean to false

                goRight = true; // set go right to true

                Librarian.RenderTransform = new RotateTransform(0, Librarian.Width / 2, Librarian.Height / 2); // rotate the librarian image to face right

            }

            if (e.Key == Key.Up && noUp == false)
            {
                // if the up key is pressed and no up is set to false
                noRight = noDown = noLeft = false; // set rest of the direction boolean to false
                goRight = goDown = goLeft = false; // set rest of the restriction boolean to false

                goUp = true; // set go up to true

                Librarian.RenderTransform = new RotateTransform(-90, Librarian.Width / 2, Librarian.Height / 2); // rotate the librarian image to face up
            }

            if (e.Key == Key.Down && noDown == false)
            {
                // if the down key is press and the no down boolean is false
                noUp = noLeft = noRight = false; // set rest of the direction boolean to false
                goUp = goLeft = goRight = false; // set rest of the restriction boolean to false

                goDown = true; // set go down to true

                Librarian.RenderTransform = new RotateTransform(90, Librarian.Width / 2, Librarian.Height / 2); // rotate the librarian image to face down
            }
        }

        // This method was adapted from WPF Tutorial 
        // https://wpf-tutorial.com/creating-game-snakewpf/continuous-movement-with-dispatchertimer/
        // WPF Tutorial

        // This method was adapted from Moo ICT
        // https://www.youtube.com/watch?v=n4s1QJPbLog
        // Moo ICT

        private void GameSetUp()
        {
            // this function will run when the program loads

            MyCanvas.Focus(); // set my canvas as the main focus for the program

            gameTimer.Tick += GameLoop; // link the game loop event to the time tick
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); // set time to tick every 20 milliseconds
            gameTimer.Start(); // start the time
       

            // Below the librarian image is being imported from the images folder and then assigned the image brush to the rectangles
            ImageBrush librarianImage = new ImageBrush();
            librarianImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/Librarian1.png"));
            Librarian.Fill = librarianImage;

        }

        // This method was adapted from microsoft 
        // https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.canvas?view=windowsdesktop-6.0
        // Microsoft

        // This method was adapted from Moo ICT
        // https://www.youtube.com/watch?v=n4s1QJPbLog
        // Moo ICT

        private void GameLoop(object sender, EventArgs e)
        {

            // this is the game loop event, this event will control all of the movements, outcome, collision and score for the game

            txtScore.Content = "Score: " + score; // show the score to the txtscore label. 

            // start moving the character in the movement directions

            if (goRight)
            {
                // if go right boolean is true then move librarian to the right direction by adding the speed to the left 
                Canvas.SetLeft(Librarian, Canvas.GetLeft(Librarian) + speed);
            }
            if (goLeft)
            {
                // if go left boolean is then move librarian to the left direction by deducting the speed from the left
                Canvas.SetLeft(Librarian, Canvas.GetLeft(Librarian) - speed);
            }
            if (goUp)
            {
                // if go up boolean is true then deduct the speed integer from the top position of the librarian
                Canvas.SetTop(Librarian, Canvas.GetTop(Librarian) - speed);
            }
            if (goDown)
            {
                // if go down boolean is true then add speed integer value to the librarian top position
                Canvas.SetTop(Librarian, Canvas.GetTop(Librarian) + speed);
            }
            // end the movement 


            // restrict the movement
            if (goDown && Canvas.GetTop(Librarian) + 80 > Application.Current.MainWindow.Height)
            {
                // if librarian is moving down the position of librarian is grater than the main window height then stop down movement
                noDown = true;
                goDown = false;
            }
            if (goUp && Canvas.GetTop(Librarian) < 1)
            {
                // if librarian is moving and position of librarian is less than 1 then stop up movement
                noUp = true;
                goUp = false;
            }
            if (goLeft && Canvas.GetLeft(Librarian) - 10 < 1)
            {
                // if librarian is moving left andlibrarian position is less than 1 then stop moving left
                noLeft = true;
                goLeft = false;
            }
            if (goRight && Canvas.GetLeft(Librarian) + 70 > Application.Current.MainWindow.Width)
            {
                // if librarian is moving right and librarian position is greater than the main window then stop moving right
                noRight = true;
                goRight = false;
            }

            LibrarianHitBox = new Rect(Canvas.GetLeft(Librarian), Canvas.GetTop(Librarian), Librarian.Width, Librarian.Height); // asssign the librarian hit box to the librarian rectangle

            // below is the main game loop that will scan through all of the rectangles available inside of the game
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                // loop through all of the rectangles inside of the game and identify them using the x variable

                Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // create a new rect called hit box for all of the available rectangles inside of the game

                // find the walls, if any of the rectangles inside of the game has the tag wall inside of it
                if ((string)x.Tag == "wall")
                {
                    // check if we are colliding with the wall while moving left if true then stop the Librarian movement
                    if (goLeft == true && LibrarianHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(Librarian, Canvas.GetLeft(Librarian) + 10);
                        noLeft = true;
                        goLeft = false;
                    }
                    // check if we are colliding with the wall while moving right if true then stop the Librarian movement
                    if (goRight == true && LibrarianHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(Librarian, Canvas.GetLeft(Librarian) - 10);
                        noRight = true;
                        goRight = false;
                    }
                    // check if we are colliding with the wall while moving down if true then stop the Librarian movement
                    if (goDown == true && LibrarianHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(Librarian, Canvas.GetTop(Librarian) - 10);
                        noDown = true;
                        goDown = false;
                    }
                    // check if we are colliding with the wall while moving up if true then stop the Librarian movement
                    if (goUp == true && LibrarianHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(Librarian, Canvas.GetTop(Librarian) + 10);
                        noUp = true;
                        goUp = false;
                    }
                }

                // check if the any of the rectangles has a book tag inside of them

                if ((string)x.Tag == "book")
                {

                    // if librarian collides with any of the book and book is still visible to the screen
                    if (LibrarianHitBox.IntersectsWith(hitBox) && x.Visibility == Visibility.Visible)
                    {
                        //Transfer Number to safe zone
                        //Check num items in list

                        CheckName(x.Name);
                        // set the book visiblity to hidden
                        x.Visibility = Visibility.Hidden;
                        // add 1 to the score
                        score++;
                    }
                }
            }

            // if the player collected 10 books in the game

            if (score == 10)
            {


                Sorting();
                // show game over function with the "Game over" message
                GameOver("GAME OVER, Press exit to return to home page");
            }
        }

        // This method was adapted from c# corner
        // https://www.c-sharpcorner.com/Resources/705/how-to-add-a-button-click-event-handler-in-wpf.aspx
        // Soft Dev
        // https://www.c-sharpcorner.com/members/soft-dev

        // Reset game button funtionality
        private void Reset_Game(object sender, RoutedEventArgs e)
        {
            ReplacingBooks RB = new ReplacingBooks();
            this.Close();
            RB.Show();
        }



        // This method was adapted from c# corner
        // https://www.c-sharpcorner.com/Resources/705/how-to-add-a-button-click-event-handler-in-wpf.aspx
        // Soft Dev
        // https://www.c-sharpcorner.com/members/soft-dev

        // Exit game button funtionality
        private void Exit_Game(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            this.Close();
            MW.Show();
        }

        // This method was adapted from W3Schools
        // https://www.w3schools.com/cs/cs_switch.php
        // W3Schools

        private void CheckName(string name)
        {
            switch (name)
            {
                case "NumEleven":
                
                    UpdateSafeSpace(TextEleven);
                    break;

                case "NumTwelve":
                
                    UpdateSafeSpace(TextTwelve);
                    break;
                case "NumThirteen":
                  
                    UpdateSafeSpace(TextThirteen);
                    break;
                case "NumFourteen":
                  
                    UpdateSafeSpace(TextFourteen);
                    break;
                case "NumFifteen":
                  
                    UpdateSafeSpace(TextFifteen);
                    break;
                case "NumSixteen":
                 
                    UpdateSafeSpace(TextSixteen);
                    break;
                case "NumSeventeen":
                  
                    UpdateSafeSpace(TextSeventeen);
                    break;
                case "NumEighteen":
                   
                    UpdateSafeSpace(TextEighteen);
                    break;
                case "NumNineteen":
                   
                    UpdateSafeSpace(TextNineteen);
                    break;
                case "NumTwenty":
                    
                    UpdateSafeSpace(TextTwenty);
                    break;
                default:
                    break;
            }
        }

        // This method was adapted from W3Schools
        // https://www.w3schools.com/cs/cs_switch.php
        // W3Schools

        private void UpdateSafeSpace(TextBlock Text)
        {

            switch (SafeSpaceList.Count)
            {
                case 0:
                    TextTen.Text = Text.Text;
                    TextTen.Visibility = Visibility.Visible;
                    NumTen.Visibility = Visibility.Visible;
                    break;
                case 1:
                    TextNine.Text = Text.Text;
                    TextNine.Visibility = Visibility.Visible;
                    NumNine.Visibility = Visibility.Visible;
                    break;
                case 2:
                    TextEight.Text = Text.Text;
                    TextEight.Visibility = Visibility.Visible;
                    NumEight.Visibility = Visibility.Visible;
                    break;
                case 3:
                    TextSeven.Text = Text.Text;
                    TextSeven.Visibility = Visibility.Visible;
                    NumSeven.Visibility = Visibility.Visible;
                    break;
                case 4:
                    TextSix.Text = Text.Text;
                    TextSix.Visibility = Visibility.Visible;
                    NumSix.Visibility = Visibility.Visible;
                    break;
                case 5:
                    TextFive.Text = Text.Text;
                    TextFive.Visibility = Visibility.Visible;
                    NumFive.Visibility = Visibility.Visible;
                    break;
                case 6:
                    TextFour.Text = Text.Text;
                    TextFour.Visibility = Visibility.Visible;
                    NumFour.Visibility = Visibility.Visible;
                    break;
                case 7:
                    TextThree.Text = Text.Text;
                    TextThree.Visibility = Visibility.Visible;
                    NumThree.Visibility = Visibility.Visible;
                    break;
                case 8:
                    TextTwo.Text = Text.Text;
                    TextTwo.Visibility = Visibility.Visible;
                    NumTwo.Visibility = Visibility.Visible;
                    break;
                case 9:
                    TextOne.Text = Text.Text;
                    TextOne.Visibility = Visibility.Visible;
                    NumOne.Visibility = Visibility.Visible;
                    break;

                default:
                    Sorting();



                    break;

            }

            // This method was adapted from tutorials teacher
            // https://www.tutorialsteacher.com/csharp/csharp-if-else
            // Tutorials Teacher


            if (!Lose)
            {
                SafeSpaceList.Add(Text.Text);
                Text.Visibility = Visibility.Hidden;
            }

        }

        // This method was adapted from the prescibed textbook c# data structures and alogorithms 
        // https://libraryconnect.iie.ac.za/client/en_US/iie/search/detailnonmodal/ent:$002f$002fSD_ILS$002f0$002fSD_ILS:22013/one?qu=Algorithmus.&qf=SUBJECT%09Subject%09C%23%09C%23&ps=300
        // Marcin Jamro

        private void Sorting()
        {
            // Sorting Algorithm
            List<int> BaseList = new List<int>();
            foreach (var item in SafeSpaceList)
            {
                BaseList.Add(Convert.ToInt32(item.Substring(0, 3)));
            }
            List<int> SortedList = new List<int>();
            SortedList.AddRange(BaseList);
            SortedList.Sort();



            for (int i = 0; i < BaseList.Count; i++)
            {
                //Debug.WriteLine(BaseList.ElementAt(i)+ " compared to " + SortedList.ElementAt(i));
                if (BaseList.ElementAt(i) != SortedList.ElementAt(i))
                {
                    Lose = true;
                }
            }

            // This method was adapted from W3Schools
            // https://www.w3schools.com/cs/cs_conditions.php
            // W3Schools

            if (Lose)
            {

                ErrorLoseMessage("Incorrect Ascending Order: You lose, press reset game to try again");



            }
            else
            {
                WinMessage("Correct Ascending Order: You win!!!, You replaced all the books");
            }
        }

        // This method was adapted from WPF Tutorial 
        // https://wpf-tutorial.com/dialogs/the-messagebox/
        // WPF Tutorial

        private void GameOver(string message)
        {
            // inside the game over function we passing in a string to show the final message to the game
            gameTimer.Stop(); // stop the game timer
            MessageBox.Show(message, "The Dewy decimal Game : Librarian Version ", MessageBoxButton.OK, MessageBoxImage.Information); // show a mesage box with the message that is passed in this function

        }

        // This method was adapted from WPF Tutorial 
        // https://wpf-tutorial.com/dialogs/the-messagebox/
        // WPF Tutorial

        // To display the error message box
        private void ErrorLoseMessage(string ErrorLoseMessage)
        {
            MessageBox.Show(ErrorLoseMessage, "The Dewy decimal Game : Librarian Version ", MessageBoxButton.OK, MessageBoxImage.Error); // show a mesage box with the message that is passed in this function
        }

        // This method was adapted from WPF Tutorial 
        // https://wpf-tutorial.com/dialogs/the-messagebox/
        // WPF Tutorial

        // To display the win message box
        private void WinMessage(string WinMessage)
        {
            MessageBox.Show(WinMessage, "The Dewy decimal Game : Librarian Version ", MessageBoxButton.OK, MessageBoxImage.Information); // show a mesage box with the message that is passed in this function
        }
    }

}
