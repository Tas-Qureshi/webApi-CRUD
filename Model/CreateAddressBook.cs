using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class CreateAddressBook
{
    [Required]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Please Enter Street Name")]
    public string? Street { get; set; }
    [Required(ErrorMessage = "Please Enter Street Number")]
    public int StreetNo { get; set; }
    [Required(ErrorMessage = "Please Enter City Name")]
    public string? City { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [DisplayName("Phone Number")]
    public string? PhoneNumber { get; set; }
}