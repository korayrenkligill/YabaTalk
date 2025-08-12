using Microsoft.EntityFrameworkCore.Design.Internal;
using YabaTalk.Application.Dtos;
using YabaTalk.Application.Dtos.Auth;
using YabaTalk.Application.Interfaces;
using YabaTalk.Application.Interfaces.Repository;
using YabaTalk.Application.Interfaces.Service;
using YabaTalk.Domain.Entity;

namespace YabaTalk.Infrastructure.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _ur;
        private readonly TokenProvider _tokenProvider;
        private readonly IActiveUserAccessor _activeUser;
        public UserService(IUserRepository ur, TokenProvider tokenProvider, IActiveUserAccessor activeUser)
        {
            _ur = ur;
            _tokenProvider = tokenProvider;
            _activeUser = activeUser;
        }

        public async Task<ResponseDto<UserRespDto>> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var current = await _ur.GetByIdAsync(id, cancellationToken);
            if (current == null)
            {
                return ResponseDto<UserRespDto>.Fail("Kullanıcı Bulunamadı");
            }

            var tokenUser = new TokenUserDto
            {
                Id = current.Id,
                PhoneE164 = current.PhoneE164
            };
            var token = _tokenProvider.Create(tokenUser);

            var user = new UserRespDto
            {
                Id = current.Id,
                AccessToken = token,
                FirstName = current.FirstName,
                LastName = current.LastName,
                PhoneE164 = current.PhoneE164,
                CreatedAt = current.CreatedAt,
            };

            if (!string.IsNullOrEmpty(current.AvatarUrl))
            {
                user.AvatarUrl = current.AvatarUrl;
            }
            if (!string.IsNullOrEmpty(current.About))
            {
                user.About = current.About;
            }

            return ResponseDto<UserRespDto>.Ok(user, "Kullanıcı başarıyla getirildi");
        }
        public async Task<ResponseDto<UserRespDto>> GetByPhoneAsync(string phoneE164, CancellationToken cancellationToken = default)
        {
            var current = await _ur.GetByPhoneAsync(phoneE164, cancellationToken);
            if (current == null)
            {
                return ResponseDto<UserRespDto>.Fail("Kullanıcı Bulunamadı");
            }

            var tokenUser = new TokenUserDto
            {
                Id = current.Id,
                PhoneE164 = current.PhoneE164
            };
            var token = _tokenProvider.Create(tokenUser);

            var user = new UserRespDto
            {
                Id = current.Id,
                AccessToken = token,
                FirstName = current.FirstName,
                LastName = current.LastName,
                PhoneE164 = current.PhoneE164,
                CreatedAt = current.CreatedAt,
            };

            if (!string.IsNullOrEmpty(current.AvatarUrl))
            {
                user.AvatarUrl = current.AvatarUrl;
            }
            if (!string.IsNullOrEmpty(current.About))
            {
                user.About = current.About;
            }

            return ResponseDto<UserRespDto>.Ok(user, "Kullanıcı başarıyla getirildi");
        }
        public async Task<ResponseDto<UserRespDto>> AddAsync(UserReqDto dto, CancellationToken cancellationToken = default)
        {
            var current = await _ur.GetByPhoneAsync(dto.PhoneE164, cancellationToken);
            if (current != null)
            {
                return ResponseDto<UserRespDto>.Fail("Bu numaraya sahip bir kullanıcı var");
            }
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneE164 = dto.PhoneE164,
            };

            if (!string.IsNullOrEmpty(dto.AvatarUrl))
            {
                user.AvatarUrl = dto.AvatarUrl;
            }
            if (!string.IsNullOrEmpty(dto.About))
            {
                user.About = dto.About;
            }

            await _ur.AddAsync(user, cancellationToken);

            var tokenUser = new TokenUserDto
            {
                Id = user.Id,
                PhoneE164 = user.PhoneE164
            };
            var token = _tokenProvider.Create(tokenUser);

            var newUser = new UserRespDto
            {
                Id = user.Id,
                AccessToken = token,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneE164 = user.PhoneE164,
                AvatarUrl = user.AvatarUrl,
                About = user.About,
                CreatedAt = user.CreatedAt,
            };

            return ResponseDto<UserRespDto>.Ok(newUser, "Kullanıcı başarıyla eklendi");
        }
        public async Task<ResponseDto<IEnumerable<UserRespWithoutDetailDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _ur.GetAllAsync(cancellationToken);

            var userDtos = users.Select(u => new UserRespWithoutDetailDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneE164 = u.PhoneE164,
                AvatarUrl = u.AvatarUrl,
                About = u.About,
                CreatedAt = u.CreatedAt,
            }).ToList();

            return ResponseDto<IEnumerable<UserRespWithoutDetailDto>>.Ok(userDtos, "Kullanıcılar listelendi");
        }
        public async Task<ResponseDto<UserRespWithoutDetailDto>> UpdateAsync(UpdateUserDto dto, CancellationToken cancellationToken = default)
        {
            var current = _activeUser.UserId!;
            var currentUser = await _ur.GetByIdAsync(current, cancellationToken);
            if(currentUser == null)
            {
                return ResponseDto<UserRespWithoutDetailDto>.Fail("Kullanıcı bulunamadı");
            }

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
            {
                currentUser.FirstName = dto.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(dto.LastName))
            {
                currentUser.LastName = dto.LastName;
            }
            if (!string.IsNullOrWhiteSpace(dto.AvatarUrl))
            {
                currentUser.AvatarUrl = dto.AvatarUrl;
            }
            if (!string.IsNullOrWhiteSpace(dto.About))
            {
                currentUser.About = dto.About;
            }

            await _ur.UpdateAsync(currentUser, cancellationToken);

            var updatedUser = new UserRespWithoutDetailDto
            {
                Id = currentUser.Id,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                PhoneE164 = currentUser.PhoneE164,
                AvatarUrl = currentUser.AvatarUrl,
                About = currentUser.About,
                CreatedAt = currentUser.CreatedAt,
            };

            return ResponseDto<UserRespWithoutDetailDto>.Ok(updatedUser,"Kullanıcı başarıyla güncellendi");
        }
    }
}
