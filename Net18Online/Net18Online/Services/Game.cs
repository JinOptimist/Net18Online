using Net18Online.Models;
using Net18Online.Models.Abstractions;

namespace Net18Online.Services
{
    public class Game
    {
        private Player _riddler;
        private Player _guesser;
        private GameSetting _gameSetting;
        private INotifier _notifier;
        private int _attemptsNumber;
        private int _minNumber;
        private int _maxNumber;
        private int _riddledNumer;

        public Game(Player riddler, Player guesser, GameSetting gameSetting, INotifier notifier)
        {
            _riddler = riddler;
            _guesser = guesser;
            _gameSetting = gameSetting;
            _notifier = notifier;
        }

        public void Play()
        {
            Initialize();

            var isWin = PlayRound();

            ShowResult(isWin);
        }

        private void Initialize()
        {
            _attemptsNumber = 0;
            _minNumber = _gameSetting.MinNumber;
            _maxNumber = _gameSetting.MaxNumber;
            _guesser.UpdateRange(_minNumber, _maxNumber);

            _notifier.Compliment($"{Environment.NewLine}A new game has started!");
        }

        private bool PlayRound()
        {
            _riddledNumer = _riddler.ThinkANumber();
            _notifier.Inform($"The player '{_riddler.Name}' made a number!");
            
            var isWin = GuessTheNumber();
            
            return isWin;
        }

        private bool GuessTheNumber()
        {
            while (_attemptsNumber < _gameSetting.GuessAttempts)
            {
                _notifier.Inform($"Attempts left: {_gameSetting.GuessAttempts - _attemptsNumber}");
                _attemptsNumber++;

                _notifier.Inform($"Select a number in the range from {_minNumber} to {_maxNumber}:");
                var guessNumber = _guesser.GuessANumber();
                if (guessNumber == _riddledNumer)
                    return true;

                SupportUserGuess(guessNumber);
            }
            return false;
        }

        private void SupportUserGuess(int guessNumber)
        {
            if (_attemptsNumber == _gameSetting.GuessAttempts)
                return;
            CheckHint(guessNumber);
            UpdateRanges(guessNumber);
        }

        private void CheckHint(int guessNumber)
        {
            if (!_gameSetting.IsEnableHints)
                return;

            var hintText = (guessNumber < _riddledNumer) ? "less" : "greater";
            _notifier.Assist($"The entered number '{guessNumber}' is {hintText} than the desired one");
        }

        private void UpdateRanges(int guessNumber)
        {
            if (!_gameSetting.IsUpdateRanges)
                return;
            if (guessNumber > _riddledNumer)
                _maxNumber = guessNumber;
            else
                _minNumber = guessNumber;
            _guesser.UpdateRange(_minNumber, _maxNumber);
        }

        private void ShowResult(bool isWin)
        {
            if (isWin)
                _notifier.Compliment($"The number is guessed in {_attemptsNumber} attempts");
            else
                _notifier.Critical("The number was not guessed");
        }
    }
}
