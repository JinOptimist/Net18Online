using System.Reflection;

var assembly = Assembly.LoadFrom(@"C:\Users\svetlana.terekhova\source\repos\Net18Online\Net18Online\ReflectionLoadTest\bin\Debug\net6.0\ReflectionLoadTest.dll");

var type = assembly.GetType("ReflectionLoadTest.Program");


Console.WriteLine(assembly);
Console.WriteLine(type);
