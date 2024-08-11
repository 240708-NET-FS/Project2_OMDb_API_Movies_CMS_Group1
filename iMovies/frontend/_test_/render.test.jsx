// Render Tests for React
import React from 'react';
import { render, screen, fireEvent , waitFor} from '@testing-library/react';
import { MemoryRouter, Routes, Route } from 'react-router-dom';
import '@testing-library/jest-dom';
import SkeletonCard from '../src/components/SkeletonCard/SkeletonCard'; 
import Dashboard from '../src/components/Dashboard/DashBoard';
import SignUp from '../src/pages/Signup/SignUp';
import Login from '../src/pages/Login/Login';
import Home from '../src/pages/Home/Home';
import Header from '../src/components/Header/Header';
import { isTokenExpired, decodeToken } from '../src/utils/jwtUtils';
import PrivateRoutes from '../src/components/PrivateRoutes';
import UserCard from '../src/components/UserCard/UserCard';
import MovieCard from '../src/components/MovieCard/MovieCard';
import Layout from "../src/components/Layout/Layout";
import Following from "../src/pages/Following/Following";
import AddMovies from "../src/pages/AddMovies/AddMovies";
import Feed from "../src/pages/Feed/Feed";
import UserProfile from "../src/pages/Userprofile/UserProfile";

// Mock the fetch API
global.fetch = jest.fn();


// Mocks
jest.mock('../src/utils/jwtUtils', () => ({
  decodeToken: jest.fn(),
  isTokenExpired: jest.fn()
}));

jest.mock('../src/utils/fetchData', () => ({
    fetchData: jest.fn(),
    fetchTopMoviesWithDetails: jest.fn()
  }));


test('renders SkeletonCard with correct elements', () => {
    const { container } = render(<SkeletonCard />);
  
    // Check if the skeleton card container is rendered
    const cardElement = container.querySelector('.skeleton-card');
    expect(cardElement).toBeInTheDocument();
  
    // Check if the skeleton thumbnail is rendered
    const thumbnailElement = container.querySelector('.skeleton-thumbnail');
    expect(thumbnailElement).toBeInTheDocument();
  
    // Check if the skeleton content is rendered
    const contentElement = container.querySelector('.skeleton-content');
    expect(contentElement).toBeInTheDocument();
  
    // Check if the skeleton title is rendered
    const titleElement = contentElement.querySelector('.skeleton-title');
    expect(titleElement).toBeInTheDocument();
  
    // Check if the skeleton genre is rendered
    const genreElement = contentElement.querySelector('.skeleton-genre');
    expect(genreElement).toBeInTheDocument();
  
    // Check if the skeleton rating is rendered
    const ratingElement = contentElement.querySelector('.skeleton-rating');
    expect(ratingElement).toBeInTheDocument();
  
    // Check if the skeleton button is rendered
    const buttonElement = contentElement.querySelector('.skeleton-button');
    expect(buttonElement).toBeInTheDocument();
  });

test('renders Dashboard with all NavLinks', () => {
    render(
      <MemoryRouter>
        <Dashboard />
      </MemoryRouter>
    );
  
    // Check if all NavLinks are rendered
    expect(screen.getByText(/Feed/i)).toBeInTheDocument();
    expect(screen.getByText(/Following/i)).toBeInTheDocument();
    expect(screen.getByText(/Add/i)).toBeInTheDocument();
    expect(screen.getByText(/Profile/i)).toBeInTheDocument();
  });

test('renders SignUp form and checks elements', () => {
    render(
      <MemoryRouter>
        <SignUp />
      </MemoryRouter>
    );
  
    // Check if the form elements are present
    expect(screen.getByText(/first name/i)).toBeInTheDocument();
    expect(screen.getByText(/last name/i)).toBeInTheDocument();
    expect(screen.getByText(/username/i)).toBeInTheDocument();
    
    // Check for multiple password labels
    const passwordLabels = screen.getAllByText(/password/i);
    expect(passwordLabels).toHaveLength(2); // Expect exactly 2 password labels
    passwordLabels.forEach(label => expect(label).toBeInTheDocument());
  
    expect(screen.getByText(/confirm password/i)).toBeInTheDocument();
    
  });

test('renders Login form and checks elements', () => {
    render(
      <MemoryRouter>
        <Login />
      </MemoryRouter>
    );
  
    // Check if the form elements are present
    expect(screen.getByText(/username/i)).toBeInTheDocument();
    expect(screen.getByText(/password/i)).toBeInTheDocument();

    // Check if the link to the signup page is present
    expect(screen.getByText(/create your imovies account/i)).toBeInTheDocument();
  });

