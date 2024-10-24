using MazeCore.Builders;
using MazeCore.Models;
using MazeCore.Models.Cells;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Maze;

namespace WebPortalEverthing.Controllers
{
    public class MazeController : Controller
    {
        private MazeBuilder _mazeBuilder;

        public MazeController(MazeBuilder mazeBuilder)
        {
            _mazeBuilder = mazeBuilder;
        }

        public IActionResult Index()
        {
            var mazeBusinessModel = _mazeBuilder.BuildEmptyMaze();

            var mazeViewModel = Map(mazeBusinessModel);

            return View(mazeViewModel);
        }

        /// <summary>
        /// Just show a page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CustomMaze()
        {
            return View();
        }

        /// <summary>
        /// Generate maze base on my data
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CustomMaze(int width, int height)
        {
            var mazeBusinessModel = _mazeBuilder.BuildEmptyMaze(width, height);

            var mazeViewModel = Map(mazeBusinessModel);

            return View(mazeViewModel);
        }

        private MazeViewModel Map(Maze mazeBusinessModel)
        {
            var mazeViewModel = new MazeViewModel();

            for (int y = 0; y < mazeBusinessModel.Height; y++)
            {
                var rowViewModel = new RowViewModel();
                for (int x = 0; x < mazeBusinessModel.Width; x++)
                {
                    var cell = mazeBusinessModel[x, y];
                    var url = CalculateUrlByCell(cell);
                    rowViewModel.CellsUrls.Add(url);
                }
                mazeViewModel.Rows.Add(rowViewModel);
            }

            return mazeViewModel;
        }

        private string CalculateUrlByCell(IBaseCell cell)
        {
            if (cell is Wall)
            {
                return "/images/maze/wall.png";
            }

            if (cell is Ground)
            {
                return "/images/maze/road.png";
            }

            throw new NotImplementedException();
        }
    }
}
