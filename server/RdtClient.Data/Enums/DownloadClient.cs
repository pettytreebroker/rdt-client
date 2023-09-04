﻿using System.ComponentModel;

namespace RdtClient.Data.Enums;

public enum DownloadClient
{
    [Description("Internal Downloader")]
    Internal,

    [Description("Aria2c")]
    Aria2c,

    [Description("Symlink Downloader")]
    Symlink,
}