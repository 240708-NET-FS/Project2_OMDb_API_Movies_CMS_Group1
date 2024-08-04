import React from 'react'
import MovieCard from '../../components/MovieCard/MovieCard'

const Landing = () => {
    const myMovies =
    [{
      name: "movie1",
      genre: "adventure",
      image: "./path/to/image.png",
      rating: "2/5",
    }, 
    
    {
      name: "movie2",
      genre: "romance",
      image: "./path/to/image.png",
      rating: "4/5",
    }, 
    {
      name: "movie3",
      genre: "horror",
      image: "./path/to/image.png",
      rating: "3/5",
    }, 
    {
      name: "movie4",
      genre: "comedy",
      image: "./path/to/image.png",
      rating: "5/5",
    }]

  return (
    <div>
        <h1>Test</h1>
        <div>
        {
            myMovies.map((movie)=> {
                <MovieCard name={movie.name} image={movie.image}
                    genre={movie.genre} rating={movie.rating}
                />
            })}
        </div>
    </div>

  )
}

export default Landing