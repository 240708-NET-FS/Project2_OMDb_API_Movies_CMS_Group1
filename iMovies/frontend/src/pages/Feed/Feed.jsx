import React, { useEffect, useState } from 'react';
import './feed.css';
import DashBoard from '../../components/Dashboard/DashBoard';
import SkeletonCard from '../../components/SkeletonCard/SkeletonCard';
import MovieModal from '../../components/MovieModal/MovieModal';

const API_KEY = ''; 

const currentUser = JSON.parse(localStorage.getItem('user'));

const topMovies = [
  {
    id: "101",
    title: "The Shawshank Redemption",
    year: "1994",
    genre: "Drama",
    poster: "/introsectionpicture.png",
    likes: 1500,
  },
  {
    id: "102",
    title: "The Godfather",
    year: "1972",
    genre: "Crime, Drama",
    poster: "/introsectionpicture.png",
    likes: 1200,
  },
  {
    id: "103",
    title: "The Dark Knight",
    year: "2008",
    genre: "Action, Crime, Drama",
    poster: "/introsectionpicture.png",
    likes: 1100,
  },
  {
    id: "104",
    title: "Inception",
    year: "2010",
    genre: "Action, Adventure, Sci-Fi",
    poster: "/introsectionpicture.png",
    likes: 1000,
  },
  {
    id: "105",
    title: "Fight Club",
    year: "1999",
    genre: "Drama",
    poster: "/introsectionpicture.png",
    likes: 950,
  }
];

const Feed = () => {
  const [activities, setActivities] = useState([]);
  const [isFollowing, setIsFollowing] = useState([]);
  const [loading, setLoading] = useState(true);
  const [movieDetails, setMovieDetails] = useState({});
  const [selectedMovieId, setSelectedMovieId] = useState(null); // State for selected movie
  const [isModalOpen, setIsModalOpen] = useState(false); // State for modal visibility
  const [likedMovies, setLikedMovies] = useState([]); // State for liked movies

  const fetchData = async () => {
    try {
      setLoading(true);
      const currentUser = JSON.parse(localStorage.getItem('user'));

      const response = await fetch('http://localhost:5299/api/Users/users-with-movies');
      const data = await response.json();
      
      // Filter out the current user
      const filteredData = data.filter(user => user.userId !== currentUser.userId);

      // Find the most recent activity timestamp for each user
      const usersWithRecentActivity = filteredData.map(user => {
        const mostRecentMovie = user.userMovies.reduce((latest, movie) => {
          const movieTimestamp = new Date(movie.updatedAt || movie.createdAt).getTime();
          return movieTimestamp > latest.timestamp ? { ...movie, timestamp: movieTimestamp } : latest;
        }, { timestamp: 0 });
        
        return {
          ...user,
          mostRecentActivity: mostRecentMovie.timestamp
        };
      });

      // Sort users based on most recent activity
      usersWithRecentActivity.sort((a, b) => b.mostRecentActivity - a.mostRecentActivity);

      setActivities(usersWithRecentActivity);
      setIsFollowing(usersWithRecentActivity.map(user => ({ userId: user.userId, isFollowing: false })));

      // Fetch movie details
      const movieIds = new Set(filteredData.flatMap(user => user.userMovies.map(movie => movie.omdbId)));
      const movieDetailsResponses = await Promise.all(
        Array.from(movieIds).map(id =>
          fetch(`https://www.omdbapi.com/?i=${id}&apikey=${API_KEY}`).then(response => response.json())
        )
      );

      const movieDetailsMap = movieDetailsResponses.reduce((acc, movie) => {
        acc[movie.imdbID] = movie;
        return acc;
      }, {});

      setMovieDetails(movieDetailsMap);
    } catch (error) {
      console.error('Error fetching activities:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();

    const handleTokenChange = () => {
      fetchData();
    };

    window.addEventListener('tokenChanged', handleTokenChange);

    return () => {
      window.removeEventListener('tokenChanged', handleTokenChange);
    };
  }, []);

  const handleFollowToggle = (userId) => {
    setIsFollowing(prevIsFollowing =>
      prevIsFollowing.map(followStatus =>
        followStatus.userId === userId
          ? { ...followStatus, isFollowing: !followStatus.isFollowing }
          : followStatus
      )
    );
  };

  const handleLikeToggle = (movieId) => {
    setLikedMovies(prevLikedMovies =>
      prevLikedMovies.includes(movieId)
        ? prevLikedMovies.filter(id => id !== movieId)
        : [...prevLikedMovies, movieId]
    );
  };

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

  return (
    <div className="wrapper">
      <DashBoard />
      <div className="main-dashboard">
        {/* Top Movies Section */}
        <div className="top-movies">
          <h3>Top Movies</h3>
          <div className="top-movie-list">
            {topMovies.map(movie => (
              <div key={movie.id} className="top-movie-card">
                <img src={movie.poster} alt={movie.title} className="top-movie-poster" />
                <div className="top-movie-details">
                  <h4>{movie.title}</h4>
                  <p className="bold">Year: {movie.year}</p>
                  <p className="bold">Genre: {movie.genre}</p>
                  <p>Likes: {movie.likes}</p>
                </div>
              </div>
            ))}
          </div>
        </div>

        {/* Activity Feed */}
        {activities.map(activity => {
          const followStatus = isFollowing.find(follow => follow.userId === activity.userId);
          return (
            <div key={activity.userId} className="activity-item">
              <div className="feed-user-info">
                <h4>{activity.userName}</h4>
                {activity.userName !== currentUser.userName && (
                  <button 
                    className='button'
                    onClick={() => handleFollowToggle(activity.userId)}
                  >
                    {followStatus?.isFollowing ? "Unfollow" : "Follow"}
                  </button>
                )}
              </div>
              <div className="movies-list">
                {activity.userMovies.map(movie => {
                  const movieDetail = movieDetails[movie.omdbId] || {};
                  return (
                    <div key={movie.userMovieId} className="feed-movie-card">
                      <img src={movieDetail.Poster || movie.poster} alt={movie.title} className="movie-poster" />
                      <div className="movie-details">
                        <h4>{movieDetail.Title || movie.title}</h4>
                        <p className="bold">Year: {movieDetail.Year || movie.year}</p>
                        <p className="bold">Genre: {movieDetail.Genre || movie.genre}</p>
                        <p>Notes: {movie.userReview}</p>
                        <p>Rating: {movie.userRating}</p>
                        <div className="feed-action-buttons">
                          <button 
                            className="button" 
                            onClick={() => handleLikeToggle(movie.omdbId)}
                          >
                            {likedMovies.includes(movie.omdbId) ? "Unlike" : "Like"}
                          </button>
                          <button 
                            className="button"
                            onClick={() => openModal(movie.omdbId)}
                          >
                            View More
                          </button>
                        </div>
                      </div>
                    </div>
                  );
                })}
              </div>
            </div>
          );
        })}

        {/* Movie Modal */}
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