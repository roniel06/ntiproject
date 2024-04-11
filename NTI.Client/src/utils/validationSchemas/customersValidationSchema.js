import * as yup from "yup"

export const customersValidationSchema = yup.object({
    name: yup.string().required('Name is required').max(50, "Name can't be more than 50 characters"),
    lastName: yup.string().required('Last Name is required').max(50, "Last Name can't be more than 50 characters"),
    email: yup.string().email('Invalid email').required('Email is required'),
    phone: yup.string().required('Phone is required'),
});