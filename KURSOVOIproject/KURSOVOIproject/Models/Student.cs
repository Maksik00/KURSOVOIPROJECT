using System;
using System.Collections.Generic;

namespace KURSOVOIproject.Models
{
    public class Student
    {
        public int Id { get; set; }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя обязательно.");
                _name = value.Trim();
            }
        }

        private string _telNumber = string.Empty;
        public string TelNumber
        {
            get => _telNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.StartsWith("+") || value.Length < 8)
                    throw new ArgumentException("Телефон должен начинаться с '+' и содержать не менее 8 символов.");
                _telNumber = value.Trim();
            }
        }

        private int _course;
        public int Course
        {
            get => _course;
            set
            {
                if (value < 1 || value > 6)
                    throw new ArgumentException("Курс должен быть от 1 до 6.");
                _course = value;
            }
        }

        public int IdSpecialization { get; set; }
        public Specialization Specialization { get; set; } = null!;

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new ArgumentException("Пароль должен содержать минимум 6 символов.");
                _password = value;
            }
        }

        /// <summary>
        /// Навыки студента, разделённые запятой (например: "C#,Java,Python").
        /// </summary>
        public string SkillsString { get; set; } = string.Empty;

        public ICollection<ApplicationEntity> Applications { get; set; } = new List<ApplicationEntity>();
    }
}
