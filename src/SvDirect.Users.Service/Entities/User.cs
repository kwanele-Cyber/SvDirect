using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SvDirect.Common;

namespace SvDirect.Users.Service.Entities
{


    public class User : IEntity
    {
        public User(Guid id, string firstName, string lastName, string email, string passwordHash, DateTime dateOfBirth, DateTimeOffset createdAt, DateTimeOffset updateAt)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            DateOfBirth = dateOfBirth;
            CreatedAt = createdAt;
            UpdateAt = updateAt;
        }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdateAt { get; set; }
    }
}