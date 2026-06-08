using System;
using System.Collections.ObjectModel;

namespace systemPracyZespo�owej.Models
{
    public class Stage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();
        public ObservableCollection<ChangeHistoryEntry> History { get; set; } = new ObservableCollection<ChangeHistoryEntry>();
    }
}
