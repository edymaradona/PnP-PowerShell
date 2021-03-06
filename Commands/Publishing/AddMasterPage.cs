﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using SharePointPnP.PowerShell.CmdletHelpAttributes;

namespace SharePointPnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Add, "SPOMasterPage")]
    [CmdletHelp("Adds a Masterpage",
        Category = CmdletHelpCategory.Publishing,
        OutputType = typeof(File),
        OutputTypeLink = "https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.file.aspx")]
    [CmdletExample(
        Code = @"PS:> Add-SPOMasterPage -SourceFilePath ""page.master"" -Title ""MasterPage"" -Description ""MasterPage for Web"" -DestinationFolderHierarchy ""SubFolder""",
        Remarks = "Adds a MasterPage to the web",
        SortOrder = 1)]
    public class AddMasterPage : SPOWebCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "Path to the file which will be uploaded")]
        public string SourceFilePath = string.Empty;

        [Parameter(Mandatory = true, HelpMessage = "Title for the page layout")]
        public string Title = string.Empty;

        [Parameter(Mandatory = true, HelpMessage = "Description for the page layout")]
        public string Description = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "Folder hierarchy where the MasterPage layouts will be deployed")]
        public string DestinationFolderHierarchy;

        [Parameter(Mandatory = false, HelpMessage = "UiVersion Masterpage. Default = 15")]
        public string UiVersion;

        [Parameter(Mandatory = false, HelpMessage = "Default CSS file for MasterPage, this Url is SiteRelative")]
        public string DefaultCssFile;

        protected override void ExecuteCmdlet()
        {
            if (!System.IO.Path.IsPathRooted(SourceFilePath))
            {
                SourceFilePath = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, SourceFilePath);
            }

            var masterPageFile = SelectedWeb.DeployMasterPage(SourceFilePath, Title, Description, UiVersion, DefaultCssFile, DestinationFolderHierarchy);
            WriteObject(masterPageFile);
        }
    }
}
