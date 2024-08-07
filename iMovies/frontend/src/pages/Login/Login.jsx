import React, { useState } from 'react';
import './login.css';
import { Link, useNavigate } from 'react-router-dom';

const Login = () => 
    {

    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        uname: '',
        pass: ''
    });

    const [errors, setErrors] = useState({});
    const [message, setMessage] = useState('');
    const [messageType, setMessageType] = useState('');

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const validateForm = () => {
        let formErrors = {};
        let valid = true;

        if (!formData.uname.trim()) {
            formErrors.uname = 'Username is required';
            valid = false;
        }

        if (!formData.pass.trim()) {
            formErrors.pass = 'Password is required';
            valid = false;
        }

        setErrors(formErrors);
        return valid;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (validateForm()) {
            try {
                const response = await fetch('http://localhost:5299/api/Auth/login', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        userName: formData.uname,
                        password: formData.pass
                    })
                });

                if (response.ok) {
                    const data = await response.json();
                    const { token } = data;

                    // Store token in local storage
                    localStorage.setItem('token', token);

                    // Emit a custom event to notify about the token change
                    window.dispatchEvent(new CustomEvent('tokenChanged'));

                    setMessage('Login successful!');
                    setMessageType('success');
                    
                    setTimeout(() => {
                        navigate('/feed'); 

                    }, 2000);
                } else {
                    const errorData = await response.json();
                    setMessage(errorData.message || 'Login failed');
                    setMessageType('error');
                }
            } catch (error) {
                setMessage('Error: ' + error.message);
                setMessageType('error');
            }
        }
    };

    return (
        <div>
            <div className='main-head'><h2>iMovies</h2></div>

            <div className="form-container-main">
                <div className="form-container-sub">
                    <div className="form-design">
                        <h1 className="form-title-head">
                            Sign in
                        </h1>
                        <form onSubmit={handleSubmit}>
                            <div className="row-input-container">
                                <label className='form-label'>Username</label>
                                <input
                                    type="text"
                                    name="uname"
                                    value={formData.uname}
                                    onChange={handleChange}
                                    required
                                />
                                {errors.uname && <span className="error">{errors.uname}</span>}
                            </div>

                            <div className="row-input-container">
                                <label className='form-label'>Password</label>
                                <input
                                    type="password"
                                    name="pass"
                                    value={formData.pass}
                                    onChange={handleChange}
                                    required
                                />
                                {errors.pass && <span className="error">{errors.pass}</span>}
                            </div>

                            <div className="row-button-container">
                                <span className="input-button-inner">
                                    <input type="submit" className='input-button' />
                                    <span className="a-button-text">
                                        Sign in
                                    </span>
                                </span>
                            </div>
                            {message && <div className={`message ${messageType}`}>{message}</div>}
                            <div className="divider-break"><h5>New to iMovies?</h5></div>
                            <span className="button-redirect-div">
                                <span className="a-button-inner">
                                    <Link to="/signup">
                                        Create your iMovies account
                                    </Link>
                                </span>
                            </span>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Login;