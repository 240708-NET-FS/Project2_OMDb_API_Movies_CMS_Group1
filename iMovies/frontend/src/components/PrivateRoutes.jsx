// PrivateRoutes.js
import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { isTokenExpired } from '../utils/jwtUtils';

const PrivateRoutes = () => {

  const token = localStorage.getItem('token');
  
  if (!token || isTokenExpired(token)) {
    // If there's no token or the token is expired, navigate to login
    return <Navigate to="/login" />;
  }

  // Token is valid
  return <Outlet />;
};

export default PrivateRoutes;