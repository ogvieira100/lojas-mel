import { Outlet } from "react-router-dom"
import NavBar from "./navBar";
const LayoutPrincipal  = () => {

    return (<>
    <NavBar />
      <Outlet />
    </>)

}

export default LayoutPrincipal;