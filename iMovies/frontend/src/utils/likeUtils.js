import { getCurrentTimestamp } from "./fetchData";

export const handleLikeToggle = async (userMovieId, likeId, setLikeId, isLiked,setLikeCount) => {
  const currentUser = JSON.parse(localStorage.getItem('user'));

  if (!currentUser) {
    alert('User not logged in');
    return false;
  }

  try {
    if (isLiked) {
      // Unlike logic
      console.log('Attempting to delete like...');
      const response = await fetch(`http://localhost:5299/api/Likes/${likeId}`, { method: 'DELETE' });
      if (!response.ok) {
        console.error(`Failed to delete like, status: ${response.status}`);
        throw new Error('Failed to delete like');
      }
      setLikeId(null);
      setLikeCount(prev => Math.max(prev - 1, 0)); 
    } else {
      // Like logic
      const newLike = {
        userId: currentUser.userId,
        userMovieId,
        createdAt: getCurrentTimestamp()
      };
      console.log('Attempting to add like:', newLike);
      const response = await fetch('http://localhost:5299/api/Likes', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newLike)
      });
      if (!response.ok) {
        console.log(`Failed to post like, status: ${response.status}`);
        throw new Error('Failed to post like');
      }

      const savedLike = await response.json();
      setLikeId(savedLike.likeId); // Updated from savedLike.id to savedLike.likeId
      setLikeCount(prev => prev + 1);
    }
    return true;
  } catch (error) {
    console.error('Error toggling like:', error);
    alert(`Error: ${error.message}`);
    return false;
  }
};