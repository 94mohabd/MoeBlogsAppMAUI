using MyFirstMauiApp.Models;
using MyFirstMauiApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyFirstMauiApp
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private readonly BlogpostService _blogpostService;
        private ObservableCollection<BlogPost> _blogPosts;
        private bool _isLoading;

        public ObservableCollection<BlogPost> BlogPosts
        {
            get => _blogPosts;
            set
            {
                _blogPosts = value;
                OnPropertyChanged();
            }
        }
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotLoading));  // Notify when IsNotLoading changes
            }
        }
        public bool IsNotLoading => !IsLoading;
        public MainPage(BlogpostService blogpostService)
        {
            InitializeComponent(); 
            _blogpostService = blogpostService;
            BindingContext = this; 
            LoadBlogPosts();
        }

        private async void LoadBlogPosts()
        {
            IsLoading = true;  // Show loading indicator

            try
            {
                var blogPosts = await _blogpostService.GetAllBlogPostsAsync();
                BlogPosts = new ObservableCollection<BlogPost>(blogPosts);
            }
            catch (Exception ex)
            {
                // Handle error if necessary
                await DisplayAlert("Error", "An error occurred while loading blog posts.", "OK");
            }
            finally
            {
                IsLoading = false;  // Hide loading indicator
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void OnReadMoreClicked(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                if (button?.CommandParameter is string id)
                {
                    // Navigate to the details page with the selected blog post
                    await Shell.Current.GoToAsync($"blogdetails?id={id}");
                }
                else
                {
                    await DisplayAlert("Error", "Invalid blog post ID.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and display a user-friendly message
                Console.WriteLine($"Error: {ex.Message}");
                await DisplayAlert("Error", "An error occurred while processing your request.", "OK");
            }
        }
    }
}