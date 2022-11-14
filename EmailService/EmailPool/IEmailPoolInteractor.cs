using EmailService.DTOS;

namespace EmailService.EmailPool
{
    public interface IEmailPoolInteractor
    {
        /// <summary>
        /// Adds the email pool to the table.
        /// </summary>
        /// <param name="emailPoolInput"><see cref="EmailPoolDto"/> model</param>
        /// <returns><<see cref="EmailResponseDto{T}"/>/returns>
        EmailResponseDto<EmailPoolDto> AddEmailPool(EmailPoolDto emailPoolInput);
    }
}
