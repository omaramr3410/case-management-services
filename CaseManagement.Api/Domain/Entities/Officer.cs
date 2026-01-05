namespace CaseManagement.Api.Domain.Entities
{
    /// <summary>
    /// DB record definition of Officer entity
    /// </summary>
    public record Officer
    {
        /// <summary>
        /// Unique identifer of Office record
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Field defining the associated User to this Officer, can have many Officers to one User
        /// </summary>
        public required Guid UserId { get; set; }

        public required string FirstName { get; set; } = null!;

        public required string LastName { get; set; } = null!;

        public required string Region { get; set; } = null!;

        public required DateTime CreatedAt { get; set; }

    }
}