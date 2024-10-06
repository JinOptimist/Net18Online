using System;

using Net18Online.PrintSimulator.Models.Enum;

namespace Net18Online.PrintSimulator.Models
{
    public static class TextProvider()
    {
        private static readonly Dictionary<PrintText, string> texts = new Dictionary<PrintText, string>
        {
            { PrintText.Text1, "The quick brown fox jumps over the lazy dog." },
            { PrintText.Text2, "In a world where technology is constantly evolving, it's important to keep up with the latest trends." },
            { PrintText.Text3, "Reading books can transport you to different worlds and introduce you to new ideas." },
            { PrintText.Text4, "The sun sets in the west, painting the sky with hues of orange and pink." },
            { PrintText.Text5, "Traveling opens your mind to diverse cultures and experiences." },
            { PrintText.Text6, "The quick brown fox jumps over the lazy dog. This sentence contains every letter of the alphabet, making it a perfect pangram for typing practice. Speed and accuracy are essential for effective communication in the digital age." },
            { PrintText.Text7, "In a world where technology is constantly evolving, it is important to keep up with the latest trends. From artificial intelligence to virtual reality, the possibilities are endless. Embrace the change and learn new skills to stay relevant." },
            { PrintText.Text8, " Reading books can transport you to different worlds and introduce you to new ideas. Whether you prefer fiction or non-fiction, there is always something to learn from a good book. Make time for reading in your daily routine." },
            { PrintText.Text9, "The sun sets in the west, painting the sky with hues of orange and pink. Each sunset is a reminder of the beauty of nature and the passage of time. Take a moment to appreciate the little things in life." },    
            { PrintText.Text10, "Traveling opens your mind to diverse cultures and experiences. Each destination has its own unique charm, from bustling cities to serene landscapes. Plan your next adventure and explore what the world has to offer." }
        };

        public static string GetText(PrintText text)
        {
            Random random = new Random();
            int index = random.Next();
            switch(index)
            {
                case 0: Console.WriteLine(TextProvider.GetText(PrintText.Text1));
                return texts[text]; break;
                case 1: Console.WriteLine(TextProvider.GetText(PrintText.Text2));
                return texts[text]; break;
                case 2: Console.WriteLine(TextProvider.GetText(PrintText.Text3));
                return texts[text]; break;
                case 3: Console.WriteLine(TextProvider.GetText(PrintText.Text4));
                return texts[text]; break;
                case 4: Console.WriteLine(TextProvider.GetText(PrintText.Text5));
                return texts[text]; break;
                case 5: Console.WriteLine(TextProvider.GetText(PrintText.Text6));
                return texts[text]; break;
                case 6: Console.WriteLine(TextProvider.GetText(PrintText.Text7));
                return texts[text]; break;
                case 7: Console.WriteLine(TextProvider.GetText(PrintText.Text8));
                return texts[text]; break;
                case 8: Console.WriteLine(TextProvider.GetText(PrintText.Text9));
                return texts[text]; break;
                case 9: Console.WriteLine(TextProvider.GetText(PrintText.Text10));
                return texts[text]; break;
                default: return;
            }
        }
    }
}