import React, { useState } from 'react';
import './addmovies.css';
import Dashboard from './../../components/Dashboard/DashBoard';

const mockMovies = [
  { id: 1, title: 'Inception', genre: 'Sci-Fi', rating: '5/5', image: '/introsectionpicture.png' },
  { id: 2, title: 'The Godfather', genre: 'Crime', rating: '5/5', image: '/introsectionpicture.png' },
  { id: 3, title: 'Pulp Fiction', genre: 'Thriller', rating: '5/5', image: '/introsectionpicture.png' },
];

const AddMovies = () => {
  const [movies, setMovies] = useState(mockMovies);
  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const filteredMovies = movies.filter(movie =>
    movie.title.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className='wrapper'>
      <Dashboard />
      <div className='container'>
        <div className='search-bar'>
          <input
            type='text'
            placeholder='Search for movies...'
            value={searchTerm}
            onChange={handleSearch}
            className='search-input'
          />
        </div>
        <div className='movies-data'>
          {filteredMovies.map((movie) => (
            <div key={movie.id} className='movie-card'>
              <div className='movies-thumbnail'>
                <img src={movie.image} alt={movie.title} className='img-movie' />
              </div>
              <div className='movies-content text-left'>
                <div className='movies-title'>
                  <h2><a href='movies-details.html'>{movie.title}</a></h2>
                </div>
                <div className='movies-genres'>
                  <h1>{movie.genre}</h1>
                </div>
                <div className='movies-rating'>
                  <h1>{movie.rating}</h1>
                </div>
                <button className='add-movie-btn'>Add Movie</button>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default AddMovies;