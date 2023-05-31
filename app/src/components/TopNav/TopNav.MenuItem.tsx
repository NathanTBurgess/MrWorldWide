import React, {ReactNode} from "react";
import {OverridableStringUnion} from "@mui/types";
import {Variant} from "@mui/material/styles/createTypography";
import {
    Badge,
    BadgeProps,
    ListItemIcon,
    ListItemText,
    MenuItem,
    Tooltip,
    Typography,
    TypographyPropsVariantOverrides,
    useTheme
} from "@mui/material";

export interface AuthMenuItemProps {
    title: string;
    icon?: ReactNode;
    badge?: BadgeProps;
    iconPositioning?: "start" | "end";
    color?: string;
    textVariant?: OverridableStringUnion<Variant | 'inherit', TypographyPropsVariantOverrides>
    onClick?: (e: React.MouseEvent<HTMLLIElement, MouseEvent>) => (void | Promise<void>);
    tooltip?: string;
}

export function TopNavMenuItem(props: AuthMenuItemProps) {
    const theme = useTheme();
    return (
        <>
            <Tooltip title={props.tooltip}>
                <MenuItem onClick={(e) => props.onClick?.call(e.currentTarget, e)} dense>
                    {(props?.iconPositioning === "end") &&
                        <ListItemText>
                            <Typography marginRight={1} color={props.color} variant={props.textVariant ?? "h6"}>
                                {props.title}
                            </Typography>
                        </ListItemText>}
                    {props.icon &&
                        <ListItemIcon>
                            {props.badge ? (
                                <Badge {...props.badge}>
                                    <Typography color={props.color}>{props.icon}</Typography>
                                </Badge>
                            ) : (
                                <Typography color={props.color ?? theme.palette.primary.contrastText}>
                                    {props.icon}
                                </Typography>
                            )}
                        </ListItemIcon>}
                    {(!props.iconPositioning || props.iconPositioning === "start") &&
                        <ListItemText>
                            <Typography color={props.color} >
                                {props.title}
                            </Typography>
                        </ListItemText>}
                </MenuItem>
            </Tooltip>
        </>
    );
}

export default React.memo(TopNavMenuItem);
