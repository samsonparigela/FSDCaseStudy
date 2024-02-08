using System.Numerics;
using MavericksBank.Contexts;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Repository;
using MavericksBank.Services;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

        builder.Services.AddScoped<IRepository<Customer, int>, CustomerRepo>();
        builder.Services.AddScoped<IRepository<Accounts, int>, AccountsRepo>();
        builder.Services.AddScoped<IRepository<BankEmployee, int>, BankEmployeeRepo>();
        builder.Services.AddScoped<IRepository<Accounts, int>, AccountsRepo>();
        builder.Services.AddScoped<IRepository<Banks, int>, BanksRepo>();
        builder.Services.AddScoped<IRepository<Branches, string>, BranchesRepo>();
        builder.Services.AddScoped<IRepository<Loan, int>, LoanRepo>();
        builder.Services.AddScoped<IRepository<Beneficiaries, int>, BeneficiariesRepo>();
        builder.Services.AddScoped<IRepository<LoanPolicies, int>, LoanPoliciesRepo>();
        builder.Services.AddScoped<IRepository<Users, string>, UsersRepo>();
        builder.Services.AddScoped<IRepository<Transactions, int>, TransactionsRepo>();

        builder.Services.AddScoped<ICustomerAdminService, CustomerService>();
        builder.Services.AddScoped<ICustomerAccountService, CustomerAccountService>();
        builder.Services.AddScoped<ICustomerBeneficiaryService, CustomerBeneficiaryService>();
        builder.Services.AddScoped<ICustomerTransactionService, CustomerTransactionService>();
        builder.Services.AddScoped<IBankEmpAccMngmtService, BankEmpAccMngmntService>();
        builder.Services.AddScoped<IEmployeeAdminService, BankEmployeeService>();
        builder.Services.AddScoped<IAccountAdminService, AccountService>();
        builder.Services.AddScoped<IBankAdminService, BankService>();
        builder.Services.AddScoped<IBankEmpLoanService, BankEmpLoanService>();
        builder.Services.AddScoped<IBranchAdminService, BranchService>();
        builder.Services.AddScoped<ICustomerLoanService, CustomerLoanService>();

        builder.Services.AddDbContext<RequestTrackerContext>(opts =>
        {
            opts.UseSqlServer(builder.Configuration.GetConnectionString("requestTrackerConnection"));
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}