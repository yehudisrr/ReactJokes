import React from 'react';


function Joke({ joke }) {

    var likedCount = joke.likes.filter(l => l.liked).length;
    var dislikedCount = joke.likes.filter(l => !l.liked).length;

    return (
        <div className="row">
            <div className="col-md-6 offset-md-3 card card-body bg-light">
                <h4>{joke.setup}</h4>
                <h4>{joke.punchline}</h4>
                <br />
                <h4>Likes:{likedCount > 0 ? likedCount : 0}</h4>
                <h4>Dislikes:{dislikedCount > 0 ? dislikedCount : 0}</h4>
                <h4>
                    <button className='btn btn-link'>Refresh</button>
                </h4>
            </div>
        </div>
    );
}

export default Joke;
