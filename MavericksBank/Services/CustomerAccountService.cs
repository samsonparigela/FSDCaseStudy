using System;
using System.Data;
using System.Security.Cryptography;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MavericksBank.Services
{
	public class CustomerAccountService:ICustomerAccountService
	{
        private readonly ILogger<CustomerAccountService> _logger;
        private readonly IRepository<Transactions, int> _TransacRepo;
        private readonly IRepository<Accounts, int> _AccRepo;
        private readonly IRepository<Banks, int> _BankRepo;
        private readonly IRepository<Branches, string> _BranchRepo;
        private readonly IRepository<Beneficiaries, int> _BenifRepo;

        public CustomerAccountService(ILogger<CustomerAccountService> logger,
            IRepository<Transactions, int> TransacRepo, IRepository<Accounts, int> AccRepo,
            IRepository<Beneficiaries, int> BenifRepo, IRepository<Banks, int> BankRepo,
            IRepository<Branches, string> BranchRepo)
        {
            _logger = logger;
            _TransacRepo = TransacRepo;
            _AccRepo = AccRepo;
            _BenifRepo = BenifRepo;
            _BankRepo = BankRepo;
            _BranchRepo = BranchRepo;

        }

        public async Task<AccountsCreateDTO> OpenAccount(AccountsCreateDTO account)
        {
            var accounts = await _AccRepo.GetAll();
            var account2 = accounts.Where(a => a.AccountNumber == account.AccountNumber);
            var myAccount = new RegisterToAccount(account).GetAccount();



            await _AccRepo.Add(myAccount);

            var myBeneficiaryAccount = new Beneficiaries();
            myBeneficiaryAccount.BeneficiaryAccountNumber = myAccount.AccountNumber;
            myBeneficiaryAccount.BeneficiaryName = "Self";
            myBeneficiaryAccount.CustomerID = myAccount.CustomerID;
            myBeneficiaryAccount.IFSCCode = myAccount.IFSCCode;
            _logger.LogInformation("Account Created");
            await _BenifRepo.Add(myBeneficiaryAccount);

            return account;

        }

        public async Task<Accounts> CloseAccount(int key)
        {
            var account = await _AccRepo.GetByID(key);
            if(account.Status== "Account Closing Approved")
            {
                if (account.Balance != 0)
                    throw new AccountDeletionException($"There is of Balance of {account.Balance} " +
                        $"in the account. Please withdraw or transfer the amount before closing");
                var benif = await _BenifRepo.GetByID(account.AccountNumber);
                account = await _AccRepo.Delete(key);
                benif = await _BenifRepo.Delete(benif.BeneficiaryAccountNumber);
                _logger.LogInformation($"Successfully Deleted Account : {key}");
            }
            else
            {
                account.Status = "Close Request";
                _logger.LogInformation($"Request submitted to close Account : {key}");
                await _AccRepo.Update(account);
            }
            return account;
        }

        public async Task<AccountsUpdateDTO> EditAccount(AccountsUpdateDTO accountDTO)
        {
            var account = await _AccRepo.GetByID(accountDTO.ID);
            account.AccountNumber = accountDTO.AccountNumber;
            account.IFSCCode = accountDTO.IFSCCode;
            account.AccountType = accountDTO.AccountType;
            await _AccRepo.Update(account);
            _logger.LogInformation($"Successfully Updated Account with ID : {account.AccountNumber}");
            return accountDTO;
        }

        public async Task<Accounts> ViewAccountByID(int key)
        {
            var account = await _AccRepo.GetByID(key);
            _logger.LogInformation("Successfully Retrieved Account");
            return account;
        }

        //public async Task<List<Transactions>> ViewAllTransactionBetweenDates(DateTime date1,DateTime date2,int ID)
        //{
        //    var transactions = await ViewAllYourTransactions(ID);
        //    List<Transactions> transacBetweenDates = new List<Transactions>();
        //    foreach(Transactions transac in transactions)
        //    {
        //        if(transac.TransactionDate>=date1 && transac.TransactionDate<= date2)
        //        transacBetweenDates.Add(transac);
        //    }

        //    _logger.LogInformation($"All transactions retrieved between {date1} and {date2}");
        //    return transacBetweenDates;

        //}

        public async Task<List<TransactionDTO>> ViewAllYourTransactions(int CID)
        {
            var accounts = await ViewAllYourAccounts(CID);
            var transactions = await _TransacRepo.GetAll();

            List<int> AccountID = new List<int>();
            List<List<Transactions>> AllTransactions = new List<List<Transactions>>();

            foreach (Accounts acc in accounts)
            {
                AccountID.Add(acc.AccountNumber);
                AllTransactions.Add(transactions.Where(t => t.SAccountID == acc.AccountNumber).ToList());
                //_logger.LogInformation($"{AllTransactions}");
            }
            //var transacList = from listOfNumList in AllTransactions
            //                  from value in listOfNumList
            //              select value;
            var transacList = AllTransactions.SelectMany(x => x).ToList();
            var transactionList = (List<Transactions>)transacList;
            List<TransactionDTO> DTOList = new List<TransactionDTO>(); 
            foreach(Transactions transac in transactionList)
            {
                var DTO = new GetTransactionDTO(transac).GetDTO();
                DTOList.Add(DTO);
            }
            return DTOList;
        }

        public async Task<List<TransactionDTO>> ViewAllTransactionsByAccount(int AID)
        {
            var transactions = await _TransacRepo.GetAll();
            var transacByAccount = transactions.Where(t => t.SAccountID == AID).ToList();

            List<TransactionDTO> DTOList = new List<TransactionDTO>();
            foreach (Transactions transac in transacByAccount)
            {
                var DTO = new GetTransactionDTO(transac).GetDTO();
                DTOList.Add(DTO);
            }
            return DTOList;
        }

        public async Task<List<TransactionDTO>> ViewAllTransactionsMadeToAnAccount(int AID,int CID)
        {
            var transactions = await ViewAllYourTransactions(CID);
            List<TransactionDTO> transacDoneToAcc = new List<TransactionDTO>();
            foreach (TransactionDTO transac in transactions)
            {
                if (transac.BeneficiaryAccountNumber == AID)
                    transacDoneToAcc.Add(transac);
            }
            return transacDoneToAcc;
        }
        public async Task<List<Accounts>> ViewAllYourAccounts(int ID)
        {
            var accounts = await _AccRepo.GetAll();
            accounts = accounts.Where(d => d.CustomerID == ID).ToList();
            _logger.LogInformation($"Successfully Retrieved all accounts of Customer : {ID}");
            return accounts;
        }

        public async Task<List<Banks>> ViewAllBanks()
        {
            var banks = await _BankRepo.GetAll();
            _logger.LogInformation($"Successfully Retrieved all Banks");
            return banks;
        }

        public async Task<List<Branches>> ViewBankBranches(int BID)
        {
            var branches = await _BranchRepo.GetAll();
            var myBranches = branches.Where(b => b.BankID == BID).ToList();
            _logger.LogInformation($"Successfully Retrieved all Branches");
            return myBranches;
        }

        public async Task<List<TransactionDTO>> ViewAllTransactionsInTheLastMonth(int CID)
        {
            var currentMonth = DateTime.Now.Month;
            var transactions = await ViewAllYourTransactions(CID);
            List<TransactionDTO> transacDoneLastMonth = new List<TransactionDTO>();
            foreach (TransactionDTO transac in transactions)
            {
                if (transac.TransactionDate.Month<currentMonth)
                    transacDoneLastMonth.Add(transac);
            }
            return transacDoneLastMonth;
        }

        public async Task<List<TransactionDTO>> ViewAllTransactionsInThisMonth(int CID)
        {
            var currentMonth = DateTime.Now.Month;
            var transactions = await ViewAllYourTransactions(CID);
            List<TransactionDTO> transacDoneThisMonth = new List<TransactionDTO>();
            foreach (TransactionDTO transac in transactions)
            {
                if (transac.TransactionDate.Month == currentMonth)
                    transacDoneThisMonth.Add(transac);
            }
            return transacDoneThisMonth;
        }



        public async Task<List<TransactionDTO>> ViewLastNTransactions(int ID,int n)
        {
            int c = 0;
            var transactions = await ViewAllYourTransactions(ID);
            var sortedTransac = transactions.OrderByDescending(t => t.TransactionDate).ToList();
            List<TransactionDTO> LastMadeTransacs = new List<TransactionDTO>();
            foreach(TransactionDTO transac in sortedTransac)
            {
                LastMadeTransacs.Add(transac);
                c += 1;
                if (c == n)
                    break;
            }

            return LastMadeTransacs;
        }
    }
}

