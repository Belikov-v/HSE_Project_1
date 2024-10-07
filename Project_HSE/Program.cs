// Беликов Владимир Владимирович БПИ-249 В15.

namespace Project_HSE
{
    /// <summary>
    /// Класс с точкой входа в программу.
    /// </summary>
    public static class Project
    {
        
        private static readonly string RootPath = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string ProjectPath = RootPath.Substring(0, RootPath.Length - "/bin/Debug/net8.0".Length - 1);
        private static readonly string InputFilePath = Path.Combine($"{ProjectPath}/WorkingFiles/input.txt");
        private static readonly string OutputFilePath = Path.Combine($"{ProjectPath}/WorkingFiles/output.txt");
        
        
        /// <summary>
        /// Программа получает из input.txt данные для массива. По данным из 𝑋 следует вычислить значение
        /// 𝑅 = 𝑚𝑖𝑛|𝑋𝑖 − 𝐶|. В output.txt программа сохраняет значение 𝑅 и индекс элемента массива, для которого R
        /// вычислено.
        /// </summary>
        public static void Main()
        {
            do
            {
                int enteredNum;
                string[] line;
                while (true)
                {
                    try
                    {
                        // Получаем значение С.
                        Manager.SendMessage("Введите значение 0 <= С < 1000");
                        enteredNum = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch (Exception e) when (e is FormatException)
                    {
                        Manager.SendMessage("Введено некорректное значение 0 <= С < 1000");
                    }
                }

                try
                {
                    // Считываем введенные пользователем значения и сохраняем в строку.
                    line = Manager.Read(InputFilePath);
                }
                catch (Exception)
                {
                    continue;
                }

                // Находим минимальное значение и индексы с этим значением.
                int[] indexes = Manager.GetIndexesOfR(line, enteredNum, out int minValue);
                // Записываем ответ в output.txt.
                Manager.Write(indexes, minValue, OutputFilePath);
            } while (Check());
        }

        private static bool Check()
        {
            Manager.SendMessage("Для выхода нажмите Esc");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            return keyInfo.Key != ConsoleKey.Escape;
        }
    }
}