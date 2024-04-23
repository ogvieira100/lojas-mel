export type UserLogin = {
    email:string,
    password:string
}

type Claim  = {
    value:string
    type:string
}

type UserToken = {
    id: number,
    email: string,
    claims:Claim[],
    name:string 
}

export type UserSignUp  = {
    accessToken:string
    expiresIn:number
    userToken:UserToken

}