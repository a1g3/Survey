using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.Domain.Exceptions
{
    [System.Serializable]
    public class NextPartException : Exception
    {
        public int NextSection { get; set; }
        public NextPartException() { }
        public NextPartException(string message) : base(message) { }
        public NextPartException(string message, Exception inner) : base(message, inner) { }
    }
}
