using To_Do_List.Dto.AccountDto;
using To_Do_List.Models;

namespace To_Do_List.Interface
{
    public interface IUserRepositroy
    {
        Task<int> SignUp(User user);
        Task<UserViewDto> GetByIdDto(int userId);
        Task<UserViewDto> GetByUsernameDto(string username);
        Task<User> GetByUsername(string username);
        Task<int> UpdateUser(UserUpdateDto userUpdate, User user);
        Task<int> DeleteUser(User user);
    }
}
