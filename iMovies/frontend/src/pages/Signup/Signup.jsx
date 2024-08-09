import React, { useState } from 'react';
import './signup.css';
import { Link, useNavigate } from 'react-router-dom';

const SignUp = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        fname: '',
        lname: '',
        uname: '',
        pass: '',
        confirmpass: ''
    });
    const [errors, setErrors] = useState({});
    const [message, setMessage] = useState('');
    const [messageType, setMessageType] = useState(''); 

    const capitalizeFirstLetter = (string) => {
        return string.charAt(0).toUpperCase() + string.slice(1).toLowerCase();
    };

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const validateForm = () => {
        let formErrors = {};
        let valid = true;
    
        //validate that the input is not empty or consisting solely of whitespace characters
        if (!formData.fname.trim()) {
            formErrors.fname = 'First name is required';
            valid = false;
        }

        if (!formData.lname.trim()) {
            formErrors.lname = 'Last name is required';
            valid = false;
        }

        if (!formData.uname.trim()) {
            formErrors.uname = 'Username is required';
            valid = false;
        }

        if (!formData.pass) {
            formErrors.pass = 'Password is required';
            valid = false;
        } else if (formData.pass.length < 6) {
            formErrors.pass = 'Password must be at least 6 characters';
            valid = false;
        }

        if (formData.pass !== formData.confirmpass) {
            formErrors.confirmpass = 'Passwords do not match';
            valid = false;
        }

        setErrors(formErrors);
        return valid;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (validateForm()) {
            try {
                // Capitalize first letter of fname and lname
                const capitalizedFname = capitalizeFirstLetter(formData.fname);
                const capitalizedLname = capitalizeFirstLetter(formData.lname);

                const response = await fetch('http://localhost:5299/api/Users/register', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        firstName: capitalizedFname,
                        lastName: capitalizedLname,
                        userName: formData.uname,
                        password: formData.pass
                    })
                });

                if (response.ok) {
                    setMessage('Registration successful!');
                    setMessageType('success');
                    
                    setTimeout(() => {
                        navigate('/login'); 
                    }, 2000); 
                } else {
                    const errorData = await response.json();
                    setMessage(errorData.message || 'Registration failed');
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
                            Sign Up
                        </h1>
                        <form onSubmit={handleSubmit}>
                            <div className="row-input-container">
                                <label className='form-label'>First Name</label>
                                <input
                                    type="text"
                                    name="fname"
                                    value={formData.fname}
                                    onChange={handleChange}
                                    required
                                />
                                {errors.fname && <span className="error">{errors.fname}</span>}
                            </div>

                            <div className="row-input-container">
                                <label className='form-label'>Last Name</label>
                                <input
                                    type="text"
                                    name="lname"
                                    value={formData.lname}
                                    onChange={handleChange}
                                    required
                                />
                                {errors.lname && <span className="error">{errors.lname}</span>}
                            </div>

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

                            <div className="row-input-container">
                                <label className='form-label'>Confirm Password</label>
                                <input
                                    type="password"
                                    name="confirmpass"
                                    value={formData.confirmpass}
                                    onChange={handleChange}
                                    required
                                />
                                {errors.confirmpass && <span className="error">{errors.confirmpass}</span>}
                            </div>

                            <div className="row-button-container">
                                <span className="input-button-inner">
                                    <input type="submit" className='input-button' />
                                    <span className="a-button-text">
                                        Sign Up
                                    </span>
                                </span>
                            </div>
                            {message && <div className={`message ${messageType}`}>{message}</div>} {/* add class based on messageType */}
                            <div className="divider-break"><h5>Already have an account?</h5></div>
                            <span className="button-redirect-div">
                                <span className="a-button-inner">
                                    <Link to="/login">
                                        Sign in to your iMovies account
                                    </Link>
                                </span>
                            </span>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default SignUp;