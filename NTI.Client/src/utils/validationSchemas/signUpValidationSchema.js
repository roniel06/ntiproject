import * as yup from 'yup';

export const signUpValidationSchema = yup.object({

    firstName: yup.string().required("First Name is required"),
    lastName: yup.string().required("Last Name is required"),
    email: yup.string().email().required("Email is required"),
    password: yup.string().required("Password is required").min(8, "Password must be at least 8 characters"),
    confirmPassword: yup.string().required("Confirm Password is required").oneOf([yup.ref('password'), null], 'Passwords must match')
});