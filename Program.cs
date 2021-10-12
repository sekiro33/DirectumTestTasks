using System.Text.Json;
using System.IO;
using FormGenerator.JsonObject;
using System;

namespace FormGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var root = JsonSerializer.Deserialize<RootObject>(File.ReadAllText(args[0]));
                string path;

                if (args.Length != 1)
                    path = args[1];
                else
                    path = Directory.GetParent(args[0]) + "\\generatedForm.html";

                File.WriteAllText(path, Generator.GenerateForm(root));

                Console.WriteLine("Успешно!");
                Console.WriteLine("Сгенерированная форма: " + path);
                Console.ReadKey();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Ошибка открытия файла: " + ex.FileName + "\n" + ex.Message);
                Console.ReadKey();
                return;
            }
            catch (JsonException ex)
            {
                Console.WriteLine("JSON ошибка: " + ex.Message);
                Console.ReadKey();
                return;
            }
            catch (SystemException ex)
            { 
                Console.WriteLine("Отсутствуют аргументы для запуска");
                Console.ReadKey();
                return;
            }
        }
    }

}
