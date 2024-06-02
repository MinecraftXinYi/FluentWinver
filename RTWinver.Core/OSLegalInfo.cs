using System;

namespace RTWinver
{
    using Helpers;
    using Services;

    public static partial class OSLegalInfo
    {
        //获取系统开发者名称
        public static string OSManufacturer
        {
            get
            {
                string osManufacturer;
                try
                {
                    string ntfile = $"{Environment.SystemDirectory}/ntoskrnl.exe";
                    System.Diagnostics.FileVersionInfo ntfileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(ntfile);
                    if (!string.IsNullOrEmpty(ntfileInfo.CompanyName))
                    {
                        osManufacturer = ntfileInfo.CompanyName;
                    }
                    else
                    {
                        osManufacturer = "Microsoft Corporation";
                    }
                }
                catch (Exception)
                {
                    osManufacturer = "Microsoft Corporation";
                }
                return osManufacturer;
            }
        }

        //获取系统注册用户名称
        public static string OSRegisteredUser
        {
            get
            {
                string registeredUser;
                RegistryHelper.TryGetInfoString(RegistryKeyPaths.NTInfoKeyPath, "RegisteredOwner", out registeredUser);
                if (string.IsNullOrEmpty(registeredUser)) registeredUser = "[Registered Owner]";
                return registeredUser;
            }
        }
    }
}
