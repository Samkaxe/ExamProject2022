using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    /*
     * https://regexlib.com/Search.aspx?k=password
     * 
     * This regular expression match can be used for validating strong password.
     * It expects atleast 1 small-case letter, 1 Capital letter, 1 digit,
     * 1 special character and the length should be between 6-10 characters.
     * The sequence of the characters is not important. This expression follows the above 4 norms
     * specified by microsoft for a strong password.
     */
    [Required]
    [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$" , ErrorMessage = "Password must have 1 upper case , 1 lower case , 1 number , at least 6 characters ")]
    public string Password { get; set; }
}