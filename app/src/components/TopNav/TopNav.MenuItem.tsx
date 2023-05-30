import React, {ReactNode} from "react";
import {Badge, BadgeProps, ListItemIcon, ListItemText, MenuItem, Tooltip, Typography} from "@mui/material";

export interface AuthMenuItemProps {
    title: string;
    icon: ReactNode;
    badge?: BadgeProps;
    tooltip?: string;

    onClick(): void | Promise<void>;
}

export function TopNavMenuItem(props: AuthMenuItemProps) {
    return (
        <>
            <Tooltip title={props.tooltip}>
                <MenuItem onClick={props.onClick}>
                    <ListItemIcon sx={(theme)=>({
                        color: theme.palette.primary.contrastText
                    })}>
                        {props.badge ? (
                            <Badge {...props.badge} >
                                <Typography>{props.icon}</Typography>
                            </Badge>
                        ) : (
                            props.icon
                        )}
                    </ListItemIcon>
                    <ListItemText>{props.title}</ListItemText>
                </MenuItem>
            </Tooltip>
        </>
    );
}

export default React.memo(TopNavMenuItem);
