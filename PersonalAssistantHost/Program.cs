using System;
using System.ServiceModel;

namespace PersonalAssistantHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(PersonalAssistantWCF.ServicePersonalAssistant)))
            {
                host.Open();
                Console.WriteLine("Хост стартовал!");
                Console.ReadLine();
            }
        }
    }
}
