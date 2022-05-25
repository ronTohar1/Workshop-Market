        export const RecieveInfo = 0;
        export const MakeCoManager = 1;
		export const RemoveCoManager = 2;
		export const MakeCoOwner = 3;
		export const RemoveCoOwner = 4;
		export const RecieiveRolesInfo = 5; 
		export const DiscountPolicyManagement = 6; 
		export const purchasePolicyManagement = 7; 

        export function prmissionNumToString(permissionNum:number):string{
            switch (permissionNum) {
                case RecieveInfo:
                    return 'recieve info permission';
                case MakeCoManager:
                    return 'make co manager permission';
                case RemoveCoManager:
                    return 'remove co manager permission';
                case MakeCoOwner:
                    return 'make co worker permission';
                case RemoveCoOwner:
                    return 'remove co worker permission';
                case RecieiveRolesInfo:
                    return 'recieve roles info permission';
                case DiscountPolicyManagement:
                    return 'discount policy managment permission';
                case purchasePolicyManagement:
                    return 'purchase policy managment permission';
                default:
                    return ""
            }

        }
