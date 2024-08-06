import React from 'react';
import { NavLink } from 'react-router-dom';
import './dashboard.css';

const Dashboard = () => {
  return (
    <div className="tabs">
      <NavLink 
        to="/feed" 
        className={({ isActive }) => `tab ${isActive ? 'active' : ''}`}
      >
        Feed
      </NavLink>
      <NavLink 
        to="/following" 
        className={({ isActive }) => `tab ${isActive ? 'active' : ''}`}
      >
        Following
      </NavLink>
      <NavLink 
        to="/addmovies" 
        className={({ isActive }) => `tab ${isActive ? 'active' : ''}`}
      >
        Add
      </NavLink>
      <NavLink 
        to="/userprofile" 
        className={({ isActive }) => `tab ${isActive ? 'active' : ''}`}
      >
        Profile
      </NavLink>
    </div>
  );
};

export default Dashboard;