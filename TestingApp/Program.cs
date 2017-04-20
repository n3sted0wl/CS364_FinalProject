using System;
using System.Collections.Generic;
using YahtzeeWithATwist.Classes;

namespace TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list of dice
            List<Dice> scoreableDice = new List<Dice>();

            Console.WriteLine($"Aces:        {Yahtzee.calculateAces(scoreableDice)}");
            Console.WriteLine($"             {Yahtzee.calculateBonus(scoreableDice, Yahtzee.calculateAces(scoreableDice))}");
        }
    }
}