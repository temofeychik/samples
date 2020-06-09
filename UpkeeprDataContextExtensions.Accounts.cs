

namespace Upkeepr.Data.Entity.Upkeepr
{
    using Accounts;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using Users;
    using Vendors;

    public static partial class UpkeeprDataContextExtensions
    {
        public static ModelBuilder ConfigureUpkeeprAccountsModel(this ModelBuilder modelBuilder)
        {
            //Account
            modelBuilder
                .Entity<Account>()
                .HasKey(entity => new { entity.Domain, entity.AccountId, entity.Id });
            modelBuilder
                .Entity<Account>()
                .Property(entity => entity.Domain)
                .HasConversion<string>();
            modelBuilder
                .Entity<Account>()
                .OwnsMany(entity => entity.ContactMethods,
                            contactMethods => {
                                contactMethods.OwnsOne(e => e.Type);
                            });
            modelBuilder
                .Entity<Account>()
                .OwnsMany(entity => entity.Users);
            modelBuilder
                .Entity<Account>()
                .OwnsMany(entity => entity.AssetReferences);
            modelBuilder
                .Entity<Account>()
                .OwnsMany(entity => entity.AssetTypes);
            modelBuilder
                .Entity<Account>()
                .HasOne(entity => entity.CreateUser)
                .WithOne()
                .HasForeignKey<Account>(entity => entity.CreateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<Account>()
                .HasOne(entity => entity.UpdateUser)
                .WithOne()
                .HasForeignKey<Account>(entity => entity.UpdateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);

            //Asset
            modelBuilder
                .Entity<Asset>()
                .HasKey(entity => new { entity.Domain, entity.AccountId, entity.Id });
            modelBuilder
                .Entity<Asset>()
                .Property(entity => entity.Domain)
                .HasConversion<string>();
            modelBuilder
                .Entity<Asset>()
                .OwnsOne(entity => entity.AssetType,
                        type =>
                        {
                            type
                                .OwnsMany(e => e.TypeFields, q => q.OwnsMany(r => r.ActiveFields));
                            type
                                .Property(e => e.DescriptionPattern)
                                .HasConversion(
                                    v => JsonConvert.SerializeObject(v),
                                    v => v == null
                                        ? new string[0]
                                        : JsonConvert.DeserializeObject<string[]>(v));
                        });
            //modelBuilder
            //    .Entity<Asset>()
            //    .OwnsMany(entity => entity.Files);
            modelBuilder
                .Entity<Asset>()
                .OwnsMany(entity => entity.Comments);
            modelBuilder
                .Entity<Asset>()
                .OwnsMany(entity => entity.Messages,
                            messages=>
                            {
                                messages.OwnsOne(e => e.SenderUser);
                                messages.OwnsOne(e => e.RecipientUser);
                            });
            modelBuilder
                .Entity<Asset>()
                .HasMany(entity => entity.AssetActivity)
                .WithOne(entity => entity.Asset)
                .HasForeignKey(entity => entity.AssetId)
                .HasPrincipalKey(entity => entity.Id);
            modelBuilder
                .Entity<Asset>()
                .HasOne(entity => entity.CreateUser)
                .WithOne()
                .HasForeignKey<Asset>(entity => entity.CreateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<Asset>()
                .HasOne(entity => entity.UpdateUser)
                .WithOne()
                .HasForeignKey<Asset>(entity => entity.UpdateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<Asset>()
                .Property(e => e.CustomFields)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => v == null
                        ? new Dictionary<string, object>()
                        : JsonConvert.DeserializeObject<Dictionary<string, object>>(v)
                );
            modelBuilder
                .Entity<Asset>()
                .Property(e => e.TypeFields)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => v == null
                        ? new Dictionary<string, object>()
                        : JsonConvert.DeserializeObject<Dictionary<string, object>>(v)
                );

            modelBuilder
                .Entity<Asset>()
                .OwnsMany<Image>(entity => entity.Images, q => q
                                 .Property(entity => entity.Provider)
                                 .HasConversion<string>());

            //AssetActivity
            modelBuilder
                .Entity<AssetActivity>()
                .HasKey(entity => new { entity.Domain, entity.AccountId, entity.Id });
            modelBuilder
                .Entity<AssetActivity>()
                .Property(entity => entity.Domain)
                .HasConversion<string>();
            modelBuilder
                .Entity<AssetActivity>()
                .HasOne(entity => entity.Account)
                .WithOne()
                .HasForeignKey<AssetActivity>(entity => entity.AccountId)
                .HasPrincipalKey<Account>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivity>()
                .HasOne(entity => entity.ActivityType)
                .WithOne()
                .HasForeignKey<AssetActivity>(entity => entity.TypeId)
                .HasPrincipalKey<ActivityType>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivity>()
                .OwnsOne(entity => entity.Status);
            modelBuilder
                .Entity<AssetActivity>()
                .HasOne(entity => entity.AssignedUser)
                .WithOne()
                .HasForeignKey<AssetActivity>(entity => entity.AssignedUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivity>()
                .Property(e => e.Schedule)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => v == null
                        ? new Schedule()
                        : JsonConvert.DeserializeObject<Schedule>(v));
            modelBuilder
                .Entity<AssetActivity>()
                .OwnsMany(entity => entity.ActivityUsers);
            modelBuilder
                .Entity<AssetActivity>()
                .HasMany(entity => entity.AssetActivityImages)
                .WithOne(entity => entity.AssetActivity)
                .HasForeignKey(entity => entity.AssetActivityId)
                .HasPrincipalKey(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivity>()
                .HasOne(entity => entity.CreateUser)
                .WithOne()
                .HasForeignKey<AssetActivity>(entity => entity.CreateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivity>()
                .HasOne(entity => entity.UpdateUser)
                .WithOne()
                .HasForeignKey<AssetActivity>(entity => entity.UpdateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivity>()
                .Property(e => e.RelatedDueDateTime)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => v == null
                        ? new List<DateTimeOffset>()
                        : JsonConvert.DeserializeObject<IEnumerable<DateTimeOffset>>(v));
            modelBuilder
                .Entity<AssetActivity>()
                .Property(e => e.CustomFields)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => v == null
                        ? new Dictionary<string, object>()
                        : JsonConvert.DeserializeObject<Dictionary<string, object>>(v)
                );

            //Schedule
            modelBuilder
                .Entity<Schedule>()
                .HasNoKey()
                .Property(e => e.RelatedDayOfWeeks)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => v == null
                        ? new List<Upkeepr.DayOfWeek>()
                        : JsonConvert.DeserializeObject<IEnumerable<Upkeepr.DayOfWeek>>(v));

            //AssetActivityImage
            modelBuilder
                .Entity<AssetActivityImage>()
                .HasKey(entity => new { entity.Domain, entity.AccountId, entity.Id });
            modelBuilder
                .Entity<AssetActivityImage>()
                .Property(entity => entity.Domain)
                .HasConversion<string>();
            modelBuilder
                .Entity<AssetActivityImage>()
                .HasOne(entity => entity.Account)
                .WithOne()
                .HasForeignKey<AssetActivityImage>(entity => entity.AccountId)
                .HasPrincipalKey<Account>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityImage>()
                .HasOne(entity => entity.File)
                .WithOne()
                .HasForeignKey<AssetActivityImage>(entity => entity.FileId)
                .HasPrincipalKey<File>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityImage>()
                .HasOne(entity => entity.CreateUser)
                .WithOne()
                .HasForeignKey<AssetActivityImage>(entity => entity.CreateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityImage>()
                .HasOne(entity => entity.UpdateUser)
                .WithOne()
                .HasForeignKey<AssetActivityImage>(entity => entity.UpdateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);

            //AssetActivityVendor
            modelBuilder
                .Entity<AssetActivityVendor>()
                .HasKey(entity => new { entity.Domain, entity.AccountId, entity.Id });
            modelBuilder
                .Entity<AssetActivityVendor>()
                .Property(entity => entity.Domain)
                .HasConversion<string>();
            modelBuilder
                .Entity<AssetActivityVendor>()
                .HasOne(entity => entity.Account)
                .WithOne()
                .HasForeignKey<AssetActivityVendor>(entity => entity.AccountId)
                .HasPrincipalKey<Account>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityVendor>()
                .HasOne(entity => entity.AssetActivity)
                .WithOne()
                .HasForeignKey<AssetActivityVendor>(entity => entity.AssetActivityId)
                .HasPrincipalKey<AssetActivity>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityVendor>()
                .HasOne(entity => entity.Vendor)
                .WithOne()
                .HasForeignKey<AssetActivityVendor>(entity => entity.VendorId)
                .HasPrincipalKey<Vendor>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityVendor>()
                .HasOne(entity => entity.CreateUser)
                .WithOne()
                .HasForeignKey<AssetActivityVendor>(entity => entity.CreateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityVendor>()
                .HasOne(entity => entity.UpdateUser)
                .WithOne()
                .HasForeignKey<AssetActivityVendor>(entity => entity.UpdateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);

            //AssetActivityUser
            modelBuilder
                .Entity<AssetActivityUser>()
                .HasKey(entity => new { entity.Domain, entity.AccountId, entity.Id });
            modelBuilder
                .Entity<AssetActivityUser>()
                .Property(entity => entity.Domain)
                .HasConversion<string>();
            modelBuilder
                .Entity<AssetActivityUser>()
                .HasOne(entity => entity.Account)
                .WithOne()
                .HasForeignKey<AssetActivityUser>(entity => entity.AccountId)
                .HasPrincipalKey<Account>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityUser>()
                .HasOne(entity => entity.AssetActivity)
                .WithOne()
                .HasForeignKey<AssetActivityUser>(entity => entity.AssetActivityId)
                .HasPrincipalKey<AssetActivity>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityUser>()
                .HasOne(entity => entity.User)
                .WithOne()
                .HasForeignKey<AssetActivityUser>(entity => entity.UserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityUser>()
                .HasOne(entity => entity.Vendor)
                .WithOne()
                .HasForeignKey<AssetActivityUser>(entity => entity.VendorId)
                .HasPrincipalKey<Vendor>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityUser>()
                .HasOne(entity => entity.CreateUser)
                .WithOne()
                .HasForeignKey<AssetActivityUser>(entity => entity.CreateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<AssetActivityUser>()
                .HasOne(entity => entity.UpdateUser)
                .WithOne()
                .HasForeignKey<AssetActivityUser>(entity => entity.UpdateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            //AssetFile
            modelBuilder
                .Entity<AssetFile>()
                .HasKey(entity => new { entity.Domain, entity.AccountId, entity.Id });
            modelBuilder
                .Entity<AssetFile>()
                .Property(entity => entity.Domain)
                .HasConversion<string>();
            modelBuilder
                .Entity<AssetFile>()
                .HasOne(entity => entity.Account)
                .WithOne()
                .HasForeignKey<AssetFile>(entity => entity.AccountId)
                .HasPrincipalKey<Account>(entity => entity.Id);
            modelBuilder
                .Entity<AssetFile>()
                .HasOne(entity => entity.Asset)
                .WithOne()
                .HasForeignKey<AssetFile>(entity => entity.AssetId)
                .HasPrincipalKey<Asset>(entity => entity.Id);
            modelBuilder
                .Entity<AssetFile>()
                .HasOne(entity => entity.File)
                .WithOne()
                .HasForeignKey<AssetFile>(entity => entity.FileId)
                .HasPrincipalKey<File>(entity => entity.Id);
            modelBuilder
                .Entity<AssetFile>()
                .HasOne(entity => entity.CreateUser)
                .WithOne()
                .HasForeignKey<AssetFile>(entity => entity.CreateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<AssetFile>()
                .HasOne(entity => entity.UpdateUser)
                .WithOne()
                .HasForeignKey<AssetFile>(entity => entity.UpdateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            //AssetImage
            modelBuilder
                .Entity<AssetImage>()
                .HasKey(entity => new { entity.Domain, entity.AccountId, entity.Id });
            modelBuilder
                .Entity<AssetImage>()
                .Property(entity => entity.Domain)
                .HasConversion<string>();
            modelBuilder
                .Entity<AssetImage>()
                .HasOne(entity => entity.Account)
                .WithOne()
                .HasForeignKey<AssetImage>(entity => entity.AccountId)
                .HasPrincipalKey<Account>(entity => entity.Id);
            modelBuilder
                .Entity<AssetImage>()
                .HasOne(entity => entity.File)
                .WithOne()
                .HasForeignKey<AssetImage>(entity => entity.FileId)
                .HasPrincipalKey<File>(entity => entity.Id);
            modelBuilder
                .Entity<AssetImage>()
                .HasOne(entity => entity.CreateUser)
                .WithOne()
                .HasForeignKey<AssetImage>(entity => entity.CreateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);
            modelBuilder
                .Entity<AssetImage>()
                .HasOne(entity => entity.UpdateUser)
                .WithOne()
                .HasForeignKey<AssetImage>(entity => entity.UpdateUserId)
                .HasPrincipalKey<User>(entity => entity.Id);

            return modelBuilder;
        }
    }
}
