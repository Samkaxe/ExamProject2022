﻿using System.ComponentModel.DataAnnotations;

namespace Core.Identity;

public class Address
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string City { get; set; }

    public string ZipCode { get; set; }
    
    [Required] // we cant allow this to be null in the db 
    public string AppUserId { get; set; }

    public AppUser AppUser { get; set; }
}