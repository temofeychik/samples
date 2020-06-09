namespace Upkeepr.Api.GraphQl.Upkeepr.Accounts.Mutation
{
    using Common.Api;
    using Types.InputTypes;
    using Domains.Accounts.Services;
    using GraphQL.Types;
    using Microsoft.Extensions.DependencyInjection;
    using Common.Api.GraphQl;
    using Common.Systems;
    using System;
    using Types;

    public static class AccountUpkpeeprMutationExtensions
    {
        public static UpkeeprMutation ConfigureAccountUpkeeprMutation(this UpkeeprMutation upkeeprMutation)
        {
            upkeeprMutation.FieldAsync<AccountType>(
                name: nameof(Domains.Accounts.Models.Account).Concatenate("Post"),
                description: "",
                arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<AccountInputType>> { Name = nameof(Domains.Accounts.Models.Account) }
                ),
                resolve: async context =>
                {
                    Domains.Accounts.Models.Account account = context.GetArgument<Domains.Accounts.Models.Account>(nameof(Domains.Accounts.Models.Account));

                    IUpkeeprApi upkeeprApi = context.ServiceProvider().GetService<IUpkeeprApi>();

                    var result = await upkeeprApi.Service<IAccountService>().PostAsync(account);
                    if (result.IsNotFaulted())
                    {
                        return result.Data;
                    }

                    context.AddErrors(result);
                    return null;
                }
            );

            upkeeprMutation.FieldAsync<AccountType>(
                name: nameof(Domains.Accounts.Models.Account).Concatenate("Put"),
                description: "",
                arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<AccountInputType>> { Name = nameof(Domains.Accounts.Models.Account) }
                ),
                resolve: async context =>
                {
                    Domains.Accounts.Models.Account account = context.GetArgument<Domains.Accounts.Models.Account>(nameof(Domains.Accounts.Models.Account));

                    IUpkeeprApi upkeeprApi = context.ServiceProvider().GetService<IUpkeeprApi>();

                    var result = await upkeeprApi.Service<IAccountService>().PutAsync(account);
                    if (result.IsNotFaulted())
                    {
                        return result.Data;
                    }

                    context.AddErrors(result);
                    return null;
                }
            );

            upkeeprMutation.FieldAsync<AccountType>(
                name: nameof(Domains.Accounts.Models.Account).Concatenate("Patch"),
                description: "",
                arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<AccountInputType>> { Name = nameof(Domains.Accounts.Models.Account) }
                ),
                resolve: async context =>
                {
                    Domains.Accounts.Models.Account account = context.GetArgument<Domains.Accounts.Models.Account>(nameof(Domains.Accounts.Models.Account));

                    IUpkeeprApi upkeeprApi = context.ServiceProvider().GetService<IUpkeeprApi>();

                    var result = await upkeeprApi.Service<IAccountService>().PatchAsync(account);
                    if (result.IsNotFaulted())
                    {
                        return result.Data;
                    }

                    context.AddErrors(result);
                    return null;
                }
            );

            upkeeprMutation.FieldAsync<AccountType>(
                name: nameof(Domains.Accounts.Models.Account).Concatenate("Save"),
                description: "",
                arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<AccountInputType>> { Name = nameof(Domains.Accounts.Models.Account) }
                ),
                resolve: async context =>
                {
                    Domains.Accounts.Models.Account account = context.GetArgument<Domains.Accounts.Models.Account>(nameof(Domains.Accounts.Models.Account));

                    IUpkeeprApi upkeeprApi = context.ServiceProvider().GetService<IUpkeeprApi>();

                    var result = await upkeeprApi.Service<IAccountService>().SaveAsync(account);
                    if (result.IsNotFaulted())
                    {
                        return result.Data;
                    }

                    context.AddErrors(result);
                    return null;
                }
            );

            upkeeprMutation.FieldAsync<AccountType>(
                name: nameof(Domains.Accounts.Models.Account).Concatenate("Delete"),
                description: "",
                arguments: new QueryArguments(
                   new QueryArgument<IdGraphType> { Name = nameof(Domains.Accounts.Models.Account.Id) }
                ),
                resolve: async context =>
                {
                    Guid? id = context.GetArgument<Guid?>(nameof(Domains.Accounts.Models.Account.Id));

                    IUpkeeprApi upkeeprApi = context.ServiceProvider().GetService<IUpkeeprApi>();

                    var result = await upkeeprApi.Service<IAccountService>().DeleteAsync(id.Value);
                    if (result.IsNotFaulted())
                    {
                        return result.Data;
                    }

                    context.AddErrors(result);
                    return null;
                }
            );

            return upkeeprMutation;
        }
    }
}
