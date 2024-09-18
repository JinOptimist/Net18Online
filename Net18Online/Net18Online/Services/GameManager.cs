using Net18Online.Models;
using Net18Online.Models.Abstractions;

namespace Net18Online.Services
{
    public class GameManager
    {
        private IChoosable _riddler;
        private IChoosable _guesser;
        private GameSetting _gameSetting;
        private int _attemptsNumber;
        private bool _isHintMode;

        public GameManager(IChoosable riddler, IChoosable guesser, GameSetting gameSetting)
        {
            _riddler = riddler;
            _guesser = guesser;
            _gameSetting = gameSetting;
        }

        public void Start()
        {
            Initialize();
            
            var riddlersNumber = _riddler.ChooseNumber();
            var gameResult = GuessTheNumber(riddlersNumber);

            ShowResult(gameResult);
        }

        private void Initialize()
        {
            _attemptsNumber = 0;

            ConsoleWriterUtil.PrintConsoleInfo("Do you want to enable hints? (Y/N)");
            _isHintMode = Console.ReadLine()?.ToLower() == "y";
        }

        private bool GuessTheNumber(int riddlersNumber)
        {
            while (_attemptsNumber < _gameSetting.GuessAttempts)
            {
                Console.WriteLine();
                ConsoleWriterUtil.PrintConsoleInfo($"Player guesses the number, attempts left: {_gameSetting.GuessAttempts - _attemptsNumber}");
                _attemptsNumber++;

                var guessNumber = _guesser.ChooseNumber();
                if (guessNumber == riddlersNumber)
                    return true;

                CheckHint(guessNumber, riddlersNumber);

            }
            return false;
        }

        private void CheckHint(int guessNumber, int riddlersNumber)
        {
            if (!_isHintMode || _attemptsNumber == _gameSetting.GuessAttempts) 
                return;

            var hintText = (guessNumber < riddlersNumber) ?
                "The entered number is less than the desired one" :
                "The entered number is greater than the desired one";
            ConsoleWriterUtil.PrintConsoleHint(hintText);
        }

        private void ShowResult(bool gameResult)
        {
            if (gameResult)
                ConsoleWriterUtil.PrintConsoleWin($"The number is guessed in {_attemptsNumber} attempts");
            else
                ConsoleWriterUtil.PrintConsoleLoss("The number was not guessed");
            Console.WriteLine();
        }
    }
}
