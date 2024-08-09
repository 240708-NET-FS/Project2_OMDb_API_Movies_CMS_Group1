const OMDB_API_KEY = "";
const OMDB_API_URL = 'http://www.omdbapi.com/';

//Fetches Data for the Feed Component
export const fetchData = async (
  getCurrentUser, 
  setActivities, 
  setLoading
) => {
  try {
    
    setLoading(true);
    const currentUser = getCurrentUser(); // Get the current logged-in user

    if (!currentUser) {
      throw new Error('No user logged in'); // If no user is logged in, throw an error
    }

    // Fetch data for all users with their movie lists from the backend
    const response = await fetch('http://localhost:5299/api/Users/users-with-movies');

    if(!response.ok)
    {
      throw new Error ("Failed to access backend");
    }

    const data = await response.json();

    // Filter out the current user from the fetched data
    const filteredData = data.filter(user => user.userId !== currentUser.userId);

    // Map through the filtered users and find their most recent movie activity
    const usersWithRecentActivity = filteredData.map(user => {
      const mostRecentMovie = user.userMovies.reduce((latest, movie) => {
        // Find the timestamp of the most recently updated or created movie
        const movieTimestamp = new Date(movie.updatedAt || movie.createdAt).getTime();
        return movieTimestamp > latest.timestamp ? { ...movie, timestamp: movieTimestamp } : latest;
      }, { timestamp: 0 }); // Initialize with a timestamp of 0

      return {
        ...user,
        mostRecentActivity: mostRecentMovie.timestamp // Add the most recent activity timestamp to the user object
      };
    });

    // Sort users by their most recent movie activity in descending order
    usersWithRecentActivity.sort((a, b) => b.mostRecentActivity - a.mostRecentActivity);

    console.log("activites: ", usersWithRecentActivity)

    // Set the activities state with the sorted users
    setActivities(usersWithRecentActivity);

  } catch (error) {
    throw new Error('Error fetching activities:', error);
  } finally {
   setLoading(false); // Set loading state to false when the fetching process is complete
  }
};

export const getCurrentTimestamp = () => {
  return new Date().toISOString();
};

//Fetches Top movies with populated data for feed
export const fetchTopMoviesWithDetails = async () => {
  try {
      // Fetch top movies
      const response = await fetch('http://localhost:5299/api/Rankings/top');
      if (!response.ok) {
          throw new Error('Failed to fetch top movies');
      }
      const movies = await response.json();

      // Sort movies by average rating in descending order
      const sortedMovies = movies.sort((a, b) => b.averageRating - a.averageRating);

      // Fetch additional details for each movie from the OMDb API
      const detailedMovies = await Promise.all(sortedMovies.map(async (movie) => {
          try {
              const omdbResponse = await fetch(`${OMDB_API_URL}?i=${movie.omdbId}&apikey=${OMDB_API_KEY}`);
              if (!omdbResponse.ok) {
                  throw new Error(`Failed to fetch details for movie ID ${movie.omdbId}`);
              }
              const details = await omdbResponse.json();

              return {
                  ...movie,
                  title: details.Title,
                  year: details.Year,
                  genre: details.Genre,
                  poster: details.Poster
              };
          } catch (omdbError) {
              console.error('Error fetching movie details from OMDb API:', omdbError);
              return {
                  ...movie,
                  title: 'N/A',
                  year: 'N/A',
                  genre: 'N/A',
                  poster: 'N/A'
              };
          }
      }));

      return detailedMovies;
  } catch (error) {
      console.error('Error fetching top movies with details:', error);
      return [];
  }
};

//Fetches the users , current user is following
export const fetchFollowingUsers = async (currentUserId) => {
  try {
    const response = await fetch(`http://localhost:5299/api/Followers/${currentUserId}/following-with-movies`);
    if (!response.ok) {
      throw new Error('Failed to fetch following users');
    }
    return await response.json();
  } catch (error) {
    console.error('Error fetching following users:', error);
    return [];
  }
};
