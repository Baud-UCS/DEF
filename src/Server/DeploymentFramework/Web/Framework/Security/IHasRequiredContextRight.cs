﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baud.Deployment.Web.Framework.Security
{
    public interface IHasRequiredContextRight
    {
        string RequiredContextRight { get; }
    }
}