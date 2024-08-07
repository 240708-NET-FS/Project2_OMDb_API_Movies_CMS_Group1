import React, { useEffect, useState } from 'react';
import './userprofile.css';
import Dashboard from '../../components/Dashboard/DashBoard';

const mockUserData = {
  username: "romero02",
  name: "John Doe",
  createdAt: "2023-01-01T00:00:00.000Z",
  followers: ["Jane Smith", "Bob Johnson", "Alice Brown", "Charlie Davis", "Eve Walker", "Alice Brown", "Charlie Davis", "Eve Walker", "Alice Brown", "Charlie Davis", "Eve Walker"]
};

const mockFavoriteMovies = [
  {
    id: "1",
    title: "Inception",
    year: "2010",
    genre: "Action, Adventure, Sci-Fi",
    poster: "/introsectionpicture.png",
    addedAt: "2023-01-02T00:00:00.000Z",
    notes: "Mind-bending thriller"
  },
];

const UserProfile = () => {
  const [user, setUser] = useState(null);
  const [favoriteMovies, setFavoriteMovies] = useState([]);

  useEffect(() => {
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

  if (!user) {
    return (
      <div className='wrapper'>
        <Dashboard />
        <div className='user-profile-loading'>Loading...</div>
      </div>
    );
  }

  const userNameParts = user.name.split(' ');
  const firstName = userNameParts[0];
  const lastName = userNameParts.slice(1).join(' ');

  return (
    <div className='wrapper'>
      <Dashboard />
      <div className="user-profile-container">
        <div className="user-profile-info">
          <div className="user-profile-details">
            <h2>{user.username}</h2>
            <h2>{firstName} {lastName}</h2>
            <p>Joined: {new Date(user.createdAt).toLocaleDateString()}</p>
          </div>
          <div className="user-profile-followers">
            <h4>Followers</h4>
            <ul>
              {user.followers.map((follower, index) => (
                <li key={index}>{follower}</li>
              ))}
            </ul>
          </div>
        </div>
        <div className="user-profile-movies">
          <h3>Favorite Movies</h3>
          <div className="user-profile-movie-cards">
            {favoriteMovies.map(movie => (
              <div key={movie.id} className="user-profile-movie-card">
                <img src={movie.poster} alt={movie.title} className="user-profile-movie-poster" />
                <div className="user-profile-movie-details">
                  <h4>{movie.title}</h4>
                  <p>Year: {movie.year}</p>
                  <p>Genre: {movie.genre}</p>
                  <p>Added on: {new Date(movie.addedAt).toLocaleDateString()}</p>
                  <p>Notes: {movie.notes}</p>
                  <div className="user-profile-movie-action-buttons">
                    <button className="button update-button" onClick={() => handleUpdate(movie.id)}>Update</button>
                    <button className="button delete-button" onClick={() => handleDelete(movie.id)}>Delete</button>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default UserProfile;