describe('Home Component', () => {
  beforeEach(() => {
    // Clear mock calls before each test
    jest.clearAllMocks();
  });

  it('redirects to /feed if token is present and not expired', () => {
    // Set up the mock for isTokenExpired
    isTokenExpired.mockReturnValue(false);
    localStorage.setItem('token', 'validToken');

    render(
      <MemoryRouter initialEntries={['/']}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/feed" element={<div>Feed Page</div>} />
        </Routes>
      </MemoryRouter>
    );

    // Check if the user is redirected to /feed
    expect(screen.getByText('Feed Page')).toBeInTheDocument();
  });

  it('renders Home component if token is absent or expired', () => {
    // Set up the mock for isTokenExpired
    isTokenExpired.mockReturnValue(true); // Token is expired or no token
    localStorage.removeItem('token');

    render(
      <MemoryRouter>
        <Home />
      </MemoryRouter>
    );

    // Check if Home component content is rendered
    expect(screen.getByText('Welcome to iMovies')).toBeInTheDocument();
    expect(screen.getByText('Sign Up')).toBeInTheDocument();
    expect(screen.getByText('Login')).toBeInTheDocument();
  });
});

describe('Header Component', () => {
  beforeEach(() => {
    jest.clearAllMocks();
    localStorage.removeItem('token');
  });

  it('should display username and logout button if token is present and valid', () => {
    const mockToken = 'validToken';
    const decodedToken = { unique_name: 'JohnDoe' };

    localStorage.setItem('token', mockToken);
    isTokenExpired.mockReturnValue(false);
    decodeToken.mockReturnValue(decodedToken);

    render(
      <MemoryRouter>
        <Header />
      </MemoryRouter>
    );

    expect(screen.getByText('JohnDoe')).toBeInTheDocument();
    expect(screen.getByText('Logout')).toBeInTheDocument();
  });

  it('should display login link if token is absent or expired', () => {
    localStorage.removeItem('token');
    isTokenExpired.mockReturnValue(true);

    render(
      <MemoryRouter>
        <Header />
      </MemoryRouter>
    );

    expect(screen.getByText('Login')).toBeInTheDocument();
    expect(screen.getByText('About')).toBeInTheDocument();
  });
});

describe('PrivateRoutes Component', () => {
    beforeEach(() => {
      // Clear mock calls before each test
      jest.clearAllMocks();
    });
  
    it('should redirect to /login if token is not present or expired', () => {
      // Set up the mock for isTokenExpired
      isTokenExpired.mockReturnValue(true);
      localStorage.removeItem('token'); // Ensure no token is present
  
      render(
        <MemoryRouter initialEntries={['/private']}>
          <Routes>
            <Route path="/" element={<div>Home</div>} />
            <Route path="/private" element={<PrivateRoutes />}>
              <Route path="/private" element={<div>Private Page</div>} />
            </Route>
            <Route path="/login" element={<div>Login Page</div>} />
          </Routes>
        </MemoryRouter>
      );
  
      // Check if the user is redirected to /login
      expect(screen.getByText('Login Page')).toBeInTheDocument();
    });
  
    it('should render Outlet if token is present and not expired', () => {
      // Set up the mock for isTokenExpired
      isTokenExpired.mockReturnValue(false);
      localStorage.setItem('token', 'validToken'); // Set a valid token
  
      render(
        <MemoryRouter initialEntries={['/private']}>
          <Routes>
            <Route path="/" element={<div>Home</div>} />
            <Route path="/private" element={<PrivateRoutes />}>
              <Route path="/private" element={<div>Private Page</div>} />
            </Route>
            <Route path="/login" element={<div>Login Page</div>} />
          </Routes>
        </MemoryRouter>
      );
  
      // Check if the private route content is rendered
      expect(screen.getByText('Private Page')).toBeInTheDocument();
    });
  });


  test('renders UserCard with user data', () => {
    const mockUser = { userId: '1', userName: 'JohnDoe' };
  
    render(<UserCard user={mockUser} followevent={false} setFollowevent={() => {}} />);
  
    expect(screen.getByText('JohnDoe')).toBeInTheDocument();
    expect(screen.getByRole('button')).toHaveTextContent('Follow');
  });


test('renders MovieCard', () => {
  const movieData = {
    Title: 'Inception',
    Year: '2010',
    Genre: 'Action, Adventure, Sci-Fi',
    Poster: 'https://example.com/poster.jpg',
  };

    const { container } =   render(
      <MemoryRouter>
        <MovieCard movie={movieData} />
      </MemoryRouter>
    );
    expect(container).toBeInTheDocument();
});

test('renders layout', ()=> {
  const { container } =   render(
    <MemoryRouter>
     <Layout />
    </MemoryRouter>
  );
  expect(container).toBeInTheDocument();
})

test('renders following', ()=> {
  const { container } =   render(
    <MemoryRouter>
     <Following />
    </MemoryRouter>
  );
  expect(container).toBeInTheDocument();
})


test('renders addmovies', ()=> {
  const { container } =   render(
    <MemoryRouter>
     <AddMovies />
    </MemoryRouter>
  );
  expect(container).toBeInTheDocument();
})

test('renders feed', ()=> {
  const { container } =   render(
    <MemoryRouter>
     <Feed />
    </MemoryRouter>
  );
  expect(container).toBeInTheDocument();
})

test('renders userprofile', ()=> {
  const { container } =   render(
    <MemoryRouter>
     <UserProfile />
    </MemoryRouter>
  );
  expect(container).toBeInTheDocument();
})

