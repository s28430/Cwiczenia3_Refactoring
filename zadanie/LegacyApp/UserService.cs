﻿using System;
using LegacyApp.Interfaces;

namespace LegacyApp
{
    public class UserService(IUserValidator userValidator, IUserCreditService creditService,
        IClientRepository clientRepository)
    {
        [Obsolete("Using default constructor is not desirable. " +
                  "Please create user service objects by explicitly injecting required dependencies.")]
        public UserService() : this(new SimpleUserValidator(), new UserCreditService(), new ClientRepository())
        {}

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (!userValidator.ValidateUser(user)) return false;

            switch (client.Type)
            {
                case ClientType.VeryImportantClient:
                    user.HasCreditLimit = false;
                    break;
                case ClientType.ImportantClient:
                {
                    using (var userCreditService = creditService)
                    {
                        var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        creditLimit *= 2;
                        user.CreditLimit = creditLimit;
                    }

                    break;
                }
                default:
                {
                    user.HasCreditLimit = true;
                    using (var userCreditService = creditService)
                    {
                        var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        user.CreditLimit = creditLimit;
                    }

                    break;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500) return false;

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
