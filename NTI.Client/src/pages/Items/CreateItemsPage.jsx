import React, { useState } from 'react'
import ItemForm from './components/ItemForm'
import { ItemsService } from '../../services/itemsService/ItemsService';
import {useToast} from '@chakra-ui/react'
import { useNavigate } from 'react-router-dom';

const CreateItemsPage = () => {

    const [item, setItem] = useState({});
    const toast = useToast();
    const navigate = useNavigate();

    const handleOnSubmit = async (values) => {
        const itemsService = new ItemsService()
        const result = await itemsService.create(values);
        if (result.isSuccessfulWithNoErrors) {
            toast({
                title: "Item Created",
                description: "The item was created successfully.",
                status: "success",
                duration: 4000,
                isClosable: true,
            })
            setTimeout(() => {
                navigate("/items")
            }, 2000)
            return ;
        }
        toast({
            title: "An error occurred.",
            description: "An error occurred while creating the item.",
            status: "error",
            duration: 4000,
            isClosable: true,
        })
        
    }
    return (
        <div>
            <ItemForm onSubmit={handleOnSubmit} item={item} />
        </div>
    )
}

export default CreateItemsPage