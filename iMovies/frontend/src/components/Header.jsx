import React from "react"
import { Link, NavLink } from "react-router-dom"

export default function Header() {
  const activeStyles = {
    fontWeight: "bold",
    textDecoration: "underline",
    color: "#161616"
  }

  return (
    <header>
      <div className="stickable-header-w">
        <div id="site-header-menu" className="site-header-menu">
          <div className="site-header-menu-inner ttm-stickable-header">
            <div className="container">
              <div className="site-branding">
                <h2 className="logo-text">OMDB</h2>

              </div>
              <ul className="nav-menu"> <li className="nav-menu-li"><NavLink
                to="/"
                style={({ isActive }) => isActive ? activeStyles : null}
              >
                Home
              </NavLink></li>
                <li className="nav-menu-li"> <NavLink
                  to="signup"
                  style={({ isActive }) => isActive ? activeStyles : null}
                >
                  SignUp
                </NavLink></li>
                <li className="nav-menu-li"> <NavLink
                  to="login"
                  style={({ isActive }) => isActive ? activeStyles : null}
                >
                  Login
                </NavLink></li></ul>
            </div>
          </div>
        </div>
      </div>
    </header>
  )
}