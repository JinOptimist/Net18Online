using Everything.Data.Repositories.Surveys;
using Microsoft.AspNetCore.SignalR;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Hubs
{
    public interface ITakingSurveyHub
    {
        Task Notify(string message);
    }

    public class TakingSurveyHub : Hub<ITakingSurveyHub>
    {
        private AuthService _authService;
        private IAnswerToQuestionRepositoryReal _answerToQuestionRepository;
        private Dictionary<string /*connectionId*/, int? /*userId*/> _connections = new();

        public TakingSurveyHub(AuthService authService, IAnswerToQuestionRepositoryReal answerToQuestionRepository)
        {
            _authService = authService;
            _answerToQuestionRepository = answerToQuestionRepository;
        }

        public override async Task OnConnectedAsync()
        {
            _connections.Add(Context.ConnectionId, _authService.GetUserId());

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connections.Remove(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        public void SetAnswerValue(int answerId, string? value)
        {
            _answerToQuestionRepository.SetTextValue(answerId, value);

            ShowToCurrentUser("Ответ сохранён!");
        }

        public void ShowToCurrentUser(string message)
        {
            Clients.Caller.Notify(message).Wait();
        }
    }
}
