// jest.config.mjs
export default {
    testEnvironment: 'jest-environment-jsdom',
    transform: {
      "^.+\\.[tj]sx?$": "babel-jest"
    },
    moduleNameMapper: {
      '\\.(gif|ttf|eot|svg|png)$': '<rootDir>/test/__mocks__/fileMock.js',
      '\\.(css|less|scss|sass)$': 'identity-obj-proxy',
    },
    moduleFileExtensions: ['js', 'jsx', 'json', 'node'],
    transformIgnorePatterns: ['/node_modules/'],
    collectCoverageFrom: [
      '<rootDir>/src/**/*.{js,jsx}', 
      '!<rootDir>/src/index.js', 
    ],
      coverageDirectory: '<rootDir>/coverage',
      coverageReporters: ['json', 'lcov', 'text', 'clover'],
  };