using System;
using SpawnTraffic.Common.Domains;

namespace SpawnTraffic.AppCmd.Helpers
{
    public class MessageDisplayHelper
    {
        public static void DisplayMessage(Result result)
        {
            foreach (var resultMessage in result.Messages)
            {
                Console.WriteLine(resultMessage.Message);
            }

            Console.WriteLine();
        }
    }
}
