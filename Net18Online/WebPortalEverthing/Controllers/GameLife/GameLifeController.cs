using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting.GameLife;
using Everything.Data.Interface.Repositories;
using Everything.Data.Interface.Models;
using LifeGame.Model;
using WebPortalEverthing.Models.LoadTesting;

namespace WebPortalEverthing.Controllers.GameLife
{
    public class GameLifeController : Controller
    {
        private IGameLifeRepository _gameLifeRepository;
        static int defWidth = 6;
        static int defHigth = 7;
        IFieldData field = new FieldData(defWidth, defHigth);

        public GameLifeController(IGameLifeRepository gameLifeRepository)
        {
            _gameLifeRepository = gameLifeRepository;
            field = new FieldData(defWidth, defHigth);
            _gameLifeRepository.Add(field);
        }

        public IActionResult GameLifeDefault()
        {
            field.Randomize();

            var rows = field.Rows;
            var cols = field.Cols;

            var fieldViewModel = new FieldViewModel();
            fieldViewModel.Cols = cols;
            fieldViewModel.Rows = rows;

            var cellViewModels = new CellViewModel[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    cellViewModels[i, j] = new CellViewModel
                    {
                        IsAlive = field.Cells[i, j].IsAlive
                    };
                }
            }

            fieldViewModel.Cells = cellViewModels;

            return View(fieldViewModel);
        }

        [HttpGet]
        public IActionResult GameLifeOwnSize()
        {
            // Возвращаем пустую модель для инициализации представления
            var emptyModel = new FieldViewModel
            {
                Rows = 0,
                Cols = 0,
                Cells = new CellViewModel[0, 0] // Пустой массив ячеек
            };

            return View(emptyModel);
        }

        [HttpPost]
        public IActionResult GameLifeOwnSize(int width, int height)
        {
            // Создаем новое поле с заданными размерами
            field = new FieldData(width, height);
            field.Randomize();

            var rows = field.Rows;
            var cols = field.Cols;

            // Создаем ViewModel для поля
            var fieldViewModel = new FieldViewModel
            {
                Cols = cols,
                Rows = rows
            };

            var cellViewModels = new CellViewModel[rows, cols];

            // Заполняем ViewModel ячейками с текущими состояниями
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    cellViewModels[i, j] = new CellViewModel
                    {
                        IsAlive = field.Cells[i, j].IsAlive
                    };
                }
            }

            fieldViewModel.Cells = cellViewModels;

            return View(fieldViewModel); // Возвращаем представление с моделью
        }
    }
}