import { ReactNode } from "react"

export type PropsChildren  = {
    children:ReactNode
}


export type PropsApi = {
    endpoint: string,
    method?: 'GET' | 'POST' | 'PUT' | 'DELETE',
    data?: object,
    withAuth?: boolean
}

export type LNotification = {
    priority: 'High' | 'Average' | 'Low'
    layer?: 'App' | 'Domain' | 'Repository' | 'Others'
    typeNotificationNoty: 'Alert' | 'Error' | 'Sucess' | 'Information' | 'BreakSystem',
    message:string 
}

export type ApiResponse<T> = {
        data:T
        success:boolean,
        errors:LNotification[]
}