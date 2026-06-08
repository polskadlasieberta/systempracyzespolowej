using System.Collections.ObjectModel;

namespace systemPracyZespo�owej.Models
{
    public class Role
    {
        public string Name { get; set; }
        public ObservableCollection<string> Users { get; set; } = new ObservableCollection<string>();

        public Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}
