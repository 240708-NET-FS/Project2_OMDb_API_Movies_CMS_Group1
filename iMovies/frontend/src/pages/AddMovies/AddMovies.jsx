import React, { useState } from 'react';
import './addmovies.css';
import Dashboard from './../../components/Dashboard/DashBoard';
import MovieModal from './../../components/MovieModal/MovieModal'; // Import the modal

const mockMovies = [
  { id: 1, title: 'Inception', genre: 'Sci-Fi', rating: '5/5', image: '/introsectionpicture.png' },
];

const AddMovies = () => {
  const [movies, setMovies] = useState(mockMovies);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedMovie, setSelectedMovie] = useState(null);

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const filteredMovies = movies.filter(movie =>
    movie.title.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleAddMovie = (movie) => {
    setMovies([...movies, movie]);
  };

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
        <div className='movies-list'>
          {filteredMovies.map((movie) => (
            <div
              key={movie.id}
              className='add-movie-card'
              onClick={() => setSelectedMovie(movie)}
            >
              <div className='movies-thumbnail'>
                <img src={movie.image} alt={movie.title} className='img-movie' />
              </div>
              <div className='movies-content text-left'>
                <div className='movies-title'>
                  <h2>{movie.title}</h2>
                </div>
                <div className='movies-genres'>
                  <h1>{movie.genre}</h1>
                </div>
                <div className='movies-rating'>
                  <h1>{movie.rating}</h1>
                </div>
                <div className='center'>
                  <button className='button'>View Details</button>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
      {selectedMovie && (
        <MovieModal 
          movie={selectedMovie}
          onClose={() => setSelectedMovie(null)}
          onAddMovie={handleAddMovie}
        />
      )}
    </div>
  );
};

export default AddMovies;