using BMT.Domain.Shared;

namespace BMT.API.Controllers.Managers
{
    public sealed record HireManagerRequest(
            Guid bossId,
            string newHireName,
            string newHirePassword,
            string newHireEmail,
            ManagerScope newHireScope,
            Guid newHireStoreId);
}
