using Microsoft.EntityFrameworkCore;
using To_Do_List.Dto;
using To_Do_List.Dto.AccountDto;
using To_Do_List.Interface;
using To_Do_List.Models;

namespace To_Do_List.Repositroy
{
    public class UserRepository : IUserRepositroy
    {
        private readonly ListContext _context;
        public UserRepository(ListContext context)
        {
            _context = context;
        }

        public async Task<int> SignUp(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<UserViewDto> GetByIdDto(int userId)
        {
            return await _context.Users.Where(u => u.UserId == userId).Select(u=>u.ToUserDto()).SingleOrDefaultAsync();
        }

        public async Task<UserViewDto> GetByUsernameDto(string username)
        {
            return await _context.Users.Where(u => u.Username == username).Select(u => u.ToUserDto()).SingleOrDefaultAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.Where(u => u.Username == username).SingleOrDefaultAsync();
        }

        public async Task<int> UpdateUser(UserUpdateDto userUpdate, User user)
        {
            user.FirstName = userUpdate.FirstName;
            user.LastName = userUpdate.LastName;
            user.Username = userUpdate.Username;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync();
        }
    }
}
