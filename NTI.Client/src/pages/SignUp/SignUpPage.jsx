import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { Formik } from "formik"
import { signUpValidationSchema } from '../../utils/validationSchemas/signUpValidationSchema'
import {
    Flex,
    Box,
    FormControl,
    FormLabel,
    Stack,
    Button,
    Heading,
    useColorModeValue,
    HStack,
    useToast
} from '@chakra-ui/react'
import InputField from '../../components/InputField'
import { AuthService } from '../../services/authService/AuthService'

const SignUpPage = () => {

    const toast = useToast();
    const navigate = useNavigate();
    const handleOnSubmit = async (values) => {
        const service = new AuthService();
        const response = await service.signup(values);
        if (response.isSuccessfulWithNoErrors) {
            toast({
                title: "Account Created.",
                description: "Your account has been created successfully.",
                status: "success",
                duration: 5000,
                isClosable: true,
            })
            setTimeout(() => {
                navigate('/')
            }, 500)
        }
    }
    const initialValues = {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: ''
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
                        <Heading fontSize={'4xl'}>Create Your Account</Heading>
                    </Stack>
                    <Box
                        rounded={'lg'}
                        bg={useColorModeValue('white', 'gray.700')}
                        boxShadow={'lg'}
                        p={8}>
                        <Stack spacing={4}>
                            <Formik
                                validationSchema={signUpValidationSchema}
                                onSubmit={handleOnSubmit}
                                enableReinitialize
                                initialValues={initialValues}>
                                {({ handleChange, handleSubmit, values, errors, touched }) => (
                                    <>
                                        <HStack>
                                            <FormControl id="firstName">
                                                <FormLabel>First Name</FormLabel>
                                                <InputField type="text" name="firstName" value={values?.firstName} onChange={handleChange} />
                                            </FormControl>
                                            <FormControl id="lastName">
                                                <FormLabel>Last Name</FormLabel>
                                                <InputField type="text" name="lastName" value={values?.lastName} onChange={handleChange} />
                                            </FormControl>
                                        </HStack>

                                        <FormControl id="email">
                                            <FormLabel>Email address</FormLabel>
                                            <InputField type="email" name="email" value={values?.email} onChange={handleChange} />
                                        </FormControl>
                                        <HStack>
                                            <FormControl id="password">
                                                <FormLabel>Password</FormLabel>
                                                <InputField type="password" name="password" value={values?.password} onChange={handleChange} />
                                            </FormControl>
                                            <FormControl id="confirmPassword">
                                                <FormLabel>Confirm Password</FormLabel>
                                                <InputField type="password" name="confirmPassword" value={values?.confirmPassword} onChange={handleChange} />
                                            </FormControl>
                                        </HStack>

                                        <Stack spacing={10}>
                                            <Button
                                                onClick={handleSubmit}
                                                bg={'blue.400'}
                                                color={'white'}
                                                _hover={{
                                                    bg: 'blue.500',
                                                }}>
                                                Sign up
                                            </Button>
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

export default SignUpPage