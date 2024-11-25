using System;
using System.Collections.Generic;
using System.Threading;
using Proxy_pattern;
public interface ISubject
{
    string Request(string request);
}
public class Program
{
    public static void Main(string[] args)
    {
        ISubject proxy = new Proxy();

        // Первый запрос, обработка через RealSubject
        Console.WriteLine(proxy.Request("Запрос 1"));

        // Второй запрос, результат будет кэширован
        Console.WriteLine(proxy.Request("Запрос 1"));

        // Ждем 7 секунд, чтобы кэш устарел
        Thread.Sleep(7000);

        // Запрос снова вызовет RealSubject, так как кэш устарел
        Console.WriteLine(proxy.Request("Запрос 1"));
    }
    
}
