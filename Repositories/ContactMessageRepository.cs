using ContactFormApi.Data;
using ContactFormApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ContactFormApi.Repositories
{
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly AppDbContext _context;

        public ContactMessageRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddMessageAsync(ContactMessage message)
        {
            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactMessage>> GetAllMessagesAsync()
        {
            return await _context.ContactMessages
                .OrderByDescending(m => m.TimeStamp)
                .ToListAsync();
        }
    }
}
