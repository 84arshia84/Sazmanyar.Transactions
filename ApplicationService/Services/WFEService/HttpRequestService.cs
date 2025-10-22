using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApplicationService.Services.WFEService
{
    public class HttpRequestService
    {
        private readonly HttpClient _httpClient;
        public HttpRequestService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetAsync(string url, string token)
        {
            // اضافه کردن Token به Header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> PostAsync<T>(string url, T data, string token)
        {
            // اضافه کردن Token به Header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> SendAsync<T>(string url, T data, string token, HttpMethod method)
        {
            // تنظیم Authorization Header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            // ساخت Request
            var request = new HttpRequestMessage(method, url);
            if (data != null)
            {
                // سریال‌سازی داده‌ها به JSON با Newtonsoft
                string jsonData = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            }
            // ارسال درخواست
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode(); // بررسی موفقیت درخواست
            // بازگرداندن پاسخ به صورت متن
            return await response.Content.ReadAsStringAsync();
        }
    }
}
