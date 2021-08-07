using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace NBS_Duplicate_Items
{
    class Program
    {
        static void Main(string[] args)
        {
            var itemsFile = File.ReadAllText("./items.json");
            var ouputFile = File.OpenWrite("./output.Json");

            if (string.IsNullOrWhiteSpace(itemsFile))
            {
                Console.WriteLine("No Items to read");
                Console.WriteLine("Press any key to close the application");
                var key = Console.ReadKey(true);
                Environment.Exit(0);
            }

            List<ItemsModel> foundDuplicates = new List<ItemsModel>();
            List<OutputModel> outputDuplicates = new List<OutputModel>();

            Console.WriteLine("Press a button to continue");
            _= Console.ReadKey(true);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var flattened = JsonHelper.NormalizeStringAndFlatten(itemsFile);

            outputDuplicates = flattened.GroupBy(f => f)
                .Where(g => g.Count() > 1)
                .Select(s => new OutputModel
                {
                    RepeatedItem = JsonSerializer.Deserialize<ItemsModel>(s.Key),
                    TimesRepeated = s.Count()
                })
                .ToList();

            foreach (var dup in outputDuplicates)
            {
                Console.WriteLine("Item: {0}, TimesRepeated: {1}", dup.RepeatedItem.ToString(), dup.TimesRepeated);
            }

            Console.WriteLine("{0} Duplicate items found", outputDuplicates.Count);

            string outputDuplicatesString = JsonSerializer.Serialize<List<OutputModel>>(outputDuplicates);

            var bytesToWrite = Encoding.UTF8.GetBytes(outputDuplicatesString);
            ouputFile.Write(bytesToWrite);
            ouputFile.Flush();
            ouputFile.Close();

            Console.WriteLine("Press a button to close");
            _ = Console.ReadKey(true);

            Environment.Exit(0);
        }
    }
}
