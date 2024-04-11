import { useField } from 'formik';

const InputField = ({ label, ...props }) => {
  const [field, meta] = useField(props);
  const errorClass = meta.touched && meta.error ? 'ring-red-500' : 'ring-gray-300';

  return (
    <div className="mt-2">
      <label htmlFor={props.id || props.name} className="block text-sm font-medium leading-6 text-gray-900">
        {label}
      </label>
      <div className={`flex rounded-md shadow-sm ring-1 ring-inset ${errorClass} focus-within:ring-2 focus-within:ring-inset focus-within:ring-indigo-600 sm:max-w-md`}>
        <input {...field} {...props} className="block flex-1 border-0 bg-transparent py-1.5 pl-1 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm sm:leading-6" />
      </div>
      {meta.touched && meta.error ? (
        <div className="text-red-500 text-sm mt-1">{meta.error}</div>
      ) : null}
    </div>
  );
};
export default InputField