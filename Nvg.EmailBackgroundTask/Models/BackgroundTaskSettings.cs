﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailBackgroundTask.Models
{
    public class BackgroundTaskSettings
    {
        public string ConnectionString { get; set; }

        public string EventBusConnection { get; set; }

        public string SubscriptionClientName { get; set; }

    }
}
