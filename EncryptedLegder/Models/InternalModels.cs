namespace EncryptedLegder.Models.InternalModels
{
    internal class PersonWithBalance<PersonIdType>
    {
        public PersonIdType PersonId { get; set; }
        public decimal PreviousBalance { get; set; }
    }
}