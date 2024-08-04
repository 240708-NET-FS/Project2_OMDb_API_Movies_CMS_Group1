import React from 'react'

const MovieCard = ({name, image, rating, genre}) => {
  return (
    <div>
      <h1>{name}</h1>
      {/* <img src = {image} about='' /> */}
      <h2>{rating}</h2>
      <h2>{genre}</h2>
    </div>
  )
}

export default MovieCard