import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Joke from '../components/Joke';

const ViewAll = () => {
    const [jokes, setJokes] = useState([]);

    useEffect(() => {
        const loadJokes = async () => {
            const { data } = await axios.get(`api/jokes/viewall`);
            setJokes(data);
        }

        loadJokes();
    }, []);


    return (
        <>
                    {jokes.map(j =>
                        <Joke
                            key={j.id}
                            joke={j}
                        />)}
        </>
    )
}

export default ViewAll;