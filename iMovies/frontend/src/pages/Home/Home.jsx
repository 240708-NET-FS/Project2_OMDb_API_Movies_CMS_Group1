import React from 'react'
import './home.css';

const Home = () => {
  return (
    <div className='main'>
        <div className='welcome_section'>
            <h1>Welcome to iMovies</h1>

            <img src="../../public/introsectionpicture.jpg" alt='iMovies intro section picture'/>

            <p>Hey there, movie lover! Welcome to iMovies! 
              Your ultimate destination for movie management and discovery.
              Dive into a world where you can keep track of all the films you've enjoyed,
              share your favorites with friends, and see what others are loving.
              </p>
        </div>
       
    </div>
  )
}

export default Home;