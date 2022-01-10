using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PetrenkoIndex
{
    class Program
    {
        private static List<StringElement> stringList;
        static void Main(string[] args)
        {
            Console.WriteLine("Все текстовые строки читаются из файла text.txt");
            Console.WriteLine("Для старта нажмите любую клавишу");
            Console.ReadKey();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                string lines = File.ReadAllText("text.txt");
                stringList = StringElements.GetList(lines);

                MakeStringOperations();
            }
            catch
            {
                Console.WriteLine("Ошибка чтения из файла. Проверьте наличие файла.");
            }
            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;
            Console.WriteLine($"Время выполнения: " + time.ToString("hh\\:mm\\:ss\\.ffff"));
            Console.ReadKey();
        }
        private static void MakeStringOperations()
        {
            var russianStrings = stringList.FindAll(s => s.Comment == "");
            var englishStrings = stringList.FindAll(s => s.Comment != "");
            foreach (var item in russianStrings)
            {
                Console.WriteLine($"{item.Text} (индекс-{item.PetrenkoIndex})");
                var sameEnglishStrings = englishStrings.FindAll(s => s.PetrenkoIndex == item.PetrenkoIndex);
                if (sameEnglishStrings.Count == 0)
                    Console.WriteLine("Соответствий не найдено!");
                else
                {
                    foreach (var element in sameEnglishStrings)
                    {
                        Console.WriteLine($"{element.Text}|{element.Comment} (индекс - {element.PetrenkoIndex})");
                    }
                }
                Console.WriteLine(new string('*', 20));
            }
        }
    }
}
