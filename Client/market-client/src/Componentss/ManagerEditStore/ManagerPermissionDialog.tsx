import * as React from "react"

import Button from "@mui/material/Button"
import TextField from "@mui/material/TextField"
import Dialog from "@mui/material/Dialog"
import DialogActions from "@mui/material/DialogActions"
import DialogContent from "@mui/material/DialogContent"
import DialogContentText from "@mui/material/DialogContentText"
import DialogTitle from "@mui/material/DialogTitle"
import Product from "../../DTOs/Product"
import { Currency, makeSetStateFromEvent } from "../../Utils"

import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import ListSubheader from '@mui/material/ListSubheader';
import Switch from '@mui/material/Switch';
import WifiIcon from '@mui/icons-material/Wifi';
import BluetoothIcon from '@mui/icons-material/Bluetooth';
import Permission, { permissionToString } from "../../DTOs/Permission"

import LockOpenIcon from '@mui/icons-material/LockOpen';
import LockIcon from '@mui/icons-material/Lock';
import LoadingCircle from "../LoadingCircle"
import { fetchResponse } from "../../services/GeneralService"
import { serverGetManagerPermission } from "../../services/StoreService"
import { getBuyerId } from "../../services/SessionService"


export default function ManagerPermissionDialog(
    {
        open,
        userId,
        storeId,
        handleClose,
        handleNewPermissions
    }: {
        open: boolean
        userId: number
        storeId: number
        handleClose: () => void
        handleNewPermissions: (p: Permission[],) => void
    }
) {
    const [checked, setChecked] = React.useState<Permission[]>([]);
    const [reload, setReload] = React.useState(false)
    const [managerPermissions, setManagerPermissions] = React.useState<Permission[] | null>(null)
    const permissionsIds: number[] = Object.values(Permission).filter((v: any) => !isNaN(v)).map((v: any) => Number(v)) // taking the ids of the roles in Roles enum
    const permissionsNames: string[] = Object.values(Permission).filter((v: any) => isNaN(v)).map((v: any) => String(v)) // Taking the names of the roles in Roles neum

    React.useEffect(() => {
        if (open)
            fetchResponse(serverGetManagerPermission(getBuyerId(), storeId, userId))
                .then((permissions: Permission[]) => {
                    setChecked(permissions)
                    setManagerPermissions(permissions)
                })
                .catch(alert)
    }, [reload, userId, open])

    const handleCloseDialog = () => {
        setManagerPermissions(null)
        handleClose()
    }

    const handleToggle = (permission: Permission) => () => {
        const isChecked = checked.includes(permission); // isCheck = before the toggle state
        const newChecked = isChecked ? checked.filter((p) => p != permission) : [...checked, permission]
        // setChecked(newChecked);
        handleNewPermissions(newChecked)
        setReload(!reload)
    };

    function NoPermissionItem(permission: Permission) {
        return (<ListItem>
            <ListItemIcon>
                <LockIcon />
            </ListItemIcon>
            <ListItemText id="permission" primary={permissionToString(permission)} />
            <Switch
                edge="end"
                onChange={handleToggle(permission)}
                checked={checked.indexOf(permission) !== -1}
                inputProps={{
                    'aria-labelledby': 'switch-list-label-wifi',
                }}
            />
        </ListItem>)
    }

    function YesPermissionItem(permission: Permission) {
        return (<ListItem>
            <ListItemIcon>
                <LockOpenIcon />
            </ListItemIcon>
            <ListItemText id="permission" primary={permissionToString(permission)} />
            <Switch
                edge="end"
                onChange={handleToggle(permission)}
                checked={checked.includes(permission)}
                inputProps={{
                    'aria-labelledby': 'switch-list-label-wifi',
                }}
            />
        </ListItem>)
    }

    function PermissionListItem(permission: Permission) {
        permission.toString()
        return (
            managerPermissions === null
                ? null
                : managerPermissions.includes(permission)
                    ? YesPermissionItem(permission)
                    : NoPermissionItem(permission)
        )
    }

    return (
        <Dialog open={open} onClose={handleCloseDialog} >
            {managerPermissions === null ? (LoadingCircle()) :
                <List
                    sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}
                    subheader={<ListSubheader>User {userId} Manager Permissions</ListSubheader>}
                >
                    {permissionsIds.map((permId: number, permIndex: number) => {
                        return PermissionListItem(permId)
                    })}
                    {/* <ListItem>
                    <ListItemIcon>
                        <WifiIcon />
                    </ListItemIcon>
                    <ListItemText id="switch-list-label-wifi" primary="Wi-Fi" />
                    <Switch
                        edge="end"
                        onChange={handleToggle('wifi')}
                        checked={checked.indexOf('wifi') !== -1}
                        inputProps={{
                            'aria-labelledby': 'switch-list-label-wifi',
                        }}
                    />
                </ListItem>
                <ListItem>
                    <ListItemIcon>
                        <BluetoothIcon />
                    </ListItemIcon>
                    <ListItemText id="switch-list-label-bluetooth" primary="Bluetooth" />
                    <Switch
                        edge="end"
                        onChange={handleToggle('bluetooth')}
                        checked={checked.indexOf('bluetooth') !== -1}
                        inputProps={{
                            'aria-labelledby': 'switch-list-label-bluetooth',
                        }}
                    />
                </ListItem> */}
                </List>
            }
        </Dialog>
    );
}