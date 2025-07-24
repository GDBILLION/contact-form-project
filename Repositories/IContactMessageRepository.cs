using ContactFormApi.Models;

namespace ContactFormApi.Repositories
{
    public interface IContactMessageRepository
    {
        Task AddMessageAsync(ContactMessage message);
        Task<IEnumerable<ContactMessage>> GetAllMessagesAsync();
    }
}
