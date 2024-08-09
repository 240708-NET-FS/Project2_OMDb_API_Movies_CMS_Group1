import React, { useState, useEffect, useCallback } from 'react';
import "./usercard.css";
import { handleFollowToggle } from '../../utils/followUtils';

const UserCard = ({ user, followevent, setFollowevent}) => {
    const [following, setFollowing] = useState(false);
    const [followerUserId, setFollowerUserId] = useState(null);

    const getCurrentUser = () => JSON.parse(localStorage.getItem('user'));

    const fetchFollowingStatus = useCallback(async () => {
        const currentUser = getCurrentUser();
        try {
            const response = await fetch(`http://localhost:5299/api/Followers/${currentUser.userId}/following-with-movies`);
            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`Failed to fetch following status with status ${response.status}, message: ${errorText}`);
            }
            const followingData = await response.json();
            console.log("Following data: ", followingData);
            const isFollowing = followingData.find(follow => follow.userId === user.userId);
            if (isFollowing) {
                setFollowing(true);
                setFollowerUserId(isFollowing.followingRelationshipId);
            } else {
                setFollowing(false);
                setFollowerUserId(null);  // Reset followerUserId if not following
            }
        } catch (error) {
            console.error('Error fetching following status:', error);
        }
    }, [user.userId]);

    useEffect(() => {
        fetchFollowingStatus();
    }, [fetchFollowingStatus]);

    const handleFollowClick = async () => {
        try {
            const success = await handleFollowToggle(followerUserId, user.userId, setFollowerUserId, setFollowing);
            if (success) {
                // Refetch following status to ensure updated state
                fetchFollowingStatus();

                // even triggered when a handle follow toggle is successful
                setFollowevent(!followevent);
                
            }
        } catch (error) {
            console.error('Error in handleFollowClick:', error);
            alert(`Error toggling follow: ${error.message}`);
        }
    };

    return (
        <div className="feed-user-info">
            <h4>{user.userName}</h4>
            <button className="button" onClick={handleFollowClick}>
                {following ? "Unfollow" : "Follow"}
            </button>
        </div>
    );
};

export default UserCard;