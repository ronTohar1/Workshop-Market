// export const RecieveInfo = 0;
// export const MakeCoManager = 1;
// export const RemoveCoManager = 2;
// export const MakeCoOwner = 3;
// export const RemoveCoOwner = 4;
// export const RecieiveRolesInfo = 5;
// export const DiscountPolicyManagement = 6;
// export const purchasePolicyManagement = 7;

enum Permission {
    RecieveInfo,
    MakeCoManager,
    RemoveCoManager,
    MakeCoOwner,
    RemoveCoOwner,
    RecieiveRolesInfo,
    DiscountPolicyManagement,
    purchasePolicyManagement,
    handlingBids
}
export default Permission

export function permissionToString(permissionNum: Permission): string {
    switch (permissionNum) {
        case Permission.RecieveInfo:
            return 'Recieve Info Permission';
        case Permission.MakeCoManager:
            return 'Make Co-Manager Permission';
        case Permission.RemoveCoManager:
            return 'Remove Co-Manager Permission';
        case Permission.MakeCoOwner:
            return 'Make Co-Owner Permission';
        case Permission.RemoveCoOwner:
            return 'Remove Co-Owner Permission';
        case Permission.RecieiveRolesInfo:
            return 'Recieve Roles Info Permission';
        case Permission.DiscountPolicyManagement:
            return 'Discount Policy Managment Permission';
        case Permission.purchasePolicyManagement:
            return 'Purchase Policy Managment Permission';
        case Permission.handlingBids:
            return 'Handling Bids';
    }

}


