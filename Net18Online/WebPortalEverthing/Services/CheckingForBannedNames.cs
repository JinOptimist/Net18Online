namespace WebPortalEverthing.Services
{
    public class CheckingForBannedNames
    {

        public bool HasBannedName(string name, List<string> bannedWords)
        {
            var namelower = name.ToLower();

            foreach (var bannedWord in bannedWords)
            {
                if (name == bannedWord.ToLower())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
