using System.ComponentModel.DataAnnotations;

// Classe utilisée pour représenter une demande de login 
public class LoginModel{
    [Required(ErrorMessage ="{0} est requis")]
    [Display(Name ="UserName")]
    public string UserName { get; set; }

    [Display(Name ="Password")]
    [Required(ErrorMessage ="{0} est requis")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
         ErrorMessage ="{0} doit avoir au moins 8 charactères, au moins 1 minuscule, 1 majuscule et un charactère spécial ")]
    public string Password { get; set; }
}