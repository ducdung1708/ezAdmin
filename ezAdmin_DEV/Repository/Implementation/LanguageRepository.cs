using Models.DBContext;
using Models.EntityModels;
using Models.Models.Response;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class LanguageRepository : RepositoryBase<Language>, ILanguageRepository
    {
        private readonly ezSQLDBContext _dbcontext;

        public LanguageRepository(ezSQLDBContext dbcontext) : base( dbcontext )
        {
            _dbcontext = dbcontext; 
        }
        public Dictionary<string, string>  GetLanguage(string language)
        {
            var languageResult = _dbcontext.LanguageKeywords
                .Where(s => s.LanguageCode == language)
                .Join(_dbcontext.Keywords, lkw => lkw.KeywordId, kw => kw.KeywordId, (LanguageKeyword, Keyword) => new 
                { 
                    Keyword, LanguageKeyword 
                })
                .Select(s => new LanguageLocaleResponse
                {
                    KeywordCode = s.Keyword.KeywordCode,
                    TranslateKeyword = s.LanguageKeyword.TranslateKeyword
                })
                .OrderBy(s => s.KeywordCode)
                .ToDictionary(s => s.KeywordCode, s => s.TranslateKeyword);
            return languageResult;
        }
    }
}
