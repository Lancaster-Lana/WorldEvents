using Abp;

namespace WorldEvents.Core.Base
{
    public class EventCloudServiceBase : AbpServiceBase
    {
        public EventCloudServiceBase()
        {
            //LocalizationSourceName = EventCloudConsts.LocalizationSourceName;
        }
    }

    public class EventCloudSettingNames
    {
        public const string MaxAllowedEventRegistrationCountInLast30DaysPerUser = "WorldEvents.MaxAllowedEventRegistrationCountInLast30DaysPerUser";
    }
}
