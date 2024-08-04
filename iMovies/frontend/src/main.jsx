import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from "./pages/Home/Home"
import SignUp from './pages/Signup/Signup';
import Layout from './components/Layout/Layout';
import './index.css';
import Login from './pages/Login/Login';
import Landing from './pages/Landing/Landing';

ReactDOM.createRoot(document.getElementById('root')).render(
  <Router>
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<Home />} />
        <Route path="signup" element={<SignUp />} />
        <Route path="login" element={<Login />} />
        <Route path="landing" element={<Landing />} />
      </Route>
    </Routes>
  </Router>
);
