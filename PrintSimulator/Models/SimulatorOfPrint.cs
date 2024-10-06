using System;

using Net18Online.Models;

namespace Net18Online.PrintSimulator.Models
{
    public class SimulatorOfPrint
    {
        public void Start() 
        {
            BotPrintText();
            GamerEnterThisText();
            //ShowSpeed();
            //ShowHowMuchFailsHaveGamer();
        }

        public void BotPrintText();
        {
            var writeText = new TextProvider();
            writeText.GetText();

            Console.WriteLine("Typing Simulator ");
            Console.WriteLine("-------------------");
            Console.WriteLine("Typing text that your see:");
            Console.WriteLine(writeText);
        }
        public void GamerEnterThisText()
        {
            var userInput = Console.ReadLine();
        }

        public void TypingIsCorrect()
        {
            if (userInput == writeText)
            {
                Console.WriteLine("Succesful!");
            }
            else
            {
                Console.WriteLine("You False");
                Console.WriteLine("Correct Text: ");
                Console.WriteLine(writeText);
            }
        }



    }
}
