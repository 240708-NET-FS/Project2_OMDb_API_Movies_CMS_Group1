import React, { useState } from 'react';
import './updatemoviemodal.css';

const UpdateMovieModal = ({ movie, onClose, onUpdate }) => {
  const [review, setReview] = useState(movie.review || '');
  const [rating, setRating] = useState(movie.rating || '');
  const [isUpdating, setIsUpdating] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');
  const userId = JSON.parse(localStorage.getItem('user')).userId;

  const handleUpdateMovie = async () => {
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

    setIsUpdating(true);
    setErrorMessage(''); 
    const requestBody = {
      userId: userId,
      omdbId: movie.omdbId,
      userRating: ratingValue,
      userReview: review
    };

    try {
      const response = await fetch(`http://localhost:5299/api/UserMovies/${movie.id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
      });

      if (!response.ok) {
        throw new Error('Failed to update movie');
      }

      const result = await response.json();
      console.log('Movie updated successfully:', result);
      onUpdate({ ...movie, rating: ratingValue, review: review }); // Update movie in parent component
      onClose(); 

    } catch (error) {
      setErrorMessage(`Error: ${error.message}`);
    } finally {
      setIsUpdating(false);
    }
  };

  const handleOverlayClick = (e) => {
    if (e.target.className === 'update-modal-overlay') {
      onClose();
    }
  };

  return (
    <div className='update-modal-overlay' onClick={handleOverlayClick}>
      <div className='update-modal-content'>
        <button className='update-modal-close' onClick={onClose}>×</button>
        <h2 className='update-movie-modal-title'>{movie.title}</h2>
        <img src={movie.poster} alt={movie.title} className='modal-img' />
        <div className='update-modal-inputs'>
          <textarea 
            placeholder='Enter your review...' 
            value={review} 
            onChange={(e) => setReview(e.target.value)}
            className='update-modal-textarea'
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
            className='update-modal-input'
            required
          />
        </div>
        <button 
          className='button'
          onClick={handleUpdateMovie}
          disabled={isUpdating}
        >
          {isUpdating ? 'Updating...' : 'Update Movie'}
        </button>
        {errorMessage && <div className='update-error-message'>{errorMessage}</div>}
        <div className='update-modal-info'>
          <p><strong>Rated:</strong> {movie.rated}</p>
          <p><strong>Released:</strong> {movie.released}</p>
          <p><strong>Runtime:</strong> {movie.runtime}</p>
          <p><strong>Genre:</strong> {movie.genre}</p>
          <p><strong>Director:</strong> {movie.director}</p>
          <p><strong>Actors:</strong> {movie.actors}</p>
          <p className='update-modal-plot'><strong>Plot:</strong> {movie.plot}</p>
        </div>
      </div>
    </div>
  );
};

export default UpdateMovieModal;