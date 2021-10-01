using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftuniCoursePlanning
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lessons = Console.ReadLine().Split(", ").ToList();

            string input = Console.ReadLine();

            while (input != "course start")
            {
                string[] command = input.Split(':');
                string lessonTitle = command[1];
                if (command[0] == "Add")
                {
                    if (!lessons.Contains(lessonTitle))
                    {
                        lessons.Add(lessonTitle);
                    }
                }
                else if (command[0] == "Insert")
                {
                    int index = int.Parse(command[2]);
                    if (index >= 0 && index < lessons.Count)
                    {
                        if (!lessons.Contains(lessonTitle))
                        {
                            lessons.Insert(index, lessonTitle);
                        }
                    }
                }
                else if (command[0] == "Remove")
                {
                    int index = lessons.IndexOf(lessonTitle);
                    if (lessons.Contains(lessonTitle + "-Exercise"))
                    {
                        lessons.RemoveAt(index + 1);
                    }
                    lessons.Remove(lessonTitle);
                }
                else if (command[0] == "Swap")
                {
                    string lessonTitleToSwap = command[2];
                    if (lessons.Contains(lessonTitle) && lessons.Contains(lessonTitleToSwap))
                    {
                        SwapLessons(lessonTitle, lessonTitleToSwap, lessons);
                    }
                }
                else if (command[0] == "Exercise")
                {
                    if (lessons.Contains(lessonTitle))
                    {
                        if (!lessons.Contains(lessonTitle + "-Exercise"))
                        {
                            int index = lessons.IndexOf(lessonTitle);
                            lessons.Insert(index + 1, lessonTitle + "-Exercise");
                        }
                        
                    }
                    else
                    {
                        lessons.Add(lessonTitle);
                        lessons.Add(lessonTitle + "-Exercise");
                    }
                }
                input = Console.ReadLine();
            }

            for (int i = 1; i <= lessons.Count; i++)
            {
                Console.WriteLine($"{i}.{lessons[i - 1]}");
            }
        }

        private static void SwapLessons(string title1, string title2, List<string> lessons)
        {
            bool ExSwap = false;

            if (lessons.Remove(title1 + "-Exercise"))
            {
                if (lessons.Remove(title2 + "-Exercise"))
                {
                    ExSwap = true;
                }

                lessons.Insert(lessons.IndexOf(title2) + 1, title1 + "-Exercise");
            }
            else if (lessons.Remove(title2 + "-Exercise") || ExSwap)
            {
                lessons.Insert(lessons.IndexOf(title1) + 1, title2 + "-Exercise");
            }

            int index1 = lessons.IndexOf(title1);
            int index2 = lessons.IndexOf(title2);

            string temp = lessons[index1];
            lessons[index1] = lessons[index2];
            lessons[index2] = temp;
        }
    }
}
