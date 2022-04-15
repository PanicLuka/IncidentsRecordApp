using System;
using System.Collections.Generic;
using EnumClassLibrary;


namespace UserService.Helpers
{
    public class GuidHelper
    {
        public static List<Guid> GetGuids()
        {
            int length = Enum.GetNames(typeof(EnumPermissions.Permissions)).Length;
            List<Guid> guids = new List<Guid>(length);

            for(int i = 0;i<length;i++)
            {
                guids.Add(Guid.NewGuid());
            }

            return guids;
        }

    }
}
