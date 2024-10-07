using System.Security;

namespace Project_HSE;

/// <summary>
/// Класс с реализацией нужных функций: чтением данных из файла, запись в файл, нахождением индексов необходимых эл-тов.
/// </summary>
public static class Manager
{
    /// <summary>
    /// Reading from a file using a specified path.
    /// </summary>
    /// <param name="path">The path to the file.</param>
    /// <returns>A string array of values read from a file.</returns>
    /// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on
    /// an unmapped drive.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed
    /// the system-defined maximum length. </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested
    /// is not permitted by the operating system for the specified <paramref name="path" />,
    /// such as when <paramref name="access" />  is <see cref="FileAccess.Write" /> or
    /// <see cref="FileAccess.ReadWrite" /> and the file or directory is set for read-only access.
    ///  -or-
    /// <see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />,
    /// but file encryption is not supported on the current platform.</exception>
    /// <exception cref="T:System.IO.IOException">An I/O error, such as specifying <see cref="FileMode.CreateNew" />
    /// when the file specified by <paramref name="path" /> already exists, occurred.
    ///  -or-
    ///  The disk was full (when <paramref name="preallocationSize" /> was provided and <paramref name="path" />
    /// was pointing to a regular file).
    ///  -or-
    ///  The file was too large (when <paramref name="preallocationSize" /> was provided and <paramref name="path" />
    /// was pointing to a regular file).</exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does
    /// not have the required permission.</exception>
    /// <exception cref="T:System.NullReferenceException">The null value is entered.</exception>
    /// <exception cref="T:System.ArgumentException">Incorrect parameter value is entered.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">A larger range of values is introduced
    /// than was previously assumed.</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">There is no suitable data.</exception>
    public static string[] Read(string path)
    {
        try
        {
            SendMessage("Считываем данные файла...");
            string[] array = File.ReadAllLines(path)[0].Split(";");
            return array;
        }
        catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException or PathTooLongException
                                      or NullReferenceException or IOException or SecurityException)
        {
            SendMessage("Проблемы с открытием файла");
            throw;
        }
        catch (Exception e) when (e is ArgumentException or ArgumentOutOfRangeException)
        {
            SendMessage("Проблемы с чтением данных из файла");
            throw;
        }
        catch (Exception e) when (e is IndexOutOfRangeException)
        {
            SendMessage("Корректных данных в файле нет");
            throw;
        }
    }
    
    /// <summary>
    /// Writing the necessary data to the file.
    /// </summary>
    /// <param name="nums">The numbers that are written to the file.</param>
    /// <param name="minValue">The minimum number to be output.</param>
    /// <param name="path">The path to the file.</param>
    /// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on
    /// an unmapped drive.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed
    /// the system-defined maximum length. </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested
    /// is not permitted by the operating system for the specified <paramref name="path" />,
    /// such as when <paramref name="access" />  is <see cref="FileAccess.Write" />
    /// or <see cref="FileAccess.ReadWrite" /> and the file or directory is set for read-only access.
    ///  -or-
    /// <see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />,
    /// but file encryption is not supported on the current platform.</exception>
    /// <exception cref="T:System.IO.IOException">An I/O error, such as specifying <see cref="FileMode.CreateNew" />
    /// when the file specified by <paramref name="path" /> already exists, occurred.
    ///  -or-
    ///  The disk was full (when <paramref name="preallocationSize" /> was provided and <paramref name="path" />
    /// was pointing to a regular file).
    ///  -or-
    ///  The file was too large (when <paramref name="preallocationSize" /> was provided and <paramref name="path" />
    /// was pointing to a regular file).</exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the
    /// required permission.</exception>
    /// <exception cref="T:System.NullReferenceException">The null value is entered.</exception>
    /// <exception cref="T:System.ArgumentException">Incorrect parameter value is entered.</exception>
    public static void Write(int[] nums, int minValue, string path)
    {
        string answer = $"{minValue} {String.Join(" ", nums)}";
        try
        {
            SendMessage("Записываем данные в файл...");
            File.WriteAllText(path, answer);
        }
        catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException or PathTooLongException 
                                      or ArgumentException or NullReferenceException or SecurityException)
        {
            SendMessage("Проблемы с открытием файла");
            throw;
        }
        catch (IOException)
        {
            SendMessage("Проблемы с записью данных в файл");
            throw;
        }
    }

    
    /// <summary>
    /// Counts the indexes of the minimum elements.
    /// </summary>
    /// <param name="defaultArray">The array used for calculations.</param>
    /// <param name="minValueDifference">The value entered by the user is necessary for further calculations.</param>
    /// <param name="minValueResult">The minimum value found by the program.</param>
    /// <returns>Numeric array of minimum coefficients indexes.</returns>
    public static int[] GetIndexesOfR(string[] defaultArray, int minValueDifference, out int minValueResult)
    {
        int[] arrayOfNums = GetNormalArray(defaultArray, out var size);
        int[] indexes = new int[defaultArray.Length];
        var minValue = GetMinResult(arrayOfNums, minValueDifference);
        var index = 0;
        for (var i = 0; i < defaultArray.Length; i++)
        {
            if (int.TryParse(defaultArray[i], out int num))
            {
                int result = Math.Abs(num - minValueDifference);
                if (result == minValue)
                {
                    indexes[index++] = i;
                }
            }
        }
        minValueResult = minValue;
        return GetStandardForm(indexes, index);
    }
    
    /// <summary>
    /// Writes the required value to the console.
    /// </summary>
    /// <param name="message">the message required to write to the console</param>
    public static void SendMessage(string message)
    {
        try
        {
            Console.WriteLine(message);
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static int GetMinResult(int[] nums, int c)
    {
        var minValue = int.MaxValue;
        foreach (var num in nums)
        {
            int result = Math.Abs(num - c);
            if (result <= minValue)
            {
                minValue = result;
            }
        }
        return minValue;
    }

    private static int[] GetNormalArray(string[] partsOfString, out int size)
    {
        var index = 0;
        var nums = new int[partsOfString.Length];
        foreach (var str in partsOfString)
        {
            if (int.TryParse(str, out int num))
            {
                nums[index++] = num;
            }
        }
        size = index;
        return GetStandardForm(nums, size);
    }

    private static int[] GetStandardForm(int[] array, int size)
    {
        int[] newArray = new int[size];
        for (var i = 0; i < size; i++)
        {
            newArray[i] = array[i];
        }
        return newArray;
    }
    
}