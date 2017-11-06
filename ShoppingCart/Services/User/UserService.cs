﻿using System.Web.WebPages;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Email;
using ShoppingCart.Data.User;

namespace ShoppingCart.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CreateUserResponse Register(string email, string password)
        {
            var response = new CreateUserResponse();

            if (email.IsEmpty() || password.IsEmpty())
            {
                response.AddError(new Error{ Message = "Email and password are required." });
                return response;
            }

            if (!EmailValidator.IsValid(email))
            {
                response.AddError(new Error { Message = "Please provide a valid email address." });
                return response;
            }

            var saveOrUpdateRequest = new SaveOrUpdateRequest
            {
                Email = email,
                Password = password
            };
            var saveOrUpdateResponse = _userRepository.Save(saveOrUpdateRequest);

            if (saveOrUpdateResponse.HasError)
            {
                response.AddError(new Error { Message = "Could not create account. Please try again later." });
                return response;
            }

            response.UserId = saveOrUpdateResponse.UserId;

            return response;
        }
    }
}