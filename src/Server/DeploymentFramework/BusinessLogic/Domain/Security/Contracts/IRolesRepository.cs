﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.BusinessLogic.Domain.Security.Contracts
{
    public interface IRolesRepository
    {
        IQueryable<Role> GetRoles();

        Role GetRoleDetail(short id);

        void Enable(short id);

        void Disable(short id);

        void UpdateName(short id);

        void AddRole(Role role);
    }
}
