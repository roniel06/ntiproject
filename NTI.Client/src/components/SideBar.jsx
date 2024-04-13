import React, { useState } from "react";
import { HiMenuAlt3, HiInbox } from "react-icons/hi";
import { MdOutlineDashboard, MdLibraryBooks } from "react-icons/md";
import { FiUsers, FiChevronDown, FiChevronRight } from "react-icons/fi";
import { Link } from "react-router-dom";
import { Outlet } from "react-router-dom"
import useRequireAuthHook from "../hooks/useRequireAuthHook";


const SideBar = () => {
    useRequireAuthHook();
    const menus = [
        {
            name: "Items", link: "/", icon: HiInbox, subMenus: [
                { name: "List of Items", link: "/app/items" },
            ]
        },
        {
            name: "Customers", link: "/", icon: FiUsers, subMenus: [
                { name: "List of Customers", link: "/app/customers" }
            ]
        },
        {
            name: "Reports", link: "/", icon: MdLibraryBooks, subMenus: [
                { name: "Customer Items By Item Number", link: "/app/reports" },
                { name: "Customer with Expensive items", link: "/app/reports/expensive" }
            ]
        }

    ];
    const [open, setOpen] = useState(true);
    const [openSubMenus, setOpenSubMenus] = useState({});

    const toggleSubMenu = (name) => {
        setOpenSubMenus((prevOpenSubMenus) => ({
            ...prevOpenSubMenus,
            [name]: !prevOpenSubMenus[name],
        }));
    };
    return (
        <section className="flex">
            <div
                className={`bg-[#0e0e0e] min-h-screen ${open ? "w-72" : "w-16"
                    } duration-500 text-gray-100 px-4`}
            >
                <div className="py-3 flex justify-end">
                    <HiMenuAlt3
                        size={26}
                        className="cursor-pointer"
                        onClick={() => setOpen(!open)}
                    />
                </div>
                <div className="mt-4 flex flex-col gap-4 relative">
                    {menus.map((menu, i) => (
                        <div key={i}>
                            <div
                                onClick={() => menu.subMenus && toggleSubMenu(menu.name)}
                                className={`transition-height 
                  ${!open && "hidden"} overflow-hidden 
                  ${menu.margin
                                    && "mt-5"
                                    } group flex items-center
                   justify-between 
                   text-sm gap-3.5 
                   font-medium p-2
                  hover:bg-gray-800
                  rounded-md cursor-pointer`}>
                                <div className="flex items-center">
                                    <div>{React.createElement(menu.icon, { size: "20" })}</div>
                                    <h2
                                        style={{
                                            transitionDelay: `${i + 3}00ms`,
                                        }}
                                        className={`whitespace-pre duration-500 ml-2 ${!open && "opacity-0 translate-x-28 overflow-hidden"
                                            }`}
                                    >
                                        {menu.name}
                                    </h2>
                                </div>
                                {menu.subMenus && (
                                    openSubMenus[menu.name]
                                        ? <FiChevronDown size="20" />
                                        : <FiChevronRight size="20" />
                                )}
                            </div>
                            <div
                                className={`${!open && "hidden"
                                    } overflow-hidden transition-all duration-300`}
                                style={{ maxHeight: openSubMenus[menu.name] ? '1000px' : '0' }}
                            >
                                {menu.subMenus && menu.subMenus.map((subMenu, subIndex) => (
                                    <Link
                                        to={subMenu.link}
                                        key={subIndex}
                                        className="pl-10 group flex items-center text-sm gap-3.5 font-medium p-2 hover:bg-gray-800 rounded-md"
                                    >
                                        {subMenu.name}
                                    </Link>
                                ))}
                            </div>
                        </div>
                    ))}
                </div>


            </div>
            <div className="container p-5 text-xl text-gray-900 font-semibold">
                <Outlet />
            </div>
        </section>
    );
};

export default SideBar;