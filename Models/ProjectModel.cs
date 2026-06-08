using System.Collections.ObjectModel;

namespace systemPracyZespo�owej.Models
{
    public class ProjectModel
    {
        public ObservableCollection<Role> Roles { get; set; } = new ObservableCollection<Role>();
        public ObservableCollection<Stage> Stages { get; set; } = new ObservableCollection<Stage>();
    }
}
