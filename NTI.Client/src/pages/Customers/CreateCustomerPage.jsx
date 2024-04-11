import CustomerForm from "./components/CustomerForm"
import { CustomersService } from "../../services/customersService/CustomersService"
import { useState } from "react"
import { useToast } from "@chakra-ui/react"
import { useNavigate } from "react-router-dom"

const CreateCustomerPage = () => {
    const [customer, setCustomer] = useState({})
    const toast = useToast();
    const navigate = useNavigate();

    const handleOnSubmit = async (values) => {
        const service = new CustomersService();
        const result = await service.create(values);
        if (result.isSuccessfulWithNoErrors) {
            toast({
                title: "Customer Created",
                description: "The customer was created successfully.",
                status: "success",
                duration: 4000,
                isClosable: true,
            })
            setTimeout(() => {
                navigate("/customers")
            }, 1000)
        }
        else {
            toast({
                title: "An error occurred.",
                description: "An error occurred while creating the customer.",
                status: "error",
                duration: 4000,
                isClosable: true,
            })
        }

    }
    return (
        <CustomerForm onSubmit={handleOnSubmit} customer={customer} />
    )
}

export default CreateCustomerPage