using System;
using System.Collections.Generic;

namespace UserService.Helpers
{
    public class GuidHelper
    {
        public static List<Guid> GetGuids()
        {
            List<Guid> guids = new List<Guid>();
            Guid PermissionOneGuid = Guid.NewGuid();
            guids.Add(PermissionOneGuid);
            Guid PermissionTwoGuid = Guid.NewGuid();
            guids.Add(PermissionTwoGuid);
            Guid PermissionThreeGuid = Guid.NewGuid();
            guids.Add(PermissionThreeGuid);
            Guid PermissionFourGuid = Guid.NewGuid();
            guids.Add(PermissionFourGuid);
            Guid PermissionFiveGuid = Guid.NewGuid();
            guids.Add(PermissionFiveGuid);
            Guid PermissionSixGuid = Guid.NewGuid();
            guids.Add(PermissionSixGuid);
            Guid PermissionSevenGuid = Guid.NewGuid();
            guids.Add(PermissionSevenGuid);
            Guid PermissionEightGuid = Guid.NewGuid();
            guids.Add(PermissionEightGuid);
            Guid PermissionNineGuid = Guid.NewGuid();
            guids.Add(PermissionNineGuid);
            Guid PermissionTenGuid = Guid.NewGuid();
            guids.Add(PermissionTenGuid);
            Guid PermissionElevenGuid = Guid.NewGuid();
            guids.Add(PermissionElevenGuid);

            return guids;
        }

    }
}
