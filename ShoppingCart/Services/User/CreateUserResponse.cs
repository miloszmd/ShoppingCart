﻿using ShoppingCart.Core.Communication;

namespace ShoppingCart.Services.User
{
    public class CreateUserResponse : CommunicationResponse
    {
        public int UserId { get; set; }
    }
}