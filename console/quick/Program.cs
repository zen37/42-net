//A 
using System.Text.Json;
using System.Text.Json.Serialization;

class Person1 
  { 
         [JsonInclude] 
         public string Phone; 
         public Person1(string phone) => this.Phone = phone; 
  } 

  //B 
class Person2 
       { 
       [JsonInclude] 
       private string phone; 
       public Person2(string phone) => this.phone = phone; 
}

  //C 
  class Person3 
  { 
         [JsonInclude] 
         public string Phone { get; private set; } 
         public Person3(string phone) => this.Phone = phone; 
  } 

  //D 
  class Person4 
  { 
         public string Phone; 
         public Person4(string phone) => this.Phone = phone; 
  }
public class Program
{
    public static void Main()
    {
       var person = new Person2("(917) 642-8073"); 
       string json = JsonSerializer.Serialize(person); 
       Console.WriteLine(json);
    }
}
