import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import BookmarkRow from '../components/BookmarkRow';
import { useAuthContext } from '../AuthContext';

const MyBookmarks = () => {
    const { user } = useAuthContext();
    const [bookmarks, setbookmarks] = useState([]);

    useEffect(() => {
        getBookmarks();
    }, []);
    const getBookmarks = async () => {
        const { data } = await axios.get(`/api/bookmark/my-bookmarks`);
        setbookmarks(data);
    }
    const onDeleteClick = async (bookmark) => {
        await axios.post('/api/bookmark/deletebookmark', { ...bookmark });
        getBookmarks();
    }
    const onUpdateClick = async (id, title) => {
        await axios.post('/api/bookmark/editbookmark', { id, title });
        getBookmarks();       
    }

    const generateTable = () => {
        return <table className='mt-3 table table-hover table-bordered table-striped'>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Url</th>
                    <th>Edit/Delete</th>
                </tr>
            </thead>
            <tbody>
                {bookmarks.map((bookmark, i) => {
                    return <BookmarkRow
                        bookmark={bookmark}
                        key={i}
                        onDeleteClick={onDeleteClick}
                        onUpdateClick={onUpdateClick}
                    />
                }
                )}

            </tbody>
        </table>
    }

    return (
        <div className='container'>
            <h1>Welcome back {user.firstName} {user.lastName}</h1>
            <Link to='/addbookmark'>
                <button className='btn btn-block btn-primary'>Add Bookmark</button>
            </Link>
            {generateTable()}
        </div>
    );
}


export default MyBookmarks;