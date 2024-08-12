import React, { useEffect, useState, useCallback } from 'react';
import './following.css'; 
import DashBoard from '../../components/Dashboard/DashBoard';
import SkeletonCard from '../../components/SkeletonCard/SkeletonCard';
import MovieModal from '../../components/MovieModal/MovieModal';
import MovieCard from '../../components/MovieCard/MovieCard';
import UserCard from '../../components/UserCard/UserCard';
import { fetchFollowingUsers } from '../../utils/fetchData';

const Following = () => {
  const [followingUsers, setFollowingUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [selectedMovieId, setSelectedMovieId] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [followevent, setFollowevent] = useState(false);

  const getCurrentUser = () => JSON.parse(localStorage.getItem('user'));

  useEffect(() => {
    const loadData = async () => {
      try {
        const currentUser = getCurrentUser();
        if (!currentUser) throw new Error('No user logged in');
        
        const usersData = await fetchFollowingUsers(currentUser.userId);
        setFollowingUsers(usersData);
      } catch (err) {
        console.error('Error loading data:', err);
        setError(true);
      } finally {
        setLoading(false);
      }
    };

    loadData();

    const handleTokenChange = () => {
      loadData();
    };

    window.addEventListener('tokenChanged', handleTokenChange);

    return () => {
      window.removeEventListener('tokenChanged', handleTokenChange);
    };
  }, [followevent]);

  const openModal = (movieId) => {
    setSelectedMovieId(movieId);
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setIsModalOpen(false);
    setSelectedMovieId(null);
  };

  if (loading) {
    return (
      <div className="wrapper">
        <DashBoard />
        <div className="main-dashboard">
          <SkeletonCard />
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="wrapper">
        <DashBoard />
        <div className="main-dashboard">
          <div className="error-message">
            <h2>Oops! Something went wrong.</h2>
            <p>We couldn't load the data. Please try again later.</p>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="wrapper">
      <DashBoard />
      <div className="main-dashboard">
        <h1>Following</h1>
        {/* Display message if no users are followed */}
        {followingUsers.length === 0 ? (
          <div className="no-following-message">
            <h2>You are not following anyone yet. Start following users to see their movies here!</h2>
          </div>
        ) : (
          <div>
            {/* Following Users Section */}
            {followingUsers.map((user) => (
              <div key={user.userId} className="activity-item">
                <UserCard
                  user={user}
                  setFollowevent={setFollowevent}
                  followevent={followevent}
                />
                <div className="movies-list">
                  {user.userMovies.length === 0 ? (
                    <div className="no-movies-message">
                      <p>{user.userName} hasn't added any movies yet.</p>
                    </div>
                  ) : (
                    user.userMovies.map((movie) => (
                      <MovieCard
                        key={movie.userMovieId}
                        userMovieId={movie.userMovieId}
                        imdbId={movie.omdbId}
                        openModal={openModal}
                        userReview={movie.userReview}
                        userRating={movie.userRating}
                      />
                    ))
                  )}
                </div>
              </div>
            ))}
          </div>
        )}

        {isModalOpen && selectedMovieId && (
          <MovieModal
            movieId={selectedMovieId}
            onClose={closeModal}
          />
        )}
      </div>
    </div>
  );
};

export default Following;