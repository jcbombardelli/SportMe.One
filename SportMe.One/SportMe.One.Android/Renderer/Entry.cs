using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SportMe.One.Droid.Renderer.Entry), typeof(SportMe.One.Controls.Entry))]
namespace SportMe.One.Droid.Renderer
{
    public class Entry : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Typeface font = Typeface.CreateFromAsset(Context.Assets, "Fonts/Roboto/Roboto-Light.ttf");
                Control.SetTypeface(font, TypefaceStyle.Normal);
            }
        }
    }
}