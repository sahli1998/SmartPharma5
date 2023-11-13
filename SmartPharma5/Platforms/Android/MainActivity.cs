using Android.App;
using Android.Content.PM;

namespace SmartPharma5
{
    [Activity(Theme = "@style/Maui.SplashTheme", Icon = "@drawable/logo" , MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : MauiAppCompatActivity
    {

    }
}