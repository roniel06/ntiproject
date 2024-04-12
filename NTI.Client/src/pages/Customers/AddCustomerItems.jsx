import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import {
    Box,
    Text,
    Input,
    Divider,
    FormControl,
    FormLabel,
    HStack,
    Select,
    useToast,
    Checkbox
} from '@chakra-ui/react'
import { CustomersService } from '../../services/customersService/CustomersService'
import { ItemsService } from '../../services/itemsService/ItemsService'
import DataTable from 'react-data-table-component'
import { CustomerItemsService } from '../../services/customerItemsService/CustomerItemsService'

const AddCustomerItems = () => {

    const [customer, setCustomer] = useState(null);
    const [customerItems, setCustomerItems] = useState([]);
    const [items, setItems] = useState([]);
    const [availableItems, setAvailableItems] = useState([]);
    const [selectedItem, setSelectedItem] = useState([]);
    const [selectedItemPrice, setSelectedItemPrice] = useState(0);
    const [selectedItemQty, setSelectedItemQty] = useState(0);
    const [isActive, setIsActive] = useState(true);
    const { id } = useParams();
    const toast = useToast();

    const columns = [
        {
            name: "Id",
            selector: row => row.id,
            sortable: true,
            grow: .5
        },
        {
            name: "Description",
            selector: row => row.item.description,
            sortable: true
        },
        {
            name: "Quantity",
            selector: row => row.quantity,
        },
        {
            name: "Price",
            selector: row => row.price > 0 ? `$${row.price}` : `$${row.item.defaultPrice}`,

        },
        {
            name: "Status",
            selector: row => row.isActive ? "Active" : "Inactive"
        },

        {
            name: "Options",
            grow: 2,
            cell: row => (
                <>

                    <button onClick={(e) => {
                        if (!confirm("Are you sure you want to delete this record?")) {
                            e.preventDefault()
                        }
                        handleDeleteItem(row.id, row.itemId);
                    }} className="bg-red-600 p-3 text-white text-sm rounded" type='button'>X</button>

                </>
            )
        }
    ];

    const handleSelectedItemPrice = (e) => {
        setSelectedItemPrice(e.target.value)
    }

    const handleSelectedItemQty = (e) => {
        setSelectedItemQty(e.target.value)
    }

    const handleSelectProduct = (e) => {
        setSelectedItemPrice(items.find(x => x.id === parseInt(e.target.value)).defaultPrice);
        setSelectedItem(e.target.value)
    }

    const handleIsActive = (e) => {
        setIsActive(e)
    }

    const handleDeleteItem = async (id, itemId) => {
        const service = new CustomerItemsService();
        const result = await service.delete(id);
        if (result.isSuccessfulWithNoErrors) {
            toast({
                title: "Item deleted successfully",
                status: "success",
                duration: 2000,
                isClosable: true,
            });
            const newItems = customerItems.filter(x => x.id !== id)
            setCustomerItems(newItems)
            const item = availableItems.find(x => x.id === itemId);
            const newItemsList = [...items, item];
            setItems(newItemsList);
        }
        else {
            toast({
                title: "Error",
                description: result.errors[0],
                status: "error",
                duration: 2000,
                isClosable: true,
            });
        }
    }

    const filterAvailableItems = (customerItems, items) => {
        const selectedItems = customerItems.map(x => x.itemId);
        const unselectedItems = items.filter(x => !selectedItems.includes(x.id));
        setItems(unselectedItems);
        setSelectedItem('');
        setSelectedItemPrice(0);
        setSelectedItemQty(0);
        setIsActive(true);
    }

    const handleAddProduct = async () => {
        const customerItemInputModel = {
            customerId: id,
            itemId: selectedItem,
            quantity: selectedItemQty,
            price: selectedItemPrice,
            isActive: isActive
        }

        const service = new CustomerItemsService();
        const result = await service.create(customerItemInputModel)
        if (result.isSuccessfulWithNoErrors) {
            toast({
                title: "Item added successfully",
                status: "success",
                duration: 2000,
                isClosable: true,
            });
            result.payload.item = items.find(x => x.id === parseInt(selectedItem));
            const newCustomerItems = [...customerItems, result.payload]
            filterAvailableItems(newCustomerItems, items);
            setCustomerItems(newCustomerItems)
        }
        else {
            toast({
                title: "Error",
                description: result.errors[0],
                status: "error",
                duration: 2000,
                isClosable: true,
            });
        }
    }

    useEffect(() => {
        const getCustomer = async () => {
            const customerService = new CustomersService();
            const customer = await customerService.get(id);
            setCustomer(customer.payload);
            setCustomerItems(customer.payload.customerItems);
            getItems(customer.payload.customerItems);
        }
        const getItems = async (customerItems) => {
            const itemsService = new ItemsService();
            const result = await itemsService.getAll();
            if (result.isSuccessfulWithNoErrors) {
                setAvailableItems(result.payload);
                const selectedItems = customerItems.map(x => x.itemId);
                const unselectedItems = result.payload.filter(x => !selectedItems.includes(x.id));
                setItems(unselectedItems);
            }
        }
        getCustomer();
    }, [])

    return (
        <>
            <Text fontSize="xl" fontWeight="bold">Add Items To Customer</Text>

            <Box p={2} w={"lg"} mt={10}>
                <HStack spacing={6}>
                    <FormControl id="name">
                        <FormLabel>Name</FormLabel>
                        <Input type="text" className='text-black' value={customer?.name} disabled />
                    </FormControl>
                    <FormControl id="lastName">
                        <FormLabel>Last Name</FormLabel>
                        <Input type="text" className='' value={customer?.lastName} disabled />
                    </FormControl>
                </HStack>

                <HStack spacing={2} mt={4}>
                    <FormControl id="phone">
                        <FormLabel>Phone</FormLabel>
                        <Input type="text" className='' value={customer?.phone} disabled />
                    </FormControl>
                    <FormControl id="email">
                        <FormLabel>Email address</FormLabel>
                        <Input type="email" className='' value={customer?.email} disabled />
                    </FormControl>
                </HStack>
            </Box>
            <Divider mt={5} />
            <Box>
                <HStack spacing={2} mt={4}>
                    <FormControl id="items">
                        <FormLabel>Items</FormLabel>
                        <Select placeholder="Select item" onChange={handleSelectProduct}>
                            {items.map((item) => <option key={item.id} value={item.id}>{item.description}</option>)}
                        </Select>
                    </FormControl>
                    <FormControl id="price">
                        <FormLabel>Price</FormLabel>
                        <Input type="number" value={selectedItemPrice} onChange={handleSelectedItemPrice} />
                    </FormControl>
                    <FormControl id="quantity">
                        <FormLabel>Quantity</FormLabel>
                        <Input type="number" value={selectedItemQty} onChange={handleSelectedItemQty} />
                    </FormControl>
                    <FormControl id="quantity">
                        <FormLabel>Active</FormLabel>
                        <Checkbox
                            name='isActive'
                            isChecked={isActive}
                            onChange={(e) => handleIsActive(e.target.checked)}
                            defaultChecked
                        >
                            Active
                        </Checkbox>
                    </FormControl>
                    <FormControl id="price">
                        <button
                            onClick={handleAddProduct}
                            className={`${(selectedItem === '' || selectedItemQty === 0) ? 'bg-blue-300' : 'bg-blue-500'} p-3 mt-8 text-white text-sm rounded`}
                            type='button'
                            disabled={selectedItem === '' || selectedItemQty === 0}
                        >
                            Add
                        </button>
                    </FormControl>
                </HStack>
                <DataTable
                    columns={columns}
                    data={customerItems}
                />
            </Box>
        </>
    )
}


export default AddCustomerItems