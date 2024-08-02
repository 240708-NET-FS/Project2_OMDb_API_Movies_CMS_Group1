import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  return (
    <div>
      <h1>Vite + React</h1>
      <div>
        <Link to="/signup">
          <button>Sign Up</button>
        </Link>
      </div>
      <p>Edit <code>src/App.jsx</code> and save to test HMR</p>
    </div>
  )
}

export default App
