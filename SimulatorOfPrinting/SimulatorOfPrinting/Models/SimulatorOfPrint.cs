using System;
using System.Threading;
using System.Diagnostics;
using System.Timers;

using SimulatorOfPrinting.Models;

namespace SimulatorOfPrinting.Models
{
    public class SimulatorOfPrint
    {
        private string _currentText;
        public void Start() 
        {
            var textProvider = new TextProvider();
            _currentText = textProvider.GetText();
            BotPrintText();
            StartTimer();
            GamerEnterThisText();
            StopTimer();
        }

        private void BotPrintText()
        {
            Console.WriteLine("Typing Simulator ");
            Console.WriteLine("-------------------");
            Console.WriteLine("Typing text that your see:");
            Console.WriteLine(_currentText);
        }
        private void GamerEnterThisText()
        {
            Console.CursorVisible = true;
            TypingIsCorrect();
        }

        private void TypingIsCorrect()
        {
            var currentIndex = 0;

            while (currentIndex < _currentText.Length)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.KeyChar == _currentText[currentIndex])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(keyInfo.KeyChar);
                    Console.ResetColor();
                    currentIndex++;
                }
                else
                {
                    HighlightError(currentIndex);
                }
            }
        }
        private void HighlightError(int currentIndex)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(_currentText[currentIndex]);
            Thread.Sleep(100);
            Console.ResetColor();
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }

        static void ShowSpeed()
        {
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Timer finish: {elapsed.TotalSeconds} seconds ");
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
    }
}