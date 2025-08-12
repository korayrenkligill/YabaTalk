using YabaTalk.Application.Dtos;
using YabaTalk.Application.Dtos.Auth;

namespace YabaTalk.Application.Interfaces.Service
{
    public interface IUserService
    {
        Task<ResponseDto<UserRespDto>> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<ResponseDto<UserRespDto>> GetByPhoneAsync(string phoneE164, CancellationToken cancellationToken = default);
        Task<ResponseDto<IEnumerable<UserRespWithoutDetailDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ResponseDto<UserRespDto>> AddAsync(UserReqDto dto, CancellationToken cancellationToken = default);
        Task<ResponseDto<UserRespWithoutDetailDto>> UpdateAsync(UpdateUserDto dto, CancellationToken cancellationToken = default);
        //Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
