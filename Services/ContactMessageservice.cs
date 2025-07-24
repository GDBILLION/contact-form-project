using ContactFormApi.DTOs;
using ContactFormApi.Models;
using ContactFormApi.Repositories;

namespace ContactFormApi.Services
{
    public class ContactMessageservice : IContactMessageservice
    {
        private readonly IContactMessageRepository _repository;
        public ContactMessageservice(IContactMessageRepository repository)
        {
            _repository = repository;
        }


        public async Task<(bool success, string message)> ProcessContactFormAsync(ContactMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Message))
            {
                return await Task.FromResult<(bool, string)>((false, "All fields are required."));
            }
            var message = new ContactMessage
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message,
                TimeStamp = dto.Timestamp
            };
            await _repository.AddMessageAsync(message);

            return (true, "Message received.");
        }
    }
}

