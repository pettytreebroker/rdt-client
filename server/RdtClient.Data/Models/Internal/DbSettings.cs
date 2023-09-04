﻿using System.ComponentModel;
using RdtClient.Data.Enums;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace RdtClient.Data.Models.Internal;

public class DbSettings
{
    [DisplayName("General")]
    [Description("")]
    public DbSettingsGeneral General { get; set; } = new();

    [DisplayName("Download Client")]
    [Description("")]
    public DbSettingsDownloadClient DownloadClient { get; set; } = new();

    [DisplayName("Provider")]
    [Description("")]
    public DbSettingsProvider Provider { get; set; } = new();

    [DisplayName("qBittorrent / *darr")]
    [Description("The following settings only apply when a torrent gets added through the qbittorrent API, usually Radarr or Sonarr.")]
    public DbSettingsIntegrations Integrations { get; set; } = new();

    [DisplayName("GUI Defaults")]
    [Description("Settings used when adding a torrent through the web interface.")]
    public DbSettingsGui Gui { get; set; } = new();

    [DisplayName("Watch")]
    [Description("The following settings only apply when a torrent gets through the watch folder.")]
    public DbSettingsWatch Watch { get; set; } = new();
}

public class DbSettingsGeneral
{
    [DisplayName("Log level")]
    [Description("Recommended level is Warning, set to Debug to get the most info.")]
    public LogLevel LogLevel { get; set; } = LogLevel.Warning;

    [DisplayName("Maximum parallel downloads")]
    [Description("Maximum amount of torrents that get downloaded to your host at the same time.")]
    public Int32 DownloadLimit { get; set; } = 2;

    [DisplayName("Maximum unpack processes")]
    [Description("Maximum amount of downloads that get unpacked on your host at the same time.")]
    public Int32 UnpackLimit { get; set; } = 1;

    [DisplayName("Categories")]
    [Description("Expose these categories through the QBittorrent API.")]
    public String? Categories { get; set; } = null;

    [DisplayName("Run external program on torrent completion")]
    [Description("Path to the executable to run when the torrent and all downloads are finished. No arguments should be passed here.When running in Docker, this command will run on your docker instance!")]
    public String? RunOnTorrentCompleteFileName { get; set; } = null;

