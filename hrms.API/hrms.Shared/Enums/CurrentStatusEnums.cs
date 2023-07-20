using System.ComponentModel;

namespace hrms.Shared.Enums
{
    public enum CurrentStatusEnums
    {
        [Description("not_started")]
        NOT_STARTED,
        [Description("working")]
        WORKING,
        [Description("finished_work")]
        FINISHED_WORK,
        [Description("break")]
        BREAK,
    }
}
