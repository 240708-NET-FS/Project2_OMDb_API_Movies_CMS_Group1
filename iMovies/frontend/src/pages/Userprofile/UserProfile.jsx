import React, { useEffect, useState } from 'react';
import './userprofile.css';

const mockUserData = {
  name: "John Doe",
  email: "john.doe@example.com",
  createdAt: "2023-01-01T00:00:00.000Z",
  followers: ["Jane Smith", "Bob Johnson"]
};

const mockFavoriteMovies = [
  {
    id: "1",
    title: "Inception",
    year: "2010",
    genre: "Action, Adventure, Sci-Fi",
    poster: "/introsectionpicture.png",
    addedAt: "2023-01-02T00:00:00.000Z",
    notes: "Mind-bending thriller",
    liked: false
  },
  {
    id: "2",
    title: "The Matrix",
    year: "1999",
    genre: "Action, Sci-Fi",
    poster: "/introsectionpicture.png",
    addedAt: "2023-01-03T00:00:00.000Z",
    notes: "A revolutionary film in sci-fi genre",
    liked: true
  },
  {
    id: "3",
    title: "The Matrix Reloaded",
    year: "2003",
    genre: "Action, Sci-Fi",
    poster: "/introsectionpicture.png",
    addedAt: "2023-01-04T00:00:00.000Z",
    notes: "An epic sequel",
    liked: false
  },
  {
    id: "4",
    title: "The Matrix Revolutions",
    year: "2003",
    genre: "Action, Sci-Fi",
    poster: "/introsectionpicture.png",
    addedAt: "2023-01-05T00:00:00.000Z",
    notes: "The grand finale",
    liked: true
  },
  {
    id: "5",
    title: "Avatar",
    year: "2009",
    genre: "Action, Adventure, Fantasy",
    poster: "/introsectionpicture.png",
    addedAt: "2023-01-06T00:00:00.000Z",
    notes: "Visually stunning",
    liked: false
  },
  {
    id: "6",
    title: "Interstellar",
    year: "2014",
    genre: "Adventure, Drama, Sci-Fi",
    poster: "/introsectionpicture.png",
    addedAt: "2023-01-07T00:00:00.000Z",
    notes: "A journey beyond stars",
    liked: true
  }
];

// Mock current logged-in user, different from mockUserData.name to show follow and like buttons
const currentUser = "Alice Doe";

const UserProfile = () => {
  const [user, setUser] = useState(null);
  const [favoriteMovies, setFavoriteMovies] = useState([]);
  const [isFollowing, setIsFollowing] = useState(false);

  useEffect(() => {
    // Simulate API call with mock data
    setTimeout(() => {
      setUser(mockUserData);
      setFavoriteMovies(mockFavoriteMovies);
    }, 1000); 
  }, []);

  const handleUpdate = (movieId) => {
    console.log(`Update movie with ID: ${movieId}`);
  };

  const handleDelete = (movieId) => {
    console.log(`Delete movie with ID: ${movieId}`);
  };

  const handleLikeToggle = (movieId) => {
    setFavoriteMovies(prevMovies => 
      prevMovies.map(movie => 
        movie.id === movieId ? { ...movie, liked: !movie.liked } : movie
      )
    );
  };

  const handleFollowToggle = () => {
    setIsFollowing(prevIsFollowing => !prevIsFollowing);
  };

  if (!user) {
    return <div>Loading...</div>;
  }

  const isCurrentUserProfile = user.name === currentUser;

  return (
    <div className="user-profile">
      <div className="user-info">
        <h2>{user.name}'s Profile</h2>
        <p>Email: {user.email}</p>
        <p>Joined: {new Date(user.createdAt).toLocaleDateString()}</p>
        <div className="followers-list">
          <h4>Followers</h4>
          <ul>
            {user.followers.map((follower, index) => (
              <li key={index}>{follower}</li>
            ))}
          </ul>
        </div>
        {!isCurrentUserProfile && (
          <button className="button follow-button" onClick={handleFollowToggle}>
            {isFollowing ? "Unfollow" : "Follow"}
          </button>
        )}
      </div>
      <div className="favorite-movies">
        <h3>Favorite Movies</h3>
        <div className="movies-list">
          {favoriteMovies.map(movie => (
            <div key={movie.id} className="movie-item">
              <img src={movie.poster} alt={movie.title} className="movie-poster" />
              <div className="movie-details">
                <h4>{movie.title}</h4>
                <p>Year: {movie.year}</p>
                <p>Genre: {movie.genre}</p>
                <p>Added on: {new Date(movie.addedAt).toLocaleDateString()}</p>
                <p>Notes: {movie.notes}</p>
                <div className="action-buttons">
                  {isCurrentUserProfile ? (
                    <>
                      <button className="button update-button" onClick={() => handleUpdate(movie.id)}>Update</button>
                      <button className="button delete-button" onClick={() => handleDelete(movie.id)}>Delete</button>
                    </>
                  ) : (
                    <button 
                      className={`button like-button ${movie.liked ? 'liked' : ''}`} 
                      onClick={() => handleLikeToggle(movie.id)}
                    >
                      {movie.liked ? "Unlike" : "Like"}
                    </button>
                  )}
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default UserProfile;