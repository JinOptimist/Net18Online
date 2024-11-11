using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.BullsAndCows;

namespace WebPortalEverthing.Controllers
{
    public class BullsAndCowsController : Controller
    {
        private static BullsAndCowsViewModel _gameModel = new BullsAndCowsViewModel();

        [HttpGet]
        public IActionResult Index()
        {
            return View(_gameModel);
        }

        [HttpPost]
        public IActionResult SetNumberForFirstGamer(int number)
        {
            if (IsValidNumber(number))
            {
                _gameModel.NumberOfTheFirstGamer = number;
                _gameModel.Turn = "Second";
                return RedirectToAction("IndexLoadVolumeView");
            }

            return View("IndexLoadVolumeView", _gameModel);
        }

        [HttpPost]
        public IActionResult SetNumberForSecondGamer(int number)
        {
            if (IsValidNumber(number))
            {
                _gameModel.NumberOfTheSecondGamer = number;
                _gameModel.Turn = "First";
                return RedirectToAction("Guess");
            }

            return View("IndexLoadVolumeView", _gameModel);
        }

        [HttpGet]
        public IActionResult Guess()
        {
            return View(_gameModel);
        }

        [HttpPost]
        public IActionResult MakeAttempt(int attempt, string player)
        {
            string result;
            if (player == "first")
            {
                result = CountOfBullsAndCows(attempt, _gameModel.NumberOfTheSecondGamer);
                if (!CheckWinCondition(attempt, _gameModel.NumberOfTheSecondGamer))
                {
                    _gameModel.Turn = "Second";
                }
                else
                {
                    _gameModel.IsFirstGamerWin = true;
                    _gameModel.Turn = "LastAttempt";
                }
            }
            else
            {
                result = CountOfBullsAndCows(attempt, _gameModel.NumberOfTheFirstGamer);
                if (CheckWinCondition(attempt, _gameModel.NumberOfTheFirstGamer))
                {
                    _gameModel.IsSecondGamerWin = true;
                }
                _gameModel.Turn = "First";
            }

            _gameModel.Attempts.Add($"Player: {player}, Guess: {attempt}, Result: {result}");

            if ((_gameModel.IsFirstGamerWin || _gameModel.IsSecondGamerWin) && _gameModel.Turn != "LastAttempt")
            {
                return RedirectToAction("ShowResult");
            }

            return RedirectToAction("Guess");
        }

        public IActionResult ShowResult()
        {
            if (_gameModel.IsFirstGamerWin && _gameModel.IsSecondGamerWin)
            {
                ViewBag.ResultMessage = "Ничья!";
            }
            else if (_gameModel.IsFirstGamerWin)
            {
                ViewBag.ResultMessage = "Поздравляем первого игрока с победой!";
            }
            else
            {
                ViewBag.ResultMessage = "Поздравляем второго игрока с победой!";
            }
            return View(_gameModel);
        }

        private bool IsValidNumber(int number)
        {
            return number.ToString().Length == _gameModel.LengthOfNumber;
        }

        private string CountOfBullsAndCows(int attempt, int targetNumber)
        {
            var attemptStr = attempt.ToString();
            var targetStr = targetNumber.ToString();

            int bulls = attemptStr
                .Where((digit, index) => digit == targetStr[index]).Count();
            int cows = attemptStr
                .Where((digit, index) => digit != targetStr[index] && targetStr.Contains(digit)).Count();

            string bullIcon = "<img src='/images/BullsAndCows/bull.png' alt='Bull'>";
            string cowIcon = "<img src='/images/BullsAndCows/cow.png' alt='Cow'>";

            return $"{bulls}{bullIcon} {cows}{cowIcon}";
        }

        private bool CheckWinCondition(int attempt, int targetNumber)
        {
            return attempt == targetNumber;
        }
    }
}
