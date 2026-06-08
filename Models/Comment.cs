using System;

namespace systemPracyZespo�owej.Models
{
    public class Comment
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
