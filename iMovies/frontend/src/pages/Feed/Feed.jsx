import React, { useEffect, useState } from 'react';
import './feed.css';
import DashBoard from '../../components/Dashboard/DashBoard';

const currentUser = "Alice Doe";

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

const mockActivities = [
  {
    userId: 1,
    username: "John Doe",
    actionType: "add",
    movies: [
      {
        id: "1",
        title: "Inception",
        year: "2010",
        genre: "Action, Adventure, Sci-Fi",
        poster: "/introsectionpicture.png",
        notes: "Mind-bending thriller",
      },
      {
        id: "2",
        title: "The Dark Knight",
        year: "2008",
        genre: "Action, Crime, Drama",
        poster: "/introsectionpicture.png",
        notes: "The epic conclusion of the trilogy",
      },
      {
        id: "3",
        title: "Interstellar",
        year: "2014",
        genre: "Adventure, Drama, Sci-Fi",
        poster: "/introsectionpicture.png",
        notes: "A journey through space and time",
      },
      {
        id: "4",
        title: "Dunkirk",
        year: "2017",
        genre: "Action, Drama, History",
        poster: "/introsectionpicture.png",
        notes: "Intense WWII thriller",
      },
      {
        id: "5",
        title: "Tenet",
        year: "2020",
        genre: "Action, Sci-Fi, Thriller",
        poster: "/introsectionpicture.png",
        notes: "Time inversion action",
      },
      {
        id: "11",
        title: "John Wick",
        year: "2014",
        genre: "Action, Crime, Thriller",
        poster: "/introsectionpicture.png",
        notes: "High-octane revenge thriller",
      },
      {
        id: "12",
        title: "John Wick",
        year: "2014",
        genre: "Action, Crime, Thriller",
        poster: "/introsectionpicture.png",
        notes: "High-octane revenge thriller",
      }
    ],
    timestamp: "2024-08-01T12:34:56.000Z"
  },
  {
    userId: 2,
    username: "Jane Smith",
    actionType: "update",
    movies: [
      {
        id: "6",
        title: "The Matrix",
        year: "1999",
        genre: "Action, Sci-Fi",
        poster: "/introsectionpicture.png",
        notes: "A revolutionary film in sci-fi genre",
      },
      {
        id: "7",
        title: "Inception",
        year: "2010",
        genre: "Action, Adventure, Sci-Fi",
        poster: "/introsectionpicture.png",
        notes: "Mind-bending thriller",
      },
      {
        id: "8",
        title: "The Matrix Reloaded",
        year: "2003",
        genre: "Action, Sci-Fi",
        poster: "/introsectionpicture.png",
        notes: "The second installment",
      },
      {
        id: "9",
        title: "The Matrix Revolutions",
        year: "2003",
        genre: "Action, Sci-Fi",
        poster: "/introsectionpicture.png",
        notes: "The epic conclusion",
      },
      {
        id: "10",
        title: "John Wick",
        year: "2014",
        genre: "Action, Crime, Thriller",
        poster: "/introsectionpicture.png",
        notes: "High-octane revenge thriller",
      }
    ],
    timestamp: "2024-08-01T13:34:56.000Z"
  }
];

const Feed = () => {
  const [activities, setActivities] = useState([]);
  const [isFollowing, setIsFollowing] = useState([]);

  useEffect(() => {
    // Simulate API call with mock data
    setTimeout(() => {
      setActivities(mockActivities);
      setIsFollowing(mockActivities.map(activity => ({ userId: activity.userId, isFollowing: false })));
    }, 1000); 
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
    console.log(`Like/unlike movie with ID: ${movieId}`);
  };

  return (
    <div className="wrapper">
      <DashBoard />
      <div className="main-dashboard">
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
        {activities.map(activity => {
          const followStatus = isFollowing.find(follow => follow.userId === activity.userId);
          return (
            <div key={activity.timestamp} className="activity-item">
              <div className="user-info">
                <h4>
                  <a href={`/profile/${activity.userId}`} className="user-link">{activity.username}</a>
                </h4>
                {activity.username !== currentUser && (
                  <button 
                    className="button follow-button" 
                    onClick={() => handleFollowToggle(activity.userId)}
                  >
                    {followStatus?.isFollowing ? "Unfollow" : "Follow"}
                  </button>
                )}
              </div>
              <div className="movie-card-list">
                {activity.movies.map(movie => (
                  <div key={movie.id} className="movie-card">
                    <img src={movie.poster} alt={movie.title} className="movie-poster" />
                    <div className="movie-details">
                      <h4>{movie.title}</h4>
                      <p className="bold">Year: {movie.year}</p>
                      <p className="bold">Genre: {movie.genre}</p>
                      <p>Notes: {movie.notes}</p>
                      <p>Action: {activity.actionType}</p>
                      <p>Timestamp: {new Date(activity.timestamp).toLocaleString()}</p>
                      <div className="action-buttons">
                        <button 
                          className="button like-button" 
                          onClick={() => handleLikeToggle(movie.id)}
                        >
                          Like/Unlike
                        </button>
                        <button 
                          className="button view-more-button"
                          onClick={() => console.log('View more details')}
                        >
                          View More
                        </button>
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
};

export default Feed;