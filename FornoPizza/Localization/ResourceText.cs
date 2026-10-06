using System.Globalization;
using System.Resources;

namespace FornoPizza.Localization;

public class Shared
{
    private static readonly ResourceManager ResourceManager = new("FornoPizza.Localization.Shared", typeof(Shared).Assembly);

    public static string Nav_Menu => Text(nameof(Nav_Menu));
    public static string Nav_Build => Text(nameof(Nav_Build));
    public static string Nav_Order => Text(nameof(Nav_Order));
    public static string Nav_Kitchen => Text(nameof(Nav_Kitchen));
    public static string Nav_Checkout => Text(nameof(Nav_Checkout));
    public static string Nav_MyOrders => Text(nameof(Nav_MyOrders));
    public static string Nav_LogOut => Text(nameof(Nav_LogOut));
    public static string Nav_LogIn => Text(nameof(Nav_LogIn));
    public static string Lang_Label => Text(nameof(Lang_Label));
    public static string Lang_Ru => Text(nameof(Lang_Ru));
    public static string Lang_En => Text(nameof(Lang_En));
    public static string Footer_Hours => Text(nameof(Footer_Hours));
    public static string Status_New => Text(nameof(Status_New));
    public static string Status_Confirmed => Text(nameof(Status_Confirmed));
    public static string Status_Cooking => Text(nameof(Status_Cooking));
    public static string Status_OnTheWay => Text(nameof(Status_OnTheWay));
    public static string Status_Delivered => Text(nameof(Status_Delivered));
    public static string Status_Canceled => Text(nameof(Status_Canceled));
    public static string Payment_Card => Text(nameof(Payment_Card));
    public static string Payment_Cash => Text(nameof(Payment_Cash));
    public static string Payment_Online => Text(nameof(Payment_Online));
    public static string Meta_Payment => Text(nameof(Meta_Payment));
    public static string Meta_Total => Text(nameof(Meta_Total));
    public static string Page_Title => Text(nameof(Page_Title));
    public static string Hero_Eyebrow => Text(nameof(Hero_Eyebrow));
    public static string Hero_Lead => Text(nameof(Hero_Lead));
    public static string Hero_Choose => Text(nameof(Hero_Choose));
    public static string Hero_Build => Text(nameof(Hero_Build));
    public static string Hero_ImageAlt => Text(nameof(Hero_ImageAlt));
    public static string Footer_Brand => Text(nameof(Footer_Brand));
    public static string Error_Promo => Text(nameof(Error_Promo));
    public static string Error_Customer => Text(nameof(Error_Customer));
    public static string Error_Pizza => Text(nameof(Error_Pizza));

    private static string Text(string name) => ResourceManager.GetString(name, CultureInfo.CurrentUICulture) ?? name;
}

public class Auth
{
    private static readonly ResourceManager ResourceManager = new("FornoPizza.Localization.Auth", typeof(Auth).Assembly);

    public static string Nav_Home => Text(nameof(Nav_Home));
    public static string Login_Title => Text(nameof(Login_Title));
    public static string Login_Eyebrow => Text(nameof(Login_Eyebrow));
    public static string Login_Heading => Text(nameof(Login_Heading));
    public static string Login_Lead => Text(nameof(Login_Lead));
    public static string Login_Label_Login => Text(nameof(Login_Label_Login));
    public static string Login_Label_Password => Text(nameof(Login_Label_Password));
    public static string Login_Button => Text(nameof(Login_Button));
    public static string Login_Switch_Text => Text(nameof(Login_Switch_Text));
    public static string Login_Switch_Link => Text(nameof(Login_Switch_Link));
    public static string Register_Title => Text(nameof(Register_Title));
    public static string Register_Eyebrow => Text(nameof(Register_Eyebrow));
    public static string Register_Heading => Text(nameof(Register_Heading));
    public static string Register_Lead => Text(nameof(Register_Lead));
    public static string Register_Label_Login => Text(nameof(Register_Label_Login));
    public static string Register_Label_Password => Text(nameof(Register_Label_Password));
    public static string Register_Label_Confirm => Text(nameof(Register_Label_Confirm));
    public static string Register_Button => Text(nameof(Register_Button));
    public static string Register_Switch_Text => Text(nameof(Register_Switch_Text));
    public static string Register_Switch_Link => Text(nameof(Register_Switch_Link));
    public static string Error_InvalidCredentials => Text(nameof(Error_InvalidCredentials));
    public static string Error_Required => Text(nameof(Error_Required));
    public static string Error_LoginTaken => Text(nameof(Error_LoginTaken));
    public static string Error_CreateFailed => Text(nameof(Error_CreateFailed));
    public static string Error_PasswordMismatch => Text(nameof(Error_PasswordMismatch));
    public static string Error_KitchenPassword => Text(nameof(Error_KitchenPassword));
    public static string Error_KitchenNotConfigured => Text(nameof(Error_KitchenNotConfigured));

