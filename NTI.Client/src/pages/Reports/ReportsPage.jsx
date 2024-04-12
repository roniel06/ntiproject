import React, { useEffect, useState } from 'react'
import { Box, Input, HStack, FormControl, FormLabel, InputLeftAddon, InputGroup, Divider } from "@chakra-ui/react"
import { CustomerItemsService } from "../../services/customerItemsService/CustomerItemsService"
import DataTable from "react-data-table-component"



const ReportsPage = () => {
  const [fromItemNumber, setFromItemNumber] = useState(0);
  const [toItemNumber, setToItemNumber] = useState(0);
  const [data, setData] = useState([]);

  const columns = [
    {
      name:"Customer Id",
      selector: row => row.customerId,
      sortable: true
    },
    {
      name:"Customer Name",
      selector: row => `${row.customer.name} ${row.customer.lastName}`,
    },
    {
      name:"Item Number",
      selector: row => row.item.itemNumber,
      sortable: true
    },
    {
      name:"Item Description",
      selector: row => row.item.description,
    },
    {
      name:"Price",
      selector: row => `$${row.price.toFixed(2)}`,
      sortable: true
    },
    {
      name:"Quantity",
      selector: row => row.quantity,
    }
  ]

  const handleSearch = async () => {
    const service = new CustomerItemsService()
    const result = await service.getByItemNumberRange(fromItemNumber, toItemNumber)
    if (result.isSuccessfulWithNoErrors) {
      setData(result.payload)
    }
  }

  return (
    <>
      <h1>Reports Page</h1>
      <Box w={"xxl"} m={5}>
        <HStack
          spacing={3}
          align="center"
          justify="center"
        >
          <FormControl>
            <InputGroup>
              <InputLeftAddon>From Item Number</InputLeftAddon>
              <Input placeholder="From Item Number" value={fromItemNumber} onChange={(e) => setFromItemNumber(e.target.value)} />
            </InputGroup>
          </FormControl>
          <FormControl>
            <InputGroup>
              <InputLeftAddon>From Item Number</InputLeftAddon>
              <Input placeholder="To Item Number" value={toItemNumber} onChange={(e) => setToItemNumber(e.target.value)} />
            </InputGroup>
          </FormControl>
          <FormControl>
            <button
              className='bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 border border-blue-700 rounded'
              onClick={handleSearch}>
              Search
            </button>
          </FormControl>
        </HStack>
      </Box>
    <Divider h={10}/>

    <Box mt={10}>
      <DataTable columns={columns} data={data} pagination/>
    </Box>
    </>
  )
}

export default ReportsPage