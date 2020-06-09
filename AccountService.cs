using System.Linq.Expressions;

namespace Upkeepr.Domains.Accounts.Services
{
    using Models;
    using Common.Api;
    using Common.Data;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Account>> PostAsync(Account account)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<Account> response = await accountRepository.PostAsync(account);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<IEnumerable<Account>>> PostAsync(IEnumerable<Account> accounts)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<IEnumerable<Account>> response = await accountRepository.PostAsync(accounts);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public IQueryable<Data.Entity.Upkeepr.Accounts.Account> Query()
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            return accountRepository.Query();
        }

        public Task<Response<IQueryable<Account>>> Search(Guid? id = null, bool? isDeleted = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Account>> GetAsync(Guid id)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<Account> response = await accountRepository.GetAsync(id);

            return response;
        }

        public async Task<Response<IObservable<Account>>> GetAsync(IEnumerable<Guid> ids)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<IObservable<Account>> response = await accountRepository.GetAsync(ids);

            return response;
        }

        public async Task<Response<Account>> PutAsync(Account account)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<Account> response = await accountRepository.PutAsync(account);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<IEnumerable<Account>>> PutAsync(IEnumerable<Account> accounts)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<IEnumerable<Account>> response = await accountRepository.PutAsync(accounts);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<Account>> PatchAsync(Account account)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<Account> response = await accountRepository.PatchAsync(account);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<IEnumerable<Account>>> PatchAsync(IEnumerable<Account> accounts)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<IEnumerable<Account>> response = await accountRepository.PatchAsync(accounts);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<Account>> SaveAsync(Account account)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<Account> response = await accountRepository.SaveAsync(account);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<IEnumerable<Account>>> SaveAsync(IEnumerable<Account> accounts)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<IEnumerable<Account>> response = await accountRepository.SaveAsync(accounts);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<Account>> DeleteAsync(Guid id)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<Account> response = await accountRepository.DeleteAsync(id);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<IEnumerable<Account>>> DeleteAsync(IEnumerable<Guid> ids)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<IEnumerable<Account>> response = await accountRepository.DeleteAsync(ids);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<Account>> PurgeAsync(Guid id)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<Account> response = await accountRepository.PurgeAsync(id);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<IEnumerable<Account>>> PurgeAsync(IEnumerable<Guid> ids)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<IEnumerable<Account>> response = await accountRepository.PurgeAsync(ids);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }

        public async Task<Response<IEnumerable<Account>>> WhereAsync(Expression<Func<Data.Entity.Upkeepr.Accounts.Account, bool>> expression)
        {
            IAccountRepository accountRepository = unitOfWork.Repository<IAccountRepository>();
            Response<IEnumerable<Account>> response = accountRepository.Where(expression);

            if (response.IsNotFaulted())
            {
                await unitOfWork.SubmitAsync();
            }

            return response;
        }
    }
}