import * as yup from "yup";

export const itemsValidationSchema = yup.object(
    {
        itemNumber: yup.number().required("Item Number is required").min(1, "Item Number must be at least 1"),
        description: yup.string().required("Description is required").max(150, "Description must be at most 100 characters"),
        defaultPrice: yup.number().required("Default Price is required").min(1, "Default Price must be at least 1")
    }
);
