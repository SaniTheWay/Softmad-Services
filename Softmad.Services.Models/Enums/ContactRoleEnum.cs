using Ardalis.SmartEnum;

namespace Softmad.Services.Models.Enums
{
    public sealed class ContactRoleEnum: SmartEnum<ContactRoleEnum>
    {
        public static readonly ContactRoleEnum Owner = new ContactRoleEnum(nameof(Owner), 0, "Owner");
        public static readonly ContactRoleEnum PurchaseHead = new ContactRoleEnum(nameof(PurchaseHead), 1, "Purchase Head");
        public static readonly ContactRoleEnum BioMedical = new ContactRoleEnum(nameof(BioMedical), 2, "Bio Medical");

        public string Description { get; set; }
        public ContactRoleEnum(string name, int value, string description): base(name, value)
        {
            Description = description;
        }
    }
}
