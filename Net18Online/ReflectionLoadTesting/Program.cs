using LoadTestingMinimalApi.Hubs;
using System.Reflection;

/// <summary>
/// передаём любой(класс) тип и получаем его сборку, в которой этот тип зарегистирован (находится в сборке)
/// </summary>
var typeOfclass = typeof(LoadChatHub);
var assembly = Assembly.GetAssembly(typeOfclass);

Console.WriteLine(assembly);
Console.WriteLine(typeOfclass);
///<summary>
///получаем из сборки все типа (классы)
/// </summary>
var types = assembly.GetTypes();
foreach (var type in types)
{
    Console.WriteLine("====Type========");
    Console.WriteLine(type.FullName);
}

/// <summary>
/// из всех типов отфильтровать только классы
///</summary> 
var classesFromAssemble = assembly
    .GetTypes()
    .Where(type => type.IsClass);

foreach (var item in classesFromAssemble)
{
    Console.WriteLine("=====Class=======");
    Console.WriteLine(item.Name);
}

/// <summary>
/// выбрать классы у которых есть базовый класс
/// </summary>
var children = assembly
    .GetTypes()
    .Where(type => type.IsClass
    && type.BaseType != null);
foreach (var child in children)
{
    Console.WriteLine("=====Child Class=======");
    Console.WriteLine($"Child class:   {child.Name}. Base clacc:  {child.BaseType.Name}");
}