import React, { useState, useEffect, useCallback } from 'react';
import "./moviecard.css";
import SkeletonCard from '../SkeletonCard/SkeletonCard';
import { handleLikeToggle } from '../../utils/likeUtils';

const MovieCard = ({ userMovieId, imdbId, openModal, userReview , userRating }) => {
  const [movieDetail, setMovieDetail] = useState(null);
  const [likeId, setLikeId] = useState(null);
  const [isLiked, setIsLiked] = useState(false);
  const [likeCount, setLikeCount] = useState(0);
  const [loading, setLoading] = useState(true);

  const OMDB_API_KEY = process.env.VITE_OMDB_API_KEY;

  const getCurrentUser = () => JSON.parse(localStorage.getItem('user'));

  const fetchMovieDetails = useCallback(async () => {
    try {
      const [movieResponse, likesResponse] = await Promise.all([
        fetch(`https://www.omdbapi.com/?i=${imdbId}&apikey=${OMDB_API_KEY}`),
        fetch(`http://localhost:5299/api/Likes/usermovies/${userMovieId}`)
      ]);

      if (!movieResponse.ok) {
        throw new Error(`Failed to fetch movie details with status ${movieResponse.status}`);
      }
      const movieData = await movieResponse.json();


      let likesData = [];
      if (likesResponse.ok) {
        const textResponse = await likesResponse.text();
        try {
          likesData = JSON.parse(textResponse);
        } catch (error) {
          console.error('Invalid JSON response for likes:', textResponse);
          if (textResponse.includes("No likes found for this movie")) {
            likesData = [];
          } else {
            throw new Error('Failed to parse likes response');
          }
        }
      } else if (likesResponse.status === 404) {
        likesData = [];
      } else {
        throw new Error(`Failed to fetch likes with status ${likesResponse.status}`);
      }

      console.log("likes data: ", likesData);
      setLikeCount(likesData.length);
      const currentUser = getCurrentUser();
      const userLike = likesData.find(like => like.userId === currentUser.userId);

      if (userLike) {
        setIsLiked(true);
        setLikeId(userLike.likeId); 
      } else {
        setIsLiked(false);
      }
      setMovieDetail(movieData);
    } catch (error) {
      console.error('Error fetching movie details or likes:', error);
    } finally {
      setLoading(false);
    }
  }, [imdbId, userMovieId]);

  useEffect(() => {
    fetchMovieDetails();
  }, [fetchMovieDetails]);

  const handleLikeClick = async () => {
    try {
      const success = await handleLikeToggle(userMovieId, likeId, setLikeId, isLiked, setIsLiked, setLikeCount);
      if (success) {
        // Refetch movie details to ensure updated state
        fetchMovieDetails();
      }
    } catch (error) {
      console.error('Error in handleLikeClick:', error);
      alert(`Error toggling like: ${error.message}`);
    }
  };

  if (loading) {
    return <SkeletonCard />;
  }

  if (!movieDetail) {
    return <h1>Failed to load movie details</h1>;
  }

  return (
    <div className="feed-movie-card">
      <img src={movieDetail.Poster} alt={movieDetail.Title} className="movie-poster" />
      <div className="movie-details">
        <h4>{movieDetail.Title}</h4>
        <p className="bold">Year: {movieDetail.Year}</p>
        <p className="bold">Genre: {movieDetail.Genre}</p>
        <p><strong>Review:</strong> {userReview}</p>
        <p><strong>Rating:</strong> {userRating}</p>
        <p><strong>Likes:</strong> {likeCount}</p>
        <div className="feed-action-buttons">
          <button 
            className="button" 
            onClick={handleLikeClick}
          >
            {isLiked ? "Unlike" : "Like"}
          </button>
          <button 
            className="button"
            onClick={() => openModal(imdbId)}
          >
            View More
          </button>
        </div>
      </div>
    </div>
  );
};

export default MovieCard;