﻿using System.Collections;
using System.Collections.Generic;
using Okarta.Data.Entities;

namespace Okarta.Data.Services
{
    public interface IMapsService
    {
        IEnumerable<Map> Query();
    }
}