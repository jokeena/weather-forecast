import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'

createRoot(document.getElementById('root')!).render( // creates React root and starts rendering App component.
  <StrictMode>
    <App />
  </StrictMode>,
)
