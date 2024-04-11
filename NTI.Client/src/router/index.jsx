import { createBrowserRouter } from "react-router-dom";
import Main from "../pages/Main";
import ItemsPage from "../pages/Items/ItemsPage";
import CreateItemsPage from "../pages/Items/CreateItemsPage";
import EditItemsPage from "../pages/Items/EditItemsPage";
import CustomersPage from "../pages/Customers/CustomersPage";
import CreateCustomerPage from "../pages/Customers/CreateCustomerPage";
import EditCustomerPage from "../pages/Customers/EditCustomerPage";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Main />,
        children: [
            {
                path: "/items",
                element: <ItemsPage />
            },
            {
                path: "/items/create",
                element: <CreateItemsPage />
            },
            {
                path: "/items/edit/:id",
                element: <EditItemsPage />
            },
            {
                path: "/customers",
                element: <CustomersPage />
            },
            {
                path: "/customers/create",
                element: <CreateCustomerPage />
            },
            {
                path: "/customers/edit/:id",
                element: <EditCustomerPage />
            },
        ]
    }
])
export default router;