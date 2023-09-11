namespace WareHousePlatForm.Models
{
    public class AppSetting
    {
        public static UserAccount Account { get; set; } = new UserAccount();
        public static AppSettingConfig Config { get; set; } = new AppSettingConfig();
    }
}
