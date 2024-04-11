import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import { ChakraProvider } from '@chakra-ui/react'
import { RouterProvider } from 'react-router-dom'
import router from "./router/index.jsx"

ReactDOM.createRoot(document.getElementById('root')).render(
  <ChakraProvider toastOptions={{ defaultOptions: { position: "bottom" } }}>
    <React.StrictMode>
      <RouterProvider router={router}/>
    </React.StrictMode>,
  </ChakraProvider>

)
