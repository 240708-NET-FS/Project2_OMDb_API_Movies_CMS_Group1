import React from 'react';
import './signup.css';
import { Link } from 'react-router-dom';

const SignUp = () => {
    return (
        <div>
            <div className='main-head'><h2>iMovies</h2></div>

            <div className="form-container-main">
                <div className="form-container-sub">
                    <div className="form-design">
                        <h1 className="form-title-head">
                            Sign Up
                        </h1>
                        <form>
                            <div className="row-input-container">
                                <label className='form-label'>First Name</label>
                                <input type="text" name="fname" required />
                            </div>

                            <div className="row-input-container">
                                <label className='form-label'>Last Name</label>
                                <input type="text" name="lname" required />
                            </div>

                            <div className="row-input-container">
                                <label className='form-label'>Username</label>
                                <input type="text" name="uname" required />
                            </div>

                            <div className="row-input-container">
                                <label className='form-label'>Password</label>
                                <input type="password" name="pass" required />
                            </div>

                            <div className="row-input-container">
                                <label className='form-label'>Confirm Password</label>
                                <input type="password" name="confirmpass" required />
                            </div>

                            <div className="row-button-container">
                                <span className="input-button-inner">
                                    <input type="submit" className='input-button' />
                                    <span className="a-button-text">
                                        Sign Up
                                    </span>
                                </span>
                            </div>
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