    [DisplayName("External program arguments")]
    [Description(@"When the executable above is executed, use these parameters.
Supports the following parameters:
%N: Torrent name
%L: Category
%F: Content path (same as root path for multifile torrent)
%R: Root path (first torrent subdirectory path)
%D: Save path
%C: Number of files
%Z: Torrent size (bytes)
%I: Info hash")]
    public String? RunOnTorrentCompleteArguments { get; set; } = null;

    [DisplayName("Authentication Type")]
    [Description("How to authenticate with the client. WARNING: when set to None anyone with access to the URL can use the client without any credentials.")]
    public AuthenticationType AuthenticationType { get; set; } = AuthenticationType.UserNamePassword;
}

public class DbSettingsDownloadClient
{
    [DisplayName("Download client")]
    [Description(@"Select which download client to use, see the
<a href=""https://github.com/rogerfar/rdt-client/"" target=""_blank"">README</a> for the various options.")]
    public DownloadClient Client { get; set; } = DownloadClient.Internal;

    [DisplayName("Download path")]
    [Description("Path in the docker container to download files to (i.e. /data/downloads), or a local path when using as a service.")]
    public String DownloadPath { get; set; } = "/data/downloads";

    [DisplayName("Mapped path")]
    [Description("Path where files are downloaded to on your host (i.e. D:\\Downloads). This path is used for *arr to find your downloads.")]
    public String MappedPath { get; set; } = @"C:\Downloads";

    [DisplayName("Download speed (in MB/s) (only used for the Internal Downloader)")]
    [Description("Maximum download speed in Megabytes per second. When set to 0 unlimited speed is used.")]
    public Int32 MaxSpeed { get; set; } = 0;

    [DisplayName("Parallel connections per download (only used for the Internal Downloader)")]
    [Description("Maximum amount of parallel threads that are used to download a single file to your host. If set to 0 no parallel downloading will be done.")]
    public Int32 ParallelCount { get; set; } = 0;

    [DisplayName("Connection Timeout (only used for the Internal Downloader)")]
    [Description("Timeout in milliseconds before the downloader times out.")]
    public Int32 Timeout { get; set; } = 5000;

    [DisplayName("Proxy Server (only used for the Internal Downloader)")]
    [Description("Address of a proxy server to download through (only used for the Internal Downloader).")]
    public String? ProxyServer { get; set; } = null;

    [DisplayName("Aria2c URL (only used for the Aria2c Downloader)")]
    [Description(@"This is the URL to your Aria2c instance. It must end in /jsonrpc. A common URL is
http://127.0.0.1:6800/jsonrpc.")]
    public String Aria2cUrl { get; set; } = "http://127.0.0.1:6800/jsonrpc";

    [DisplayName("Aria2c Secret (only used for the Aria2c Downloader)")]
    [Description("The secret of your Aria2c instance. Optional.")]
    public String Aria2cSecret { get; set; } = "mysecret123";

    [DisplayName("Rclone mount path (only used for the Symlink Downloader)")]
    [Description("Path where Rclone is mounted. Required for Symlink Downloader.")]
    public String RcloneMountPath { get; set; } = "/mnt/rd/";
}

public class DbSettingsProvider
{
    [DisplayName("Provider")]
    [Description(@"The following 3 providers are supported:
<a href=""https://real-debrid.com/?id=1348683"" target=""_blank"" rel=""noopener"">https://real-debrid.com</a>
<a href=""https://alldebrid.com/?uid=2v91l&lang=en"" target=""_blank"" rel=""noopener"">https://alldebrid.com</a>
<a href=""https://www.premiumize.me/"" target=""_blank"" rel=""noopener"">https://www.premiumize.me/</a>
At this point only 1 provider can be used at the time.")]
    public Provider Provider { get; set; } = Provider.RealDebrid;

    [DisplayName("API Key")]
    [Description(@"You can find your API key here:
<a href=""https://real-debrid.com/apitoken"" target=""_blank"" rel=""noopener"">https://real-debrid.com/apitoken</a>
or
<a href=""""https://alldebrid.com/apikeys/"""" target=""""_blank"""" rel=""""noopener"""">https://alldebrid.com/apikeys/</a>
or
<a href=""https://www.premiumize.me/account/"" target=""_blank"" rel=""noopener"">https://www.premiumize.me/account/</a>")]
    public String ApiKey { get; set; } = "";

    [DisplayName("Automatically import and process torrents added to provider")]
    [Description("When selected, import downloads that are not added through RealDebridClient but have been directly added to Real-Debrid, AllDebrid or Premiumize.")]
    public Boolean AutoImport { get; set; } = false;

    [DisplayName("Automatically delete downloads removed from provider")]
    [Description("When selected, cancel and delete downloads that have been removed from Real-Debrid, AllDebrid or Premiumize.")]
    public Boolean AutoDelete { get; set; } = false;

    [DisplayName("Connection Timeout")]
    [Description("Timeout in seconds to make a connection to the provider. Increase if you experience timeouts in the logs.")]
    public Int32 Timeout { get; set; } = 10;

    [DisplayName("Check Interval")]
    [Description("The interval to check the torrents info on the providers API. Minumum is 3 seconds. When there are no active downloads this limit is increased * 3.")]
    public Int32 CheckInterval { get; set; } = 10;

    [DisplayName("Auto Import Defaults")]
    public DbSettingsDefaultsWithCategory Default { get; set; } = new();
}

public class DbSettingsIntegrations
{
    public DbSettingsDefaultsWithDownload Default { get; set; } = new();
}

public class DbSettingsGui
{
    public DbSettingsDefaultsWithDownload Default { get; set; } = new();
}

public class DbSettingsWatch
{
    [DisplayName("Watch Path")]
    [Description("Watch this path for .torrent or .magnet files. When a file is found it will be automatically imported.")]
    public String? Path { get; set; } = null;

    [DisplayName("Error Path")]
    [Description("When an error occurs the torrent file is moved to this directory. When unset it will be moved to /error in the watchpath.")]
    public String? ErrorPath { get; set; } = null;

    [DisplayName("Processed Path")]
    [Description("When a torrent file is added succesfully it will be moved to this directory. When unset it will be moved to /processed in the watchpath.")]
    public String? ProcessedPath { get; set; } = null;

    [DisplayName("Check  Interval")]
    [Description("Time in seconds to check the folder for new files.")]
    public Int32 Interval { get; set; } = 60;

    [DisplayName("Import Defaults")]
    public DbSettingsDefaultsWithDownload Default { get; set; } = new();
}

public class DbSettingsDefaultsWithDownload : DbSettingsDefaultsWithCategory
{
    [DisplayName("Post Torrent Download Action")]
    [Description("When a torrent is finished downloading on the provider, perform this action. Use this setting if you only want to add files to Real-Debrid but not download them to the host.")]
    public TorrentHostDownloadAction HostDownloadAction { get; set; }
}

public class DbSettingsDefaultsWithCategory : DbSettingsDefaults
{
    [DisplayName("Category")]
    [Description("When a torrent is imported assign it this category.")]
    public String? Category { get; set; } = null;

    [DisplayName("Post Download Action")]
    [Description("When all files are downloaded from the provider to the host, perform this action.")]
    public TorrentFinishedAction FinishedAction { get; set; } = TorrentFinishedAction.RemoveAllTorrents;
}

public class DbSettingsDefaults
{
    [DisplayName("Only download available files on debrid provider")]
    [Description("When selected, it will only download files in the torrent that have been download by Real-Debrid. You can use this in combination with the Min File size setting above.")]
    public Boolean OnlyDownloadAvailableFiles { get; set; } = true;

    [DisplayName("Minimum file size to download")]
    [Description("Files that are smaller than this setting are skipped and not downloaded. When set to 0 all files are downloaded. When downloading from Radarr or Sonarr it's recommended to keep this setting at atleast a few MB to avoid the debrid provider having to re-download the torrent.")]
    public Int32 MinFileSize { get; set; } = 0;

    [DisplayName("Automatic retry torrent")]
    [Description("When a single download has failed multiple times (see setting above) or when the torrent itself received an error it will retry the full torrent this many times before marking it failed.")]
    public Int32 TorrentRetryAttempts { get; set; } = 1;

    [DisplayName("Automatic retry downloads")]
    [Description("When a single download fails it will retry it this many times before marking it as failed.")]
    public Int32 DownloadRetryAttempts { get; set; } = 3;

    [DisplayName("Delete download when in error")]
    [Description("When a download has been in error for this many minutes, delete it from the provider and the client. 0 to disable.")]
    public Int32 DeleteOnError { get; set; } = 0;

    [DisplayName("Torrent maximum lifetime")]
    [Description("The maximum lifetime of a torrent in minutes. When this time has passed, mark the torrent as error. If the torrent is completed and has downloads, the lifetime setting will not apply. 0 to disable.")]
    public Int32 TorrentLifetime { get; set; } = 0;

    [DisplayName("Priority")]
    [Description("Set the priority of a torrent, 1 = highest, 0 = disabled.")]
    public Int32 Priority { get; set; } = 0;
}