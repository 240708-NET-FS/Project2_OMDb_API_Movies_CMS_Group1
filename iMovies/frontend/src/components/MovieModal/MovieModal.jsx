import React, { useState, useEffect } from 'react';
import './moviemodal.css';

const MovieModal = ({ movieId, onClose }) => {
  const [movie, setMovie] = useState(null);
  const [review, setReview] = useState('');
  const [rating, setRating] = useState('');
  const [isAdding, setIsAdding] = useState(false);
  const [isAdded, setIsAdded] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');
  const userId = JSON.parse(localStorage.getItem('user')).userId;
  const OMDB_API_KEY = "";

  useEffect(() => {
    const fetchMovieDetails = async () => {
      try {
        const response = await fetch(`https://www.omdbapi.com/?i=${movieId}&apikey=${OMDB_API_KEY}`);
        const data = await response.json();
        setMovie(data);
        await checkIfMovieAdded(data.imdbID);
      } catch (error) {
        console.error('Error fetching movie details:', error);
      }
    };

    const checkIfMovieAdded = async (omdbId) => {
      try {
        const response = await fetch(`http://localhost:5299/api/UserMovies/user/${userId}`);
        const result = await response.json();

        // Check if the movie with the given omdbId is in the user's added movies
        const movieExists = result.some(movie => movie.omdbId === omdbId);
        setIsAdded(movieExists);
      } catch (error) {
        console.error('Error checking if movie is added:', error);
      }
    };

    fetchMovieDetails();
  }, [movieId, userId]);

  const handleAddMovie = async () => {
    if (isAdded) {
      alert('Movie already added!');
      return;
    }

    // Validation
    if (!review.trim()) {
      setErrorMessage('Review cannot be empty.');
      return;
    }

    const ratingValue = parseFloat(rating);
    if (isNaN(ratingValue) || ratingValue < 1 || ratingValue > 5) {
      setErrorMessage('Rating must be a number between 1 and 5.');
      return;
    }

    setIsAdding(true);
    setErrorMessage(''); // Clear previous error messages
    const requestBody = {
      userId: userId,
      omdbId: movie.imdbID,
      userRating: ratingValue,
      userReview: review
    };

    try {
      const response = await fetch('http://localhost:5299/api/UserMovies', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
      });

      if (!response.ok) {
        throw new Error('Failed to add movie');
      }

      const result = await response.json();
      setIsAdded(true);
     
    } catch (error) {
      setErrorMessage(`Error: ${error.message}`);
    } finally {
      setIsAdding(false);
    }
  };

  const handleOverlayClick = (e) => {
    if (e.target.className === 'modal-overlay') {
      onClose();
    }
  };

  useEffect(() => {
    // Clear error message when the modal is closed or movie details change
    setErrorMessage('');
  }, [movieId, onClose]);

  if (!movie) {
    return null;
  }

  return (
    <div className='modal-overlay' onClick={handleOverlayClick}>
      <div className='modal-content'>
        <button className='modal-close' onClick={onClose}>×</button>
        <h2 className='movie-modal-title'>{movie.Title}</h2>
        <img src={movie.Poster} alt={movie.Title} className='modal-img' />
        {!isAdded && (
          <div className='modal-inputs'>
            <textarea 
              placeholder='Enter your review...' 
              value={review} 
              onChange={(e) => setReview(e.target.value)}
              className='modal-textarea'
              rows="3"
              required
            />
            <input 
              type='number' 
              min='1' 
              max='5' 
              step='0.1' 
              placeholder='Rate (1-5)' 
              value={rating}
              onChange={(e) => setRating(e.target.value)}
              className='modal-input'
              required
            />
          </div>
        )}
        <button 
          className='button'
          onClick={handleAddMovie}
          disabled={isAdding || isAdded}
        >
          {isAdded ? 'Added' : isAdding ? 'Adding...' : 'Add Movie'}
        </button>
        {errorMessage && <div className='error-message'>{errorMessage}</div>}
        <div className='modal-info'>
          <p><strong>Rated:</strong> {movie.Rated}</p>
          <p><strong>Released:</strong> {movie.Released}</p>
          <p><strong>Runtime:</strong> {movie.Runtime}</p>
          <p><strong>Genre:</strong> {movie.Genre}</p>
          <p><strong>Director:</strong> {movie.Director}</p>
          <p><strong>Actors:</strong> {movie.Actors}</p>
          <p className='modal-plot'><strong>Plot:</strong> {movie.Plot}</p>
        </div>
      </div>
    </div>
  );
};

export default MovieModal;