import { Route, Routes } from "react-router-dom"
import LayoutPrincipal from "./layouts/layoutPrincipal"
import NotFound from "../pages/notFound"
import About from "../pages/about"
import Login from "../pages/login"
import Auth from "../middleware/auth"
import Home from "../pages/home"
import  ListCustomer from '../pages/customer/list'

const MainRoute = () => {
    return (<>
        <Routes >
            <Route element={<LayoutPrincipal />}>
                <Route
                    index
                    path="/"
                    element={<Auth><Home /></Auth>}
                />
                <Route
                    path="*"
                    element={<Auth><NotFound /></Auth>}
                />

                <Route

                    path="/principal"
                    element={<Auth><Home /></Auth>}
                />
                 <Route
                    path="/clientes"
                    element={<Auth><ListCustomer /></Auth>}
                    />
                <Route
                    path="/sobre"
                    element={<Auth><About /></Auth>}
                />
            </Route>
            <Route
                    path="/entrar"
                    element={<Login />}
                />
        </Routes>
    </>)
}
export default MainRoute