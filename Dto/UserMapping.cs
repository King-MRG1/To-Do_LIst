using To_Do_List.Dto.AccountDto;
using To_Do_List.Models;

namespace To_Do_List.Dto
{
    public static class UserMapping
    {
        public static User ToUser(this SignUpDto signUpDto)
        {
            return new User
            {
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,
                Username = signUpDto.Username,
                Email = signUpDto.Email,
                Password = signUpDto.Password
            };
        }

        public static UserViewDto ToUserDto(this User user)
        {
            return new UserViewDto
            {
                UserId = user.UserId,
                Name = $"{user.FirstName} {user.LastName}",
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
