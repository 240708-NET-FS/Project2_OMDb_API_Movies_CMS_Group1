import { getCurrentTimestamp } from "./fetchData";

export const handleFollowToggle = async (followerUserId, userId, setFollowerUserId, setFollowing) => {
    const currentUser = JSON.parse(localStorage.getItem('user'));

    if (!currentUser) {
        alert('User not logged in');
        return false;
    }

    try {
        if (followerUserId) {
            // Unfollow logic
            console.log('Attempting to unfollow...');
            const response = await fetch(`http://localhost:5299/api/Followers/${followerUserId}`, { method: 'DELETE' });

            if (!response.ok) {
                const errorText = await response.text();
                console.error(`Failed to unfollow, status: ${response.status}, message: ${errorText}`);
                alert(`Error: ${errorText}`);
                return false;
            }
            setFollowerUserId(null);
            setFollowing(false);
            
        } else {
            // Follow logic
            console.log('Attempting to follow:', { userId, followerUserId: currentUser.userId });
            const newFollower = {
                userId: userId,
                followerUserId: currentUser.userId,
                createdAt: getCurrentTimestamp()
            };
            const response = await fetch('http://localhost:5299/api/Followers', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newFollower)
            });

            if (!response.ok) {
                const errorText = await response.text();
                console.error(`Failed to follow, status: ${response.status}, message: ${errorText}`);
                alert(`Error: ${errorText}`);
                return false;
            }

            const followData = await response.json();
            console.log('Successfully followed: ', followData);
            setFollowerUserId(followData.FollowingRelationshipId);
            setFollowing(true);
        }
        return true;
    } catch (error) {
        console.error('Error toggling follow:', error);
        alert(`Error: ${error.message}`);
        return false;
    }
};