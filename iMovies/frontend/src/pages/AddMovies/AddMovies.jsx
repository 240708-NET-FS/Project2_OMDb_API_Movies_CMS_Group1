import React, { useState, useEffect } from 'react';
import './addmovies.css';
import Dashboard from './../../components/Dashboard/DashBoard';
import MovieModal from './../../components/MovieModal/MovieModal';
import SkeletonCard from './../../components/SkeletonCard/SkeletonCard';

const AddMovies = () => {
  const [movies, setMovies] = useState([]);
  const [popularMovies, setPopularMovies] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedMovieId, setSelectedMovieId] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [isSearching, setIsSearching] = useState(false);
  const key = "";

  useEffect(() => {
    const fetchPopularMovies = async () => {
      setIsLoading(true);
      try {
        const response = await fetch(`https://www.omdbapi.com/?s=marvel&type=movie&apikey=${key}`);
        const data = await response.json();
        if (data.Search) {
          const formattedMovies = data.Search.map(movie => ({
            id: movie.imdbID,
            title: movie.Title,
            year: movie.Year,
            image: movie.Poster
          }));
          setPopularMovies(formattedMovies);
          setMovies(formattedMovies);
        }
      } catch (error) {
        console.error('Error fetching movies:', error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchPopularMovies();
  }, []);

  useEffect(() => {
    const fetchSearchResults = async () => {
      setIsLoading(true);
      if (searchTerm.trim() === '') {
        // Reset to popular movies when search term is empty
        setMovies(popularMovies); 
        setIsLoading(false);
        return;
      }
      setIsSearching(true);
      try {
        const response = await fetch(`https://www.omdbapi.com/?s=${searchTerm}&type=movie&apikey=${key}`);
        const data = await response.json();
        if (data.Search) {
          const formattedMovies = data.Search.map(movie => ({
            id: movie.imdbID,
            title: movie.Title,
            year: movie.Year,
            image: movie.Poster
          }));
          setMovies(formattedMovies);
        } else {
          setMovies([]);
        }
      } catch (error) {
        console.error('Error fetching search results:', error);
        setMovies([]);
      } finally {
        setIsSearching(false);
        setIsLoading(false);
      }
    };

    const debounceFetch = setTimeout(() => {
      fetchSearchResults();
    }, 300);

    return () => clearTimeout(debounceFetch);
  }, [searchTerm, popularMovies]);

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleMovieClick = (movieId) => {
    setSelectedMovieId(movieId);
  };

  const handleModalClose = () => {
    setSelectedMovieId(null);
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
          {isSearching || isLoading ? (
            <div className='skeleton-wrapper'>
              {[...Array(5)].map((_, index) => (
                <SkeletonCard key={index} />
              ))}
            </div>
          ) : movies.length === 0 && searchTerm ? (
            <div className='no-results'>No results found</div>
          ) : (
            movies.map((movie) => (
              <div
                key={movie.id}
                className='add-movie-card'
                onClick={() => handleMovieClick(movie.id)}
              >
                <div className='movies-thumbnail'>
                  <img src={movie.image} alt={movie.title} className='img-movie' />
                </div>
                <div className='movies-content text-left'>
                  <div className='movies-title'>
                    <h2>{movie.title}</h2>
                  </div>
                  <div className='movies-year'>
                    <h3>{movie.year}</h3>
                  </div>
                  <div className='center'>
                    <button className='button'>View Details</button>
                  </div>
                </div>
              </div>
            ))
          )}
        </div>
      </div>
      {selectedMovieId && (
        <MovieModal 
          movieId={selectedMovieId}
          onClose={handleModalClose}
        />
      )}
    </div>
  );
};

export default AddMovies;