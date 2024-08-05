import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import './header.css';

export default function Header() {
  const [isOpen, setIsOpen] = useState(false);


  const toggleMenu = () => {
    setIsOpen(!isOpen);
  };

  const closeMenu = () => {
    setIsOpen(false);
  };

  return (
    <header>
      <div className="site-header-menu">
      <div className="site-branding">
          <NavLink to="/" className="logo-link">
            <img src="../../public/iMoviesLogo.png" alt="Logo" className="logo-image" />
            <h2 className="logo-text">iMovies</h2>
          </NavLink>
        </div>
        <button className="hamburger" onClick={toggleMenu}>
          ☰
        </button>
        <nav className={`nav-menu ${isOpen ? 'open' : ''}`}>
          <li className="nav-menu-li">
            <NavLink to="/" onClick={closeMenu}>
              Home
            </NavLink>
          </li>
          <li className="nav-menu-li">
            <NavLink to="login" onClick={closeMenu}>
              Login
            </NavLink>
          </li>
        </nav>
      </div>
    </header>
  );
}
