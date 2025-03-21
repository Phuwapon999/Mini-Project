using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json; // Use System.Text.Json
using Newtonsoft.Json;


namespace StudentScore.ViewModel;
public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    string email = "";

    [ObservableProperty]
    string password = "";


    private async Task<List<User>> LoadUsersFromJsonAsync()
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.json");

        if (File.Exists(filePath))
        {
            var json = await File.ReadAllTextAsync(filePath);
            System.Diagnostics.Debug.WriteLine($"Loaded JSON: {json}");  // เช็คข้อมูล JSON

            var users = JsonConvert.DeserializeObject<List<User>>(json);
            System.Diagnostics.Debug.WriteLine($"Total users loaded: {users?.Count ?? 0}");

            foreach (var user in users)
            {
                System.Diagnostics.Debug.WriteLine($"User: {user.email}, Password: {user.password}");
            }

            return users ?? new List<User>();  // ถ้า null ให้ return [] แทน
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("students.json file not found!");
        }

        return new List<User>();
    }



    // เช็คข้อมูล Login
    [RelayCommand]
    async Task Login()
    {
        var users = await LoadUsersFromJsonAsync();  // โหลดข้อมูลผู้ใช้จากไฟล์ JSON

        // ตรวจสอบข้อมูลอีเมลและรหัสผ่าน
        var user = users.Find(u => u.email == email && u.password == password);
        if (user != null)
        {
            // ถ้าพบผู้ใช้ที่มีข้อมูลตรงกับที่กรอก
            System.Diagnostics.Debug.WriteLine($"Login successful for {Email}");
            await GoToPage("home");
        }
        else
        {
            // ถ้าไม่พบผู้ใช้ที่ตรงกับข้อมูล
            System.Diagnostics.Debug.WriteLine("Invalid email or password.");
        }
    }



    //Task => Future (in flutter)
    [RelayCommand]
    async Task GoToPage(string page)
    {
        await Shell.Current.GoToAsync(page);  // This line throws exception if Shell.Current is null
    }

}

public class User
{
    public string email { get; set; }
    public string password { get; set; }
}