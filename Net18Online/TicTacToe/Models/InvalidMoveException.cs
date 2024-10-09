using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException()
        {
        }

        public InvalidMoveException(string message) : base(message)
        {
        }

        public InvalidMoveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidMoveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
