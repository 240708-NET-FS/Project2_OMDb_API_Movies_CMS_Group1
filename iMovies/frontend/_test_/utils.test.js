import { decodeToken, isTokenExpired, getCurrentTimestamp } from './../src/utils/jwtUtils';
import { jwtDecode } from "jwt-decode";
import { handleFollowToggle } from './../src/utils/followUtils';

// Mock the jwtDecode function
jest.mock('jwt-decode');

// Mock the necessary modules and functions
jest.mock('../src/utils/fetchData', () => ({
    getCurrentTimestamp: jest.fn()
  }));

// Mock the fetch function
global.fetch = jest.fn(); 

describe('Token Utilities', () => {
  describe('decodeToken', () => {
    it('should decode a valid token', () => {
      const mockToken = 'validToken';
      const decodedToken = { sub: 'user123', exp: Math.floor(Date.now() / 1000) + 3600 };
      
      jwtDecode.mockReturnValue(decodedToken);
      
      const result = decodeToken(mockToken);
      
      expect(result).toEqual(decodedToken);
      expect(jwtDecode).toHaveBeenCalledWith(mockToken);
    });

    it('should return null for an invalid token', () => {
      const mockToken = 'invalidToken';
      
      jwtDecode.mockImplementation(() => { throw new Error('Invalid token'); });
      
      const result = decodeToken(mockToken);
      
      expect(result).toBeNull();
      expect(jwtDecode).toHaveBeenCalledWith(mockToken);
    });
  });

  describe('isTokenExpired', () => {
    it('should return false if token is not expired', () => {
      const mockToken = 'validToken';
      const decodedToken = { exp: Math.floor(Date.now() / 1000) + 3600 };
      
      jwtDecode.mockReturnValue(decodedToken);
      
      const result = isTokenExpired(mockToken);
      
      expect(result).toBe(false);
      expect(jwtDecode).toHaveBeenCalledWith(mockToken);
    });

    it('should return true if token is expired', () => {
      const mockToken = 'expiredToken';
      const decodedToken = { exp: Math.floor(Date.now() / 1000) - 3600 };
      
      jwtDecode.mockReturnValue(decodedToken);
      
      const result = isTokenExpired(mockToken);
      
      expect(result).toBe(true);
      expect(jwtDecode).toHaveBeenCalledWith(mockToken);
    });

    it('should return true for an invalid token', () => {
      const mockToken = 'invalidToken';
      
      jwtDecode.mockImplementation(() => { throw new Error('Invalid token'); });
      
      const result = isTokenExpired(mockToken);
      
      expect(result).toBe(true);
      expect(jwtDecode).toHaveBeenCalledWith(mockToken);
    });
  });

  describe('getCurrentTimestamp', () => {
    it('should return the current timestamp in ISO format', () => {
      const timestamp = getCurrentTimestamp();
      const now = new Date().toISOString();
  
      // Allow a small tolerance for the timestamp comparison
      const tolerance = 1000; // 1 second tolerance
      const timestampDiff = Math.abs(new Date(timestamp).getTime() - new Date(now).getTime());
  
      expect(timestampDiff).toBeLessThanOrEqual(tolerance);
    });
  });
});

describe('handleFollowToggle', () => {
  it('should return true or false', async () => {
    const result = await handleFollowToggle(null, 'userId', jest.fn(), jest.fn());
    expect(typeof result).toBe('boolean');
  });
})