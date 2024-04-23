import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { UserSignUp } from '../../types/user';

export interface AuthState{

    user: UserSignUp | null,
    authStatus: "authenticated" | "not_authenticated" | "not_verified"
}

const initialState: AuthState = {
    user: null,
    authStatus: "not_verified"
}

export const authSlice = createSlice({

        name : 'auth',
        initialState,
        reducers:{
                        setUserAuth:(state,action:PayloadAction<AuthState['user']>)=>{
                                state.user = action.payload
                        },
                        setAuthStatus: (state, action: PayloadAction<AuthState['authStatus']>) => {
                                state.authStatus = action.payload
                        },
                }
})


export const { setUserAuth,  setAuthStatus } = authSlice.actions

export default authSlice.reducer