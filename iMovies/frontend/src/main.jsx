import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from "./pages/Home/Home"
import SignUp from './pages/Signup/SignUp';
import Layout from './components/Layout/Layout';
import './index.css';
import Login from './pages/Login/Login';
import UserProfile from './pages/Userprofile/UserProfile';
import Feed from './pages/Feed/Feed';
import AddMovies from './pages/AddMovies/AddMovies';
import Following from './pages/Following/Following';
import PrivateRoutes from './components/PrivateRoutes';

ReactDOM.createRoot(document.getElementById('root')).render(
  <Router>
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<Home />} />
        <Route path="signup" element={<SignUp />} />
        <Route path="login" element={<Login />} />

        <Route element={<PrivateRoutes />}>
            <Route path="userprofile" element={<UserProfile />} />
            <Route path="feed" element={<Feed />} />
            <Route path="addmovies" element={<AddMovies />} />
            <Route path="following" element={<Following />} />
          </Route>

      </Route>
    </Routes>
    </Router>
);
