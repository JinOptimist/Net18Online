namespace WebPortalEverthing.Services
{
    public class CheckingForBannedNames
    {

        public bool HasBannedName(string name, List<string> bannedWords)
        {
            var namelower = name.ToLower();

            return bannedWords.Any(bannedWord => bannedWord.ToLower() == namelower);
        }
    }
}
