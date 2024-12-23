using MudBlazor;
using MudBlazor.Utilities;

namespace PassIn.Web.Layout;

public sealed class ScreendsoundPallete : PaletteDark
{
    private ScreendsoundPallete()
    {
        Primary = new MudColor("#B3AC9E");
        Secondary = new MudColor("#E0DED8");
        Tertiary = new MudColor(" #F4F1EB");
    }

    public static ScreendsoundPallete CreatePallete => new();
}