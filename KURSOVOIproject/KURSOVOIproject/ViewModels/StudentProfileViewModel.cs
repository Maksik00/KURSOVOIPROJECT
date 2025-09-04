using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    public class StudentProfileViewModel : BaseViewModel
    {
        private readonly IStudentService _studentService;
        private readonly ISpecializationService _specService;

        private string _name = "Студент";
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _telNumber = "";
        public string TelNumber
        {
            get => _telNumber;
            set
            {
                if (_telNumber != value)
                {
                    _telNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _course;
        public int Course
        {
            get => _course;
            set
            {
                if (_course != value)
                {
                    _course = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _specializationName = "Не задана";
        public string SpecializationName
        {
            get => _specializationName;
            set
            {
                if (_specializationName != value)
                {
                    _specializationName = value;
                    OnPropertyChanged();
                }
            }
        }

        // Мы храним в модели студента строку SkillsString,
        // здесь удобное свойство для отображения
        private string _skillsDisplay = "";
        public string SkillsDisplay
        {
            get => _skillsDisplay;
            set
            {
                if (_skillsDisplay != value)
                {
                    _skillsDisplay = value;
                    OnPropertyChanged();
                }
            }
        }

        public StudentProfileViewModel(
            IStudentService studentService,
            ISpecializationService specService)
        {
            _studentService = studentService;
            _specService = specService;
            LoadStudentData();
        }

        private async void LoadStudentData()
        {
            int studentId = Preferences.Default.Get("CurrentStudentId", 0);
            if (studentId == 0) return;

            Student? student = await _studentService.GetByIdAsync(studentId);
            if (student == null) return;

            Name = student.Name;
            TelNumber = student.TelNumber;
            Course = student.Course;

            if (student.IdSpecialization != 0)
            {
                var spec = await _specService.GetByIdAsync(student.IdSpecialization);
                SpecializationName = spec?.Name ?? "—";
            }
            else
            {
                SpecializationName = "—";
            }

            // SkillsString может быть пустой или содержать "C#,Java,Python"
            SkillsDisplay = string.IsNullOrWhiteSpace(student.SkillsString)
                ? "—"
                : student.SkillsString;
        }
    }
}
