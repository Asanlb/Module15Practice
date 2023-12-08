using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

class MyClass
{
    private int privateField;
    public int PublicProperty { get; set; }

    private string PrivateMethod()
    {
        return "Private method invoked.";
    }

    public void PublicMethod()
    {
        Console.WriteLine("Public method invoked.");
    }

    public MyClass(int value)
    {
        PublicProperty = value;
    }

    public MyClass(){}
}

class Program
{
    static void Main()
    {
        Type myClassType = typeof(MyClass);

        Console.WriteLine($"Имя класса: {myClassType.Name}");

        ConstructorInfo[] constructors = myClassType.GetConstructors();
        Console.WriteLine("\nСписок конструкторов:");
        foreach (var constructor in constructors)
        {
            Console.WriteLine($"{constructor.Name}, Модификатор доступа: {constructor.Attributes}");
        }

        FieldInfo[] fields = myClassType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        PropertyInfo[] properties = myClassType.GetProperties();
        Console.WriteLine("\nСписок полей и свойств:");
        foreach (var field in fields)
        {
            Console.WriteLine($"Поле: {field.Name}, Тип: {field.FieldType}, Модификатор доступа: {field.Attributes}");
        }
        foreach (var property in properties)
        {
            Console.WriteLine($"Свойство: {property.Name}, Тип: {property.PropertyType}, Модификатор доступа: {property.Attributes}");
        }

        MethodInfo[] methods = myClassType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        Console.WriteLine("\nСписок методов:");
        foreach (var method in methods)
        {
            Console.WriteLine($"Метод: {method.Name}, Возвращаемый тип: {method.ReturnType}, Модификатор доступа: {method.Attributes}");
        }

        Console.WriteLine("\n--------------------------------\n");
        object myObject = Activator.CreateInstance(myClassType);
        if (myObject is MyClass myInstance)
        {
            Console.WriteLine($"Экземпляр MyClass успешно создан. PublicProperty: {myInstance.PublicProperty}");
        }

        Console.WriteLine("\n--------------------------------\n");
        myClassType.GetProperty("PublicProperty").SetValue(myObject, 42);
        myClassType.GetMethod("PublicMethod").Invoke(myObject, null);

        Console.WriteLine("\n--------------------------------\n");
        MethodInfo privateMethod = myClassType.GetMethod("PrivateMethod", BindingFlags.Instance | BindingFlags.NonPublic);

        if (privateMethod != null)
        {
            privateMethod.Invoke(myObject, null);
        }
    }
}
