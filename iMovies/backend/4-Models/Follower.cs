using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMDbProject.Models;

    public class Follower
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FollowerId { get; set; } //primary key for the follower relationship(each record)

        [Required]
        public int UserId { get; set; } //UserId of the User being followed

        [Required]
        public int FollowerUserId { get; set; } //UserId of the Follower

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } //Navigation property User User uses UserId to create a reference to the user being followed.

        [ForeignKey(nameof(FollowerUserId))]
        public User? FollowerUser { get; set; } //Navigation property User FollowerUser uses FollowerUserId to create a reference to the user who is doing the following
    }

