using System;
using CodeStrikes.Sdk;
using CodeStrikes.Sdk.Bots;
using CodeStrikes.TestingApp;

namespace CodeStrikes.TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
            
            DataCollector dataCollector = new DataCollector();
            Analisys analis = new Analisys();
            PlayerBot playerBot = new PlayerBot();
            
            Kickboxer kickboxer = new Kickboxer();
            Boxer boxer = new Boxer();
            

            Console.WriteLine($"Executing fight: {playerBot} vs {kickboxer}");
            Fight fight = new Fight(playerBot, kickboxer, new StandardGameLogic());
            var result = fight.Execute();
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
            Console.WriteLine($"Executing fight: {playerBot} vs {boxer}");
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);
            fight = new Fight(playerBot, boxer, new StandardGameLogic());
            result = fight.Execute();
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
