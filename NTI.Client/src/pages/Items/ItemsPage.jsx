import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { ItemsService } from '../../services/itemsService/ItemsService'
import DataTable from "react-data-table-component"
import { useToast, Spinner } from '@chakra-ui/react'
import { categoryEnum } from '../../utils/enums/categoryEnum'

const ItemsPage = () => {

  const [items, setItems] = useState([])
  const navigate = useNavigate();
  const toast = useToast();
  const columns = [
    {
      name: "Id",
      selector: row => row.id
    },
    {
      name: "Item Number",
      selector: row => row.itemNumber
    },
    {
      name: "Description",
      selector: row => row.description
    },
    {
      name: "Default Price",
      selector: row => row.defaultPrice
    },
    {
      name: "Category",
      selector: row => categoryEnum[row.category]
    },
    {
      name: "Status",
      selector: row => row.isActive ? "Active" : "Inactive"
    },
    {
      name: "Options",
      cell: row => (
        <>
          <button className="bg-orange-400 m-2 p-3 text-white text-sm rounded" onClick={() => navigate(`/items/edit/${row.id}`)}>Edit</button>
          <button onClick={(e) => {
            if (!confirm("Are you sure you want to delete this record?")) {
              e.preventDefault()
            }
            handleOnDelete(row.id);
          }} className="bg-red-600 p-3 text-white text-sm rounded" type='button'>Delete</button>
        </>
      )
    }
  ]

  const handleOnDelete = async (id) => {
    const itemsService = new ItemsService()
    const result = await itemsService.delete(id)
    if (result.isSuccessfulWithNoErrors) {
      const newItems = items.filter(x => x.id !== id)
      setItems(newItems)
      toast({
        title: "Item Deleted",
        description: "The item was deleted successfully.",
        status: "success",
        duration: 4000,
        isClosable: true,
      })
    }
    else {
      toast({
        title: "An error occurred.",
        description: "An error occurred while deleting the item.",
        status: "error",
        duration: 4000,
        isClosable: true,
      })
      console.log(result)
    }
  }

  useEffect(() => {
    const getData = async () => {
      const itemsService = new ItemsService()
      const result = await itemsService.getAll()
      if (result.isSuccessfulWithNoErrors) {
        setItems(result.payload)
      }

    }
    getData();
  }, [])
  return (

    <>
      <div className="flex flex-col items-end">
        <button
          className='bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 border border-blue-700 rounded'
          onClick={() => navigate("/items/create")}>
          Create Item
        </button>
      </div>
      <div>
        <DataTable
          title="List Of Items"
          columns={columns}
          data={items}
          pagination
          progressPending={items.length === 0}
          progressComponent={<Spinner thickness='5px' speed='0.65s' emptyColor='gray.200' color='blue.500' size="xl" />}

        />
      </div>
    </>
  )
}

export default ItemsPage