import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { useAuthContext } from '../AuthContext';

const Home = () => {

    const { user } = useAuthContext();

    const [joke, setJoke] = useState({
        id: '',
        setup: '',
        punchline: '',
        likes: []
    });

    const [likedCount, setLikedCount] = useState('');
    const [dislikedCount, setDislikedCount] = useState('');
    const [disableLikeBtn, setDisableLikeBtn] = useState(false);
    const [isDisliked, setIsDisliked] = useState(false);
    const [isLiked, setIsLiked] = useState(false);
    const [disableDislikeBtn, setDisableDislikeBtn] = useState(false);
    const [timedOut, setTimedOut] = useState(false);

    const setJokeAndLikes = (data) => {
        setJoke(data);
        setLikedCount(data.likes.filter(l => l.liked).length);
        setDislikedCount(data.likes.filter(l => !l.liked).length);
        setIsLiked((data.likes.filter(l => l.liked).length) > 0);
        console.log((data.likes.filter(l => l.liked).length));
        setIsDisliked(data.likes.some(l => !l.liked && l.user === user));
    }

    useEffect(() => {
        const getJoke = async () => {
            const { data } = await axios.get('/api/jokes/getjoke');
            setJokeAndLikes(data);
        }
        getJoke();
   
    }, []);

    useEffect(() => {
        const timeout = setTimeout(() => {
            if (disableLikeBtn) {
                setTimedOut(true);
            }
    }, 5000);
    }, [disableLikeBtn]);

    useEffect(() => {
        const timeout = setTimeout(() => {
            if (disableDislikeBtn) {
                setTimedOut(true);
            }
        }, 5000);
    }, [disableDislikeBtn]);

    const onLikeClick = async (id) => {
        const { data } = await axios.post('/api/jokes/like', { jokeId: id, liked: true });
        setJokeAndLikes(data);
        setIsLiked(true);
        setIsDisliked(false);
        setDisableDislikeBtn(true);
    }

    const onDislikeClick = async (id) => {
        const { data } = await axios.post('/api/jokes/like', { jokeId: id, liked: false });
        setJokeAndLikes(data);
        setIsLiked(false);
        setIsDisliked(true);
        setDisableLikeBtn(true);
    }

    const { id, setup, punchline } = joke;

    return (
        <div className="row">
            <div className="col-md-6 offset-md-3 card card-body bg-light">
                <h4>{setup}</h4>
                <h4>{punchline}</h4>
                {!user && <div>
                    <Link to='/login'>Login to your account to like/dislike this joke</Link>
                </div>
                    }
                <br />
                {user &&
                    <>
                    <button id="LikeBtn" className="btn btn-primary btn-sm"
                        disabled={isLiked || timedOut}
                        onClick={() => onLikeClick(id)}>Like</button>
                    <button id="DislikeBtn" className="btn btn-danger btn-sm"
                        disabled={isDisliked || timedOut}
                        onClick={() => onDislikeClick(id)}>Dislike</button>
                    </>
                }
                <h4>Likes:{likedCount > 0? likedCount : 0}</h4>
                <h4>Dislikes:{dislikedCount > 0? dislikedCount : 0}</h4>
                <h4>
                    <button className='btn btn-link' onClick={() => window.location.reload(false)}>Refresh</button>
                </h4>
            </div>
        </div>
    )
}

export default Home;