import React, { useEffect, useState, useCallback } from 'react';
import './userprofile.css';
import Dashboard from '../../components/Dashboard/DashBoard';
import SkeletonCard from '../../components/SkeletonCard/SkeletonCard';
import UpdateMovieModal from '../../components/UpdateMovieModal/UpdateMovieModal';

const UserProfile = () => {
  const [favoriteMovies, setFavoriteMovies] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [selectedMovie, setSelectedMovie] = useState(null);
  const [followers, setFollowers] = useState([]);
  const user = JSON.parse(localStorage.getItem('user'));
  const userId = user?.userId;
  const username = user?.userName;
  const firstname = user?.firstName;
  const lastname = user?.lastName;
  const date = user?.createdAt;
  const OMDB_API_KEY = "";

  const fetchFavoriteMovies = useCallback(async () => {
    try {
      const response = await fetch(`http://localhost:5299/api/UserMovies/user/${userId}`);
      const movieData = await response.json();

      const movieDetailsPromises = movieData.map(async (movie) => {
        const omdbResponse = await fetch(`http://www.omdbapi.com/?i=${movie.omdbId}&apikey=${OMDB_API_KEY}`);
        const omdbData = await omdbResponse.json();
        
        // Fetch the like count for the movie
        const likesResponse = await fetch(`http://localhost:5299/api/Likes/usermovies/${movie.userMovieId}`);
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

        return {
          id: movie.userMovieId,
          title: omdbData.Title,
          year: omdbData.Year,
          genre: omdbData.Genre,
          poster: omdbData.Poster,
          addedAt: movie.createdAt,
          rating: movie.userRating,
          review: movie.userReview,
          rated: omdbData.Rated,
          released: omdbData.Released,
          runtime: omdbData.Runtime,
          director: omdbData.Director,
          actors: omdbData.Actors,
          plot: omdbData.Plot,
          likeCount: likesData.length // Add like count
        };
      });

      const moviesWithDetails = await Promise.all(movieDetailsPromises);
      setFavoriteMovies(moviesWithDetails);
    } catch (error) {
      console.error('Error fetching favorite movies:', error);
    } finally {
      setIsLoading(false); 
    }
  }, [userId]);

  const fetchFollowers = useCallback(async () => {
    try {
      const response = await fetch(`http://localhost:5299/api/Followers/${userId}`);

      if(response.ok)
      {
        const followers = await response.json();

        const followerDetailsPromises = followers.map(async (follower) => {
          const userResponse = await fetch(`http://localhost:5299/api/Users/${follower.followerUserId}`);
          const userData = await userResponse.json();
          return userData.userName;
        });
  
        const usernames = await Promise.all(followerDetailsPromises);
        setFollowers(usernames);
      }

    }catch(error)
    {
      console.log("!failed to get followers!");
    }

  }, [userId])

  useEffect(() => {
    if (userId) {
      fetchFavoriteMovies();
      fetchFollowers();
    } else {
      setIsLoading(false); 
    }
  }, [userId, fetchFavoriteMovies]);

  const handleUpdate = (movie) => {
    setSelectedMovie(movie); 
    setModalOpen(true);
  };

  const handleCloseModal = () => {
    setModalOpen(false); 
    setSelectedMovie(null); 
  };

  const handleDelete = async (movieId) => {
    const confirmed = window.confirm('Are you sure you want to delete this movie?');
    console.log("movieID: ", movieId);
    if (confirmed) {
      try {
        const response = await fetch(`http://localhost:5299/api/UserMovies/${movieId}`, {
          method: 'DELETE',
        });

        if (!response.ok) {
          alert("Failed to Delete Movie");
        }

        setFavoriteMovies(favoriteMovies.filter(movie => movie.id !== movieId));
        console.log('Movie deleted successfully');
      } catch (error) {
        alert('Error deleting movie:', error);
      }
    }
  };

  if (isLoading) {
    return (
      <div className='wrapper'>
        <Dashboard />
        <div className='user-profile-loading'>
          {[...Array(6)].map((_, index) => (
            <SkeletonCard key={index} />
          ))}
        </div>
      </div>
    );
  }

  return (
    <div className='wrapper'>
      <Dashboard />
      <div className="user-profile-container">
        <div className="user-profile-info"> 
          <div class="user-profile-bg-layer"></div>
          <div className="user-profile-details">
            <h2 className="user-profile-username">{username}</h2>
            <h2 className="user-profile-flname">{firstname} {lastname}</h2>
            <p>Joined: {new Date(date).toLocaleDateString()}</p>
          </div>
          <div className="user-profile-followers">
            <h4>Followers</h4>
            {followers.length === 0 ? (
              <p>No users have followed you yet.</p>
            ) : (
              <ul>
                {followers.map((follower, index) => (
                  <li key={index}>{follower}</li>
                ))}
              </ul>
            )}
          </div>
          </div>
        <div className="user-profile-movies">
          <h3>Favorite Movies</h3>
          {favoriteMovies.length === 0 ? (
            <p>No favorite movies added yet.</p>
          ) : (
            <div className="user-profile-movie-cards">
              {favoriteMovies.map(movie => (
                <div key={movie.id} className="user-profile-movie-card">
                  <img src={movie.poster || '/placeholder.png'} alt={movie.title} className="user-profile-movie-poster" />
                  <div className="user-profile-movie-details">
                    <h4>{movie.title}</h4>
                    <p><strong>Year:</strong> {movie.year}</p>
                    <p><strong>Genre:</strong> {movie.genre}</p>
                    <p><strong>Added on:</strong> {new Date(movie.addedAt).toLocaleDateString()}</p>
                    <p><strong>My Rating:</strong> {movie.rating}</p>
                    <p><strong>Review:</strong> {movie.review}</p>
                    <p><strong>Likes:</strong> {movie.likeCount}</p> {/* Display like count */}
                    <div className="user-profile-movie-action-buttons">
                      <button className="button update-button" onClick={() => handleUpdate(movie)}>Update</button>
                      <button className="button delete-button" onClick={() => handleDelete(movie.id)}>Delete</button>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      </div>
      {modalOpen && selectedMovie && (
        <UpdateMovieModal
          movie={selectedMovie}
          onClose={handleCloseModal}
          onUpdate={(updatedMovie) => {
            setFavoriteMovies(favoriteMovies.map(movie => movie.id === updatedMovie.id ? updatedMovie : movie));
            handleCloseModal();
          }}
        />
      )}
    </div>
  );
};

export default UserProfile;