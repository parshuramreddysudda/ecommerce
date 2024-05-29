using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Customers.DB;

public class Customer
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }= string.Empty;
    public string LastName { get; set; }= string.Empty;
    [Required]
    public string Gender { get; set; }= string.Empty;
    [Required]
    public string Phone { get; set; }= string.Empty;
    public string ProfilePic { get; set; }= string.Empty;
    public int UserId { get; set; } 
}