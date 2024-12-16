using System.Security.Claims;
using Everything.Data;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Everything.Data.Repositories;
using WebPortalEverthing.Models.Ecology;
using Everything.Data.Models;
using Microsoft.AspNetCore.SignalR;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Hubs;
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
    private IWebHostEnvironment _webHostEnvironment;
    public IHubContext<ChatHub, IChatHub> _chatHub;
    
    public EcologyController(IEcologyRepositoryReal ecologyRepository, 
        ICommentRepositoryReal commentRepositoryReal,
        IUserRepositryReal userRepositryReal,
        AuthService authService,
        WebDbContext webDbContext,
        IWebHostEnvironment webHostEnvironment,
        IHubContext<ChatHub, IChatHub> chatHub)
    {
        _ecologyRepository = ecologyRepository;
        _commentRepositoryReal = commentRepositoryReal;
        _webDbContext = webDbContext;
        _userRepositryReal = userRepositryReal;
        _authService = authService;
        _webHostEnvironment = webHostEnvironment;
        _chatHub = chatHub;
    }

    public IActionResult Index()
    {
        var mainPagePosts = _ecologyRepository.GetAllWithUsersAndComments() 
            .Where(p => p.ForMainPage == 1) 
            .Select(post => new EcologyViewModel 
            { 
                PostId = post.Id, 
                ImageSrc = post.ImageSrc, 
                Texts = post.Text, 
                UserName = post.User?.Login ?? "Unknown", 
                CanDelete = false, // Перенесенные посты не могут быть удалены
                CanMove = false // Перенесенные посты не могут быть снова перенесены
            }) 
            .ToList(); 
        
        var viewModel = new MovedPostsViewModel
        {
            Posts = mainPagePosts
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult SetForMainPage(Type postId)
    {
        _ecologyRepository.SetForMainPage(postId);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult EcologyProfile()
    {
        var viewModel = new EcologyProfileViewModel();
        var userId = _authService.GetUserId();

        if (userId != null)
        {
            viewModel.AvatarUrl = _userRepositryReal.GetAvatarUrl(userId!.Value);

            var info = _commentRepositoryReal.GetCommentAuthors((int)userId);
        
            viewModel.Comments = info
                .Comments
                .Select(dbComment => new CommentForProfileViewModel()
                {
                    CommentId = dbComment.Id,
                    CommentText = dbComment.CommentText
                })
                .ToList();
        
            viewModel.Posts = info
                .Posts
                .Select(dbPost => new EcologyForProfileViewModel
                {
                    ImageSrc = dbPost.ImageSrc,
                    Texts = dbPost.Text,
                })
                .ToList();
        }
        else 
        {
            viewModel.UserName = "Guest";
            viewModel.AvatarUrl = "~/images/Ecology/defaltavatar.JPG";
            viewModel.Posts = new List<EcologyForProfileViewModel>();
            viewModel.Comments = new List<CommentForProfileViewModel>();
        }
        
        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult EcologyChat()
    {
        var ecologyFromDb = _ecologyRepository.GetAllWithUsersAndComments();
        var currentUserId = _authService.GetUserId();
        var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin"); 
        
        if (currentUserId is null)
        {
            return RedirectToAction("Index");
        }

        var user = _userRepositryReal.Get(currentUserId.Value);

        if (user.Coins < 150)
        {
            return RedirectToAction("Index");
        }
    
        var ecologyViewModels = ecologyFromDb
            .Select(dbEcology =>
                new EcologyViewModel
                {
                    PostId = dbEcology.Id,
                    ImageSrc = dbEcology.ImageSrc,
                    Texts = dbEcology.Text,
                    UserName = dbEcology.User?.Login ?? "Unknown",
                    //Text = dbEcology.Comments?.CommentText ?? "Without comments",
                    CanDelete = dbEcology.User?.Id == currentUserId || isAdmin,
                    CanMove = isAdmin,
                    PostsForMainPage = dbEcology.ForMainPage == 1,
                    LikeCount = dbEcology.UsersWhoLikeIt.Count(),                                                                                                     
                    IsLiked = dbEcology.UsersWhoLikeIt.Any(x => x.UserId == user.Id)
                }
            )
            .ToList();

        var viewModel = new PostViewModel
        {
            Ecologies = ecologyViewModels,
            Posts = new List<PostCreationViewModel>()
        };
        
        return View(viewModel);
    }
    
    [HttpPost] //Это и есть создание 
    public IActionResult EcologyChat(PostCreationViewModel viewModel, IFormFile imageFile)
    {
        if (CalcCountWorldRepeat.IsEclogyTextHas(viewModel.Text) >= 4)
        {
            ModelState.AddModelError(nameof(PostCreationViewModel.Text), "so similar texts");
        }

        if (!ModelState.IsValid)
        {
            return View("EcologyChat");
        }

        var currentUserId = _authService.GetUserId();
        
        string imageUrl = null; //изначально null для того, чтобы затем получить либо URL, либо путь к загруженному изображению с компьютера. Для того, чтобы  использовать одно из значений в объекте EcologyData
        
        if (imageFile != null && imageFile.Length > 0)
        {
            var webRootPath = _webHostEnvironment.WebRootPath; 
            var fileName = Path.GetFileNameWithoutExtension(imageFile.FileName); 
            var extension = Path.GetExtension(imageFile.FileName); 
            var newFileName = $"{fileName}-{currentUserId}{extension}";
            var path = Path.Combine(webRootPath, "images", "uploads", newFileName);
        
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }
            imageUrl = $"/images/Ecology/ecologyPosts/{newFileName}";
        }
        else if (!string.IsNullOrEmpty(viewModel.Url))
        {
            imageUrl = viewModel.Url;
        }
        else
        {
            ModelState.AddModelError("", "Please provide either an image URL or upload an image."); 
            return View("EcologyChat");
        }

        var ecology = new EcologyData
        {
            ImageSrc = imageUrl,
            Text = viewModel.Text
        };

        _ecologyRepository.Create(ecology, currentUserId!.Value, viewModel.PostId);
        //_ecologyRepository.Add(ecology);
        
        // Отправка уведомления о новом посте
        _chatHub.Clients.All.NewMessageAdded($"User {viewModel.UserName} create a new post: {viewModel.Text}");

        return RedirectToAction("EcologyChat");
    }

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
        var comm = _commentRepositoryReal.GetCommentsByPostId(postId);
        
        return View(comm);
    }
    
    [IsAuthenticated]
    [HttpPost]
    public IActionResult UpdateAvatar(IFormFile avatar)
    {
        var webRootPath = _webHostEnvironment.WebRootPath;

        var userId = _authService.GetUserId()!.Value;
        var avatarFileName = $"avatar-{userId}.jpg";

        var path = Path.Combine(webRootPath, "images", "avatars", avatarFileName);
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            avatar
                .CopyToAsync(fileStream)
                .Wait();
        }

        var avatarUrl = $"/images/avatars/{avatarFileName}";
        _userRepositryReal.UpdateAvatarUrl(userId, avatarUrl);

        return RedirectToAction("EcologyProfile");
    }
}
}