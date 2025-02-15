using MyFirstMauiApp.Models;
using MyFirstMauiApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyFirstMauiApp
{
    [QueryProperty(nameof(BlogPostId), "id")]
    public partial class BlogDetailsPage : ContentPage, INotifyPropertyChanged
    {
        private readonly BlogpostService _blogpostService;

        // The property to bind the ID from the query parameters
        private string _blogPostId;

        public string BlogPostId
        {
            get => _blogPostId;
            set
            {
                _blogPostId = value;
                OnPropertyChanged();
                LoadBlogPost(); // Load blog post when the ID is set
            }
        }

        // The property to hold the blog post object
        private BlogPost _blogPost;
        public BlogPost BlogPost
        {
            get => _blogPost;
            set
            {
                _blogPost = value;
                OnPropertyChanged();
            }
        }

        // Loading state
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotLoading)); // Notify when IsNotLoading changes
            }
        }

        public bool IsNotLoading => !IsLoading;

        public BlogDetailsPage(BlogpostService blogpostService)
        {
            InitializeComponent();
            _blogpostService = blogpostService;
            BindingContext = this; // Set BindingContext to this page
        }


        private async void LoadBlogPost()
        {
            if (string.IsNullOrEmpty(BlogPostId))
            {
                await DisplayAlert("Error", "Invalid blog post ID.", "OK");
                return;
            }

            IsLoading = true; // Show loading indicator

            try
            {
                // Fetch the blog post based on the id
                var blogPost = await _blogpostService.GetBlogPostByIdAsync(BlogPostId);

                if (blogPost != null)
                {
                    BlogPost = blogPost; // Set the blog post property
                }
                else
                {
                    await DisplayAlert("Error", "Blog post not found.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and display a user-friendly message
                Console.WriteLine($"Error: {ex.Message}");
                await DisplayAlert("Error", "An error occurred while loading the blog post.", "OK");
            }
            finally
            {
                IsLoading = false; // Hide loading indicator
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }    

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Load the blog post once the page appears
            LoadBlogPost();
        }



    }
}