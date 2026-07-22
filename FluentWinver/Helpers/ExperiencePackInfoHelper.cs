using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

namespace FluentWinver.Helpers;

public static class ExperiencePackInfoHelper
{
    public static readonly string WindowsCBSPackageFamilyName = "MicrosoftWindows.Client.CBS_cw5n1h2txyewy";

    public static Package GetPackageForWindowsCBSPackage()
        => packageManager.FindPackages(WindowsCBSPackageFamilyName).ToList()[default];

    private static readonly PackageManager packageManager = new();
}
