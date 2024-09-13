using BCrypt.Net;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using CampusEatsLibrary.Services;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class AccountDAO
    {
        private ApplicationDbContext _context;
        private static AccountDAO _instance;
        private static readonly object _instanceLock = new object();
        private readonly IBaseService _baseService;

        public static AccountDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        throw new InvalidOperationException("AccountDAO has not been initialized. Call Initialize method first.");
                    }
                    return _instance;
                }
            }
        }

        public AccountDAO(ApplicationDbContext dbContext, IBaseService baseService)
        {
            _context = dbContext;
            _baseService = baseService;
        }

        public static void Initialize(ApplicationDbContext dbContext, IBaseService baseService)
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new AccountDAO(dbContext, baseService);
                }
            }
        }

        public async Task<AccountDTO> LoginAsync(string phone, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException(nameof(phone) + " " + nameof(password));
                }

                Account account = await _context.Account.FirstOrDefaultAsync(a => a.Phone.Equals(phone));

                if (account == null)
                {
                    throw new Exception("Account not found");
                }

                if (!BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    throw new Exception("Phone or password invalid");
                }

                return new AccountDTO
                {
                    Id = account.Id,
                    CreateAt = account.CreateAt,
                    Phone = phone,
                    Status = account.Status,
                    Name = account.Name,
                    Gender = account.Gender,
                    Email = account.Email,
                    Address = account.Address
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddAccountAsync(AccountDTO accountDTO)
        {
            try
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(accountDTO.Password);

                Account account = new Account
                {
                    Phone = accountDTO.Phone,
                    Password = passwordHash,
                    CreateAt = _baseService.GetCurrentDate(),
                    Id = accountDTO.Id,
                    Status = AccountStatus.ACTIVATE.ToString(),
                    Address = accountDTO.Address,
                    Email = accountDTO.Email,
                    Gender = accountDTO.Gender,
                    Name = accountDTO.Name
                };

                await _context.Account.AddAsync(account);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdatePasswordAsync(string phone, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
                {
                    return 0;
                }

                Account account = await _context.Account.FirstOrDefaultAsync(a => a.Phone.Equals(phone));

                if (account == null)
                {
                    return 0;
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

                account.Password = passwordHash;

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteAccountAsync(string id)
        {
            try
            {
                Account account = await _context.Account.FirstOrDefaultAsync(a => a.Id.Equals(id));

                if (account == null)
                {
                    return 0;
                }

                _context.Account.Remove(account);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
