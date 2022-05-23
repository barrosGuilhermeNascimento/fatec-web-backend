using System.ComponentModel.DataAnnotations;
using ApiFatecWeb.Core.Model.Enum;

namespace ApiFatecWeb.Core.Model;

public class UserModel
{
    public int IdUser { get; set; }
    public Guid Identifier { get; set; }

    [EmailAddress, Required]
    public string Email { get; set; }

    [MinLength(8)]
    public string Password { get; set; }

    [MinLength(8)]
    public string Name { get; set; }
    public RoleEnum IdRole { get; set; }
    public string? Role { get; set; }
    public DateTime DtUpdate { get; set; }
}

public class UserModelLogin : UserModel
{
    public string token { get; set; }
}
public class UserLoginModel
{
    [EmailAddress, Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

public class UserRegisterModel
{
    [EmailAddress, Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public RoleEnum IdRole { get; set; }
}

public class UserChangePasswordModel
{
    [EmailAddress, Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }

    [Required]
    public int RecoverToken { get; set; }
}



public enum ErrorType
{
    Missing,
    Invalid,
    NonExistent
}

public class ErrorHandle
{
    private readonly string _location;

    public ErrorHandle(string location)
    {
        _location = location;
    }

    public string CreateErrorString(ErrorType error, string model)
    {
        return $"There was and error in {_location}: {model} is {error}";
    }
}

