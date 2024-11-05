using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laboratory_1
{
    internal class Encryptor
    {
        // ---------------------- Переменные ----------------------
        private static readonly bool isRussianEnabled = true;

        // -------------------- Вспомогательные --------------------
        private static bool CheckRussianInLine(string line)
        {
            return !Regex.IsMatch(line, @"\P{IsCyrillic}");
        }

        // -------------------- Шифраторы --------------------
        // Шифратор Цезаря
        private static string CaesarsCipher(string line, int shift)
        {
            string result;

            char[] buffer = line.ToCharArray();

            bool isRussianLetters = CheckRussianInLine(line);

            int alfabetCount = isRussianLetters ? 33 : 26;

            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];

                if (char.IsLetter(letter))
                {
                    char letterOffset;

                    if (isRussianLetters)
                    {
                        letterOffset = char.IsUpper(letter) ? 'А' : 'а';
                    }
                    else
                    {
                        letterOffset = char.IsUpper(letter) ? 'A' : 'a';
                    }

                    letter = (letter + shift - letterOffset) > 0
                        ? (char)(((letter + shift - letterOffset) % alfabetCount) + letterOffset)
                        : (char)(((letter + shift + alfabetCount - letterOffset) % alfabetCount) + letterOffset);
                }

                buffer[i] = letter;
            }

            result = new string(buffer);

            return result;
        }

        // Лозунговый метод
        private static string SloganBased(string line, string key, bool encrypt = true)
        {
            string result = string.Empty;

            bool isRussianLetters = CheckRussianInLine(line);

            string alphabet = isRussianLetters
                ? "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"
                : "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            alphabet = string.Concat(key.ToUpper().Concat(alphabet.Except(key.ToUpper())).Distinct());

            foreach (char symbol in line)
            {
                if (encrypt)
                {
                    result += char.IsUpper(symbol)
                        ? alphabet[symbol - (isRussianLetters ? 'А' : 'A')]
                        : char.ToLower(alphabet[symbol - (isRussianLetters ? 'а' : 'a')]);
                }
                else
                {
                    result += char.IsUpper(symbol)
                        ? (char)((isRussianLetters ? 'А' : 'A') + alphabet.IndexOf(symbol))
                        : char.ToLower((char)((isRussianLetters ? 'А' : 'A') + alphabet.IndexOf(char.ToUpper(symbol))));
                }
            }

            return result;
        }

        // Полибианский квадрат
        private static string PolybianSquare(string line, bool encrypt = true)
        {
            string result = string.Empty;

            // Всегда будет false при дешифрации, т.к. на вход будет поступать набор цифер
            bool isRussianLetters = isRussianEnabled; // CheckRussianInLine(line);

            char[] square;

            if (isRussianLetters)
            {
                square = new char[] {
                    'А', 'Б', 'В', 'Г', 'Д',
                    'Е', 'Ё', 'Ж', 'З', 'И',
                    'Й', 'К', 'Л', 'М', 'Н',
                    'О', 'П', 'Р', 'С', 'Т',
                    'У', 'Ф', 'Х', 'Ц', 'Ч',
                    'Ш', 'Щ', 'Ъ', 'Ы', 'Ь',
                    'Э', 'Ю', 'Я', ' ', ' '
                };
            }
            else
            {
                square = new char[] {
                    'A', 'B', 'C', 'D', 'E',
                    'F', 'G', 'H', 'I', 'J',
                    'K', 'L', 'M', 'N', 'O',
                    'P', 'Q', 'R', 'S', 'T',
                    'U', 'V', 'W', 'X', 'Y',
                    'Z', ' ', ' ', ' ', ' '
                };
            }

            int upperLimit = isRussianLetters ? 27 : 20;
            int startLimit = isRussianLetters ? 2 : 0;

            if (encrypt)
            {
                foreach (char symbol in line)
                {
                    int index = Array.IndexOf(square, char.ToUpper(symbol));

                    int numLine = index / 5;
                    int numColumn = index % 5;

                    int newIndex = index > upperLimit
                        ? numColumn
                        : (numLine + 1) * 5 + numColumn;

                    numLine = newIndex / 5;
                    numColumn = newIndex % 5;

                    result += (numLine + 1).ToString() + (numColumn + 1).ToString();
                }
            }
            else
            {
                for (int i = 0; i < line.Length; i += 2)
                {
                    int numLine = int.Parse(line[i].ToString()) - 1;
                    int numColumn = int.Parse(line[i + 1].ToString()) - 1;

                    int index = numLine * 5 + numColumn;

                    char newChar = index < 5
                        ? square[upperLimit + numColumn - startLimit]
                        : square[(numLine - 1) * 5 + numColumn];

                    result += newChar;
                }
            }

            return result;
        }

        // Ситсема Трисемиуса
        private static string Trisemus(string line, string key, bool encrypt = true)
        {
            string result = string.Empty;

            bool isRussianLetters = CheckRussianInLine(line);

            string alphabet = isRussianLetters
                ? "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"
                : "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            char[] square = string.Concat(key.ToUpper().Concat(alphabet.Except(key.ToUpper())).Distinct()).ToCharArray();

            int upperLimit = isRussianLetters ? 26 : 19;
            int startLimit = isRussianLetters ? 2 : 1;

            foreach (char symbol in line)
            {
                char newChar;

                int index = Array.IndexOf(square, char.ToUpper(symbol));

                int numLine = index / 6;
                int numColumn = index % 6;
                
                if (encrypt)
                {
                    newChar = index > upperLimit
                        ? square[numColumn]
                        : square[(numLine + 1) * 6 + numColumn];

                    result += char.IsUpper(symbol)
                        ? char.ToUpper(newChar)
                        : char.ToLower(newChar);
                }
                else
                {
                    newChar = index < 6
                        ? square[upperLimit + numColumn - startLimit]
                        : square[(numLine - 1) * 6 + numColumn];

                    result += char.IsUpper(symbol)
                        ? char.ToUpper(newChar)
                        : char.ToLower(newChar);
                }

            }

            return result;
        }

        // Шифр Playfair
        private static string Playfair(string line, string key, bool encrypt = true)
        {
            string result = string.Empty;

            bool isRussianLetters = CheckRussianInLine(line);

            // Преобразование исходной строки
            string newLine = string.Empty;
            line = string.Concat(line.Replace(" ", string.Empty));

            char prevCh = '\0';
            int numChar = 0;
            foreach (char ch in line)
            {
                if (numChar == 1)
                {
                    numChar++;
                }
                else
                {
                    numChar--;
                    if (ch == prevCh)
                    {
                        newLine += isRussianLetters ? 'Я' : 'X';
                    }
                }

                newLine += ch;
                prevCh = ch;
            }

            if (newLine.Length % 2 != 0)
            {
                newLine += isRussianLetters ? 'Я' : 'X';
            }

            newLine = newLine.ToUpper();

            string alphabet = isRussianLetters
                ? "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"
                : "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            char[] square = string.Concat(key.ToUpper().Concat(alphabet.Except(key.ToUpper())).Distinct()).ToCharArray();

            for (int i = 0; i < newLine.Length; i+=2)
            {
                int rowFirst = 0, colFirst = 0, rowLast = 0, colLast = 0;
                char newCharFirst, newCharLast;

                bool firstFinded = false,
                     secondFinded = false;

                for (int j = 0; j < (square.Length / 5); j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (!firstFinded && square[j * 5 + k] == newLine[i])
                        {
                            firstFinded = true;

                            rowFirst = j;
                            colFirst = k;
                        }

                        if (!secondFinded && square[j * 5 + k] == newLine[i + 1])
                        {
                            secondFinded = true;

                            rowLast = j;
                            colLast = k;
                        }
                    }
                }

                if (rowFirst == rowLast)
                {
                    int newColFirst, newColLast;

                    if (encrypt)
                    {
                        newColFirst = (colFirst + 1) % 5;
                        newColLast = (colLast + 1) % 5;
                    }
                    else
                    {
                        newColFirst = colFirst - 1 >= 0 ? (colFirst - 1) % 5 : 4;
                        newColLast = colLast - 1 >= 0 ? (colLast - 1) % 5 : 4;
                    }

                    newCharFirst = square[rowFirst * 5 + newColFirst];
                    newCharLast = square[rowLast * 5 + newColLast];
                }
                else if (colFirst == colLast)
                {
                    int newRowFirst, newRowLast;

                    if (encrypt)
                    {
                        newRowFirst = (rowFirst + 1) % 5;
                        newRowLast = (rowLast + 1) % 5;
                    }
                    else
                    {
                        newRowFirst = rowFirst - 1 >= 0 ? (rowFirst- 1) % 5 : 4;
                        newRowLast = rowLast - 1 >= 0 ? (rowLast - 1) % 5 : 4;
                    }

                    newCharFirst = square[newRowFirst * 5 + colFirst];
                    newCharLast = square[newRowLast * 5 + colLast];
                }
                else
                {
                    newCharFirst = square[rowFirst * 5 + colLast];
                    newCharLast = square[rowLast * 5 + colFirst];
                }

                result += char.IsUpper(line[i])
                        ? char.ToUpper(newCharFirst)
                        : char.ToLower(newCharFirst);

                if (i + 1 < line.Length)
                {
                    result += char.IsUpper(line[i + 1])
                            ? char.ToUpper(newCharLast)
                            : char.ToLower(newCharLast);
                }
            }

            return result;
        }

        // Шифрация омофонами
        private static string Homophones(string line, bool encrypt = true)
        {
            string result = string.Empty;

            line = line.ToUpper().Replace(" ", string.Empty);

            // Всегда будет false при дешифрации, т.к. на вход будет поступать набор цифер
            bool isRussionLetter = isRussianEnabled; // CheckRussianInLine(line);

            // Заполнение таблицы омофонов
            var homophonesMap = new Dictionary<char[], string[]>
            {
                { new char[] { 'А', 'A' }, new string[] { "001", "002" } },
                { new char[] { 'Б', 'B' }, new string[] { "-03", "004" } },
                { new char[] { 'В', 'C' }, new string[] { "005", "006" } },
                { new char[] { 'Г', 'D' }, new string[] { "007", "008" } },
                { new char[] { 'Д', 'E' }, new string[] { "009", "010" } },
                { new char[] { 'Е', 'F' }, new string[] { "011", "012" } },
                { new char[] { 'Ё', 'G' }, new string[] { "013", "014" } },
                { new char[] { 'Ж', 'H' }, new string[] { "015", "016" } },
                { new char[] { 'З', 'I' }, new string[] { "017", "018" } },
                { new char[] { 'И', 'J' }, new string[] { "019", "020" } },
                { new char[] { 'Й', 'K' }, new string[] { "021", "022" } },
                { new char[] { 'К', 'L' }, new string[] { "023", "024" } },
                { new char[] { 'Л', 'M' }, new string[] { "025", "026" } },
                { new char[] { 'М', 'N' }, new string[] { "027", "028" } },
                { new char[] { 'Н', 'O' }, new string[] { "029", "030" } },
                { new char[] { 'О', 'P' }, new string[] { "031", "032" } },
                { new char[] { 'П', 'Q' }, new string[] { "033", "034" } },
                { new char[] { 'Р', 'R' }, new string[] { "035", "036" } },
                { new char[] { 'С', 'S' }, new string[] { "037", "038" } },
                { new char[] { 'Т', 'T' }, new string[] { "039", "040" } },
                { new char[] { 'У', 'U' }, new string[] { "041", "042" } },
                { new char[] { 'Ф', 'V' }, new string[] { "043", "044" } },
                { new char[] { 'Х', 'W' }, new string[] { "045", "046" } },
                { new char[] { 'Ц', 'X' }, new string[] { "047", "048" } },
                { new char[] { 'Ч', 'Y' }, new string[] { "049", "050" } },
                { new char[] { 'Ш', 'Z' }, new string[] { "051", "052" } },
                { new char[] { 'Щ' }, new string[] { "053", "054" } },
                { new char[] { 'Ъ' }, new string[] { "055", "056" } },
                { new char[] { 'Ы' }, new string[] { "057", "058" } },
                { new char[] { 'Ь' }, new string[] { "059", "060" } },
                { new char[] { 'Э' }, new string[] { "061", "062" } },
                { new char[] { 'Ю' }, new string[] { "063", "064" } },
                { new char[] { 'Я' }, new string[] { "065", "066" } }
            };

            for (int i = 0; i < line.Length; i++)
            {
                if (encrypt)
                {
                    char currentChar = line[i];

                    foreach (KeyValuePair<char[], string[]> kvp in homophonesMap)
                    {
                        if (kvp.Key.Contains(currentChar))
                        {
                            // Выбираем случайный шифр из массива значений
                            Random random = new Random();
                            int randomIndex = random.Next(0, kvp.Value.Length);
                            result += kvp.Value[randomIndex];
                            result += " ";
                        }
                    }
                }
                else
                {
                    string cipherValue = line.Substring(i, 3);

                    foreach (KeyValuePair<char[], string[]> kvp in homophonesMap)
                    {
                        if (kvp.Value.Contains(cipherValue))
                        {
                            result += kvp.Key[isRussionLetter ? 0 : 1];
                        }
                    }

                    i+=2;
                }
            }


            return result;
        }

        private static string Vigenera(string line, string key, bool encrypt = true)
        {
            string result = string.Empty;

            string newLine = line.ToUpper().Replace(" ", string.Empty);

            string newKey = key.ToUpper().Replace(" ", string.Empty);

            bool isRussianLetters = CheckRussianInLine(newLine);

            string alphabet = isRussianLetters
                ? "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"
                : "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int keyIndex = 0;
            while (newKey.Length < newLine.Length)
            {
                newKey += key.ToUpper()[keyIndex];
                keyIndex = (keyIndex + 1) % key.Length;
            }

            for (int i = 0; i < newLine.Length; i++)
            {
                int mIndex = alphabet.IndexOf(newLine[i]);
                int kIndex = alphabet.IndexOf(newKey[i]);
                
                char newChar = encrypt
                    ? alphabet[(mIndex + kIndex) % alphabet.Length]
                    : alphabet[(mIndex - kIndex + alphabet.Length) % alphabet.Length];

                result += char.IsUpper(line[i])
                        ? char.ToUpper(newChar)
                        : char.ToLower(newChar);
            }

            return result;
        }

        // -------------------- Шифраторы --------------------
        // Функция по шифрации методом Цезаря
        public static string EncryptionCaesarsCipher(string line)
        {
            string result;

            result = CaesarsCipher(line, 3);

            return result;
        }

        // Функция по шифрации лозунговым методом
        public static string EncryptionSloganBased(string line)
        {
            string result;

            string key = CheckRussianInLine(line) ? "Слоган" : "Slogan";

            result = SloganBased(line, key);

            return result;
        }

        // Функция по шифрации методом полибианского квадрата
        public static string EncryptionPolybianSquare(string line)
        {
            string result;

            result = PolybianSquare(line);

            return result;
        }

        // Функция по шифрации шифром Трисемуса
        public static string EncryptionTrisemus(string line)
        {
            string result;

            string key = CheckRussianInLine(line) ? "Дядина" : "Dyadina";

            result = Trisemus(line, key);

            return result;
        }

        // Функция по шифрации шифром Playfair
        public static string EncryptionPlayfair(string line)
        {
            string result;

            string key = CheckRussianInLine(line) ? "Дядина" : "Dyadina";

            result = Playfair(line, key);

            return result;
        }

        // Функция по шифрации методом системы омофонов
        public static string EncryptionHomophones(string line)
        {
            string result;

            result = Homophones(line);

            return result;
        }

        // Функция по шифрации шифром Виженера
        public static string EncryptionVigenera(string line)
        {
            string result;

            string key = CheckRussianInLine(line) ? "Дядина" : "Dyadina";

            result = Vigenera(line, key);

            return result;
        }

        // -------------------- Дешифраторы --------------------
        // Функция по дешифрации методом Цезаря
        public static string DecryptionCaesarsCipher(string line)
        {
            string result;

            result = CaesarsCipher(line, -3);

            return result;
        }

        // Функция по дешифрации лозунговым методом
        public static string DecryptionSloganBased(string line)
        {
            string result;

            string key = CheckRussianInLine(line) ? "Слоган" : "Slogan";

            result = SloganBased(line, key, false);

            return result;
        }

        // Функция по дешифрации методом полибианского квадрата
        public static string DecryptionPolybianSquare(string line)
        {
            string result;

            result = PolybianSquare(line, false);

            return result;
        }

        // Функция по дешифрации шифром Трисемуса
        public static string DecryptionTrisemus(string line)
        {
            string result;

            string key = CheckRussianInLine(line) ? "Дядина" : "Dyadina";

            result = Trisemus(line, key, false);

            return result;
        }

        // Функция по дешифрации шифром Playfair
        public static string DecryptionPlayfair(string line)
        {
            string result;

            string key = CheckRussianInLine(line) ? "Дядина" : "Dyadina";

            result = Playfair(line, key, false);

            return result;
        }

        // Функция по дешифрации методом системы омофонов
        public static string DecryptionHomophones(string line)
        {
            string result;

            result = Homophones(line, false);

            return result;
        }

        // Функция по дешифрации шифром Виженера
        public static string DecryptionVigenera(string line)
        {
            string result;

            string key = CheckRussianInLine(line) ? "Дядина" : "Dyadina";

            result = Vigenera(line, key, false);

            return result;
        }
    }
}
