using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EmployeeManager.Domain.Enums;
using System.Text.RegularExpressions;

namespace EmployeeManager.Domain.Entities
{
    public class EmployeeEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        public string FirstName { get; private set; }

        [Required]
        public string LastName { get; private set; }

        [Required]
        [EmailAddress]
        public string Email { get; private set; }

        [Required]
        public string DocumentNumber { get; private set; } // Must be unique

        [Required]
        public int Age { get; private set; } 

        [Required]
        public JobLevelEnum JobLevel { get; set; }

        public List<string> PhoneNumbers { get; private set; } = new();

        public Guid? ManagerId { get; private set; }
        public EmployeeEntity? Manager { get; private set; }

        // Store hashed password and salt for security
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }

        public DateTime? CreatedAt { get; private set; }
        
        public DateTime? UpdatedAt { get; private set; }

        public EmployeeEntity() { }

        public EmployeeEntity(string firstName, string lastName, string email, string documentNumber)
        {
            /*FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            DocumentNumber = documentNumber ?? throw new ArgumentNullException(nameof(documentNumber));*/

            //(PasswordHash, PasswordSalt) = GeneratePasswordHash(plainPassword);
        }


        public bool IsValidPlainPassword(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                return false;

            if (plainPassword.Length < 8 || plainPassword.Length > 12)
                return false;

            // Veririca se contém pelo menos uma letra maiúscula
            if (!Regex.IsMatch(plainPassword, @"(?=.*[A-Z])"))
                return false;

            // Verificar se contém pelo menos uma letra minúscula
            if (!Regex.IsMatch(plainPassword, @"(?=.*[a-z])"))
                return false;

            // Verificar se contém pelo menos um número
            if (!Regex.IsMatch(plainPassword, @"(?=.*\d)"))
                return false;

            // 6. Verificar se contém pelo menos um caractere especial
            if (!Regex.IsMatch(plainPassword, @"(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\-])"))
                return false;

            return true;
        }

        public void GeneratePassword(string plainPassword)
        {
            if (!IsValidPlainPassword(plainPassword))
                throw new ArgumentException("Password not permited");

            (PasswordHash, PasswordSalt) = GeneratePasswordHash(plainPassword);
        }

        public void AddPhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                PhoneNumbers.Add(phoneNumber);
        }

        public void AssignManager(EmployeeEntity manager)
        {
            if (manager == null || manager.Id == this.Id)
                throw new ArgumentException("Invalid manager");

            ManagerId = manager.Id;
            Manager = manager;
        }

        private (string hash, string salt) GeneratePasswordHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

            using var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            string salt = Convert.ToBase64String(saltBytes);

            using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, saltBytes, 100_000);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            string hash = Convert.ToBase64String(hashBytes);

            return (hash, salt);
        }

        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");
            if (string.IsNullOrWhiteSpace(storedHash))
                throw new ArgumentException("Stored hash cannot be empty");
            if (string.IsNullOrWhiteSpace(storedSalt))
                throw new ArgumentException("Stored salt cannot be empty");

            // Converte o salt armazenado de volta para bytes
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            // Aplica a mesma função de derivação de chave na senha fornecida pelo usuário
            // e no salt armazenado.
            // É CRÍTICO usar o MESMO número de iterações (100_000)
            using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, saltBytes, 100_000);
            byte[] hashedPasswordBytes = pbkdf2.GetBytes(32); // Usar o mesmo tamanho de hash (32 bytes)

            // Converte o hash recém-gerado para string Base64
            string newHash = Convert.ToBase64String(hashedPasswordBytes);

            // Compara o hash recém-gerado com o hash armazenado
            // Recomenda-se usar uma comparação segura para evitar ataques de tempo (timing attacks)
            return SlowEquals(newHash, storedHash);
        }

        private bool SlowEquals(string a, string b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }
    }

}