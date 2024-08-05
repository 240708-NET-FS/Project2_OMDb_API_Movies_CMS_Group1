import React from 'react';
import './login.css';
import { Link } from 'react-router-dom';

const Login = () => {
    return (
        <div>
            <div className='main-head'><h2>iMovies</h2></div>

            <div className="form-container-main">
                <div className="form-container-sub">
                    <div className="form-design">
                        <h1 className="form-title-head">
                            Sign in
                        </h1>
                        <form>
                            <div className="row-input-container">
                                <label className='form-label'>Username </label>
                                <input type="text" name="uname" required />
                            </div>

                            <div className="row-input-container">
                                <label className='form-label'>Password </label>
                                <input type="password" name="pass" required />
                            </div>

                            <div className="row-buttun-container">
                                <span className="input-button-inner">
                                    <input type="submit" className='input-button' />
                                    <span className="a-button-text">
                                        Sign in
                                    </span>
                                </span>
                            </div>
                            <div className="divider-break"><h5>New to iMovies?</h5></div>
                            <span id="" className="button-redirect-div">
                                <span className="a-button-inner">
                                    <Link id="createAccountSubmit" to="/signup">
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