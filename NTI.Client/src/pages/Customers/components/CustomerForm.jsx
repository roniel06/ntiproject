import React from 'react'
import { Formik, Field } from 'formik'
import InputField from '../../../components/InputField'
import { customersValidationSchema } from '../../../utils/validationSchemas/customersValidationSchema'

const CustomerForm = ({ customer, onSubmit }) => {

    const handleOnSubmit = (values) => {
        onSubmit(values);
    }
    return (
        <>
            <Formik
                validationSchema={customersValidationSchema}
                onSubmit={handleOnSubmit}
                initialValues={customer}
                enableReinitialize>
                {({ handleChange, handleSubmit, values, errors, touched }) => (
                    <form onSubmit={handleSubmit}>
                        <div className="space-y-12">
                            <div className="border-b border-gray-900/10 pb-12">
                                <h2 className="text-base font-semibold leading-7 text-gray-900">Create Customer</h2>
                                <p className="mt-1 text-sm leading-6 text-gray-600">
                                    Fill the form to create a Customer
                                </p>

                                <InputField
                                    type="text"
                                    name="name"
                                    label={"Name"}
                                    id="name"
                                    className="block flex-1 border-0 bg-transparent py-1.5 pl-1 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6"
                                    onChange={handleChange}
                                    value={values.name}
                                />
                                <InputField type="text"
                                    label={"Last Name"}
                                    name="lastName"
                                    id="lastName"
                                    className="block flex-1 border-0 bg-transparent py-1.5 pl-1 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6"
                                    onChange={handleChange}
                                    value={values.lastName} />
                                <InputField type="text"
                                    label={"Phone"}
                                    name="phone"
                                    id="phone"
                                    className="block flex-1 border-0 bg-transparent py-1.5 pl-1 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6"
                                    onChange={handleChange}
                                    value={values.phone} />
                                <InputField type="text"
                                    label={"email"}
                                    name="email"
                                    id="email"
                                    className="block flex-1 border-0 bg-transparent py-1.5 pl-1 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6"
                                    onChange={handleChange}
                                    value={values.email} />
                                <div className='w-full md:w-1/2 px-3 mb-6 md:mb-0'>
                                    <label htmlFor="isActive" className="text-sm font-medium text-gray-900 dark:text-gray-900">Active?</label>
                                    <Field id="isActive" name='isActive' type="checkbox" checked={values.isActive} onChange={handleChange} className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600 ml-2 mt-4" />
                                </div>
                            </div>
                            <button type='submit' className='bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full mt-5'>
                                {
                                    customer.id > 0 ? "Update" : "Create"
                                }
                            </button>
                        </div>
                    </form>
                )}
            </Formik>
        </>
    )
}

export default CustomerForm