import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { CustomersService } from '../../services/customersService/CustomersService'
import DataTable from "react-data-table-component"
import {Spinner} from "@chakra-ui/react"


const CustomersPage = () => {

    const [customers, setCustomers] = useState([]);
    const navigate = useNavigate()
    const columns = [

        {
            name: "Id",
            selector: row => row.id,
            sortable: true,
            grow:.5
        },
        {
            name: "Name",
            selector: row => row.name,
            sortable: true
        },
        {
            name: "LastName",
            selector: row => row.lastName
        },
        {
            name: "Phone",
            selector: row => row.phone
        },
        {
            name: "Email",
            selector: row => row.email
        },
        {
            name: "Status",
            selector: row => row.isActive ? "Active" : "Inactive"
        },
        {
            name: "Options",
            grow:2,   
            cell: row => (
                <>
                    <button className='bg-green-400 m-2 p-3 text-white text-sm rounded' onClick={() => navigate(`/app/customers/${row.id}/addItems`)}>Items</button>
                    <button className="bg-orange-400 m-2 p-3 text-white text-sm rounded" onClick={() => navigate(`/app/customers/edit/${row.id}`)}>Edit</button>
                    <button onClick={(e) => {
                        if (!confirm("Are you sure you want to delete this record?")) {
                            e.preventDefault()
                        }
                        handleOnDelete(row.id);
                    }} className="bg-red-600 p-3 text-white text-sm rounded" type='button'>Delete</button>

                </>
            )
        }
    ];

    const handleOnDelete = async (id) => {
        const customersService = new CustomersService();
        const result = await customersService.delete(id)
        if (result.isSuccessfulWithNoErrors) {
            const newCustomers = customers.filter(x => x.id !== id)
            setCustomers(newCustomers)
        }
        else {
            console.log(result)
        }
    }

    useEffect(() => {
        const getData = async () => {
            const service = new CustomersService()
            const result = await service.getAll()
            if (result.isSuccessfulWithNoErrors) {
                setCustomers(result.payload)
                return;
            }
            else
                console.log(result)
        }

        getData()
    }, [])

    return (
        <>
            <div className="flex flex-col items-end">
                <button
                    className='bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 border border-blue-700 rounded'
                    onClick={() => navigate("/app/customers/create")}>
                    Create Customer
                </button>
            </div>
            <div>
                <DataTable
                    title="List Of Customers"
                    columns={columns}
                    data={customers}
                    pagination
                    progressPending={customers.length === 0}
                    progressComponent={<Spinner thickness='5px' speed='0.65s' emptyColor='gray.200' color='blue.500' size="xl" />}

                />
            </div>
        </>
    )
}

export default CustomersPage