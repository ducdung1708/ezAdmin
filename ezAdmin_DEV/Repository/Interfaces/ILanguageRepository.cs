using Models.EntityModels;

namespace Repository.Interfaces
{
    public interface ILanguageRepository : IRepositoryBase<Language>
    {
        Dictionary<string, string> GetLanguage(string languageCode);
    }
}
