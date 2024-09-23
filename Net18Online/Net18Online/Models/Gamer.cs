using System;
namespace Net18Online.Net18Online.Models
{
    public class Gamer
    {
        private int _id;
        var attempt = 0;

        public Gamer( int number )
        {
            this._id = number;
        }
        public int GetId() { return _id; }

        public int GetAttempt() { return attempt; }
        /// <summary>
        /// Установка количества попыток
        /// </summary>
        /// <param name="attemptsLeft"></param>
        public void SetAttempt(int attemptsLeft) { attempt = attemptsLeft; }
    }
}