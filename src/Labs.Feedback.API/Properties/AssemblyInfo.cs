﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Labs.Feedback.API.UtilTest")
         , InternalsVisibleTo("Labs.Feedback.API.UnitTests")
         , InternalsVisibleTo("Labs.Feedback.API.IntegrationTests")]

                   