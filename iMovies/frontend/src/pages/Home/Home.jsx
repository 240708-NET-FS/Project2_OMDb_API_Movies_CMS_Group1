import React from 'react';
import { NavLink } from 'react-router-dom';
import './home.css';

const Home = () => {
  return (
    <div className='main'>
      <h1 className='title'>
          <NavLink to='/' className='logo-link'>
            <img src="/iMoviesLogo.png" alt='iMovies logo' className='logo-image' />
          </NavLink>
          Welcome to iMovies
        </h1>
      <div className='welcome_section'>

        <img src="/introsectionpicture.png" alt='iMovies intro section picture'/>

        <p>
          Hey there, movie lover! Welcome to iMovies! 
          Your ultimate destination for movie management and discovery.
          Dive into a world where you can keep track of all the films you've enjoyed,
          share your favorites with friends, and see what others are loving.
        </p>

        <div className='home-buttons'>
          <NavLink to='/signup' className='button'>Sign Up</NavLink>
          <NavLink to='/login' className='button'>Login</NavLink>
        </div>
      </div>
    </div>
  );
};

export default Home;