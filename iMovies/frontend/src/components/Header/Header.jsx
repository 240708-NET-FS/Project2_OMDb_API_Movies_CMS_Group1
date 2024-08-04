import React from "react"
import { NavLink } from "react-router-dom"
import './header.css';

export default function Header() {
  const activeStyles = {
    fontWeight: "bold",
    textDecoration: "underline",
    color: "#5E00FF"
  }

  return (
    <header>
        <div className="site-header-menu">
              <div className="site-branding">
                <h2 className="logo-text">iMovies</h2>
              </div>
              <nav className="nav-menu"> 
                <li className="nav-menu-li">
                  <NavLink to="/" style={({ isActive }) => isActive ? activeStyles : null}>
                    Home
                   </NavLink>
                   </li>
                <li className="nav-menu-li"> 
                <NavLink to="signup" style={({ isActive }) => isActive ? activeStyles : null}>
                  SignUp
                </NavLink>
                </li>
                <li className="nav-menu-li"> <NavLink to="signup" style={({ isActive }) => isActive ? activeStyles : null}>
                  Login
                </NavLink>
                </li>
                </nav>
          </div>
    </header>
  )
}