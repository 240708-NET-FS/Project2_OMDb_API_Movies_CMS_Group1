namespace OMDbProject.Services
    public interface IUserService
    {
        Task<User> RegisterUserAsync(UserRegistrationDTO userRegistrationDTO);
        Task<User> GetUserByIdAsync(int id);
    }
