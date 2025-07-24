using ContactFormApi.DTOs;

namespace ContactFormApi.Services
{
    public interface IContactMessageservice
    {
        Task<(bool success, string message)> ProcessContactFormAsync(ContactMessageDto dto);
    }
}
