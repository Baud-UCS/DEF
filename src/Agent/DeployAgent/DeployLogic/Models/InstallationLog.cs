﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.DeployLogic.Models
{
    public class InstallationLog
    {
        public DateTime Timestamp { get; set; }
        public int Level { get; set; }
        public LogSeverity Severity { get; set; }
        public string Text { get; set; }
    }
}