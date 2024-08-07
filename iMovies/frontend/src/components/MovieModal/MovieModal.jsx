import React, { useState } from 'react';
import './moviemodal.css';

const MovieModal = ({ movie, onClose, onAddMovie }) => {
  const [review, setReview] = useState('');
  const [rating, setRating] = useState('');

  const handleSubmit = () => {
    onAddMovie({ ...movie, review, rating });
    onClose();
  };

  return (
    <div className='modal-overlay'>
      <div className='modal-content'>
        <button className='modal-close' onClick={onClose}>×</button>
        <h2>{movie.title}</h2>
        <img src={movie.image} alt={movie.title} className='modal-img' />
        <p><strong>Genre:</strong> {movie.genre}</p>
        <p><strong>Rating:</strong> {movie.rating}</p>
        <textarea 
          placeholder='Enter your review...' 
          value={review} 
          onChange={(e) => setReview(e.target.value)}
          className='modal-textarea'
        />
        <input 
          type='number' 
          min='1' 
          max='5' 
          placeholder='Rate (1-5)' 
          value={rating}
          onChange={(e) => setRating(e.target.value)}
          className='modal-input'
        />
        <button className='modal-button' onClick={handleSubmit}>Add Movie</button>
      </div>
    </div>
  );
};

export default MovieModal;