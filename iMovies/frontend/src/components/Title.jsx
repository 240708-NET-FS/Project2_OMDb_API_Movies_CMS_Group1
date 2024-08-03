import React from 'react';

const Title = ({titlenumber}) => {

  const [ state, useState] = React.useState(0);

    const handleButton = () => 
        {
    
            useState(state => state + 1);
    
        }

  return (
    <div>
        <div className='class'>Title {titlenumber}</div>
        <p>{state}</p>
        <button onClick={handleButton}>Click me to add number</button>
    </div>
  )
}

export default Title;