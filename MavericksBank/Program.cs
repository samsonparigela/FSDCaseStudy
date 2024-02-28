using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using MavericksBank.Contexts;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Repository;
using MavericksBank.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(opts=>
        {
            opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            opts.JsonSerializerOptions.WriteIndented = true;
        }
        );
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt=>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please Enter Token",
                Name="Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        }
        );

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });


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
        builder.Services.AddScoped<IBankAdminService, BankService>();
        builder.Services.AddScoped<IBankEmpLoanService, BankEmpLoanService>();
        builder.Services.AddScoped<IBranchAdminService, BranchService>();
        builder.Services.AddScoped<ICustomerLoanService, CustomerLoanService>();
        builder.Services.AddScoped<ITokenService, TokenService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MavericksBankPolicy", opts =>
            {
                opts.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
            });
        });


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

        app.UseCors("MavericksBankPolicy");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}