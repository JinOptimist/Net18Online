using System;
using System.Threading;
using System.Diagnostics;
using System.Timers;

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

        private void BotPrintText()
        {
            var writeText = new TextProvider();
            writeText.GetText();

            Console.WriteLine("Typing Simulator ");
            Console.WriteLine("-------------------");
            Console.WriteLine("Typing text that your see:");
            Console.WriteLine(writeText);
        }

        private void GamerEnterThisText()
        {
            Console.CursorVisible = false;
            TypingIsCorrect();
        }

        private void TypingIsCorrect()
        {
            var currentIndex = 0;
            var writeText = new TextProvider().GetText();

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

        static Stopwatch stopwatch = new Stopwatch();
        static void StartTimer()
        {
            stopwatch.Start();
        }

        static void StopTimer()
        {
            stopwatch.Stop();
            ShowSpeed();
        }
        
        static void ShowSpeed()
        {
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Timer finish: {elapsed.TotalSeconds} seconds ");
        }
    }
}
