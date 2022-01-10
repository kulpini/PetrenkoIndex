using System;
using System.Collections.Generic;
using System.Linq;

namespace PetrenkoIndex
{
    public class StringElement
    {
        public string Text { get; }
        public string Comment { get; }
        public double PetrenkoIndex
        {
            get { return CalculatePetrenkoIndex(Text) + CalculatePetrenkoIndex(Comment); }
        }
        public StringElement(string text)
        {
            int index = text.LastIndexOf('|');
            Comment = index == -1 ? "" : text[(index + 1)..];
            Text = index == -1 ? text : text[0..(index - 1)];
        }
        private static double CalculatePetrenkoIndex(string text)
        {
            string onlyLettersString = new string(text.Where(Char.IsLetter).ToArray());
            double index = 0;
            for (int i = 0; i < onlyLettersString.Length; i++)
            {
                index += i + 0.5;
            }
            return index * onlyLettersString.Length;
        }
    }

    public static class StringElements
    {
        public static List<StringElement> GetList(string stringSet)
        {
            List<StringElement> list = new List<StringElement>();
            foreach (string line in stringSet.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                list.Add(new StringElement(line));
            }
            return list;
        }
    }
}
