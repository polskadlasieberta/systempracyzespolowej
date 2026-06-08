using System;

namespace systemPracyZespo�owej.Models
{
    public class ChangeHistoryEntry
    {
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
