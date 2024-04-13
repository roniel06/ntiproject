import React, { useEffect, useState } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { ItemsService } from '../../services/itemsService/ItemsService';
import ItemForm from './components/ItemForm';
import { useToast } from '@chakra-ui/react';

const EditItemsPage = () => {
    const { id } = useParams();
    const navigate = useNavigate()
    const [item, setItem] = useState({})
    const toast = useToast();

    useEffect(() => {
        const getData = async () => {
            const service = new ItemsService()
            const result = await service.get(id)
            if (result.isSuccessfulWithNoErrors) {
                setItem(result.payload)
                return;
            }
            else
                console.log(result)
        }

        getData()
    }, [])

    const handleSubmit = async (values) => {
        console.log(values)
        const service = new ItemsService()
        const result = await service.update(id, values)
        if (result.isSuccessfulWithNoErrors) {
            toast({
                title: "Item Updated",
                description: "The item was updated successfully.",
                status: "success",
                duration: 4000,
                isClosable: true,
            })
            setTimeout(() => {
                navigate('/app/items')
            }, 500)
        }
        else {
            toast({
                title: "An error occurred.",
                description: "An error occurred while updating the item.",
                status: "error",
                duration: 4000,
                isClosable: true,
            })
        }

    }

    return (
        <>
            <ItemForm item={item} onSubmit={handleSubmit} />
        </>
    )
}

export default EditItemsPage