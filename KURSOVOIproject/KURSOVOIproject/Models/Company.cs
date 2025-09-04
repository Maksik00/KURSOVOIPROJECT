using System;
using System.Collections.Generic;

namespace KURSOVOIproject.Models
{
    public class Company
    {
        public int Id { get; set; }
        /// <summary>
        /// Путь к файлу аватарки в локальном хранилище
        /// </summary>
        public string AvatarPath { get; set; } = string.Empty;

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название компании обязательно.");
                _name = value.Trim();
            }
        }

        private string _city = string.Empty;
        public string City
        {
            get => _city;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Город обязателен.");
                _city = value.Trim();
            }
        }

        private string _street = string.Empty;
        public string Street
        {
            get => _street;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Улица обязательна.");
                _street = value.Trim();
            }
        }

        private string _building = string.Empty;
        public string Building
        {
            get => _building;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Номер здания обязателен.");
                _building = value.Trim();
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new ArgumentException("Контактный email обязателен и должен содержать '@'.");
                _email = value.Trim();
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new ArgumentException("Пароль должен быть не менее 6 символов.");
                _password = value;
            }
        }

        public ICollection<Internship> Internships { get; set; } = new List<Internship>();
    }
}
