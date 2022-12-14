using EmailService.Data.Entities;
using EmailService.DTOS;
using System.Collections.Generic;

namespace EmailService.Data.EmailPool
{
    public interface IEmailPoolRepository
    {
        /// <summary>
        /// Adds the email pool into the database.
        /// </summary>
        /// <param name="emailPoolInput"><see cref="EmailPoolTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailPoolTable> AddEmailPool(EmailPoolTable emailPoolInput);

        /// <summary>
        /// Updates the email pool into the database.
        /// </summary>
        /// <param name="emailPoolInput"><see cref="EmailPoolTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailPoolTable> UpdateEmailPool(EmailPoolTable emailPoolInput);

        /// <summary>
        /// Adds the email pool into the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<string> DeleteEmailPool(string poolID);

        /// <summary>
        /// Gets the email pool by pool name.
        /// </summary>
        /// <param name="poolName">Pool name</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailPoolTable> GetEmailPoolByName(string poolName);

        /// <summary>
        /// Gets all the email pools.
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailPoolTable>> GetEmailPools();

        /// <summary>
        /// Gets all the email pool names
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailPoolTable>> GetEmailPoolNames();

        /// <summary>
        /// Checks if the Pool ID exists in the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<string> CheckIfEmailPoolIDIsValid(string poolID);

        /// <summary>
        /// Checks if Pool ID and Name exists in the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <param name="poolName">Pool Name</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<string> CheckIfEmailPoolIDNameValid(string poolID, string poolName);
    }
}
