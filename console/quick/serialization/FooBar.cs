using System;
using System.Collections.Generic;
using System.Text.Json;

namespace serialization;
public class Account
{
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public IList<string>? Roles { get; set; }
}

public class Program
{
    public static void NoMain()
    {
        var account = new Account
        {
            Email = "example@example.com",
            IsActive = true,
            CreatedDate = new DateTime(2023, 1, 20, 0, 0, 0, DateTimeKind.Utc),
            Roles = new List<string>
            {
                "User",
                "Admin"
            }
        };

        string json = Foo(account);
        Console.WriteLine(json);
        json = Bar(account);
        Console.WriteLine(json);
        json = Baz(account);
        Console.WriteLine(json);
        json = Quz(account);
        Console.WriteLine(json);
    }

    public static string Foo(Account account)
    {
        var options = new JsonSerializerOptions() { WriteIndented = true };
        return JsonSerializer.Serialize(account, options);
    }

    public static string Bar(Account account)
    {
        var options = new JsonSerializerOptions() { WriteIndented = true };
        return JsonSerializer.Serialize<Account>(account, options);
    }

    public static string Baz(Account account)
    {
        var options = new JsonSerializerOptions() { WriteIndented = false };
        return JsonSerializer.Serialize(account, options);
    }

    public static string Quz(Account account)
    {
        return JsonSerializer.Serialize<Account>(account);
    }
}
