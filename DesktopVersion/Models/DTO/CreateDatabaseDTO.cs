﻿using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Models.DTO
{
    public class CreateDatabaseDTO
    {
        public IRelayCommand CreateCallBack { get; set; }

    }
}