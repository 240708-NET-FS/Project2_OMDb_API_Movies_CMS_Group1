import React from 'react';
import { NavLink } from 'react-router-dom';
import './home.css';

const Home = () => {
  return (
    <div className='main'>
      <h1 className='title'>
          <NavLink to='/' className='logo-link'>
            <img src="../../public/iMovieslogo.png" alt='iMovies logo' className='logo-image' />
          </NavLink>
          Welcome to iMovies
        </h1>
      <div className='welcome_section'>

        <img src="../../public/introsectionpicture.png" alt='iMovies intro section picture'/>

        <p>
          Hey there, movie lover! Welcome to iMovies! 
          Your ultimate destination for movie management and discovery.
          Dive into a world where you can keep track of all the films you've enjoyed,
          share your favorites with friends, and see what others are loving.
        </p>

        <div className='action-buttons'>
          <NavLink to='/signup' className='button signup-button'>Sign Up</NavLink>
          <NavLink to='/login' className='button login-button'>Login</NavLink>
        </div>
      </div>
    </div>
  );
};

export default Home;
