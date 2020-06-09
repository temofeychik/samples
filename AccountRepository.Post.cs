namespace Upkeepr.Domains.Accounts.Services.Repositories
{
    using Data.Entity.Upkeepr;
    using Models;
    using Common.Api;
    using Common.Systems.Collections.Generic;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Entity.Upkeepr.Users.References;

    public partial class AccountRepository
    {
        public async Task<Response<Account>> ValidatePostAsync(Account account, int? index = null, IEnumerable<Account> accounts = null)
        {
            Response<Account> response = await ValidateAuthorization(account, index, accounts);

            if (response.IsNotFaulted())
            {
                if (account == null)
                {
                    response.AddError(new ValidationError<Account>(x => x, ValidationErrorType.Required));
                    return response;
                }
                Response<Account> accountNameValiationResponse = await ValidateAccountName(account, index, accounts);
                if (accountNameValiationResponse.IsFaulted())
                {
                    response.AddError(accountNameValiationResponse.Errors);
                }
            }

            return response;
        }

        public async Task<Response<IEnumerable<Account>>> ValidatePostAsync(IEnumerable<Account> accounts)
        {
            Response<IEnumerable<Account>> response = new Response<IEnumerable<Account>>();

            if (accounts.IsNotNullOrEmpty())
            {
                response.AddError(new ValidationError<Account>(x => x, ValidationErrorType.Required));
            }
            else
            {
                response.Errors = (await Task.WhenAll(accounts.Select((account, index) => ValidatePostAsync(account, index)))).SelectMany(r => r.Errors).ToList();
            }

            return response;
        }

        public async Task<Response<Account>> PostAsync(Account account, bool isValidated = false)
        {
            Response<Account> response = isValidated ? new Response<Account>() : await ValidatePostAsync(account);

            if (response.IsNotFaulted())
            {
                Data.Entity.Upkeepr.Accounts.Account entity = new Data.Entity.Upkeepr.Accounts.Account
                {
                    Name = account.Name,
                    Description = account.Description,
                    Users = mapper.Map<ICollection<UserReference>>(account.Users)
                };
                entity.SetCreated(session.User.Id);
                entity.AccountId = entity.Id;

                await dataAccess.PostAsync(entity);

                response.Data = mapper.Map<Account>(entity);
            }

            return response;
        }

        public async Task<Response<IEnumerable<Account>>> PostAsync(IEnumerable<Account> accounts)
        {
            Response<IEnumerable<Account>> response = await ValidatePostAsync(accounts);

            if (response.IsNotFaulted())
            {
                response.Data = (await Task.WhenAll(accounts.Select(account => PostAsync(account, isValidated: true)))).Select(r => r.Data);
            }

            return response;
        }
    }
}
