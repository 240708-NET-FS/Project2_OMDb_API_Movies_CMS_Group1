import React, { useEffect, useState } from 'react';
import './feed.css';
import DashBoard from '../../components/Dashboard/DashBoard';
import SkeletonCard from '../../components/SkeletonCard/SkeletonCard';
import MovieModal from '../../components/MovieModal/MovieModal';
import MovieCard from '../../components/MovieCard/MovieCard';
import { fetchData, fetchTopMoviesWithDetails } from '../../utils/fetchData';
import UserCard from '../../components/UserCard/UserCard';

const Feed = () => {
  const [activities, setActivities] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [selectedMovieId, setSelectedMovieId] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [topMovies, setTopMovies] = useState([]);
  const [followevent, setFollowevent] = useState(false);

  const getCurrentUser = () => JSON.parse(localStorage.getItem('user'));

  useEffect(() => {
    const loadData = async () => {
      try {
        // Fetch user activities and top movies
        await fetchData(getCurrentUser, setActivities, setLoading);
        await fetchTopMovieData();
      } catch (err) {
        console.error('Error usseffect:', err);
        setError(true); 
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
  }, []);

  const openModal = (movieId) => {
    setSelectedMovieId(movieId);
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setIsModalOpen(false);
    setSelectedMovieId(null);
  };

  const fetchTopMovieData = async () => {
    const topMoviesData = await fetchTopMoviesWithDetails();
    setTopMovies(topMoviesData);
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
        {/* Top Movies Section */}
        <div className="top-movies">
          <h3>Top Movies</h3>
          <div className="top-movie-list">
            {topMovies.map((movie) => (
              <div key={movie.omdbId} className="top-movie-card">
                <img src={movie.poster} alt={movie.title} className="top-movie-poster" />
                <div className="top-movie-details">
                  <h4>{movie.title}</h4>
                  <p className="bold">Year: {movie.year}</p>
                  <p className="bold">Genre: {movie.genre}</p>
                  <p className="bold">Average Rating: <span className="green">{movie.averageRating ? movie.averageRating.toFixed(1) : 'N/A'}</span></p>
                </div>
              </div>
            ))}
          </div>
        </div>
        
        {/* User Activities Section */}
        {activities.length === 0 ? (
          <div className="no-activities-message">
            <p>No recent activities to show. Follow users to see their activities and movies here!</p>
          </div>
        ) : (
          activities.map((activity) => (
            <div key={activity.userId} className="activity-item">
              <UserCard
                user={activity}
                setFollowevent={setFollowevent}
                followevent={followevent}
              />
              <div className="movies-list">
                {activity.userMovies.length === 0 ? (
                  <div className="no-movies-message">
                    <p>{activity.userName} hasn't added any movies yet.</p>
                  </div>
                ) : (
                  activity.userMovies.map((movie) => (
                    <MovieCard
                      key={movie.userMovieId}
                      userMovieId={movie.userMovieId}
                      imdbId={movie.omdbId}
                      openModal={openModal}
                    />
                  ))
                )}
              </div>
            </div>
          ))
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

export default Feed;