using System;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface ITokenService
	{
        public Task<string> GenerateToken(LoginDTO user);
    }
}

