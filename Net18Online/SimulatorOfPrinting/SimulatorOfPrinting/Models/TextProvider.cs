using System;
using System.Threading;

namespace SimulatorOfPrinting.Models
{
    public class TextProvider
    {
        private readonly List<string> texts = new List<string>
        {
            "The quick brown fox jumps over the lazy dog.",
            "In a world where technology is constantly evolving, it's important to keep up with the latest trends.",
            "Reading books can transport you to different worlds and introduce you to new ideas.",
            "The sun sets in the west, painting the sky with hues of orange and pink.",
            "Traveling opens your mind to diverse cultures and experiences.",
            "This sentence contains every letter of the alphabet, making it a perfect pangram for typing practice.",
            "Speed and accuracy are essential for effective communication in the digital age.",
            "From artificial intelligence to virtual reality, the possibilities are endless.",
            "Make time for reading in your daily routine.",
            "Each sunset is a reminder of the beauty of nature and the passage of time.",
            "Take a moment to appreciate the little things in life.",
            "Each destination has its own unique charm, from bustling cities to serene landscapes.", 
            "Plan your next adventure and explore what the world has to offer."
        };

        public string GetText()
        {
            Random random = new Random();
            var randomText = random.Next(texts.Count);
            return texts[randomText];
        }
    }
}