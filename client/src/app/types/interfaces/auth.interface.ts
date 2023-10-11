export interface ILogin {
    username: string
    password: string
}

export interface IRegister extends ILogin {
    email: string
}

export interface ILoginResponse {
    token: string
    validTo: string
}