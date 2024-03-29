﻿namespace PharmaGOBackend.Contract.Authentication;
public record RegisterRequest(string FirstName, string LastName, string Email, string Password) {
    public Guid PharmacyId { get; set; }
}
