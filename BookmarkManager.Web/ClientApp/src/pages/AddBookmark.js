import axios from 'axios';
import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';


const AddBookmark = () => {    
    const [formData, setFormData] = useState({ title: '', url: ''});
    const history = useHistory();
    const onTextChange = e => {
        const copy = { ...formData };
        copy[e.target.name] = e.target.value;
        setFormData(copy);
    }
    const onFormSubmit = async e => {
        e.preventDefault();      
        await axios.post('/api/bookmark/addbookmark', formData);
        history.push(`/mybookmarks`);
    }

    return (
        <div className='row'>
            <div className='col-md-6 offset-md-3 card card-body bg-light'>
                <h4>Add Bookmark</h4>
                <form onSubmit={onFormSubmit}>
                    <input onChange={onTextChange} value={formData.title} type="text"
                        name="title" placeholder="Title" className="form-control" />
                    <br />
                    <input onChange={onTextChange} value={formData.url} type="text"
                        name="url" placeholder="Url" className="form-control" />
                    <br />
                    <button className='btn btn-primary'>Add</button>
                </form>
            </div>
        </div>
    );
}

export default AddBookmark;