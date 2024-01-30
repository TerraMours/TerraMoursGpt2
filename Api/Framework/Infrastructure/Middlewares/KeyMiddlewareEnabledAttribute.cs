namespace TerraMours_Gpt.Framework.Infrastructure.Middlewares;

[AttributeUsage(AttributeTargets.Method)]
public class KeyMiddlewareEnabledAttribute : Attribute
{
    public bool Enabled { get; }

    public KeyMiddlewareEnabledAttribute()
    {
        Enabled = true;
    }
}
