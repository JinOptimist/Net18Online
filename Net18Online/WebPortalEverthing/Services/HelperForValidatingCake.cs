namespace WebPortalEverthing.Services
{
    public class HelperForValidatingCake
    {
        public int QuantityWords(string description)
        {
            var quantityWords = description.Split(' ');
            return quantityWords.Length;
        }
    }
}
