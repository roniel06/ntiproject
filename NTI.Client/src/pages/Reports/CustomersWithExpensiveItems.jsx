import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { Box } from '@chakra-ui/react'
import DataTable from "react-data-table-component"
import { CustomersService } from '../../services/customersService/CustomersService'

const CustomersWithExpensiveItems = () => {

    const [data, setData] = useState([]);
    const navigate = useNavigate();


    const columns = [
        {
            name: "Id",
            selector: row => row.customerId,
            sortable: true
        },
        {
            name: "Customer Name",
            selector: row => `${row.customerName} ${row.customerLastName}`,
            sortable: true
        },
        {
            name: "Item Description",
            selector: row => row.itemDescription,
            sortable: true
        },
        {
            name: "Item Price",
            selector: row => `$${row.price.toFixed(2)}`,
            sortable: true
        }
    ]

    useEffect(() => {
        const fetchData = async () => {
            const service = new CustomersService();
            const response = await service.getCustomersWithExpensiveItems();
            setData(response.payload);
        }
        fetchData();
    },[])


    return (
        <>
            <Box>
                <DataTable
                    title="Customers With Most Expensive Items"
                    columns={columns}
                    data={data}
                    pagination
                    highlightOnHover
                />
            </Box>
        </>
    )
}

export default CustomersWithExpensiveItems