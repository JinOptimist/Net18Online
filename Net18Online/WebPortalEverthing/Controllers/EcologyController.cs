using Everything.Data;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Everything.Data.Repositories;
using WebPortalEverthing.Models.Ecology;
using Everything.Data.Models;
using WebPortalEverthing.Services;


namespace WebPortalEverthing.Controllers
{
    
public class EcologyController : Controller
{ 
    private IEcologyRepositoryReal _ecologyRepository;
    private IUserRepositryReal _userRepositryReal;
    private WebDbContext _webDbContext;
    private ICommentRepositoryReal _commentRepositoryReal;
    private AuthService _authService;

    public EcologyController(IEcologyRepositoryReal ecologyRepository, 
        ICommentRepositoryReal commentRepositoryReal,
        IUserRepositryReal userRepositryReal,
        AuthService authService,
        WebDbContext webDbContext)
    {
        _ecologyRepository = ecologyRepository;
        _commentRepositoryReal = commentRepositoryReal;
        _webDbContext = webDbContext;
        _userRepositryReal = userRepositryReal;
        _authService = authService;
    }

    public IActionResult Index()
    {
        var model = new EcologyViewModel();
        return View(model);
    }

    [HttpGet]
    public IActionResult EcologyChat()
    {
        var id = _authService.GetUserId();
        if (id is null)
        {
            return RedirectToAction("Index", "Home");
        }

        var user = _userRepositryReal.Get(id.Value);
        if (user.Coins < 150)
        {
            return RedirectToAction("Index");
        }

        var ecologyFromDb = _ecologyRepository.GetAll();

        var ecologyViewModels = ecologyFromDb
            .Select(dbEcology =>
                new EcologyViewModel
                {
                    PostId = dbEcology.Id,
                    ImageSrc = dbEcology.ImageSrc,
                    Texts = dbEcology.Text
                }
            )
            .ToList();
        return View(ecologyViewModels);
    }

    [HttpPost]
    public IActionResult EcologyChat(PostCreationViewModel viewModel)
    {
        if (_ecologyRepository.IsEclogyTextHas(viewModel.Text))
        {
            ModelState.AddModelError(
                nameof(PostCreationViewModel.Text),
                "so similar texts");
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        var ecology = new EcologyData
        {
            ImageSrc = viewModel.Url,
            Text = viewModel.Text
        };
        _ecologyRepository.Add(ecology);
            
        return RedirectToAction("EcologyChat");
    }
        
    [HttpPost]
    public IActionResult UpdatePost(int id, string url, string text)
    {
        _ecologyRepository.UpdatePost(id, url, text);
        return RedirectToAction("EcologyChat");
    }

    [HttpPost]
    public IActionResult Remove(int id)
    {
        _ecologyRepository.Delete(id);
        return RedirectToAction("EcologyChat");
    }
    
    /*[HttpPost]
    public IActionResult AddComment(int postId, string commentTect, string userId)
    {
       //var userId
        if (ModelState.IsValid && userId != null)
        {
            var comment = new CommentViewModel()
            {
                PostId = postId,
                CommentText = commentText,
                UserId = userId.Value
            };
            _commentRepositoryReal.Add(comment);
            return RedirectToAction("EcologyChat");
        }
        return BadRequest("Invalid comment data.");
    }

    [HttpGet]
    public async Task<IActionResult> CommentsForPost(int postId)
    {
        var comments = await _commentRepositoryReal.Comments
            .Where(c => c.PostId == postId)
            .Include(c => c.User)
            .ToListAsync();
        return View(comments as string);
    }*/

    [HttpPost]
    public IActionResult AddComment(int postId, string commentText)
    {
        //var userId
        if (!ModelState.IsValid) return BadRequest("Invalid comment data.");
        var comment = new CommentData()
        {
            PostId = postId, 
            CommentText = commentText
        }; 
        _commentRepositoryReal.Add(comment); 
        return RedirectToAction("EcologyChat");
    }

    [HttpGet]
    public IActionResult CommentsForPost(int postId)
    {
        var comm = _commentRepositoryReal.GetAll();

        var commentsFiltered = comm
            .Where(c => c.PostId == postId).ToList();
        return View(commentsFiltered);
    }
}
}