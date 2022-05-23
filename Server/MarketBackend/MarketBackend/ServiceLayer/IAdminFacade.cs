﻿using MarketBackend.ServiceLayer.ServiceDTO;

namespace MarketBackend.ServiceLayer
{
    public interface IAdminFacade
    {
        Response<IReadOnlyCollection<ServicePurchase>> GetBuyerPurchaseHistory(int currUserId, int buyerId);
        Response<IList<int>> GetLoggedInMembers(int requestingId);
        Response<ServiceMember> GetMemberInfo(int requestingId, int memberId);
        Response<IReadOnlyCollection<ServicePurchase>> GetStorePurchaseHistory(int currUserId, int storeId);
        Response<bool> MemberExists(int memberId);
        Response<bool> RemoveMember(int requestingId, int memberToRemoveId);
        Response<bool> RemoveMemberIfHasNoRoles(int requestingId, int memberToRemoveId);
    }
}