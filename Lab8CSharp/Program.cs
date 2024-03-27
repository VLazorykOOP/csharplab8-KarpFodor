using System.Text;
using System.Text.RegularExpressions;

class Lab8T1
{
    public void Run()
    {
        Console.WriteLine("===   Task1   ===\n");

        string inputFilePath = "C:\\Users\\timep\\OneDrive\\Документы\\Cours_3.2\\C#\\Lab_8\\Lab8CSharp\\T1\\input.txt";

        string outputFilePath = "C:\\Users\\timep\\OneDrive\\Документы\\Cours_3.2\\C#\\Lab_8\\Lab8CSharp\\T1\\output.txt";

        string formatPattern = @"\(\s*\d+\s*,\s*\d+\s*\)";

        try
        {
            string content = File.ReadAllText(inputFilePath);

            MatchCollection matches = Regex.Matches(content, formatPattern);

            int vectorCount = matches.Count;

            File.WriteAllLines(outputFilePath, matches.Cast<Match>().Select(match => match.Value));


            Console.WriteLine($"Кiлькiсть знайдених векторiв: {vectorCount}");
            Console.WriteLine($"Вектори записано у файл: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    class Lab8T2
    {
        public void Run()
        {
            Console.WriteLine("\n\n===   Task2   ===\n");

            string inputFile = "C:\\Users\\timep\\OneDrive\\Документы\\Cours_3.2\\C#\\Lab_8\\Lab8CSharp\\T2\\input2.txt";
            string outputFile = "C:\\Users\\timep\\OneDrive\\Документы\\Cours_3.2\\C#\\Lab_8\\Lab8CSharp\\T2\\output2.txt";

            string text;
            using (StreamReader reader = new StreamReader(inputFile, Encoding.UTF8))
            {
                text = reader.ReadToEnd();
            }

            // Регулярний вираз для визначення iдентифiкаторiв
            Regex regex = new Regex(@"\b[a-zA-Z_][a-zA-Z0-9_]*\b");

            // Вилучення iдентифiкаторiв та пiдрахунок їх кiлькостi
            int removedCount = 0;
            string result = regex.Replace(text, match =>
            {
                removedCount++;
                return ""; 
            });


            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                writer.Write(result);
            }

            Console.WriteLine($"Iдентифiкаторiв видалено:{removedCount}. Результат записано у файл output2.txt.");
        }
    }

    class Lab8T3
    {
        public void Run()
        {
            Console.WriteLine("\n\n===   Task3   ===\n");
           
            string firstTextFile = "C:\\Users\\timep\\OneDrive\\Документы\\Cours_3.2\\C#\\Lab_8\\Lab8CSharp\\T3\\input3.txt";
            string secondTextFile = "C:\\Users\\timep\\OneDrive\\Документы\\Cours_3.2\\C#\\Lab_8\\Lab8CSharp\\T3\\input3.1.txt";
            string outputFile = "C:\\Users\\timep\\OneDrive\\Документы\\Cours_3.2\\C#\\Lab_8\\Lab8CSharp\\T3\\output3.txt";

            string firstText = File.ReadAllText(firstTextFile);
            string secondText = File.ReadAllText(secondTextFile);
            
            string wordToInsert = "WORD";

            string result = firstText.Replace(wordToInsert, wordToInsert + " " + secondText);

            File.WriteAllText(outputFile, result);

            Console.WriteLine("Результат записано у файл output3.txt.");
        }
    }

    class Lab8T4
    {
        public void Run()
        {
            Console.WriteLine("\n\n===   Task4   ===\n");

            string sentence = "Створити файл i записати в нього всi символи, вiдмiннi вiд роздiлових знакiв.";

            char[] separators = { ' ', '.', ',', ';', ':', '!', '?', '\n', '\r' };

            using (StreamWriter writer = new StreamWriter("C:\\Users\\timep\\OneDrive\\Документы\\Cours_3.2\\C#\\Lab_8\\Lab8CSharp\\Task4.txt"))
            {
                foreach (char c in sentence)
                {
                    // Перевiрка, чи символ не є роздiловим знаком
                    if (Array.IndexOf(separators, c) == -1)
                    {
                        writer.Write(c);
                    }
                }
            }

            string fileContent = File.ReadAllText("C:\\Users\\timep\\OneDrive\\Документы\\Cours_3.2\\C#\\Lab_8\\Lab8CSharp\\Task4.txt");
            Console.WriteLine("Вмiст файлу:");
            Console.WriteLine(fileContent);
        }
    }


    class Lab8T5
    {
        public void Run()
        {
            Console.WriteLine("\n\n===   Task5   ===\n");

            // Task1
            string studentName = "Karp";

            string folder1Path = $"C:\\temp\\{studentName}1";
            string folder2Path = $"C:\\temp\\{studentName}2";

            Directory.CreateDirectory(folder1Path); 
            Directory.CreateDirectory(folder2Path);

            // Task2
            string t1FilePath = Path.Combine(folder1Path, "t1.txt");
            string t2FilePath = Path.Combine(folder1Path, "t2.txt");

            string t1Text = "Шевченко Степан iванович, 2001 року народження, мiсце проживання м. Суми";
            string t2Text = "Комар Сергiй Федорович, 2000 року народження, мiсце проживання м. Київ";

            File.WriteAllText(t1FilePath, t1Text);
            File.WriteAllText(t2FilePath, t2Text);

            // Task3
            string t3FilePath = Path.Combine(folder2Path, "t3.txt");

            File.AppendAllText(t3FilePath, File.ReadAllText(t1FilePath));
            File.AppendAllText(t3FilePath, File.ReadAllText(t2FilePath));

            // Task4
            PrintFileInfo(t1FilePath);
            PrintFileInfo(t2FilePath);
            PrintFileInfo(t3FilePath);

            // Task5
            string movet2FilePath = Path.Combine(folder2Path, "t2.txt");
            File.Move(t2FilePath, movet2FilePath);

            // Task6
            string moveT1FilePath = Path.Combine(folder2Path, "t1.txt");
            File.Copy(t1FilePath, moveT1FilePath);

            // Task7
            string allFolderPath = $"C:\\temp\\ALL";
            Directory.Move(folder2Path, allFolderPath);
            Directory.Delete(folder1Path, true);

            // Task8
            Console.WriteLine("\nFiles in ALL directory:");
            string[] filesInAll = Directory.GetFiles(allFolderPath);
            foreach (string file in filesInAll)
            {
                PrintFileInfo(file);
            }
        }

        static void PrintFileInfo(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine(
                $"File: {fileInfo.Name}, Size: {fileInfo.Length} bytes, Last Modified: {fileInfo.LastWriteTime}"
            );
        }
}

    class Program
    {
        static void Main()
        {
            Lab8T1 lab8task1 = new Lab8T1();
            Lab8T2 lab8task2 = new Lab8T2();
            Lab8T3 lab8task3 = new Lab8T3();
            Lab8T4 lab8task4 = new Lab8T4();
            Lab8T5 lab8task5 = new Lab8T5();

            lab8task1.Run();
            lab8task2.Run();
            lab8task3.Run();
            lab8task4.Run();
            lab8task5.Run();
        }
    }
}