    private static string Text(string name) => ResourceManager.GetString(name, CultureInfo.CurrentUICulture) ?? name;
}

public class Kitchen
{
    private static readonly ResourceManager ResourceManager = new("FornoPizza.Localization.Kitchen", typeof(Kitchen).Assembly);

    public static string Title => Text(nameof(Title));
    public static string Eyebrow => Text(nameof(Eyebrow));
    public static string Heading => Text(nameof(Heading));
    public static string Lead => Text(nameof(Lead));
    public static string InProgress => Text(nameof(InProgress));
    public static string Empty_Title => Text(nameof(Empty_Title));
    public static string Empty_Text => Text(nameof(Empty_Text));
    public static string Meta_Client => Text(nameof(Meta_Client));
    public static string Meta_Phone => Text(nameof(Meta_Phone));
    public static string Meta_Address => Text(nameof(Meta_Address));
    public static string Action_Confirm => Text(nameof(Action_Confirm));
    public static string Action_Cook => Text(nameof(Action_Cook));
    public static string Action_Handoff => Text(nameof(Action_Handoff));
    public static string Action_Delivered => Text(nameof(Action_Delivered));
    public static string Action_Next => Text(nameof(Action_Next));
    public static string Action_Cancel => Text(nameof(Action_Cancel));
    public static string Error_Status => Text(nameof(Error_Status));
    public static string Error_Cancel => Text(nameof(Error_Cancel));
    public static string Footer => Text(nameof(Footer));

    private static string Text(string name) => ResourceManager.GetString(name, CultureInfo.CurrentUICulture) ?? name;
}

public class Orders
{
    private static readonly ResourceManager ResourceManager = new("FornoPizza.Localization.Orders", typeof(Orders).Assembly);

    public static string Title => Text(nameof(Title));
    public static string Eyebrow => Text(nameof(Eyebrow));
    public static string Heading => Text(nameof(Heading));
    public static string Lead => Text(nameof(Lead));
    public static string Total => Text(nameof(Total));
    public static string Empty_Title => Text(nameof(Empty_Title));
    public static string Empty_Text => Text(nameof(Empty_Text));
    public static string Size_Small => Text(nameof(Size_Small));
    public static string Size_Medium => Text(nameof(Size_Medium));
    public static string Size_Big => Text(nameof(Size_Big));
    public static string Dough_Thin => Text(nameof(Dough_Thin));
    public static string Dough_Classic => Text(nameof(Dough_Classic));
    public static string Dough_Cheese => Text(nameof(Dough_Cheese));
    public static string Footer => Text(nameof(Footer));
    public static string Success_Title => Text(nameof(Success_Title));
    public static string Success_Eyebrow => Text(nameof(Success_Eyebrow));
    public static string Success_Heading => Text(nameof(Success_Heading));
    public static string Success_Lead => Text(nameof(Success_Lead));
    public static string Success_Number => Text(nameof(Success_Number));
    public static string Success_NewOrder => Text(nameof(Success_NewOrder));
    public static string Success_Home => Text(nameof(Success_Home));
    public static string Success_ImageAlt => Text(nameof(Success_ImageAlt));
    public static string Success_Footer => Text(nameof(Success_Footer));

    private static string Text(string name) => ResourceManager.GetString(name, CultureInfo.CurrentUICulture) ?? name;
}
