using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using systemPracyZespołowej.Models;

namespace systemPracyZespołowej
{

    public partial class MainWindow : Window
    {
        private readonly string _storagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TeamWorkSystem", "project.json");
        public ProjectModel Project { get; set; } = new ProjectModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Project.Roles.Add(new Role("Manager") { Users = new ObservableCollection<string> { "Anna" } });
            Project.Roles.Add(new Role("Developer") { Users = new ObservableCollection<string> { "Marek", "Basia" } });

            Project.Stages.Add(new Stage { Name = "Planowanie", Description = "Zdefiniuj zakres i wymagania." });
            Project.Stages.Add(new Stage { Name = "Realizacja", Description = "Implementacja funkcji." });

            LbRoles.SelectionChanged += LbRoles_SelectionChanged;
            LbStages.SelectionChanged += LbStages_SelectionChanged;

            RefreshBindings();
        }

        private void RefreshBindings()
        {
            LbRoles.ItemsSource = Project.Roles;
            LbStages.ItemsSource = Project.Stages;

            if (Project.Roles.Count > 0)
            {
                LbRoles.SelectedIndex = 0;
                UpdateUsersList();
            }

            if (Project.Stages.Count > 0)
            {
                LbStages.SelectedIndex = 0;
                UpdateStageDetails(Project.Stages[0]);
            }
        }

        private void LbRoles_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateUsersList();

        private void UpdateUsersList()
        {
            if (LbRoles.SelectedItem is Role role)
                LbUsers.ItemsSource = role.Users;
            else
                LbUsers.ItemsSource = null;
        }

        private void BtnAddRole_Click(object sender, RoutedEventArgs e)
        {
            var name = TxtNewRole.Text?.Trim();
            if (string.IsNullOrEmpty(name)) { MessageBox.Show("Podaj nazwę roli."); return; }
            Project.Roles.Add(new Role(name));
            TxtNewRole.Clear();
            LbRoles.SelectedIndex = Project.Roles.Count - 1;
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var user = TxtNewUser.Text?.Trim();
            if (string.IsNullOrEmpty(user) || !(LbRoles.SelectedItem is Role role)) { MessageBox.Show("Wybierz rolę i podaj nazwę użytkownika."); return; }
            role.Users.Add(user);
            TxtNewUser.Clear();
        }

        private void BtnAddStage_Click(object sender, RoutedEventArgs e)
        {
            var name = TxtNewStage.Text?.Trim();
            if (string.IsNullOrEmpty(name)) { MessageBox.Show("Podaj nazwę etapu."); return; }
            var stage = new Stage { Name = name, Description = string.Empty };
            Project.Stages.Add(stage);
            TxtNewStage.Clear();
            LbStages.SelectedItem = stage;
        }

        private void LbStages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbStages.SelectedItem is Stage st)
                UpdateStageDetails(st);
        }

        private void UpdateStageDetails(Stage stage)
        {
            if (stage == null) return;
            TxtStageDescription.Text = stage.Description;
            LbComments.ItemsSource = stage.Comments;
            LbHistory.ItemsSource = stage.History;
        }

        private void TxtStageDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LbStages.SelectedItem is Stage st)
            {
                st.Description = TxtStageDescription.Text;
                st.History.Add(new ChangeHistoryEntry { Author = "System", Description = "Zmieniono opis etapu." });
            }
        }

        private void BtnAddComment_Click(object sender, RoutedEventArgs e)
        {
            if (LbStages.SelectedItem is not Stage st) { MessageBox.Show("Wybierz etap."); return; }
            var author = TxtCommentAuthor.Text?.Trim();
            var text = TxtComment.Text?.Trim();
            if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(text)) { MessageBox.Show("Uzupełnij autora i treść komentarza."); return; }

            var comment = new Comment { Author = author, Text = text };
            st.Comments.Add(comment);
            st.History.Add(new ChangeHistoryEntry { Author = author, Description = $"Dodano komentarz: {Short(text)}" });

            TxtComment.Clear();
        }

        private static string Short(string text, int max = 60) => text.Length <= max ? text : text.Substring(0, max) + "...";

    }
}
