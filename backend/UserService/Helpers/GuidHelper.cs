using System;
using System.Collections.Generic;

namespace UserService.Helpers
{
    public class GuidHelper
    {
        public static List<Guid> GetGuids()
        {
            List<Guid> Guids = new List<Guid>();
            Guid PermissionOneGuid = Guid.NewGuid();
            Guids.Add(PermissionOneGuid);
            Guid PermissionTwoGuid = Guid.NewGuid();
            Guids.Add(PermissionTwoGuid);
            Guid PermissionThreeGuid = Guid.NewGuid();
            Guids.Add(PermissionThreeGuid);
            Guid PermissionFourGuid = Guid.NewGuid();
            Guids.Add(PermissionFourGuid);
            Guid PermissionFiveGuid = Guid.NewGuid();
            Guids.Add(PermissionFiveGuid);
            Guid PermissionSixGuid = Guid.NewGuid();
            Guids.Add(PermissionSixGuid);
            Guid PermissionSevenGuid = Guid.NewGuid();
            Guids.Add(PermissionSevenGuid);
            Guid PermissionEightGuid = Guid.NewGuid();
            Guids.Add(PermissionEightGuid);
            Guid PermissionNineGuid = Guid.NewGuid();
            Guids.Add(PermissionNineGuid);
            Guid PermissionTenGuid = Guid.NewGuid();
            Guids.Add(PermissionTenGuid);
            Guid PermissionElevenGuid = Guid.NewGuid();
            Guids.Add(PermissionElevenGuid);

            return Guids;
        }

    }
}
