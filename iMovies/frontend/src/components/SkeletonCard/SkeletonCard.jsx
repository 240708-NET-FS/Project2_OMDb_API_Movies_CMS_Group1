import React from 'react';
import './skeletoncard.css';

const SkeletonCard = () => {
  return (
    <div className='skeleton-card'>
      <div className='skeleton-thumbnail'></div>
      <div className='skeleton-content'>
        <div className='skeleton-title'></div>
        <div className='skeleton-genre'></div>
        <div className='skeleton-rating'></div>
        <div className='skeleton-button'></div>
      </div>
    </div>
  );
};

export default SkeletonCard;