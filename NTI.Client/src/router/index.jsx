import { createBrowserRouter } from "react-router-dom";
import Main from "../pages/Main";
import ItemsPage from "../pages/Items/ItemsPage";
import CreateItemsPage from "../pages/Items/CreateItemsPage";
import EditItemsPage from "../pages/Items/EditItemsPage";
import CustomersPage from "../pages/Customers/CustomersPage";
import CreateCustomerPage from "../pages/Customers/CreateCustomerPage";
import EditCustomerPage from "../pages/Customers/EditCustomerPage";
import AddCustomerItems from "../pages/Customers/AddCustomerItems";
import ReportsPage from "../pages/Reports/ReportsPage";
import CustomersWithExpensiveItems from "../pages/Reports/CustomersWithExpensiveItems";
import LoginPage from "../pages/Login/LoginPage";
import SignUpPage from "../pages/SignUp/SignUpPage";

const router = createBrowserRouter([
    {
        path: "/",
        element: <LoginPage />,
    },
    {
        path: "/signup",
        element: <SignUpPage />
    },
    {
        path: "/app",
        element: <Main />,
        children: [

            {
                path: "items",
                element: <ItemsPage />
            },
            {
                path: "items/create",
                element: <CreateItemsPage />
            },
            {
                path: "items/edit/:id",
                element: <EditItemsPage />
            },
            {
                path: "customers",
                element: <CustomersPage />
            },
            {
                path: "customers/create",
                element: <CreateCustomerPage />
            },
            {
                path: "customers/edit/:id",
                element: <EditCustomerPage />
            },
            {
                path: "customers/:id/addItems/",
                element: <AddCustomerItems />
            },
            {
                path: "reports",
                element: <ReportsPage />
            },
            {
                path: "reports/expensive",
                element: <CustomersWithExpensiveItems />
            }

        ]
    }

])
export default router;