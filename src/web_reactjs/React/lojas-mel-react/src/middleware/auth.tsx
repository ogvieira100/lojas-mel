import { Navigate } from "react-router-dom";
import { PropsChildren } from "../types/global";

const Auth = ({children}:PropsChildren) => {
    
    const isAuth  = true;

    if (isAuth)
    {
        return children;

    }else 
    {
        return <Navigate to='/login' />
    }
}
export default Auth;