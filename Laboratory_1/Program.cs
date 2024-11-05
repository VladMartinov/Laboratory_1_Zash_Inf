using System;

namespace Laboratory_1
{
    internal class Program
    {
        static void Main()
        {
            string encrypted;
            string decrypted;

            Console.WriteLine("Введите значение для шифрации/дешифрации:");
            string line = Console.ReadLine();

            // Шфирация методом Цезаря
            Console.WriteLine("\nШифр Цезаря:");
            encrypted = Encryptor.EncryptionCaesarsCipher(line);
            decrypted = Encryptor.DecryptionCaesarsCipher(encrypted);
            Console.WriteLine("Шифрация: " + encrypted);
            Console.WriteLine("Дешифрация: " + decrypted);

            // Шфир Лозунгового
            Console.WriteLine("\nШифр лозунгового:");
            encrypted = Encryptor.EncryptionSloganBased(line);
            decrypted = Encryptor.DecryptionSloganBased(encrypted);
            Console.WriteLine("Шифрация: " + encrypted);
            Console.WriteLine("Дешифрация: " + decrypted);

            // Шфирация методом полибианского квадрата
            Console.WriteLine("\nШифр полибианского квадрата:");
            encrypted = Encryptor.EncryptionPolybianSquare(line);
            decrypted = Encryptor.DecryptionPolybianSquare(encrypted);
            Console.WriteLine("Шифрация: " + encrypted);
            Console.WriteLine("Дешифрация: " + decrypted);

            // Шфирация методом системы Трисемиуса
            Console.WriteLine("\nШифр системы Трисемиуса:");
            encrypted = Encryptor.EncryptionTrisemus(line);
            decrypted = Encryptor.DecryptionTrisemus(encrypted);
            Console.WriteLine("Шифрация: " + encrypted);
            Console.WriteLine("Дешифрация: " + decrypted);

            // Шфирация методом Playfair
            Console.WriteLine("\nШифр Playfair:");
            encrypted = Encryptor.EncryptionPlayfair(line);
            decrypted = Encryptor.DecryptionPlayfair(encrypted);
            Console.WriteLine("Шифрация: " + encrypted);
            Console.WriteLine("Дешифрация: " + decrypted);

            // Шфирация методом системы омофонов
            Console.WriteLine("\nШифр системы омофонов:");
            encrypted = Encryptor.EncryptionHomophones(line);
            decrypted = Encryptor.DecryptionHomophones(encrypted);
            Console.WriteLine("Шифрация: " + encrypted);
            Console.WriteLine("Дешифрация: " + decrypted);

            // Шфирация методом Виженера
            Console.WriteLine("\nШифр Виженера:");
            encrypted = Encryptor.EncryptionVigenera(line);
            decrypted = Encryptor.DecryptionVigenera(encrypted);
            Console.WriteLine("Шифрация: " + encrypted);
            Console.WriteLine("Дешифрация: " + decrypted);

            Console.WriteLine();
        }
    }
}
