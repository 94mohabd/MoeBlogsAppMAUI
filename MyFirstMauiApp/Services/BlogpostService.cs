using MyFirstMauiApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMauiApp.Services
{
    public class BlogpostService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public BlogpostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiBaseUrl = "https://moeblogs-api.onrender.com";
        }

        public async Task<List<BlogPost>> GetAllBlogPostsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<BlogPost>>($"{_apiBaseUrl}/api/blogposts");
                       
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"API request failed: {ex.Message}");
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            // Return empty list on failure
            return new List<BlogPost>();
        }

        public async Task<BlogPost> GetBlogPostByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<BlogPost>($"{_apiBaseUrl}/api/blogposts/{id}");
        }

        public async Task<BlogPost> GetBlogPostByUrlHandleAsync(string urlHandle)
        {
            return await _httpClient.GetFromJsonAsync<BlogPost>($"{_apiBaseUrl}/api/blogposts/{urlHandle}");
        }

        public async Task<BlogPost> CreateBlogPostAsync(AddBlogPost model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/blogposts?addAuth=true", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BlogPost>();
        }

        public async Task<BlogPost> UpdateBlogPostAsync(string id, UpdateBlogPostRequest updateBlogPostRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/blogposts/{id}?addAuth=true", updateBlogPostRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BlogPost>();
        }

        public async Task DeleteBlogPostAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/blogposts/{id}?addAuth=true");
            response.EnsureSuccessStatusCode();
        }
    }
}
