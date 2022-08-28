using Sagar.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sagar.BookStore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguage();
    }
}