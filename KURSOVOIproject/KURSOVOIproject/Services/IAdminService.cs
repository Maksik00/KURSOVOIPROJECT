namespace KURSOVOIproject.Services
{
    /// <summary>
    /// Интерфейс для работы с административными операциями (удаление/блокировка пользователей, генерация статистики и т. д.)
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Блокирует студента по его Id.
        /// </summary>
        Task BlockStudentAsync(int studentId);

        /// <summary>
        /// Блокирует компанию по ее Id.
        /// </summary>
        Task BlockCompanyAsync(int companyId);

        /// <summary>
        /// Возвращает статистику по стажировкам в разрезе специализаций.
        /// </summary>
        Task<Dictionary<string, int>> GetInternshipsCountBySpecializationAsync();

        /// <summary>
        /// Возвращает топ N самых активных компаний.
        /// </summary>
        Task<List<(int CompanyId, string CompanyName, int InternshipCount)>> GetTopCompaniesByInternshipCountAsync(int topN);

        // TODO: по ходу разработки можно добавить методы удаления стажировок, просмотра списка всех пользователей и т. д.
    }
}
