using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Repositories;
using OMDbProject.Repositories.Interfaces;

public class UserRepositoryTests
{
    private readonly Mock<DbSet<User>> _mockUserSet;
    private readonly Mock<ApplicationDbContext> _mockContext;
    private readonly UserRepository _userRepository;

    public UserRepositoryTests()
    {
        _mockUserSet = new Mock<DbSet<User>>();
        _mockContext = new Mock<ApplicationDbContext>();
        _userRepository = new UserRepository(_mockContext.Object);
    }


}
