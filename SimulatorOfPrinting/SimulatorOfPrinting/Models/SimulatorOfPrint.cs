using System;
using System.Threading;

using SimulatorOfPrinting.Models;

namespace SimulatorOfPrinting.Models
{
    public class SimulatorOfPrint
    {
        public void Start() 
        {
            BotPrintText();
            StartTimer();
            GamerEnterThisText();
            StopTimer();
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
            Console.CursorVisible = false;
            TypingIsCorrect;
        }

        private void TypingIsCorrect()
        {
            var currentIndex = 0;
            while (currentIndex < writeText.Length)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.KeyChar == writeText[currentIndex])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(keyInfo.KeyChar);
                    Console.ResetColor();
                    currentIndex++;
                }
                else
                {
                    HighlightError(writeText, currentIndex);
                }
            }
        }
        private void HighlightError(string writeText, int currentIndex)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(writeText[currentIndex]);
            Thread.Sleep(500);
            Console.ResetColor();
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }

        private void ShowSpeed()
        {
            Console.WriteLine($"Timer finish and number of second: {stopwatch.Elapsed.TotalSeconds}");
        }

        static Stopwatch stopwatch = new Stopwatch();
        static Timer timer = new Timer(1000);
        static void StartTimer()
        {
            stopwatch.Start();
            timer.Start();
        }

        static void StopTimer()
        {
            stopwatch.Stop();
            timer.Stop();
            ShowSpeed();
        }
    }
}
