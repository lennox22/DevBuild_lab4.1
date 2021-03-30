using System;
using System.Collections.Generic;

namespace lab4._1DiceRolling
{
    class Program
    {
        public static int diceSides;   //making global variables to use in multiple methods
        public static int noDice;
        public static int sum = 0;

        public static List<int> rolls = new List<int>();   //global list to use in methods
        public static List<int> totals = new List<int>();  //probably unused, but wanted to keep a list of the totals as user proceeds.

        static void Main(string[] args)
        {
            bool cont = true;

            Console.Write($"Welcome to the Grand Circus Casino!\n");

            do                        //loop to roll again
            {


                Console.Write("How many sides should each die have? [2-100]: ");
                diceSides = SidesQ();                  //method to get number of dice sides

                Console.Write($"\nHow many dice would you like to roll? [1-10]: ");
                noDice = DiceAmount();                   //method to get number of dice rolled

                RollIt();                                   //method to roll dice

                PrintIt();                                    //method to print the rolls

                if (diceSides == 6)                  //for the craps game   
                {
                    GameCraps();                       //method for craps game
                }

                cont = RollAgain();                     //method to roll again verify

            } while (cont);


        }

        static bool RollAgain()                                    // method to roll again and loop
        {
            bool yn = true;
            string input;
            bool keepLoop = true;

            do
            {

                Console.Write($"\nRoll again? (y/n): ");         //message to user
                input = Console.ReadLine().ToLower();
                //input.ToLower();                                   //making input all lower case to validate easier

                if (input != "y" && input != "n")             //if user enters anything but a y or an n
                {
                    InvalidMessage(3);                    //error message method
                }
                else                                      //valid answer since user put in a y or n
                {
                    yn = false;                             //ends loop

                    if (input == "n")                      //if user enters no, return early from method to end the game
                    {
                        keepLoop = false;
                        return keepLoop;                    //false will end loop in main
                    }

                    rolls.Clear();                           //in order to roll again the list must be cleared
                    keepLoop = true;
                    return keepLoop;                       //true keeps the loop going in main
                }

            } while (yn);                              //to loop if invalid answer

            return keepLoop;                           //logically not needed. needed for the method though
        }

        static void GameCraps()           //craps game
        {
            int is6 = 0;
            int is1 = 0;
            if (sum == 2 || sum == 3 || sum == 12)       // for Craps!
            {
                Console.Write($"Craps!\n");
            }
            else if (sum == 7 || sum == 11)                 //for Win!
            {
                Console.Write($"Win!\n");
            }

            if (rolls.Contains(1) && rolls.Contains(2))       //for Ace Deuce
            {
                Console.Write($"Ace Deuce\n");
            }

            for (int i = 0; i < rolls.Count; i++)               // to count if there are 2 or more 6's or 1's
            {
                if(rolls[i] == 6)
                {
                    is6++;
                }
                else if (rolls[i] == 1)
                {
                    is1++;
                }
            }
            if(is6 > 1)                                  //boxcar
            {
                Console.Write($"Boxcars\n");
            }
            if (is1 > 1)                                // snake eyes
            {
                Console.Write($"Snake Eyes\n");
            }
        }
        static void PrintIt()                          //to print the rolls
        {
            

            Console.Write($"You rolled a ");

            if (rolls.Count == 1)                   // to print just 1 roll
            {
                Console.Write($"{rolls[0]}. ({rolls[0]} total)");
                sum = rolls[0];
            }
            else if (rolls.Count == 2)                    // to print 2 rolls
            {
                Console.Write($"{rolls[0]} and {rolls[1]}. ({rolls[0] + rolls[1]} total)");
                sum = rolls[0] + rolls[1];

            }
            else                                                   // to print more than 2 rolls
            {
                for (int i = 0; i < rolls.Count - 1; i++)
                {
                    sum = sum + rolls[i];
                    Console.Write($"{rolls[i]}, ");
                }

                sum = sum + rolls[rolls.Count - 1];                          //this bit is to get the grammer right with the oxford comma
                Console.Write($"and {rolls[rolls.Count - 1]}. ({sum} total)\n");

            }
            totals.Add(sum);                                         //adding the total to the list
        }

        static void RollIt()                                  // rolling method
        {
            int temp;
            for (int i = noDice; i > 0; i--)                  //to cycle through all the dice
            {
                Random rand = new Random();
                temp = rand.Next(1, diceSides + 1);              //to roll the die with the selected side count
                //Console.WriteLine(temp); //testing
                rolls.Add(temp);                                   // adding to the roll list
            }
        }

        static int SidesQ()                                          // finding how many sides for the die
        {
            //Console.WriteLine(diceSides);   // testing
            bool loop = true;
            do                                                     //loop to validate
            {
                bool validAnswer = false;
                string input = Console.ReadLine();
                validAnswer = ValidNumber(input);                     //method to validate a number

                if (validAnswer == true)
                {
                    diceSides = Int32.Parse(input);

                    if (diceSides < 101 && diceSides > 1)              // range for dice sides
                    {
                        loop = false;
                    }
                    else
                    {
                        loop = InvalidMessage(1);                    //method for all invalid messages

                    }

                }
                else
                {
                    loop = InvalidMessage(1);                         //method for all invalid messages

                }

            } while (loop);




            return diceSides;
        }

        static int DiceAmount()                                        //method to find number of dice
        {
            bool loop = true;
            do
            {
                bool validAnswer = false;

                string input = Console.ReadLine();
                validAnswer = ValidNumber(input);                           //calling method to validate number

                if (validAnswer == true)
                {
                    noDice = Int32.Parse(input);

                    if (noDice < 11 && noDice > 0)                            //range for number of dice
                    {
                        loop = false;
                    }
                    else
                    {
                        loop = InvalidMessage(2);                             //invalid message method

                    }

                }
                else
                {
                    loop = InvalidMessage(2);                              //invalid message method

                }
            } while (loop);
            return noDice;
        }

        static bool ValidNumber(string input)                      //valid number method
        {
            bool valid = false;
            int numTest = 0;
            try
            {
                numTest = Int32.Parse(input);               //try catch block to validate if a number was entered
                valid = true;
            }
            catch (Exception)
            {
                valid = false;                             //looping back
            }

            return valid;
        }

        static bool InvalidMessage(int option)                 //all error messages in one method
        {
            bool loopValue = true;
            switch (option)
            {
                case 1:
                    Console.Write($"\n\nThat was not a valid number of sides for dice.\nPlease enter an integer between 2 and 100: ");    //invalid dice side message

                    break;

                case 2:
                    Console.Write($"\n\nThat was not a valid number of dice.\nPlease enter an integer between 1 and 10: ");            //invalid dice number message
                    break;

                case 3:
                    Console.Write($"\n\nThat was not a valid answer. Please enter y or n.\n");
                    break;
            }


            return loopValue;
        }
    }
}
