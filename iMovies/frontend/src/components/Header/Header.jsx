import React, { useState, useEffect } from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { decodeToken, isTokenExpired } from '../../utils/jwtUtils';
import './header.css';

export default function Header() {
  const [isOpen, setIsOpen] = useState(false);
  const navigate = useNavigate();
  const [token, setToken] = useState(localStorage.getItem('token'));
  const [username, setUsername] = useState('');

  useEffect(() => {
    const updateAuthInfo = () => {
      const storedToken = localStorage.getItem('token');
      setToken(storedToken);

      if (storedToken && !isTokenExpired(storedToken)) {
        const decodedToken = decodeToken(storedToken);
        setUsername(decodedToken ? decodedToken.unique_name : '');
      } else {
        setUsername('');
      }
    };

    updateAuthInfo();

    window.addEventListener('tokenChanged', updateAuthInfo);

    return () => {
      window.removeEventListener('tokenChanged', updateAuthInfo);
    };
  }, []);

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    setToken(null);
    setUsername('');
    navigate('/login');
  };

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
            <img src="/iMoviesLogo.png" alt="Logo" className="logo-image" />
            <h2 className="logo-text">iMovies</h2>
          </NavLink>
        </div>
        <button className="hamburger" onClick={toggleMenu}>
          ☰
        </button>
        <nav className={`nav-menu ${isOpen ? 'open' : ''}`}>
          {token && !isTokenExpired(token) ? (
            <>
              <li className="nav-menu-li">
                <span>{username}</span>
              </li>
              <li className="nav-menu-li">
                <button onClick={handleLogout}>Logout</button>
              </li>
            </>
          ) : (
            <>
                <li className="nav-menu-li">
                  <NavLink to="/" onClick={closeMenu}>
                    About
                  </NavLink>
                </li>
                <li className="nav-menu-li">
                    <NavLink to="login" onClick={closeMenu}>
                      Login
                    </NavLink>
                </li>
            </>
            
          )}
        
        </nav>
      </div>
    </header>
  );
}