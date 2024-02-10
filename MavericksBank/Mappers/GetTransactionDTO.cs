using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Mappers
{
	public class GetTransactionDTO
	{
		TransactionDTO DTO;
		public GetTransactionDTO(Transactions transaction)
		{
			DTO = new TransactionDTO();
			DTO.TransactionID = transaction.TransactionID;
			DTO.TransactionType = transaction.TransactionType;
			DTO.TransactionDate = transaction.TransactionDate;
			DTO.Status = transaction.Status;
			DTO.SAccountID = transaction.SAccountID;
			DTO.Description = transaction.Description;
			DTO.BeneficiaryAccountNumber = transaction.BeneficiaryAccountNumber;
			DTO.Amount = transaction.Amount;
		}
		public TransactionDTO GetDTO()
		{
			return DTO;
		}
	}
}

