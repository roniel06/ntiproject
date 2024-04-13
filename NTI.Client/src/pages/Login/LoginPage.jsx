import React from 'react'
import { useNavigate } from "react-router-dom"
import {
    Flex,
    Box,
    FormControl,
    FormLabel,
    Stack,
    Button,
    Heading,
    useColorModeValue,
} from '@chakra-ui/react'
import { Formik } from 'formik'
import { loginValidationSchema } from "../../utils/validationSchemas/loginValidationSchema"
import InputField from '../../components/InputField'
import { AuthService } from '../../services/authService/AuthService'

const LoginPage = () => {

    const navigate = useNavigate();
    const handleOnSubmit = async (values) => {
        const service = new AuthService();
        const result = await service.login(values);
        if (result.isSuccessfulWithNoErrors) {
            navigate("/app/items")
        }
    }
    const initialValues = {
        email: '',
        password: ''
    }
    return (
        <>
            <Flex
                minH={'100vh'}
                align={'center'}
                justify={'center'}
                bg={useColorModeValue('gray.50', 'gray.800')}>
                <Stack spacing={8} mx={'auto'} maxW={'lg'} py={12} px={6}>
                    <Stack align={'center'}>
                        <Heading fontSize={'4xl'}>Sign in to your account</Heading>
                    </Stack>
                    <Box
                        rounded={'lg'}
                        bg={useColorModeValue('white', 'gray.700')}
                        boxShadow={'lg'}
                        p={8}>
                        <Stack spacing={4}>
                            <Formik
                                validationSchema={loginValidationSchema}
                                onSubmit={handleOnSubmit}
                                enableReinitialize
                                initialValues={initialValues}>
                                {({ handleChange, handleSubmit, values, errors, touched }) => (
                                    <>
                                        <FormControl id="email">
                                            <FormLabel>Email address</FormLabel>
                                            <InputField type="email" name="email" onChange={handleChange} value={values?.email} />
                                        </FormControl>
                                        <FormControl id="password">
                                            <FormLabel>Password</FormLabel>
                                            <InputField type="password" name="password" onChange={handleChange} value={values?.password} />
                                        </FormControl>
                                        <Stack spacing={10}>
                                            <Button
                                                bg={'blue.400'}
                                                color={'white'}
                                                _hover={{
                                                    bg: 'blue.500',
                                                }} onClick={handleSubmit}>
                                                Sign in
                                            </Button>
                                            <Button color={'blue.400'} onClick={() => { navigate("/signup") }}>Create New User</Button>

                                        </Stack>
                                    </>
                                )}
                            </Formik>
                        </Stack>
                    </Box>
                </Stack>
            </Flex>
        </>
    )
}

export default LoginPage