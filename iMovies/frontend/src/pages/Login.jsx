import React from 'react'

const Login = () => {
    return (
        <div>
            <div className='main-head'><h2>OMDb</h2></div>

            <div className="form-container-main">
                <div className="form-container-sub">
                    <div className="form-design">
                        <h1 class="form-title-head">
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
                                <span class="input-button-inner">
                                    <input type="submit" className='input-button' />
                                    <span class="a-button-text">
                                        Sign in
                                    </span>
                                </span>
                            </div>
                            <div class="divider-break"><h5>New to OMDb?</h5></div>
                            <span id="" class="button-redirect-div"><span class="a-button-inner">
                                <a id="createAccountSubmit" href="#">
                                    Create your OMDb account
                                </a></span></span>
                        </form>
                    </div>

                </div>
            </div>
        </div>
    )
}

export default Login;
