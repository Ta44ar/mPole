using Microsoft.Extensions.Localization;

namespace mPole.Resources
{
    public class SharedResource
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
    }
}
