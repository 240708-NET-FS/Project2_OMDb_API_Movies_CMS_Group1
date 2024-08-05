using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMDbProject.Models;

    public class Follower
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FollowerId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int FollowerUserId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(FollowerUserId))]
        public User FollowerUser { get; set; }
    }

