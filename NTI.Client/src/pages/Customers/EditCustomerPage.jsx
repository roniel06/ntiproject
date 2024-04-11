import React, { useEffect } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { useToast } from '@chakra-ui/react'
import { useState } from 'react'
import CustomerForm from './components/CustomerForm'
import { CustomersService } from '../../services/customersService/CustomersService'

const EditCustomerPage = () => {
    const { id } = useParams();
    const navigate = useNavigate()
    const [customer, setCustomer] = useState({})
    const toast = useToast();


    const handleOnSubmit = async (values) => {
        const service = new CustomersService()
        const result = await service.update(id, values)
        if (result.isSuccessfulWithNoErrors) {
            toast({
                title: "Customer Updated",
                description: "The customer was updated successfully.",
                status: "success",
                duration: 4000,
                isClosable: true,
            })
            setTimeout(() => {
                navigate('/customers')
            }, 500)
        }
        else {
            toast({
                title: "An error occurred.",
                description: "An error occurred while updating the customer.",
                status: "error",
                duration: 4000,
                isClosable: true,
            })
        }
    }

    useEffect(() => {
        const getData = async () => {

            const service = new CustomersService()
            const result = await service.get(id)
            if (result.isSuccessfulWithNoErrors) {
                setCustomer(result.payload)
                return;
            }
            else
                console.log(result)
        }
        getData();
    })
    return (
        <>
            <CustomerForm customer={customer} onSubmit={handleOnSubmit} />
        </>
    )
}

export default EditCustomerPage